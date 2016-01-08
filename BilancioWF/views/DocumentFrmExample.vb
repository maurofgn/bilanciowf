Imports System.Text
Imports System.Data.OleDb

Public Class DocumentFrmExample

    Private Sub DocumentFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Bind the DataGridView controls to the BindingSource
        ' components and load the data from the database.
        masterDataGridView.DataSource = masterBindingSource
        detailDataGridView.DataSource = detailBindingSource
        GetData()

        ' Resize the master DataGridView columns to fit the newly loaded data.
        masterDataGridView.AutoResizeColumns()

        ' Configure the details DataGridView so that its columns automatically
        ' adjust their widths when the data changes.
        detailDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub

    Private Sub GetData()

        Try

            Using connection = getConnectionOpened()

                ' Create a DataSet.
                Dim data As New DataSet()
                data.Locale = System.Globalization.CultureInfo.InvariantCulture

                Dim sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM Document c ")
                'If (Not IsNothing(code)) Then
                '    sb.Append("where c.code = ? ")
                'End If
                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString(), connection)
                'If (Not IsNothing(code)) Then
                '    cmd.Parameters.Add("@Code", OleDbType.VarChar).Value = code
                'End If

                Using oledbAdapter = New OleDbDataAdapter(cmd)
                    Dim dt As New DataSet()
                    oledbAdapter.Fill(data, "Document")
                    oledbAdapter.Dispose()
                End Using

                sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM DocumentRow c ")
                'If (Not IsNothing(code)) Then
                '    sb.Append("where c.code = ? ")
                'End If
                cmd = New OleDbCommand(sb.ToString(), connection)
                'If (Not IsNothing(code)) Then
                '    cmd.Parameters.Add("@Code", OleDbType.VarChar).Value = code
                'End If

                Using oledbAdapter = New OleDbDataAdapter(cmd)
                    Dim dt As New DataSet()
                    oledbAdapter.Fill(data, "DocumentRow")
                    oledbAdapter.Dispose()
                End Using


                ' Establish a relationship between the two tables.
                Dim relation As New DataRelation("Documentrows", _
                    data.Tables("Document").Columns("ID"), _
                    data.Tables("DocumentRow").Columns("Document_ID"))
                data.Relations.Add(relation)

                ' Bind the master data connector to the Customers table.
                masterBindingSource.DataSource = data
                masterBindingSource.DataMember = "Document"

                ' Bind the details data connector to the master data connector,
                ' using the DataRelation name to filter the information in the 
                ' details table based on the current row in the master table. 
                detailBindingSource.DataSource = masterBindingSource
                detailBindingSource.DataMember = "Documentrows"

            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

End Class