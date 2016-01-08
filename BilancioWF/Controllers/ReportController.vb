Imports BilancioWF.Models
Imports System.Text
Imports System.Data.OleDb

Public Class ReportController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "Report"
    Public Overrides Property TYPE As Type = GetType(Report)

    Function save(ByRef current As Report) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Dim sb = New StringBuilder()
        If (current.ID <= 0) Then

            sb.Append("INSERT INTO " & TABLE_NAME & " ")
            sb.Append("( ")
            sb.Append("   dateCreated , ")
            sb.Append("   lastUpdate, ")
            sb.Append("   Code, ")
            sb.Append("   Name, ")
            sb.Append("   Active, ")
            sb.Append("   ModelName, ")
            sb.Append("   FormatType, ")
            sb.Append("   OutFileName, ")
            sb.Append("   ActionName, ")
            sb.Append("   ControllerName ")
            sb.Append(") ")
            sb.Append("VALUES (GETDATE(),GETDATE(),?,?,?,?,?,?,?,?) ")
            sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'per l'insert ritorno l'id utilizzato
        Else

            sb.Append("UPDATE " & TABLE_NAME & " SET ")
            sb.Append("Code=?, ")
            sb.Append("Name=?, ")
            sb.Append("Active=?, ")
            sb.Append("ModelName=?, ")
            sb.Append("FormatType=?, ")
            sb.Append("OutFileName=?, ")
            sb.Append("ActionName=?, ")
            sb.Append("ControllerName=?, ")
            'sb.Append("dateCreated={ts '2015-11-16 11:08:57'}, ")
            sb.Append("lastUpdate=GETDATE() ")
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

                    com.Parameters.Add("@ModelName", OleDbType.VarChar).Value = current.ModelName
                    com.Parameters.Add("@FormatType", OleDbType.Integer).Value = current.FormatType
                    com.Parameters.Add("@OutFileName", OleDbType.VarChar).Value = current.OutFileName
                    com.Parameters.Add("@ActionName", OleDbType.VarChar).Value = current.ActionName
                    com.Parameters.Add("@ControllerName", OleDbType.VarChar).Value = current.ControllerName

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

End Class

