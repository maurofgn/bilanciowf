Imports System.Data.OleDb
Imports System.Text

Public Class DocumentSearchFrm

    Public Property selectedId As Integer = 0


    Private Sub DocumentSearchFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ComboBoxDocType.Tag = True
        Dim dtc = New DocumentTypeController()
        PopulateCombo(ComboBoxDocType, dtc.comboItems(True), "ID", "Name")
        Me.ComboBoxDocType.Tag = False

        Dim oggi = Date.Now()

        DateTimePickerDre.Value = oggi.AddYears(-2)
        DateTimePickerDre2.Value = oggi

        DateTimePickerDoc.Value = oggi.AddYears(-2)
        DateTimePickerDoc2.Value = oggi

    End Sub


    Private Sub loadGrid(maxRecords As Integer)
        Dim cmd As New OleDbCommand
        Dim table As DataTable = New DataTable()
        Using connection = getConnectionOpened()
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sqlString(maxRecords)

            If (Not String.IsNullOrEmpty(TextBoxNrDoc.Text)) Then
                cmd.Parameters.Add("@docNr", OleDbType.VarChar).Value = "%" & TextBoxNrDoc.Text & "%"
            End If

            If (Not String.IsNullOrEmpty(TextBoxNote.Text)) Then
                cmd.Parameters.Add("@note", OleDbType.VarChar).Value = "%" & TextBoxNote.Text & "%"
            End If

            Dim da As New OleDbDataAdapter(cmd)

            Try
                da.Fill(table)
            Catch ex As Exception
                Trace.WriteLine(ex.Message & " " & cmd.CommandText)
            End Try

        End Using

        DataGridView1.DataSource = table
        DataGridView1.Columns("ID").Visible = False
        DataGridView1.Columns("tipo_doc").Width = 100
        DataGridView1.Columns("tipo_doc").HeaderText = "Tipo Doc"

        DataGridView1.Columns("dateReg").Width = 75
        DataGridView1.Columns("dateReg").HeaderText = "Data Reg"
        DataGridView1.Columns("dateReg").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DataGridView1.Columns("dateDoc").Width = 75
        DataGridView1.Columns("dateDoc").HeaderText = "Data Doc"
        DataGridView1.Columns("dateDoc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DataGridView1.Columns("docNr").Width = 50
        DataGridView1.Columns("docNr").HeaderText = "Nr Doc"

        DataGridView1.Columns("amount").Width = 60
        DataGridView1.Columns("amount").HeaderText = "totale"
        DataGridView1.Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DataGridView1.Columns("note").Width = 140
        DataGridView1.Columns("note").HeaderText = "note"

        If (table.Rows.Count = 1) Then
            selectedId = CType(table.Rows(0).Item(0), Integer)
        End If

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        selectedId = CType(DataGridView1.Rows(e.RowIndex).Cells(0).Value, Integer)
        Me.DialogResult = If(selectedId > 0, Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Cancel)
    End Sub

    Private Sub Search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (selectedId > 0) Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        loadGrid(0)
    End Sub


    '@return sql string search
    Private Function sqlString(top As Integer) As String

        Dim sb = New StringBuilder()

        sb.Append("SELECT ")
        If (top > 0) Then
            sb.Append("top " & top & " ")
        End If

        sb.Append("d.ID, dt.name tipo_doc, CONVERT(VARCHAR(10), d.dateReg, 103) dateReg, CONVERT(VARCHAR(10), d.dateDoc, 103) dateDoc, d.docNr, d.amount, d.note ")
        sb.Append("FROM Document d ")
        sb.Append("left join DocumentType dt on dt.id = d.DocumentType_ID ")
        sb.Append("where 1=1 ")

        If (ComboBoxDocType.SelectedValue > 0) Then
            sb.Append("and d.DocumentType_ID  = " & ComboBoxDocType.SelectedValue & " ")
        End If

        sb.Append("and d.dateReg between " & escapeDateTime(DateTimePickerDre.Value) & " and " & escapeDateTime(DateTimePickerDre2.Value, True) & " ")
        sb.Append("and d.dateDoc between " & escapeDateTime(DateTimePickerDoc.Value) & " and " & escapeDateTime(DateTimePickerDoc2.Value, True) & " ")

        If (Not String.IsNullOrEmpty(TextBoxNrDoc.Text)) Then
            sb.Append("and d.docNr like ? ")
        End If

        If (Not String.IsNullOrEmpty(TextBoxNote.Text)) Then
            sb.Append("and d.note like ? ")
        End If

        If (Not String.IsNullOrEmpty(TextBoxAmount.Text) OrElse Not String.IsNullOrEmpty(TextBoxAmount2.Text)) Then
            sb.Append("and d.amount between " & parseDecimal(TextBoxAmount.Text, False) & " and " & parseDecimal(TextBoxAmount2.Text, True) & " ")
        End If


        sb.Append("order by cast(d.dateReg As Date) desc, cast(d.dateDoc As Date), d.docNr, d.ID ")

        sqlString = sb.ToString

    End Function

    Private Function escapeDateTime(d As Date, Optional max As Boolean = False)
        Dim myDate = New DateTime(d.Year, d.Month, d.Day)
        escapeDateTime = "'" & myDate.ToString("yyyy-MM-dd") & "T" & IIf(max, "23:59:59", "00:00:00") & "'"

    End Function

    Private Function parseDecimal(value As String, Optional defaultUseMax As Boolean = False) As Decimal

        If (String.IsNullOrEmpty(value)) Then
            Return IIf(defaultUseMax, Decimal.MaxValue, Decimal.Zero)
        End If

        Try
            parseDecimal = Convert.ToDecimal(value)
        Catch ex As Exception
            parseDecimal = Decimal.Zero
        End Try

    End Function


End Class