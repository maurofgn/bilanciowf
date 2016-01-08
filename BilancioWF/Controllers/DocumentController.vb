Imports BilancioWF.Models
Imports System.Text
Imports System.Data.OleDb

Public Class DocumentController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "Document"
    Public Overloads Property TABLE_NAME_ROW As String = "DocumentRow"

    Public Overrides Property TYPE As Type = GetType(Document)

    Function save(ByRef current As Document) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Dim sb = New StringBuilder()
        If (current.ID <= 0) Then
            sb.Append("INSERT INTO " & TABLE_NAME & " (dateReg, dateDoc, docNr, note, amount, DocumentType_ID) ")
            sb.Append("VALUES (?,?,?,?,?,?) ")
            sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'per l'insert ritorno l'id utilizzato
        Else
            sb.Append("UPDATE " & TABLE_NAME & " SET ")
            sb.Append("dateReg=?, ")
            sb.Append("dateDoc=?, ")
            sb.Append("docNr=?, ")
            sb.Append("note=?, ")
            sb.Append("amount=?, ")
            sb.Append("DocumentType_ID=? ")
            sb.Append("WHERE ")
            sb.Append("ID=? ")

        End If

        Using connection = getConnectionOpened()
            'Using transaction As OleDbTransaction = connection.BeginTransaction()
            Try
                Using com As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                    com.Parameters.Add("@dateReg", OleDbType.Date).Value = current.dateReg
                    com.Parameters.Add("@dateDoc", OleDbType.Date).Value = current.dateDoc
                    com.Parameters.Add("@docNr", OleDbType.VarChar).Value = current.docNr
                    com.Parameters.Add("@note", OleDbType.VarChar).Value = current.note
                    com.Parameters.Add("@amount", OleDbType.Decimal).Value = current.amount
                    com.Parameters.Add("@DocumentType_ID", OleDbType.Integer).Value = current.DocumentType_ID
                    If (current.ID > 0) Then
                        com.Parameters.Add("@ID", OleDbType.Integer).Value = current.ID
                    End If

                    'Dim rowNr = com.ExecuteNonQuery()
                    Dim lastId = com.ExecuteScalar()

                    If (current.ID <= 0 AndAlso Not IsNothing(lastId)) Then
                        current.ID = lastId
                        'Trace.WriteLine("id da SCOPE_IDENTITY = " & lastId & " id da code: " & getIdFromValue(current.Code))
                    End If

                End Using

                'transaction.Commit()
                save = True

            Catch ex As Exception
                'transaction.Rollback()
                Trace.WriteLine(ex.Message)
                'Finally
            End Try

            'End Using
        End Using

    End Function

    Function save(currentRow As DocumentRow) As Boolean

        If (IsNothing(currentRow)) Then
            Return False
        End If

        If (currentRow.Document_ID <= 0) Then

            Dim doc As Document = currentRow.Document

            If (Not save(doc)) Then
                Return False    'testata non salvata
            End If

            currentRow.Document_ID = doc.ID

        End If

        If (currentRow.rowNr <= 0) Then
            currentRow.rowNr = lastRowNr(currentRow.Document_ID) + 10
        End If

        save = False

        Dim sb = New StringBuilder()
        If (currentRow.ID <= 0) Then
            sb.Append("INSERT INTO DocumentRow ")
            sb.Append("(rowNr ,debit ,amount ,note  ,AccountChart_ID, dateCreated, Document_ID) VALUES ")
            sb.Append("(?     ,?     ,?      ,?     ,?              ,GETDATE()   , " & currentRow.Document_ID & ") ")
            sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'per l'insert ritorno l'id utilizzato
        Else
            sb.Append("UPDATE DocumentRow SET ")
            sb.Append("rowNr=?, ")
            sb.Append("debit=?, ")
            sb.Append("amount=?, ")
            sb.Append("note=?, ")
            sb.Append("AccountChart_ID=? ")
            sb.Append("WHERE ")
            sb.Append("ID=? ")

        End If

        Dim isNewRow As Boolean = currentRow.ID <= 0

        Using connection = getConnectionOpened()
            'Using transaction As OleDbTransaction = connection.BeginTransaction()
            Try
                Using com As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                    com.Parameters.Add("@rowNr", OleDbType.Integer).Value = currentRow.rowNr
                    com.Parameters.Add("@debit", OleDbType.Integer).Value = currentRow.debit
                    com.Parameters.Add("@amount", OleDbType.Decimal).Value = currentRow.amount
                    com.Parameters.Add("@note", OleDbType.VarChar).Value = currentRow.note
                    com.Parameters.Add("@AccountChart_ID", OleDbType.Integer).Value = currentRow.AccountChart_ID

                    If (currentRow.ID > 0) Then
                        com.Parameters.Add("@ID", OleDbType.Integer).Value = currentRow.ID
                    End If

                    'Dim rowNr = com.ExecuteNonQuery()
                    Dim lastId = com.ExecuteScalar()

                    If (currentRow.ID <= 0 AndAlso Not IsNothing(lastId)) Then
                        currentRow.ID = lastId
                        'Trace.WriteLine("id da SCOPE_IDENTITY = " & lastId & " id da code: " & getIdFromValue(current.Code))
                    End If

                End Using

                'transaction.Commit()
                save = True

                If (isNewRow) Then
                    currentRow.Document.documentRows.Add(currentRow)
                End If

            Catch ex As Exception
                'transaction.Rollback()
                Trace.WriteLine(ex.Message)
                'Finally
            End Try

            'End Using
        End Using


    End Function

    Public Overloads Function Edit(Optional ByVal id As Integer = 0) As Object

        Dim doc As Document = MyBase.Edit(id)
        loadRows(doc)
        Return doc

    End Function

    Private Sub loadRows(doc As Document)

        If (doc.ID <= 0) Then
            Return
        End If

        Dim sb = New StringBuilder()

        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append(",a.* ")
        sb.Append(",e.* ")
        sb.Append("from DocumentRow n ")
        sb.Append("inner join AccountChart a on a.ID = n.AccountChart_ID ")
        sb.Append("inner join AccountCee e on e.ID = a.AccountCeeID ")
        sb.Append("where n.Document_ID = " & doc.ID & " ")
        sb.Append("order by n.rowNr, n.AccountChart_ID ")

        Dim objsType As List(Of Type) = New List(Of Type)({GetType(DocumentRow), GetType(AccountChart), GetType(AccountCee)})

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)

            For i = 0 To dt.Rows.Count - 1

                Dim objs As List(Of Object) = CaricaDaDB(dt.Rows(i), objsType)

                Dim row As DocumentRow = objs(0)
                Dim acc As AccountChart = objs(1)
                Dim ace As AccountCee = objs(2)
                acc.AccountCee = ace
                row.AccountChart = acc
                row.Document = doc
                doc.documentRows.Add(row)
            Next

        End Using

    End Sub

    Function removeLine(r As DocumentRow) As Boolean

        removeLine = True

        If (r.ID <= 0) Then
            Return True
        End If

        If (removeLine(r.ID)) Then
            removeLine = r.Document.documentRows.Remove(r)
        End If

    End Function
    Function removeLine(id As Integer) As Boolean
        removeLine = True
        If (id > 0) Then
            removeLine = MyBase.remove(id, TABLE_NAME_ROW)
        End If

    End Function

    Private Function lastRowNr(id As Integer) As Integer

        If (id <= 0) Then
            Return 0
        End If

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("max(rowNr) ")
        sb.Append("FROM DocumentRow r ")
        sb.Append("where r.Document_ID = ? ")

        Dim retValue As Integer = 0

        Using connection = getConnectionOpened()

            Try
                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                cmd.Parameters.Add("@Document_ID", OleDbType.Integer).Value = id

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

End Class
