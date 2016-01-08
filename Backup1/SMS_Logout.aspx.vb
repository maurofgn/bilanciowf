Public Class SMS_Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(0)
        Session("MeSIS_SMS_myTyUtente") = tyUtente_Azzera()
        Session.Abandon()
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
    End Sub

End Class