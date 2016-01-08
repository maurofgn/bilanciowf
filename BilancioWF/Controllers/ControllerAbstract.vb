Imports System.Text
Imports System.Data.OleDb

Public MustInherit Class ControllerAbstract

    Public MustOverride Property TABLE_NAME As String
    Public MustOverride Property TYPE As Type
    Public Property TABLE_NAME_ROW As String = Nothing


    ''@param connection
    ''@param current oggetto di cui fare il save
    'Public MustOverride Function commandSave(ByRef connection As OleDbConnection, ByRef current As Object) As OleDbCommand

    ''@param rowNr nr righe coinvolte nell'operazione sql
    ''@param current oggetto in esame
    'MustOverride Sub rowUpdatedInserted(ByRef rowNr As Integer, ByRef current As Object)

    '@param rowNr nr righe coinvolte nella delete
    'MustOverride Sub rowDeleted(ByRef rowNr As Integer)

    'Function save(ByRef objToSave As Object) As Boolean
    '    If (IsNothing(objToSave)) Then
    '        Return False
    '    End If

    '    Dim retValue As Boolean = False

    '    Using connection = getConnectionOpened()

    '        Dim com As OleDbCommand = commandSave(connection, objToSave)

    '        If (com.CommandType = CommandType.TableDirect) Then

    '        End If

    '        If (Not IsNothing(com)) Then
    '            Try
    '                Dim rowNr = com.ExecuteNonQuery()

    '                'SELECT SCOPE_IDENTITY() AS last_id

    '                rowUpdatedInserted(rowNr, objToSave)
    '                retValue = True
    '            Catch ex As Exception
    '                Trace.WriteLine(ex.Message)
    '                'Finally
    '            End Try
    '        End If

    '    End Using

    '    Return retValue

    'End Function

    Function getFromValue(Optional code As String = Nothing) As Object

        Dim retValue As Object = Nothing

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM " & TABLE_NAME & " c ")
                If (Not IsNothing(code)) Then
                    sb.Append("where c.code = ? ")
                End If


                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString(), connection)
                If (Not IsNothing(code)) Then
                    cmd.Parameters.Add("@Code", OleDbType.VarChar).Value = code
                End If

                Using oledbAdapter = New OleDbDataAdapter(cmd)

                    Dim dt As New DataTable()
                    oledbAdapter.Fill(dt)
                    oledbAdapter.Dispose()
                    retValue = CaricaDaDB(dt.Rows, TYPE)

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            Finally
                ConnectionClose(connection)
            End Try

        End Using

        Return retValue

    End Function

    Function getIdFromValue(code As String) As Integer

        If (String.IsNullOrEmpty(code)) Then
            Return 0
        End If

        Dim retValue As Integer = 0

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()
                sb.Append("SELECT c.id ")
                sb.Append("FROM " & TABLE_NAME & " c ")
                sb.Append("where c.code = ? ")

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                cmd.Parameters.Add("@code", OleDbType.VarChar).Value = code

                Using reader = cmd.ExecuteReader()

                    If reader.HasRows Then
                        If (reader.Read()) Then
                            retValue = reader.GetInt32(0)
                        End If
                    End If

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function

    'Controlle dell'esistenza del codice
    Public Function codeExist(id As Integer, code As String) As Boolean

        If (String.IsNullOrEmpty(code)) Then
            Return False
        End If

        Dim retValue As Boolean = True

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()
                sb.Append("SELECT c.id ")
                sb.Append("FROM " & TABLE_NAME & " c ")
                sb.Append("where c.code = ? ")
                sb.Append("and c.id != ? ")

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                cmd.Parameters.Add("@code", OleDbType.VarChar).Value = code
                cmd.Parameters.Add("@ID", OleDbType.Integer).Value = id

                Dim dr = cmd.ExecuteReader
                retValue = dr.Read()

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function

    'elimina il record passato
    Function remove(rec As CodeName) As Boolean
        If (IsNothing(rec)) Then
            Return True
        Else
            Return remove(rec.ID)
        End If
    End Function
    'Elimina un record
    '@param key
    Function remove(key As Integer, Optional tableName As String = Nothing) As Boolean
        If (key <= 0) Then
            Return True
        End If

        Dim retValue As Boolean = True
        Using connection = getConnectionOpened()
            Try
                Dim com As OleDbCommand = New OleDbCommand("delete from " & IIf(String.IsNullOrEmpty(tableName), TABLE_NAME, tableName) & " where ID = ?", connection)
                com.Parameters.Add("@ID", OleDbType.Integer).Value = key
                Dim rowNr As Integer = com.ExecuteNonQuery()
                Trace.WriteLine("Nr righe eliminate: " & rowNr)
            Catch ex As Exception
                retValue = False
            End Try
        End Using

        Return retValue

    End Function

    'Function removeLine(key As Integer, Optional tableName As String = Nothing) As Boolean
    '    If (key <= 0) Then
    '        Return True
    '    End If

    '    Dim retValue As Boolean = True
    '    Using connection = getConnectionOpened()
    '        Try
    '            Dim com As OleDbCommand = New OleDbCommand("delete from " & IIf(String.IsNullOrEmpty(tableName), TABLE_NAME_ROW, tableName) & " where ID = ?", connection)
    '            com.Parameters.Add("@ID", OleDbType.Integer).Value = key
    '            Dim rowNr As Integer = com.ExecuteNonQuery()
    '            Trace.WriteLine("Nr righe eliminate: " & rowNr)
    '        Catch ex As Exception
    '            retValue = False
    '        End Try
    '    End Using

    '    Return retValue

    'End Function

    ''@return record della chiave passata, se l'id non è valido ritorna una nuova istanze dell'oggetto vuoto
    ''@param id key del record da ritornare
    Public Function Edit(Optional ByVal id As Integer = 0) As Object

        Dim retValue = getInstance(TYPE)

        If (id <= 0) Then
            Return retValue
        End If

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM " & TABLE_NAME & " c ")
                sb.Append("where c.ID = " & id)

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString(), connection)

                Using oledbAdapter = New OleDbDataAdapter(cmd)

                    Dim dt As New DataTable()
                    oledbAdapter.Fill(dt)
                    oledbAdapter.Dispose()
                    retValue = CaricaDaDB(dt.Rows, TYPE)

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function

    '@return true se la chiave non è usata all'interno della tabella tableUse
    '@param id key da verirficare
    Function keyNotUsed(id As Integer) As Boolean

        Return Not keyUsed(id)

    End Function

    '@return true se la chiave è usata all'interno di una tabella come chiave esterna
    '@param id key da verirficare
    Public Function keyUsed(id As Integer) As Boolean
        If (IsNothing(id) OrElse id <= 0) Then
            Return False
        End If

        Dim fk As List(Of Foreignkey) = getForeignkey(TABLE_NAME)

        If (fk.Count = 0) Then
            Return False
        End If

        Dim retValue As Boolean = False

        Dim sb = New StringBuilder()

        For Each t In fk
            If (sb.Length = 0) Then
                sb.Append("SELECT 1 where ")
            Else
                sb.Append("or ")
            End If

            sb.Append(t.getExists(id))
        Next

        Using connection = getConnectionOpened()
            Try
                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                Using reader = cmd.ExecuteReader()
                    retValue = reader.Read()
                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function

    Function validateErrors(code As String, name As String, id As Integer) As List(Of String)

        Dim errors = New List(Of String)

        If (String.IsNullOrWhiteSpace(code)) Then
            errors.Add("Il codice (univoco) è necessario")
        ElseIf (code.Trim().Length = 0 Or code.Trim().Length > 20) Then
            errors.Add("Il codice deve essere di almeno 1 carattere e massimo 20.")
        End If

        If (name.Trim().Length < 3 Or name.Trim().Length > 60) Then
            errors.Add("Il nome deve essere di almeno 3 caratteri e massimo 60.")
        End If

        If (codeExist(id, code)) Then
            errors.Add("codice già esistente")
        End If

        Return errors

    End Function

    '@return lista di codeName
    Function comboItems(Optional withVoid As Boolean = False) As List(Of CodeName)

        comboItems = New List(Of CodeName)

        If (withVoid) Then
            comboItems.Add(CodeName.VOID)
        End If

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append("from " & TABLE_NAME & " n ")
        sb.Append("Order by n.name")

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)
            For i = 0 To dt.Rows.Count - 1
                comboItems.Add(CaricaDaDB(dt.Rows(i), TYPE))
            Next

        End Using

    End Function

End Class


