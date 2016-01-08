Imports BilancioWF.Models

Public Class AccountChartFrm

    Private controller As AccountChartController = New AccountChartController()
    '    Private controllerCee As AccountCeeController = New AccountCeeController()
    Private current As AccountChart

    Dim leafs As Dictionary(Of Integer, CodeName)


    'Assegna il record alla view
    Private Sub assignView()
        If (IsNothing(current)) Then
            current = New AccountChart()
        End If

        TextBoxCode.Text = current.Code
        TextBoxName.Text = current.Name
        CheckBoxActive.Checked = current.Active

        If (current.AccountCeeID > 0 AndAlso leafs.ContainsKey(current.AccountCeeID)) Then
            Dim link As CodeName = leafs.Item(current.AccountCeeID)
            ComboAccountCee.Text = link.CodeName
        Else
            ComboAccountCee.Text = ""
            ComboAccountCee.SelectedIndex = 0
        End If

        If (current.Debit <> 0) Then
            LabelDebit.Text = current.Debit.ToString
        Else
            LabelDebit.Text = ""
        End If

        ButtonNew.Enabled = current.ID > 0
        ButtonDel.Enabled = current.ID > 0 AndAlso controller.keyNotUsed(current.ID)

    End Sub

    'assegna oggetto dalla view
    Private Function assigneCurrent() As Boolean

        Dim errors = validateErrors()
        If (errors.Count > 0) Then
            MsgBox(String.Join(Environment.NewLine, errors.ToArray()))
            Return False
        End If

        If (IsNothing(current)) Then
            current = New AccountChart()
        End If

        current.Code = TextBoxCode.Text
        current.Name = TextBoxName.Text
        current.Active = CheckBoxActive.Checked

        Dim acee As AccountCee = getAccountCee()

        current.Debit = acee.Debit
        current.AccountCeeID = acee.ID

        Return True

    End Function

    Private Function getAccountCee() As AccountCee

        '        ComboAccountCee.SelectedText

        If (IsNothing(ComboAccountCee.SelectedItem)) Then
            Return Nothing
        End If

        Try
            getAccountCee = CType(ComboAccountCee.SelectedItem, AccountCee)
        Catch ex As Exception
            getAccountCee = Nothing
        End Try

    End Function

    Private Function validateErrors() As IEnumerable(Of String)

        Dim id As Integer = 0
        If (Not IsNothing(current)) Then
            id = current.ID
        End If

        Dim errors As List(Of String) = controller.validateErrors(TextBoxCode.Text, TextBoxName.Text, id)

        Dim ace As AccountCee = getAccountCee()

        If (IsNothing(ace) OrElse ace.ID = 0) Then
            errors.Add("Definire il conto CEE")
        End If

        Return errors

    End Function

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Dim sea As Search = New Search("AccountChart", "code", "name", "id", "active", TextBoxCode.Text, TextBoxName.Text)


        Try
            If (DialogResult.OK = sea.ShowDialog(Me)) Then
                current = controller.Edit(sea.selectedId())
                assignView()
            End If

        Finally
            sea.Dispose()
        End Try
    End Sub


    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        current = Nothing
        ButtonNew.Enabled = False
        ButtonDel.Enabled = True
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If (assigneCurrent()) Then

            Dim successful As Boolean = controller.save(current)
            ButtonNew.Enabled = successful
            ButtonDel.Enabled = True

            If (Not successful) Then
                MsgBox("Salvataggio non riuscito")
            End If

        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        If (controller.remove(current)) Then
            'MsgBox("Eliminato")
            current = Nothing
            assignView()
        Else
            MsgBox("Non è possibile eliminare questo record")
        End If
    End Sub

    Private Sub AccountChartFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ComboAccountCee.Tag = True

        Dim leafList = New List(Of CodeName)

        'leafList.Insert(0, AccountCee.VOID)
        Try
            leafList.Add(AccountCee.VOID)
            leafList.AddRange(AccountCeeController.getLeaf())

            leafs = New Dictionary(Of Integer, CodeName)

            leafList.ForEach(Sub(a) leafs.Add(a.ID, a))

            PopulateCombo(ComboAccountCee, leafList, "id", "codeName")
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        Me.ComboAccountCee.Tag = False
        ButtonNew.Enabled = False

    End Sub

    Private Sub ComboAccountCee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboAccountCee.SelectedIndexChanged

        If (ComboAccountCee.Tag) Then
            Return
        End If

        Dim acee As AccountCee = getAccountCee()
        If (IsNothing(acee)) Then
            LabelDebit.Text = ""
        Else
            LabelDebit.Text = acee.Debit.ToString
        End If

    End Sub
End Class