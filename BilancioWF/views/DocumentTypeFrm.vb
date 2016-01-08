Imports BilancioWF.Models

Public Class DocumentTypeFrm

    Private controller As DocumentTypeController = New DocumentTypeController()
    Private current As DocumentType


    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

        Dim sea As Search = New Search("DocumentType", "code", "name", "id", "active", TextBoxCode.Text, TextBoxName.Text)

        Try
            If (DialogResult.OK = sea.ShowDialog(Me)) Then
                current = controller.Edit(sea.selectedId())
                assignView()
            End If

        Finally
            sea.Dispose()
        End Try

    End Sub

    'Assegna il record alla view
    Private Sub assignView()
        If (IsNothing(current)) Then
            current = New DocumentType()
        End If

        TextBoxCode.Text = current.Code
        TextBoxName.Text = current.Name
        CheckBoxActive.Checked = current.Active

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
            current = New DocumentType()
        End If

        current.Code = TextBoxCode.Text
        current.Name = TextBoxName.Text
        current.Active = CheckBoxActive.Checked

        Return True

    End Function

    Private Function validateErrors() As IEnumerable(Of String)
        Dim id As Integer = 0
        If (Not IsNothing(current)) Then
            id = current.ID
        End If

        Dim errors As List(Of String) = controller.validateErrors(TextBoxCode.Text, TextBoxName.Text, id)

        Return errors

    End Function

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

    Private Sub DocumentTypeFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonNew.Enabled = False
    End Sub

End Class