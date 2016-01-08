Imports System.Data.OleDb
Imports System.Text
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class Search

    Public Sub New(ByVal tableName As String, ByVal codeColName As String, ByVal nameColName As String,
                   Optional ByVal idColName As String = "ID", _
                   Optional ByVal activeColName As String = Nothing, _
                   Optional ByVal codeValue As String = Nothing,
                   Optional ByVal nameValue As String = Nothing _
                   )
        MyBase.New()
        InitializeComponent()

        Me.tableName = tableName
        Me.codeColName = codeColName
        Me.nameColName = nameColName
        Me.idColName = idColName
        Me.activeColName = activeColName
        If (Not String.IsNullOrWhiteSpace(codeValue)) Then
            Me.TextBoxSea.Text = codeValue
        ElseIf (Not String.IsNullOrWhiteSpace(nameValue)) Then
            Me.TextBoxSea.Text = nameValue
        End If

        loadGrid(IIf(String.IsNullOrWhiteSpace(TextBoxSea.Text), MAX_RECORDS_TO_SEARCH, 0))

    End Sub

    Private Property tableName As String
    Private Property codeColName As String
    Private Property nameColName As String
    Private Property idColName As String
    Private Property activeColName As String
    Public Property selectedId As Integer = 0

    '@return sql string search
    Private Function sqlString(top As Integer) As String

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        If (top > 0) Then
            sb.Append("top " & top & " ")
        End If
        sb.Append("t." & idColName & " as id")
        sb.Append(",t." & codeColName & " as code")
        sb.Append(",t." & nameColName & " as name")
        If (Not String.IsNullOrWhiteSpace(activeColName)) Then
            sb.Append(",t." & activeColName & " as active")
        End If

        sb.Append(" FROM ")
        sb.Append(tableName & " t ")
        sb.Append("where ")
        'If (Not String.IsNullOrWhiteSpace(activeColName)) Then
        '    sb.Append("t.Active <> 0 and ")
        'End If
        sb.Append("( ")
        sb.Append("t." & codeColName & " like ? ")
        sb.Append("or ")
        sb.Append("t." & nameColName & " like ? ")
        sb.Append(") ")

        sqlString = sb.ToString

    End Function

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        loadGrid(0)
    End Sub

    Private Sub loadGrid(maxRecords As Integer)
        Dim cmd As New OleDbCommand
        Dim table As DataTable = New DataTable()
        Using connection = getConnectionOpened()
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sqlString(maxRecords)
            cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = "%" & TextBoxSea.Text & "%"
            cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = "%" & TextBoxSea.Text & "%"
            Dim da As New OleDbDataAdapter(cmd)
            da.Fill(table)
        End Using

        DataGridView1.DataSource = table
        DataGridView1.Columns("id").Visible = False
        DataGridView1.Columns("code").Width = 100
        DataGridView1.Columns("code").HeaderText = "Cod."
        DataGridView1.Columns("name").Width = 177
        DataGridView1.Columns("name").HeaderText = "Nome"
        DataGridView1.Columns("active").Width = 40
        DataGridView1.Columns("active").HeaderText = "attivo"

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
End Class