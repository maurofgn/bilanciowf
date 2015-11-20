Imports System.Text
Imports BilancioWF.Models
Imports System.Data.OleDb

Public Class AccountCeeController

    Public Function loadAall(Optional lazy As Boolean = True) As Dictionary(Of Integer, AccountCee)

        Dim nodes As Dictionary(Of Integer, AccountCee) = New Dictionary(Of Integer, AccountCee)

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append("from accountCee n ")

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)
            Dim t As Type = GetType(AccountCee)

            For i = 0 To dt.Rows.Count - 1
                Dim row As AccountCee = CaricaDaDB(dt.Rows(i), t)
                nodes.Add(row.ID, row)
            Next

            If (Not lazy) Then
                loadChart(nodes, connection)
            End If

        End Using

        'definisce il legame padre/figlio
        For Each s As AccountCee In nodes.Values
            If (Not IsNothing(s.ParentID)) Then
                Dim p As AccountCee = nodes.Item(s.ParentID)
                p.Sons.Add(s)
                s.Parent = p
            End If
        Next

        Return nodes

    End Function

    Private Sub loadChart(nodes As Dictionary(Of Integer, AccountCee), Optional connection As OleDbConnection = Nothing)

        Dim sb = New StringBuilder()
        sb.Append("SELECT c.* ")
        sb.Append("FROM AccountChart c ")
        sb.Append("where c.AccountCeeID is not null ")
        If (nodes.Count > 0 AndAlso nodes.Count <= MAX_ELEMENTS_ON_WHERE_IN) Then
            sb.Append("and c.AccountCeeID in (")
            sb.Append(String.Join(",", nodes.Keys.ToArray))
            sb.Append(")")
        End If
        'sb.Append("order by c.AccountCeeID ")


        Dim dt As DataTable = Nothing
        If (IsNothing(connection)) Then
            Using connectionTmp = getConnectionOpened()
                dt = GetDataTable(connectionTmp, sb.ToString)
            End Using
        Else
            dt = GetDataTable(connection, sb.ToString)
        End If

        Dim t = GetType(AccountChart)

        For i = 0 To dt.Rows.Count - 1
            Dim row As AccountChart = CaricaDaDB(dt.Rows(i), t)
            If (nodes.ContainsKey(row.AccountCeeID)) Then
                Dim accountCee = nodes.Item(row.AccountCeeID)
                accountCee.AccountCharts.Add(row)
                row.AccountCee = accountCee
            End If
        Next

    End Sub

    Sub dispose()

    End Sub

    '@param nodes elenco dei nodi da cui ricavare la lista (ordinata) dei summary
    Public Function getSummary(nodes As ICollection(Of AccountCee)) As IEnumerable(Of AccountCee)
        If (IsNothing(nodes) OrElse nodes.Count = 0) Then
            Return Nothing
        End If

        getSummary = nodes.ToList _
            .Where(Function(a) a.Summary And a.Active And a.NodeType > NodeType.ECONOMICO) _
            .OrderBy(Function(a) IIf(a.NodeType = NodeType.ALTRO, NodeType.ROOT, a.NodeType)) _
            .ThenBy(Function(a) a.Code) _
            .ThenBy(Function(a) a.Name) _
            .ToList()

    End Function

    'Private Function getRoot() As DataSet
    '    Dim ds As New DataSet()
    '    Try

    '        Using connection = New OleDbConnection(getConnectionString())

    '            Using da = New OleDbDataAdapter("SELECT * from accountCee where nodeType = 0", connection)

    '                connection.Open()
    '                da.Fill(ds, "AccountCee")
    '                connection.Close()

    '            End Using

    '        End Using

    '    Catch ex As Exception
    '        Trace.WriteLine(ex.Message)
    '    End Try

    '    Return ds

    'End Function

    Function save(accountCee As AccountCee) As Boolean
        If (IsNothing(accountCee)) Then
            Return True
        End If

        Dim retValue As Boolean = True

        'Dim oledbAdapter As OleDbDataAdapter = New OleDbDataAdapter()
        'oledbAdapter.InsertCommand()

        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                If (accountCee.ID <= 0) Then
                    sb.Append("INSERT AccountCee ")
                    sb.Append("(Code, Name, Active, SeqNo, Summary, Total, Debit, NodeType, ParentID) ")
                    sb.Append("VALUES ")
                    sb.Append("(?, ?, ?, ?, ?, ?, ?, ?, ?) ")
                Else
                    sb.Append("UPDATE AccountCee SET ")
                    sb.Append("Code=?, ")
                    sb.Append("Name=?, ")
                    sb.Append("Active=?, ")
                    sb.Append("SeqNo=?, ")
                    sb.Append("Summary=?, ")
                    sb.Append("Total=?, ")
                    sb.Append("Debit=?, ")
                    sb.Append("NodeType=?, ")
                    sb.Append("ParentID=? ")
                    sb.Append("WHERE ")
                    sb.Append("ID=? ")
                End If

                Dim com As OleDbCommand = New OleDbCommand(sb.ToString, connection)
                com.Parameters.Add("@Code", OleDbType.VarChar).Value = accountCee.Code
                com.Parameters.Add("@Name", OleDbType.VarChar).Value = accountCee.Name
                com.Parameters.Add("@Active", OleDbType.Boolean).Value = accountCee.Active
                com.Parameters.Add("@SeqNo", OleDbType.Integer).Value = accountCee.SeqNo
                com.Parameters.Add("@Summary", OleDbType.Boolean).Value = accountCee.Summary
                com.Parameters.Add("@Total", OleDbType.Boolean).Value = accountCee.Total
                com.Parameters.Add("@Debit", OleDbType.Integer).Value = accountCee.Debit
                com.Parameters.Add("@NodeType", OleDbType.Integer).Value = accountCee.NodeType
                com.Parameters.Add("@ParentID", OleDbType.Integer).Value = accountCee.ParentID

                If (accountCee.ID > 0) Then
                    com.Parameters.Add("@ID", OleDbType.Integer).Value = accountCee.ID
                End If

                com.ExecuteNonQuery()

                'Trace.WriteLine(IIf(accountCee.ID <= 0, "Insert", "Update"))

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                retValue = False
                'Finally
            End Try

        End Using

        Return retValue

    End Function

    Private Function sqlBoolValue(val As Boolean)
        sqlBoolValue = IIf(val, 1, 0)
    End Function

    Function remove(accountCee As AccountCee) As Boolean

        Return remove(IIf(IsNothing(accountCee), 0, accountCee.ID))

    End Function

    Function remove(key As Integer) As Boolean

        If (key <= 0) Then
            Return True
        End If

        Dim retValue As Boolean = True
        Using connection = getConnectionOpened()
            Try
                Dim com As OleDbCommand = New OleDbCommand("delete from AccountCee where ID = ?", connection)
                com.Parameters.Add("@ID", OleDbType.Integer).Value = key
                com.ExecuteNonQuery()
                MsgBox("record removed")
            Catch ex As Exception
                retValue = False
                MsgBox("record not removed " & ex.Message)
            End Try
        End Using

        Return retValue

    End Function

    ''load parent, sons e charts
    'Sub loadFK(Optional ByRef connection As OleDbConnection = Nothing)

    '    Dim newConn As Boolean = IsNothing(connection)

    '    If (newConn) Then
    '        connection = getConnectionOpened()
    '    End If

    '    Try
    '        loadParent(connection)
    '        'loadSons(connection)
    '        'loadCharts(connection)

    '    Catch ex As Exception
    '        Trace.WriteLine(ex.Message)
    '    Finally
    '        If (newConn) Then
    '            ConnectionClose(connection)
    '        End If
    '    End Try

    'End Sub

    'Private Sub loadCharts(connection As OleDbConnection)

    '    If (IsNothing(connection) OrElse connection.State <> ConnectionState.Open) Then
    '        Return
    '    End If

    '    AccountCharts.Clear()

    '    Dim sb = New StringBuilder()
    '    sb.Append("SELECT ")
    '    sb.Append("n.* ")
    '    sb.Append("from AccountChart n ")
    '    sb.Append("where n.AccountCeeID = " & ID)

    '    Dim dt As DataTable = GetDataTable(connection, sb.ToString)

    '    For i = 0 To dt.Rows.Count - 1
    '        AccountCharts.Add(CaricaDaDB(dt.Rows(i), GetType(AccountChart)))
    '    Next

    'End Sub

    'Private Sub loadSons(ByRef connection As OleDbConnection)

    '    If (IsNothing(connection) OrElse connection.State <> ConnectionState.Open) Then
    '        Return
    '    End If

    '    Sons.Clear()

    '    Dim sb = New StringBuilder()
    '    sb.Append("SELECT ")
    '    sb.Append("n.* ")
    '    sb.Append("from accountCee n ")
    '    sb.Append("where n.ParentID = " & ID)

    '    Dim dt As DataTable = GetDataTable(connection, sb.ToString)

    '    For i = 0 To dt.Rows.Count - 1
    '        Sons.Add(CaricaDaDB(dt.Rows, GetType(AccountCee), i))
    '    Next

    'End Sub
    'Private Sub loadParent(ByRef connection As OleDbConnection)

    '    If (IsNothing(ParentID) OrElse ParentID <= 0 OrElse IsNothing(connection) OrElse connection.State <> ConnectionState.Open) Then
    '        Return
    '    End If

    '    Dim sb = New StringBuilder()
    '    sb.Append("SELECT ")
    '    sb.Append("n.* ")
    '    sb.Append("from accountCee n ")
    '    sb.Append("where n.ID = " & ParentID)

    '    Parent = CaricaDaDB(connection, sb.ToString, GetType(AccountCee))

    'End Sub

    Friend Function getNodeFromType(Optional nodeType As NodeType = NodeType.ROOT) As AccountCee

        'Dim db As New BilancioContext

        'Try
        '    Return db.AccountCees().Where(Function(p) p.NodeType = nodeType).Take(1).First
        'Catch ex As Exception
        '    Return Nothing
        'End Try

        Dim retValue As AccountCee = Nothing

        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                sb.Append("SELECT ")
                sb.Append("n.* ")
                sb.Append("from accountCee n ")
                sb.Append("where n.nodeType = " & nodeType)

                Using oledbAdapter = New OleDbDataAdapter(sb.ToString, connection)

                    Dim ds As New DataSet()
                    oledbAdapter.Fill(ds, "AccountCee")
                    oledbAdapter.Dispose()
                    retValue = CaricaDaDB(ds.Tables(0).Rows, GetType(AccountCee), 0)

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            Finally
                ConnectionClose(connection)
            End Try

        End Using

        Return retValue

    End Function

    Function getFromValue(code As String) As AccountCee

        Dim retValue As AccountCee = Nothing

        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM AccountCee c ")
                sb.Append("where c.code = ? ")

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString(), connection)
                cmd.Parameters.Add("@Code", OleDbType.VarChar).Value = code

                Using oledbAdapter = New OleDbDataAdapter(cmd)

                    Dim ds As New DataSet()
                    oledbAdapter.Fill(ds, "AccountCee")
                    oledbAdapter.Dispose()
                    retValue = CaricaDaDB(ds.Tables(0).Rows, GetType(AccountCee), 0)

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            Finally
                ConnectionClose(connection)
            End Try

        End Using

        Return retValue
    End Function

    'Controlle della non esistenza del codice
    Public Function codeExist(id As Integer, code As String) As Boolean

        If (String.IsNullOrEmpty(code)) Then
            Return False
        End If

        Dim retValue As Boolean = True

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()
                sb.Append("SELECT c.* ")
                sb.Append("FROM AccountCee c ")
                sb.Append("where c.code = ? ")
                sb.Append("and c.id != ? ")

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                'cmd.Parameters.AddWithValue("@code", code)
                cmd.Parameters.Add("@code", OleDbType.VarChar).Value = code

                'cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.Add("@ID", OleDbType.Integer).Value = id

                Dim dr = cmd.ExecuteReader
                retValue = dr.Read()

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function


End Class
