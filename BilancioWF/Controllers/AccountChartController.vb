Imports BilancioWF.Models
Imports System.Text
Imports System.Data.OleDb

Public Class AccountChartController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "AccountChart"
    Public Overrides Property TYPE As Type = GetType(AccountChart)

    Function save(ByRef current As AccountChart) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                If (current.ID <= 0) Then
                    sb.Append("INSERT " & TABLE_NAME & " ")
                    sb.Append("(Code, Name, Active, Debit, AccountCeeID) ")
                    sb.Append("VALUES ")
                    sb.Append("(?, ?, ?, ?, ?) ")
                    sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'ritorna l'id utilizzato

                Else
                    sb.Append("UPDATE " & TABLE_NAME & " SET ")
                    sb.Append("Code=?, ")
                    sb.Append("Name=?, ")
                    sb.Append("Active=?, ")
                    sb.Append("Debit=? ,")
                    sb.Append("AccountCeeID=? ")
                    sb.Append("WHERE ")
                    sb.Append("ID=? ")
                End If

                Dim com As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                com.Parameters.Add("@Code", OleDbType.VarChar).Value = current.Code
                com.Parameters.Add("@Name", OleDbType.VarChar).Value = current.Name
                com.Parameters.Add("@Active", OleDbType.Boolean).Value = current.Active
                com.Parameters.Add("@Debit", OleDbType.Integer).Value = current.Debit
                com.Parameters.Add("@AccountCeeID", OleDbType.Integer).Value = current.AccountCeeID

                If (current.ID > 0) Then
                    com.Parameters.Add("@ID", OleDbType.Integer).Value = current.ID
                End If

                'Dim rowNr = com.ExecuteNonQuery()
                Dim lastId = com.ExecuteScalar()

                If (current.ID <= 0 AndAlso Not IsNothing(lastId)) Then
                    current.ID = lastId
                    'Trace.WriteLine("id da SCOPE_IDENTITY = " & lastId & " id da code: " & getIdFromValue(current.Code))
                End If

                save = True

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                'Finally
            End Try

        End Using

    End Function

    ''@return record della chiave passata, se l'id non è valido ritorna una nuova istanze dell'oggetto vuoto
    ''@param id key del record da ritornare
    Public Overloads Function Edit(Optional ByVal id As Integer = 0) As AccountChart

        Dim retValue = New AccountChart

        If (id <= 0) Then
            Return retValue
        End If

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("a.* ")
        sb.Append(",e.* ")
        sb.Append("from AccountChart a ")
        sb.Append("inner join AccountCee e on e.ID = a.AccountCeeID ")
        sb.Append("where a.ID = " & id & " ")

        Dim objsType As List(Of Type) = New List(Of Type)({GetType(AccountChart), GetType(AccountCee)})

        Using connection = getConnectionOpened()

            Try
                Dim dt = GetDataTable(connection, sb.ToString)

                If dt.Rows.Count > 0 Then

                    Dim objs As List(Of Object) = CaricaDaDB(dt.Rows(0), objsType)
                    retValue = objs(0)
                    retValue.AccountCee = objs(1)
                End If

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

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

End Class
