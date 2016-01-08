Public Class SMS_LogIn
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MeSIS_SMS_myTyUtente") = tyUtente_Azzera()
    End Sub

    Private Sub LoginSMS_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles LoginSMS.Authenticate
        Dim sErrore As String = ""
        Dim bOK As Boolean
        Dim myTyUtente As tyUtente = tyUtente_Azzera()

        Call FaiAccesso(LoginSMS.UserName, LoginSMS.Password, sErrore, myTyUtente)

        If sErrore <> "" Then
            LoginSMS.FailureText = sErrore & "<br>"
            bOK = False
        Else
            Session("MeSIS_SMS_myTyUtente") = myTyUtente
            Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_View_Errori)
            bOK = True
        End If

        e.Authenticated = bOK
    End Sub

    Private Sub LoginSMS_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginSMS.LoggedIn
        Response.Redirect("~/SMS_Private/CercaSpedizioni.aspx")
    End Sub
End Class