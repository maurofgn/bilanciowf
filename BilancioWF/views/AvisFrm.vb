Imports BilancioWF.Models

Public Class AvisFrm

    Private controller As AvisController = New AvisController()
    Private current As Avis


    'Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

    '    Dim sea As Search = New Search("Avis", "code", "name", "id", "active", TextBoxCode.Text, TextBoxName.Text)

    '    Try
    '        If (DialogResult.OK = sea.ShowDialog(Me)) Then
    '            current = controller.Edit(sea.selectedId())
    '            assignView()
    '        End If

    '    Finally
    '        sea.Dispose()
    '    End Try

    'End Sub

    'Assegna il record alla view
    Private Sub assignView()
        If (IsNothing(current)) Then
            current = New Avis()
        End If

        TextBoxName.Text = current.Name
        CheckBoxActive.Checked = current.Active
        TextBoxName.Text = current.Name
        TextBoxAddress.Text = current.Address
        TextBoxCity.Text = current.City
        TextBoxPostalCode.Text = current.PostalCode
        TextBoxRegion.Text = current.Region
        TextBoxEmail.Text = current.Email
        TextBoxPhone.Text = current.Phone
        TextBoxContactName.Text = current.ContactName

        'ButtonNew.Enabled = current.ID > 0
        'ButtonDel.Enabled = current.ID > 0 AndAlso controller.keyNotUsed(current.ID)

    End Sub

    'assegna oggetto dalla view
    Private Function assigneCurrent() As Boolean

        Dim errors = validateErrors()
        If (errors.Count > 0) Then
            MsgBox(String.Join(Environment.NewLine, errors.ToArray()))
            Return False
        End If

        If (IsNothing(current)) Then
            current = New Avis()
        End If

        current.Name = TextBoxName.Text
        current.Address = TextBoxAddress.Text
        current.City = TextBoxCity.Text
        current.PostalCode = TextBoxPostalCode.Text
        current.Region = TextBoxRegion.Text
        current.Email = TextBoxEmail.Text
        current.Phone = TextBoxPhone.Text
        current.ContactName = TextBoxContactName.Text
        'current.Active = CheckBoxActive.Checked

        Return True

    End Function

    Private Function validateErrors() As IEnumerable(Of String)
        Dim id As Integer = 0
        If (Not IsNothing(current)) Then
            id = current.ID
        End If

        Dim errors = New List(Of String)


        If (Name.Trim().Length < 3 Or Name.Trim().Length > 60) Then
            errors.Add("Il nome deve essere di almeno 3 caratteri e massimo 60.")
        End If

        Return errors

    End Function

    'Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
    '    current = Nothing
    '    ButtonNew.Enabled = False
    '    ButtonDel.Enabled = True
    'End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If (assigneCurrent()) Then

            Dim successful As Boolean = controller.save(current)
            'ButtonNew.Enabled = successful
            'ButtonDel.Enabled = True

            If (Not successful) Then
                MsgBox("Salvataggio non riuscito")
            End If

        End If
    End Sub

    'Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
    '    If (controller.remove(current.ID)) Then
    '        'MsgBox("Eliminato")
    '        current = Nothing
    '        assignView()
    '    Else
    '        MsgBox("Non è possibile eliminare questo record")
    '    End If

    'End Sub

    Private Sub AvisFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ButtonNew.Enabled = False
        current = controller.getFromValue()
        assignView()
    End Sub
End Class