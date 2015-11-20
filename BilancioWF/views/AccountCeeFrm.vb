Imports System.Data.OleDb
Imports System.Text
Imports BilancioWF.Models
Imports System.Text.RegularExpressions

Public Class AccountCeeFrm
    Implements Traverse

    Dim allNodes As Dictionary(Of Integer, AccountCee)
    Private Property currentNode As AccountCee

    Private controller As AccountCeeController = New AccountCeeController()

    Private Sub ContoCee_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

        controller.dispose()

    End Sub

    Private Sub ContoCee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        allNodes = controller.loadAall(False)
        populateTree()
        PopulateCombo(ComboBoxParent, controller.getSummary(allNodes.Values), "id", "codeName")

        TreeView1.ExpandAll()

    End Sub

    Private Sub populateTree()

        allNodes.Values.ToList.ForEach(Sub(oneRow) AddNode(oneRow))

    End Sub

    Private Sub AddNode(oneRow As AccountCee)

        Dim nodes As New List(Of TreeNode)

        nodes.AddRange(TreeView1.Nodes.Find(oneRow.ParentID.ToString, True))

        Dim n As TreeNode = New TreeNode()
        n.Text = oneRow.CodeName
        n.Name = oneRow.ID.ToString
        n.Tag = oneRow

        If Not nodes.Any Then
            TreeView1.Nodes.Add(n)
        Else
            nodes(0).Nodes.Add(n)
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

        Try
            Dim key As Integer = Integer.Parse(e.Node.Name)
            currentNode = allNodes.Item(key)

            TextBoxCode.Text = currentNode.Code
            TextBoxName.Text = currentNode.Name
            TextBoxSeqNo.Text = currentNode.SeqNo

            CheckBoxParent.Enabled = currentNode.AccountCharts.Count = 0 AndAlso currentNode.Sons.Count = 0

            CheckBoxParent.Checked = currentNode.Summary
            CheckBoxTotal.Checked = currentNode.Total

            CheckBoxTotal.Enabled = currentNode.AccountCharts.Count = 0 AndAlso currentNode.Summary

            CheckBoxDebit.Checked = currentNode.Debit = DareAvere.Dare
            CheckBoxActive.Checked = currentNode.Active

            If (Not IsNothing(e.Node.Parent)) Then
                ComboBoxParent.Text = e.Node.Parent.Tag.CodeName
            Else
                ComboBoxParent.Text = ""
            End If

            ComboBoxParent.Enabled = currentNode.NodeType = NodeType.ALTRO

            ButtonNew.Enabled = currentNode.NodeType = NodeType.ALTRO
            ButtonSave.Enabled = currentNode.NodeType > NodeType.ROOT
            ButtonDel.Enabled = currentNode.NodeType = NodeType.ALTRO AndAlso currentNode.AccountCharts.Count = 0 AndAlso currentNode.Sons.Count = 0

        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try
    End Sub

    'Private Sub TreeView1_Click(sender As Object, e As EventArgs) Handles TreeView1.Click
    '    'ClearBackColor()
    'End Sub


    'Private Sub ClearBackColor()

    '    Dim nodes As TreeNodeCollection = TreeView1.Nodes
    '    For Each n In nodes
    '        ClearRecursive(n)
    '    Next

    'End Sub

    '' called by ClearBackColor function
    'Private Sub ClearRecursive(treeNode As TreeNode)
    '    For Each tn In treeNode.Nodes
    '        tn.BackColor = Color.White
    '        ClearRecursive(tn)
    '    Next
    'End Sub


    Private Sub CheckBoxParent_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxParent.CheckedChanged
        Dim c As CheckBox = CType(sender, CheckBox)
        If (Not c.Checked) Then
            CheckBoxTotal.Checked = False
        End If

        CheckBoxTotal.Enabled = c.Checked

    End Sub

    Function oneSon(son As AccountCee, level As Integer) As Boolean Implements Traverse.oneSon
        Return True
    End Function
    Function oneParent(parent As AccountCee, level As Integer) As Boolean Implements Traverse.oneParent
        Return parent.ID <> currentNode.ID
    End Function

    Private Sub ComboBoxParent_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBoxParent.SelectedValueChanged
        If (IsNothing(currentNode) OrElse currentNode.isLeaf) Then
            Return
        End If

        Dim ac = TreeView1.Nodes.Find(ComboBoxParent.SelectedValue.ToString, True)
        If (ac.Count <> 1) Then
            MessageBox.Show("Il padre non è presente nell'albero", "Padre non valido")
            ComboBoxParent.SelectedValue = currentNode.ParentID.ToString
            Return  'nodo padre non trovato nell'albero
        End If

        Dim newParent As AccountCee = ac(0).Tag

        If (Not newParent.traverseParents(Me)) Then
            MessageBox.Show("Il padre non può essere anche un figlio", "Padre non valido")
            ComboBoxParent.SelectedValue = currentNode.ParentID.ToString
        End If

    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click


        If (Not assigneCurrent()) Then
            Return
        End If

        Dim newRec As Boolean = IsNothing(currentNode.ID) OrElse currentNode.ID = 0
        controller.save(currentNode)
        If (newRec) Then
            Dim n As AccountCee = controller.getFromValue(currentNode.Code)
            If (Not IsNothing(n)) Then
                allNodes.Add(n.ID, n)
                AddNode(n)
                '                TreeView1.SelectedNode.ExpandAll()
                TreeView1.Refresh()
            End If
        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click

        controller.remove(currentNode.ID)
        If (allNodes.ContainsKey(currentNode.ID)) Then
            allNodes.Remove(currentNode.ID)

            TreeView1.Nodes.RemoveByKey(currentNode.ID)
            'TreeView1.Refresh()
            'treeView1.SelectedNode.Remove();


        End If

    End Sub

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click

        currentNode = New AccountCee()

    End Sub

    Private Function validateErrors() As IEnumerable(Of String)
        Dim errors = New List(Of String)

        If (String.IsNullOrWhiteSpace(TextBoxCode.Text)) Then
            errors.Add("Il codice (univoco) è necessario")
        ElseIf (TextBoxCode.Text.Trim().Length = 0 Or TextBoxCode.Text.Trim().Length > 20) Then
            errors.Add("Il codice deve essere di almeno 1 carattere e massimo 20.")
        End If

        If (TextBoxName.Text.Trim().Length < 3 Or TextBoxName.Text.Trim().Length > 60) Then
            errors.Add("Il nome deve essere di almeno 3 caratteri e massimo 60.")
        End If

        If (IsNothing(ComboBoxParent.SelectedValue)) Then
            errors.Add("Il padre è necessario")
        End If

        'If (controller.codeExist(currentNode.ID, TextBoxCode.Text)) Then
        '    errors.Add("codice già esistente")
        'End If

        Return errors

    End Function


    Private Function assigneCurrent() As Boolean

        Dim errors = validateErrors()
        If (errors.Count > 0) Then
            MsgBox(String.Join(Environment.NewLine, errors.ToArray()))
            Return False
        End If

        currentNode.Code = TextBoxCode.Text
        currentNode.Name = TextBoxName.Text
        currentNode.SeqNo = TextBoxSeqNo.Text
        currentNode.Summary = CheckBoxParent.Checked
        currentNode.Total = CheckBoxTotal.Checked
        currentNode.Debit = IIf(CheckBoxDebit.Checked, DareAvere.Dare, DareAvere.Avere)
        currentNode.Active = CheckBoxActive.Checked

        Dim newParent As AccountCee = allNodes.Item(ComboBoxParent.SelectedValue)

        If (IsNothing(currentNode.Parent)) Then
            newParent.Sons.Add(currentNode)
        ElseIf (currentNode.ParentID <> newParent.ID) Then
            currentNode.Parent.Sons.Remove(currentNode)
            newParent.Sons.Add(currentNode)
        End If

        currentNode.ParentID = newParent.ID
        currentNode.Parent = newParent

        Return True

    End Function

    Private Sub TextBoxCode_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCode.LostFocus
        If (currentNode.ID > 0 AndAlso TextBoxCode.Text = currentNode.Code) Then
            Return
        End If

        If (controller.codeExist(currentNode.ID, TextBoxCode.Text)) Then
            MsgBox("codice già esistente")
            TextBoxCode.Text = currentNode.Code
        End If

    End Sub

#Region "Search node"
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

        Dim searchString As String = Nothing

        Dim sea As SearchParameter = New SearchParameter()
        Try
            If (sea.ShowDialog(Me) = DialogResult.OK) Then
                searchString = sea.TextBoxSearch.Text
            End If
        Finally
            sea.Dispose()
        End Try

        ClearBackColor()
        If (Not String.IsNullOrWhiteSpace(searchString)) Then
            FindByText(searchString)
        End If
    End Sub

    Private Sub FindByText(search As String)

        Dim searchRegEx As String = ".*(?i:(" & search & ")).*"     'case insensitive
        'Dim searchRegEx As String = ".*(?i:(" & search & ")).*"    'case sensitive

        'Dim searchRegEx As String = ".*?i:" & search & ".*"
        Dim re As Regex = New Regex(searchRegEx)

        Dim nodes As TreeNodeCollection = TreeView1.Nodes
        For Each n In nodes
            FindRecursive(n, re)
        Next

    End Sub

    Private Sub FindRecursive(treeNode As TreeNode, re As Regex)
        For Each tn In treeNode.Nodes
            If (re.Match(tn.Text.ToUpper).Success() OrElse re.Match(tn.name.ToUpper).Success()) Then
                tn.BackColor = Color.Yellow
            End If

            FindRecursive(tn, re)
        Next

    End Sub

#End Region

#Region "Remove BackColor"

    '' recursively move through the treeview nodes
    '' and reset backcolors to white
    Private Sub ClearBackColor()
        Dim nodes As TreeNodeCollection = TreeView1.Nodes
        For Each n In nodes
            ClearRecursive(n)
        Next

    End Sub


    ''called by ClearBackColor function
    Private Sub ClearRecursive(treeNode As TreeNode)
        For Each tn In treeNode.Nodes
            tn.BackColor = Color.White
            ClearRecursive(tn)
        Next
    End Sub

#End Region



End Class