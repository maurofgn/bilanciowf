Imports System.Data.OleDb
Imports System.Reflection
Imports System.Data.Common
Imports BilancioWF.Models

Public Module Module1

    Public MAX_ELEMENTS_ON_WHERE_IN As Integer = 300

    Public Enum DareAvere As Integer
        Dare = 1
        Avere = -1
    End Enum

    Private Const Provider = "sqloledb"
    Private Const host = "localhost"
    Private Const dbName = "bilancio"
    Private Const userName = "bilancio"
    Private Const psw = "balance"

    Public Function getConnectionString()
        'Return getCommon() & "Integrated Security=False;User Id=" & userName & ";Password=" & psw & ";"
        Return getCommon() & "User Id=" & userName & ";Password=" & psw & ";"
    End Function

    Public Function getTrustedConnectionString()
        Return getCommon() & "Integrated Security=SSPI;"
    End Function

    Private Function getCommon()
        Return "Provider=" & Provider & ";Data Source=" & host & ";Initial Catalog=" & dbName & ";"
    End Function


    Public Function getConnectionOpened() As OleDbConnection
        Dim retValue = Nothing
        Try
            retValue = New OleDbConnection(getConnectionString())
            retValue.Open()

        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        Return retValue

    End Function

    'Public Function getDBConnectionOpened() As DbConnection
    '    Dim retValue = Nothing
    '    Try
    '        retValue = New SqlClient.SqlConnection(getConnectionString())
    '        retValue.Open()

    '    Catch ex As Exception
    '        Trace.WriteLine(ex.Message)
    '    End Try

    '    Return retValue

    'End Function

    Public Function ConnectionClose(ByRef connection As OleDbConnection) As Boolean
        ConnectionClose = False
        Try
            connection.Close()
            connection.Dispose()
            connection = Nothing
            ConnectionClose = True
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

    End Function
    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, ByVal dt As DataTable)
        'Calls the full method using our assumptions
        PopulateCombo(cbo, dt, dt.Columns(0).ToString, dt.Columns(1).ToString)
    End Sub

    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, _
                            ByVal dt As DataTable, _
                            ByVal strValueMember As String, _
                            ByVal strDisplayMember As String)
        With cbo
            .DataSource = dt
            .DisplayMember = strDisplayMember
            .ValueMember = strValueMember
        End With
    End Sub


    'A much simpler call that assumes the table is the first table
    'in the DataSet is the correct one and it assumes the first
    'column is the identity column and the second one is the data
    'column.
    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, ByVal dst As DataSet)
        'Calls the full method using our assumptions
        PopulateCombo(cbo, dst, dst.Tables(0).Columns(0).ToString, _
                      dst.Tables(0).Columns(1).ToString, _
                      dst.Tables(0).ToString)
    End Sub
    'The complete method that populates a ComboBox reference with
    'data from the created DataSet.  This is the full method.
    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, _
                             ByVal dst As DataSet, _
                             ByVal strValueMember As String, _
                             ByVal strDisplayMember As String, _
                             ByVal strTableName As String)
        With cbo
            .DataSource = dst.Tables(strTableName)
            .DisplayMember = strDisplayMember
            .ValueMember = strValueMember
        End With
    End Sub

    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, _
                             ByVal list As IEnumerable, _
                             ByVal strValueMember As String, _
                             ByVal strDisplayMember As String)

        With cbo
            .DataSource = list
            .DisplayMember = strDisplayMember
            .ValueMember = strValueMember
        End With
    End Sub


    Public Function negate(da As DareAvere) As DareAvere

        If (DareAvere.Dare.Equals(da)) Then
            Return DareAvere.Avere
        ElseIf (DareAvere.Avere.Equals(da)) Then
            Return DareAvere.Dare
        End If

        Return DareAvere.Avere  ''il default è di norma il Dare, per cui essendo negato torna Avere

    End Function

    Public Function DebitString(debit As DareAvere) As String
        Return [Enum].GetName(GetType(DareAvere), debit)
    End Function

    Public Function sqlBoolValue(val As Boolean) As Boolean
        sqlBoolValue = IIf(val, 1, 0)
    End Function

    Public Interface EntityBase

        Property dateCreated As DateTime
        Property lastUpdate As DateTime

    End Interface


    'Public Sub CaricaDaDB(dr As DataRow, ByRef oClasse As clsGenericaListViewItem)
    '    Dim objType As Type = Me.GetType()

    '    For Each col As DataColumn In dr.Table.Columns
    '        Dim appo As PropertyInfo = objType.GetProperty(col.ColumnName, BindingFlags.Public Or BindingFlags.Instance)
    '        If Not appo Is Nothing Then
    '            If dr(col.ColumnName) Is DBNull.Value Then
    '                appo.SetValue(oClasse, Nothing, Nothing)
    '            Else
    '                appo.SetValue(oClasse, dr(col.ColumnName), Nothing)
    '            End If
    '        End If
    '    Next
    'End Sub

    'Public Sub CaricaDaDB(dr As DataRow, ByRef oClasse As Object)
    '    Dim objType As Type = oClasse.GetType()

    '    For Each col As DataColumn In dr.Table.Columns
    '        Dim appo As PropertyInfo = objType.GetProperty(col.ColumnName, BindingFlags.Public Or BindingFlags.Instance)
    '        If Not appo Is Nothing Then
    '            If dr(col.ColumnName) Is DBNull.Value Then
    '                appo.SetValue(oClasse, Nothing, Nothing)
    '            Else
    '                appo.SetValue(oClasse, dr(col.ColumnName), Nothing)
    '            End If
    '        End If
    '    Next
    'End Sub



    '@param connection connessione
    '@param sql da eseguire
    '@param objType da restituire caricato
    '@param trans transazione
    '@param groupPos indice da aggiungere al nome del tipo per prendere i dati dal recordset. Se la select ha lo stesso nome per più colonne, il recordset aggiunge un progressivo al nome
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro diverso da 0
    '@return oggetto di tipo objType caricato usando la prima riga (0 base) del datarow
    Public Function CaricaDaDB(ByRef connection As DbConnection, sql As String, ByVal objType As Type, Optional trans As Common.DbTransaction = Nothing, Optional groupPos As Integer = 0) As Object

        Dim instance As Object = Nothing

        Dim dt As DataTable = GetDataTable(connection, sql, "", Nothing, trans)

        Return CaricaDaDB(dt.Rows, objType, 0, groupPos)

    End Function

    '@param collection di datarow
    '@param objType da restituire caricato
    '@param rowPos nr riga (0 based)
    '@param groupPos indice da aggiungere al nome del tipo per prendere i dati dal recordset. Se la select ha lo stesso nome per più colonne, il recordset aggiunge un progressivo al nome
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro diverso da 0
    '@return oggetto di tipo objType caricato usando l'iesimo datarow
    Public Function CaricaDaDB(ByRef drCollection As DataRowCollection, ByVal objType As Type, Optional rowPos As Integer = 0, Optional groupPos As Integer = 0) As Object

        If (rowPos < 0 Or IsNothing(drCollection) Or drCollection.Count = 0 Or drCollection.Count <= rowPos) Then
            Return Nothing
        End If

        Return CaricaDaDB(drCollection(rowPos), objType, groupPos)

    End Function

    '@param datarow
    '@param objType da restituire caricato
    '@param groupPos indice da aggiungere al nome del tipo per prendere i dati dal recordset. Se la select ha lo stesso nome per più colonne, il recordset aggiunge un progressivo al nome
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro diverso da 0
    '@return oggetto di tipo objType caricato usando il datarow
    Public Function CaricaDaDB(ByRef dr As DataRow, ByVal objType As Type, Optional groupPos As Integer = 0) As Object

        If (IsNothing(dr)) Then
            Return Nothing
        End If

        'Trace.WriteLine(dr("ID1"))
        'For Each dc As DataColumn In dr.Table.Columns
        '    Trace.WriteLine(dc)
        'Next


        Dim instance = getInstance(objType)

        Dim pojoCols As IEnumerable(Of System.Reflection.PropertyInfo) = objType.GetProperties(BindingFlags.Public Or BindingFlags.Instance)

        For Each prop As PropertyInfo In pojoCols

            Dim propertyName As String = prop.Name & IIf(groupPos > 0, groupPos, "")

            If (Not dr.Table.Columns.Contains(propertyName)) Then
                Continue For
            End If

            Dim v As Object = dr(prop.Name)

            If IsNothing(v) Or v Is DBNull.Value Then
                prop.SetValue(instance, Nothing, Nothing)
            Else
                prop.SetValue(instance, v, Nothing)
            End If

            'For Each col As DataColumn In dr.Table.Columns
            '    If (col.ColumnName.Equals(prop.Name)) Then

            '        If dr(col.ColumnName) Is DBNull.Value Then
            '            prop.SetValue(instance, Nothing, Nothing)
            '        Else
            '            prop.SetValue(instance, dr(col.ColumnName), Nothing)
            '        End If
            '    End If

            '    Exit For

            'Next


        Next



        'For Each col As DataColumn In dr.Table.Columns
        '    Dim appo As PropertyInfo = objType.GetProperty(col.ColumnName, BindingFlags.Public Or BindingFlags.Instance)
        '    If Not appo Is Nothing Then
        '        If dr(col.ColumnName) Is DBNull.Value Then
        '            appo.SetValue(instance, Nothing, Nothing)
        '        Else
        '            appo.SetValue(instance, dr(col.ColumnName), Nothing)
        '        End If
        '    End If
        'Next

        Return instance

    End Function

    'Ritorna un'istance dell'oggetto passato a condizione che abbia il costruttore vuoto
    Private Function getInstance(ByVal t As System.Type) As Object
        Return t.GetConstructor(New System.Type() {}).Invoke(New Object() {})
    End Function

    Public Function GetDataTable(ByRef cn As DbConnection,
                                ByVal sSql As String,
                                Optional ByVal sDataTableName As String = "",
                                Optional ByRef oDa As OleDb.OleDbDataAdapter = Nothing,
                                Optional trans As Common.DbTransaction = Nothing) As DataTable
        Dim table As DataTable = New DataTable()
        Try

            'If Not oDa Is Nothing Then
            '    oDa = New OleDb.OleDbDataAdapter(sSql, cn)

            '    Dim builder = New OleDb.OleDbCommandBuilder(oDa)
            '    builder.GetUpdateCommand()

            '    Dim ds As DataSet = libOT.MeSIS.OT.SQL.clSQL.GetDataTable(oDa)
            '    table = ds.Tables(0)

            'Else
            Using cmd As DbCommand = cn.CreateCommand()
                cmd.Transaction = trans
                cmd.CommandText = sSql
                cmd.CommandTimeout = 600
                Using rd As DbDataReader = cmd.ExecuteReader()
                    table.Load(rd)
                End Using
                table.TableName = sDataTableName
            End Using
            'End If

            Return table
        Catch ex As Exception
            'WriteLog(" ------ Errore durante libSQL.GetDataTable!!!")
            Throw New Exception("Errore durante libSQL.GetDataTable :: " & ex.Message)


        End Try
    End Function

    Public Interface Traverse
        Function oneSon(son As AccountCee, level As Integer) As Boolean
        Function oneParent(son As AccountCee, level As Integer) As Boolean

    End Interface

End Module
