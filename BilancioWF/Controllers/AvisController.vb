Imports BilancioWF.Models
Imports System.Text
Imports System.Data.OleDb

Public Class AvisController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "Avis"
    Public Overrides Property TYPE As Type = GetType(Avis)

    Function save(ByRef current As Avis) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                If (current.ID <= 0) Then
                    sb.Append("INSERT INTO " & TABLE_NAME & " ")
                    sb.Append("( ")
                    sb.Append("   Active, ")
                    sb.Append("   dateCreated, ")
                    sb.Append("   Name, ")
                    sb.Append("   Address, ")
                    sb.Append("   City, ")
                    sb.Append("   PostalCode, ")
                    sb.Append("   Region, ")
                    sb.Append("   Email, ")
                    sb.Append("   Phone, ")
                    sb.Append("   ContactName ")
                    sb.Append(") ")
                    sb.Append("VALUES (1,GETDATE(),?,?,?,?,?,?,?,?) ")
                    sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'ritorna l'id utilizzato

                Else
                    sb.Append("UPDATE " & TABLE_NAME & " SET ")
                    sb.Append("Name=?,Address=?,City=?,PostalCode=?,Region=?,Email=?,Phone=?,ContactName=? ")
                    sb.Append("WHERE ID=? ")
                End If

                Dim com As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                com.Parameters.Add("@Name", OleDbType.VarChar).Value = current.Name
                com.Parameters.Add("@Address", OleDbType.VarChar).Value = current.Address
                com.Parameters.Add("@City", OleDbType.VarChar).Value = current.City
                com.Parameters.Add("@PostalCode", OleDbType.VarChar).Value = current.PostalCode
                com.Parameters.Add("@Region", OleDbType.VarChar).Value = current.Region
                com.Parameters.Add("@Email", OleDbType.VarChar).Value = current.Email
                com.Parameters.Add("@Phone", OleDbType.VarChar).Value = current.Phone
                com.Parameters.Add("@ContactName", OleDbType.VarChar).Value = current.ContactName

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

End Class
