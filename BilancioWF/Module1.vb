Imports System.Data.OleDb
Imports System.Reflection
Imports System.Data.Common
Imports BilancioWF.Models
Imports System.Data.SqlClient

Public Module Module1

    Public MAX_ELEMENTS_ON_WHERE_IN As Integer = 300
    Public MAX_RECORDS_TO_SEARCH As Integer = 100

    Public Enum DareAvere As Integer
        Dare = 1
        Avere = -1
    End Enum

    Private Const Provider = "sqloledb"
    Private Const host = "localhost"
    Private Const dbName = "bilancio"
    Private Const userName = "bilancio"
    Private Const psw = "balance"

    Dim tablesFk As Dictionary(Of String, List(Of Foreignkey))

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

    Public Function getSqlConnection() As SqlConnection


        Dim connectionString As String = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bilancio;Data Source=localhost"


        Return New SqlConnection(connectionString)
    End Function

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
                             Optional ByVal strDisplayMember As String = Nothing)

        If (IsNothing(strDisplayMember)) Then
            strDisplayMember = strValueMember
        End If

        Try
            With cbo
                .DataSource = list
                .DisplayMember = strDisplayMember
                .ValueMember = strValueMember
            End With
        Catch ex As Exception
            Trace.WriteLine(ex.Message)

        End Try

    End Sub

    Public Sub PopulateCombo(ByRef cbo As Windows.Forms.ComboBox, ByVal list As IEnumerable, Optional ByVal noEmpty As Boolean = True)

        cbo.Items.Clear()

        For Each e In list
            cbo.Items.Add(e)
            
        Next

        If (noEmpty And cbo.Items.Count > 0) Then
            cbo.SelectedIndex = 0
        End If

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

    '@param connection connessione
    '@param sql da eseguire
    '@param objType da restituire caricato
    '@param trans transazione
    '@param groupPos indice da aggiungere al nome del tipo per prendere i dati dal recordset. Se la select ha lo stesso nome per più colonne, il recordset aggiunge un progressivo al nome
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro diverso da 0
    '@return oggetto di tipo objType caricato usando la prima riga (0 base) del datarow
    Public Function CaricaDaDB(ByRef connection As DbConnection, sql As String, ByVal objType As Type, Optional trans As Common.DbTransaction = Nothing) As Object

        Dim instance As Object = Nothing

        Dim dt As DataTable = GetDataTable(connection, sql, "", Nothing, trans)

        Return CaricaDaDB(dt.Rows, objType, 0)

    End Function

    '@param collection di datarow
    '@param objType da restituire caricato
    '@param rowPos nr riga (0 based)
    '@param groupPos indice da aggiungere al nome del tipo per prendere i dati dal recordset. Se la select ha lo stesso nome per più colonne, il recordset aggiunge un progressivo al nome
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro diverso da 0
    '@return oggetto di tipo objType caricato usando l'iesimo datarow
    Public Function CaricaDaDB(ByRef drCollection As DataRowCollection, ByVal objType As Type, Optional rowPos As Integer = 0) As Object

        If (rowPos < 0 Or IsNothing(drCollection) Or drCollection.Count = 0 Or drCollection.Count <= rowPos) Then
            Return Nothing
        End If

        Return CaricaDaDB(drCollection(rowPos), objType)

    End Function

    '@param datarow
    '@param objType da restituire caricato
    '   in modo da renderlo univoco, se si vuole far riferimento ad un gruppo di colonne successivo al primo (0 based) va specificato questo parametro > 0.
    '   Attenzione ai nomi di colonna, colonne con lo stesso nome hanno un progressivo come postfisso e la ricerca viene fatta dal progressivo massimo (groupPos) a scendere fino allo 0 (senza postfisso)
    '   casi con tre (o più) tabelle dove una colonna è presente su una sola tabella (nome colonna univoco) o su tutte le tabelle (nome colonna con postfisso per tutte le tabelle) è ok, 
    '   ma colonne non univoche e non presenti su tutte le tabelle assumono un progressivo minore del max, per cui quando viene fatta la ricerca del gruppo 1 (seconda tabella) questa acquisisce
    '   le colonne della terza tabella che non sono presenti nella prima.
    '   esempio: la colonna code è presente nella seconda e terza tabella, ma non nella prima, per cui la select resituirà code e code1, in fase di ricerca per il secondo gruppo code1 viene trovato
    '@return oggetto di tipo objType caricato usando il datarow
    Public Function CaricaDaDB(ByRef dataRow As DataRow, ByVal objType As Type) As Object

        If (IsNothing(objType)) Then
            Return Nothing
        End If

        Dim lis = New List(Of Type)(1)
        lis.Add(objType)

        Dim retLis As List(Of Object) = CaricaDaDB(dataRow, lis)

        If (IsNothing(retLis) OrElse retLis.Count = 0) Then
            Return Nothing
        End If

        Return retLis(0)

    End Function

    '@param datarow
    '@param objType lista di tipi da caricare
    '@return istanze di objType con valori ottenuti da datarow
    Public Function CaricaDaDB(ByRef dataRow As DataRow, ByVal objType As List(Of Type)) As List(Of Object)

        If (IsNothing(dataRow) OrElse IsNothing(objType) OrElse objType.Count = 0) Then
            Return Nothing
        End If

        Dim maxPrg = 0
        Dim retValue = New List(Of Object)(objType.Count)
        Dim colsNameUsed = New HashSet(Of String)

        For Each oneType In objType

            Dim instance = getInstance(oneType)
            retValue.Add(instance)
            Dim pojoCols As IEnumerable(Of System.Reflection.PropertyInfo) = oneType.GetProperties(BindingFlags.Public Or BindingFlags.Instance)

            For Each prop As PropertyInfo In pojoCols

                For count As Integer = 0 To maxPrg
                    Dim propertyName As String = prop.Name.ToUpper & IIf(count > 0, count, "")
                    If (colsNameUsed.Contains(propertyName)) Then
                        Continue For    'colonna esistente ma già usata
                    End If

                    If (dataRow.Table.Columns.Contains(propertyName)) Then

                        Dim value As Object = dataRow(propertyName)

                        If (IsNothing(value) Or value Is DBNull.Value) Then
                            prop.SetValue(instance, Nothing, Nothing)
                        Else
                            prop.SetValue(instance, value, Nothing)
                        End If

                        colsNameUsed.Add(propertyName)

                    End If
                    Exit For
                Next
            Next
            maxPrg += 1
        Next

        Return retValue

    End Function

    'Ritorna un'istance dell'oggetto passato a condizione che abbia il costruttore vuoto
    Public Function getInstance(ByVal t As System.Type) As Object
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

    '@return una lista di Foreignkey per la tabella
    '@param tableName tabella per cui si vuole l'elenco delle foreign key
    Public Function getForeignkey(tableName As String) As List(Of Foreignkey)

        Dim fk As List(Of Foreignkey) = Nothing

        If (IsNothing(tablesFk)) Then
            tablesFk = New Dictionary(Of String, List(Of Foreignkey))()
        ElseIf (tablesFk.ContainsKey(tableName)) Then
            fk = tablesFk.Item(tableName)
            If (Not IsNothing(fk)) Then
                Return fk
            End If
        End If


        fk = New List(Of Foreignkey)
        tablesFk.Add(tableName, fk)

        Using connection = getConnectionOpened()
            Try
                Dim cmd As OleDbCommand = New OleDbCommand("sp_fkeys", connection)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@TableName", OleDbType.VarChar).Value = tableName

                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        fk.Add(New Foreignkey() With {.columnName = reader.GetString(7), .tableName = reader.GetString(6)})
                    End While
                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return fk

    End Function

    Public Function parseDecimal(value As String, Optional defaultUseMin As Boolean = False, Optional defaultUseMax As Boolean = False) As Decimal

        If (String.IsNullOrEmpty(value)) Then
            Return IIf(defaultUseMax, Decimal.MaxValue, IIf(defaultUseMin, Decimal.MinValue, Decimal.Zero))
        End If

        Try
            parseDecimal = Convert.ToDecimal(value)
        Catch ex As Exception
            parseDecimal = Decimal.Zero
        End Try

    End Function

    Public Function parseInteger(value As String, Optional defaultUseMin As Boolean = False, Optional defaultUseMax As Boolean = False) As Integer

        If (String.IsNullOrEmpty(value)) Then
            Return IIf(defaultUseMax, Integer.MaxValue, IIf(defaultUseMin, Integer.MinValue, 0))
        End If

        Try
            parseInteger = Convert.ToInt32(value)
        Catch ex As Exception
            parseInteger = 0
        End Try

    End Function

    Public Interface Traverse
        Function oneSon(son As AccountCee, level As Integer) As Boolean
        Function oneParent(son As AccountCee, level As Integer) As Boolean

    End Interface

    Class Foreignkey
        Property tableName As String
        Property columnName As String

        Function getSelect(keyVal As Integer) As String
            Return "SELECT d.* FROM " & tableName & " d where d." & columnName & " = " & keyVal
        End Function
        Function getExists(keyVal As Integer) As String
            Return "case when exists (" & getSelect(keyVal) & ") then 1 else 0 end > 0"
        End Function

    End Class

End Module
