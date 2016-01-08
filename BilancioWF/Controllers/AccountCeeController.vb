Imports System.Text
Imports BilancioWF.Models
Imports System.Data.OleDb
Imports BilancioWF.ViewModels

Public Class AccountCeeController
    Inherits ControllerAbstract

    Public Overrides Property TABLE_NAME As String = "AccountCee"
    Public Overrides Property TYPE As Type = GetType(AccountCee)


    Public Shared Function baseBalance() As List(Of AccountCee)

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append("from accountCee n ")
        sb.Append("where n.nodeType <= " & NodeType.ECONOMICO & " ")
        sb.Append("order by n.nodeType ")

        Dim retValue = New List(Of AccountCee)
        Dim t = GetType(AccountCee)

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)

            For i = 0 To dt.Rows.Count - 1
                retValue.Add(CaricaDaDB(dt.Rows(i), t))
            Next

        End Using

        Return retValue

    End Function

    Public Shared Function baseYears() As List(Of Integer)

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("distinct year(d.dateReg) yy ")
        sb.Append("FROM Document d ")
        sb.Append("order by year(d.dateReg) desc ")

        Dim retValue = New List(Of Integer)
        Using connection = getConnectionOpened()
            Try
                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                Dim dr = cmd.ExecuteReader
                While dr.Read
                    retValue.Add(dr.Item("yy"))
                End While

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function


    Public Shared Function loadAll(Optional lazy As Boolean = True) As Dictionary(Of Integer, AccountCee)

        Dim nodes As Dictionary(Of Integer, AccountCee) = New Dictionary(Of Integer, AccountCee)

        Dim sb = New StringBuilder()
        sb.Append("SELECT ")
        sb.Append("n.* ")
        sb.Append("from AccountCee n ")

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)
            Dim t = GetType(AccountCee)

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

    Private Shared Sub loadChart(nodes As Dictionary(Of Integer, AccountCee), Optional connection As OleDbConnection = Nothing)

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
    Public Shared Function getSummary(nodes As ICollection(Of AccountCee)) As List(Of AccountCee)
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

    Public Shared Function getLeaf() As List(Of AccountCee)

        Dim ht As Dictionary(Of Integer, AccountCee) = loadAll()
        Return getLeaf(ht.Values)
    End Function

    '@param nodes elenco dei nodi da cui ricavare la lista (ordinata) delle foglie
    Public Shared Function getLeaf(nodes As ICollection(Of AccountCee)) As List(Of AccountCee)
        If (IsNothing(nodes) OrElse nodes.Count = 0) Then
            Return Nothing
        End If

        getLeaf = nodes.ToList _
            .Where(Function(a) Not a.Summary And a.Active And a.NodeType = NodeType.ALTRO) _
            .OrderBy(Function(a) IIf(a.ID <= 0, NodeType.ROOT, a.NodeType)) _
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
    Function nextSeqNo(parentId As Integer) As Integer

        Dim retValue As Integer = 10

        If (parentId <= 0) Then
            Return 10
        End If

        Using connection = getConnectionOpened()

            Try
                Dim sb = New StringBuilder()

                sb.Append("SELECT ")
                sb.Append("max(seqNo)/10*10+10 next_seqNo ")
                sb.Append("FROM AccountCee ")
                sb.Append("where parentID = ? ")

                Dim cmd As OleDbCommand = New OleDbCommand(sb.ToString, connection)

                cmd.Parameters.Add("@parentID", OleDbType.VarChar).Value = parentId

                Dim dr = cmd.ExecuteReader
                If (dr.Read()) Then
                    retValue = dr.Item("next_seqNo")
                End If

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

        End Using

        Return retValue

    End Function


    'Overrides Sub rowUpdatedInserted(ByRef rowNr As Integer, ByRef current As Object)
    '    If (current.ID > 0) Then
    '        Trace.WriteLine("Righe modificate: " & rowNr)
    '    Else
    '        Trace.WriteLine("Righe inserite: " & rowNr)
    '        current.ID = getIdFromValue(current.Code)
    '    End If

    'End Sub

    'Public Overrides Function commandSave(ByRef connection As OleDbConnection, ByRef objToSave As Object) As OleDbCommand

    '    Dim current As AccountCee = TryCast(objToSave, AccountCee)
    '    If (IsNothing(current)) Then
    '        Return Nothing
    '    End If

    '    Dim ancestor = current.getAncestor()

    '    Dim sb = New StringBuilder()
    '    If (current.ID <= 0) Then
    '        sb.Append("INSERT AccountCee ")
    '        sb.Append("(Code, Name, Active, SeqNo, Summary, Total, Debit, NodeType, ParentID) ")
    '        sb.Append("VALUES ")
    '        sb.Append("(?, ?, ?, ?, ?, ?, ?, ?, ?) ")
    '    Else
    '        sb.Append("UPDATE AccountCee SET ")
    '        sb.Append("Code=?, ")
    '        sb.Append("Name=?, ")
    '        sb.Append("Active=?, ")
    '        sb.Append("SeqNo=?, ")
    '        sb.Append("Summary=?, ")
    '        sb.Append("Total=?, ")
    '        sb.Append("Debit=?, ")
    '        sb.Append("NodeType=?, ")
    '        sb.Append("ParentID=? ")
    '        sb.Append("WHERE ")
    '        sb.Append("ID=? ")
    '    End If

    '    Dim com As OleDbCommand = Nothing
    '    Try
    '        com = New OleDbCommand(sb.ToString, connection)
    '        com.Parameters.Add("@Code", OleDbType.VarChar).Value = current.Code
    '        com.Parameters.Add("@Name", OleDbType.VarChar).Value = current.Name
    '        com.Parameters.Add("@Active", OleDbType.Boolean).Value = current.Active
    '        com.Parameters.Add("@SeqNo", OleDbType.Integer).Value = current.SeqNo
    '        com.Parameters.Add("@Summary", OleDbType.Boolean).Value = current.Summary
    '        com.Parameters.Add("@Total", OleDbType.Boolean).Value = current.Total
    '        com.Parameters.Add("@Debit", OleDbType.Integer).Value = ancestor.Debit            'dare/avere è sempre eriditato da Attivo/Passivo/Costi/ricavi
    '        com.Parameters.Add("@NodeType", OleDbType.Integer).Value = current.NodeType
    '        com.Parameters.Add("@ParentID", OleDbType.Integer).Value = current.ParentID

    '        If (current.ID > 0) Then
    '            com.Parameters.Add("@ID", OleDbType.Integer).Value = current.ID
    '        End If

    '    Catch ex As Exception
    '        Trace.WriteLine(ex.Message)
    '    End Try

    '    Return com

    'End Function

    'save di current. Se il record è nuovo (id <= 0, cioè insert) il nuovo id viene assegnato a current
    Function save(ByRef current As AccountCee) As Boolean
        If (IsNothing(current)) Then
            Return False
        End If

        save = False

        Dim ancestor = current.getAncestor()
        Using connection = getConnectionOpened()

            Try

                Dim sb = New StringBuilder()
                If (current.ID <= 0) Then
                    sb.Append("INSERT AccountCee ")
                    sb.Append("(Code, Name, Active, SeqNo, Summary, Total, Debit, NodeType, ParentID) ")
                    sb.Append("VALUES ")
                    sb.Append("(?, ?, ?, ?, ?, ?, ?, ?, ?) ")
                    sb.Append(";SELECT SCOPE_IDENTITY() AS last_id")    'per l'insert ritorno l'id utilizzato
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
                com.Parameters.Add("@Code", OleDbType.VarChar).Value = current.Code
                com.Parameters.Add("@Name", OleDbType.VarChar).Value = current.Name
                com.Parameters.Add("@Active", OleDbType.Boolean).Value = current.Active
                com.Parameters.Add("@SeqNo", OleDbType.Integer).Value = current.SeqNo
                com.Parameters.Add("@Summary", OleDbType.Boolean).Value = current.Summary
                com.Parameters.Add("@Total", OleDbType.Boolean).Value = current.Total
                com.Parameters.Add("@Debit", OleDbType.Integer).Value = ancestor.Debit            'dare/avere è sempre eriditato da Attivo/Passivo/Costi/ricavi
                com.Parameters.Add("@NodeType", OleDbType.Integer).Value = current.NodeType
                com.Parameters.Add("@ParentID", OleDbType.Integer).Value = current.ParentID

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

    Private Function sqlBoolValue(val As Boolean)
        sqlBoolValue = IIf(val, 1, 0)
    End Function

    'Public Overloads Function remove(accountCee As AccountCee) As Boolean

    '    If (IsNothing(accountCee)) Then
    '        Return True
    '    Else
    '        Return MyBase.remove(accountCee.ID)
    '    End If

    'End Function

    'Public Function remove(key As Integer) As Boolean
    '    Return False
    'End Function

    'Function remove(key As Integer) As Boolean

    '    If (key <= 0) Then
    '        Return True
    '    End If

    '    Dim retValue As Boolean = True
    '    Using connection = getConnectionOpened()
    '        Try
    '            Dim com As OleDbCommand = New OleDbCommand("delete from " & TABLE_NAME & " where ID = ?", connection)
    '            com.Parameters.Add("@ID", OleDbType.Integer).Value = key
    '            com.ExecuteNonQuery()
    '        Catch ex As Exception
    '            retValue = False
    '        End Try
    '    End Using

    '    Return retValue

    'End Function

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

    Friend Shared Function getNodeFromType(Optional nodeType As NodeType = NodeType.ROOT) As AccountCee

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
                    retValue = CaricaDaDB(ds.Tables(0).Rows, GetType(AccountCee))

                End Using

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            Finally
                ConnectionClose(connection)
            End Try

        End Using

        Return retValue

    End Function

    Function balance(Optional ByVal nodeType As NodeType = NodeType.ROOT, Optional ByVal year As Integer = 0) As List(Of CreditDebitAccount)


        Dim yy = IIf(year > 0, year, Now.Year)

        Dim nodes As Dictionary(Of Integer, AccountCee) = loadAll(False)

        Dim base As AccountCee = nodes.Values.ToList.Find(Function(a) a.NodeType = nodeType)

        Dim sons = base.getSons()   'elenco foglie per cui calcolare i totali contabili

        'Dim listId As List(Of Integer) = sons.Select(Function(x) x.ID).ToList

        Dim allAccountChart As Dictionary(Of Integer, AccountChart) = New Dictionary(Of Integer, AccountChart)
        'sons.ForEach(Sub(x) allAccountChart.UnionWith(x.AccountCharts))

        sons.ForEach(Sub(x) x.AccountCharts.ToList.ForEach(Sub(a) allAccountChart.Add(a.ID, a)))
        Trace.WriteLine("Nr conti su cui calcolare il bilancio: " & allAccountChart.Count)

        loadDocs(yy, allAccountChart)

        balance = base.getBalance(yy)

        Trace.WriteLine("Nr records x balance: " & balance.Count)

    End Function

    'carica le righe e le relative testate documenti per ogni conto
    '@param year last year
    '@param accountChar to load
    Private Sub loadDocs(year As Integer, ByRef allAccountChart As Dictionary(Of Integer, AccountChart))

        Dim listIdStr As String = String.Join(",", allAccountChart.Keys)

        Dim sb = New StringBuilder()

        sb.append("SELECT ")
        sb.append("dh.* ")
        sb.Append(",dr.* ")
        sb.Append(",dt.* ")
        sb.Append("FROM  DocumentRow dr ")
        sb.Append("inner join Document dh on dr.Document_ID = dh.id ")
        sb.Append("inner join DocumentType dt on dt.ID = dh.DocumentType_ID ")
        sb.Append("where ")
        sb.append("year(dh.dateReg) in (" & year - 1 & ", " & year & ") ")
        sb.append("and dr.AccountChart_ID in (" & listIdStr & ") ")
        sb.append("order by ")
        sb.append("dr.Document_ID, dr.id ")

        Dim objType As List(Of Type) = New List(Of Type)({GetType(Document), GetType(DocumentRow), GetType(DocumentType)})
        Dim docType As DocumentType = Nothing

        Using connection = getConnectionOpened()

            Dim dt = GetDataTable(connection, sb.ToString)
            Dim master As Document = Nothing

            For i = 0 To dt.Rows.Count - 1
                Dim x As List(Of Object) = CaricaDaDB(dt.Rows(i), objType)
                Dim m As Document = x(0)
                Dim detail As DocumentRow = x(1)
                Dim t As DocumentType = x(2)

                If (IsNothing(docType) OrElse t.ID <> docType.ID) Then
                    docType = t
                    docType.Document = New List(Of Document)
                End If

                If (IsNothing(master) OrElse master.ID <> m.ID) Then
                    master = m
                    master.documentType = docType
                    docType.Document.Add(master)
                End If

                master.documentRows.Add(detail)
                detail.Document = master

                If allAccountChart.ContainsKey(detail.AccountChart_ID) Then
                    allAccountChart.Item(detail.AccountChart_ID).addDocumentRow(detail)
                End If

                Trace.WriteLine(master)
            Next

        End Using

    End Sub

End Class

Public Class Trav
    Implements Traverse


    Public Function oneParent(son As AccountCee, level As Integer) As Boolean Implements Traverse.oneParent
        Return True
    End Function

    Public Function oneSon(son As AccountCee, level As Integer) As Boolean Implements Traverse.oneSon
        Return True
    End Function

End Class
