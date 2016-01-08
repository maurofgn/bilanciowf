Public Class SMS
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.IsAuthenticated Then
            CType(HeadLoginView.FindControl("lblUltimoAccesso"), Label).Text = CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sUltimoAccesso
        End If
    End Sub

End Class