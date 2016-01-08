Imports BilancioWF.Models
Imports System.Text
Imports System.Data.OleDb

Public Class DocumentTypeController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "DocumentType"
    Public Overrides Property TYPE As Type = GetType(DocumentType)

    Function save(ByRef current As DocumentType) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Dim sb = New StringBuilder()
        If (current.ID <= 0) Then
            sb.Append("INSERT " & TABLE_NAME & " ")
            sb.Append("(Code, Name, Active) ")
            sb.Append("VALUES ")
            sb.Append("(?, ?, ?) ")
            sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'per l'insert ritorno l'id utilizzato
        Else
            sb.Append("UPDATE " & TABLE_NAME & " SET ")
            sb.Append("Code=?, ")
            sb.Append("Name=?, ")
            sb.Append("Active=? ")
            sb.Append("WHERE ")
            sb.Append("ID=? ")
        End If

        Using connection = getConnectionOpened()
            'Using transaction As OleDbTransaction = connection.BeginTransaction()
            Try
                Using com As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                    com.Parameters.Add("@Code", OleDbType.VarChar).Value = current.Code
                    com.Parameters.Add("@Name", OleDbType.VarChar).Value = current.Name
                    com.Parameters.Add("@Active", OleDbType.Boolean).Value = current.Active

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

    Public Shared Function loadAll(Optional lazy As Boolean = True) As Dictionary(Of Integer, DocumentType)

        Dim allRecords As Dictionary(Of Integer, DocumentType) = New Dictionary(Of Integer, DocumentType)

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append("from DocumentType n ")

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)
            Dim t = GetType(DocumentType)

            For i = 0 To dt.Rows.Count - 1
                Dim row As DocumentType = CaricaDaDB(dt.Rows(i), t)
                allRecords.Add(row.ID, row)
            Next

            'If (Not lazy) Then
            '    loadChart(nodes, connection)
            'End If

        End Using

        Return allRecords

    End Function

    'Function comboItems() As List(Of CodeName)

    '    comboItems = New List(Of CodeName)

    '    Dim sb = New StringBuilder()
    '    sb.Append("SELECT ")
    '    sb.Append("n.* ")
    '    sb.Append("from DocumentType n ")
    '    sb.Append("Order by n.Name ")

    '    Using connection = getConnectionOpened()

    '        Dim dt = GetDataTable(connection, sb.ToString)
    '        Dim t = GetType(DocumentType)

    '        For i = 0 To dt.Rows.Count - 1
    '            'Dim row As DocumentType = CaricaDaDB(dt.Rows(i), t)
    '            comboItems.Add(CaricaDaDB(dt.Rows(i), t))
    '        Next

    '    End Using

    'End Function

End Class
