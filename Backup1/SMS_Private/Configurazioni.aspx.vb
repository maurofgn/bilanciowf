Public Class Configurazioni
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblErrore.Text = ""
        If Not Page.IsPostBack Then
            Call Configurazioni_CollegaCampi()
        End If

    End Sub

    Private Sub Configurazioni_CollegaCampi()
        Dim myAppoUtente As tyUtente

        myAppoUtente = CType(Session("MeSIS_SMS_myTyUtente"), tyUtente)

        txtGgViewErrori.Text = myAppoUtente.iGG_View_Errori
        txtGgStoricoVerificaNum.Text = myAppoUtente.iGG_Storico_Verifica_Num

        Call ComboBox_CreaListItem(cboNotificaInvioPacchetti, enum_Notifica_Invio_Pacchetti.e_Notifica_Sempre, "Invia sempre la notifica di spedizione", myAppoUtente.iNotifica_Invio_Pacchetti)
        Call ComboBox_CreaListItem(cboNotificaInvioPacchetti, enum_Notifica_Invio_Pacchetti.e_Notifica_SoloErrori, "Invia la notifica solo se il pacchetto contiene errori", myAppoUtente.iNotifica_Invio_Pacchetti)
        Call ComboBox_CreaListItem(cboNotificaInvioPacchetti, enum_Notifica_Invio_Pacchetti.e_Notifica_CreditiInsufficienti, "Invia la notifica solo in caso di 'Crediti insufficienti'", myAppoUtente.iNotifica_Invio_Pacchetti)
    End Sub

    Protected Sub cmdSalva_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSalva.Click
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsCmdEx As SqlClient.SqlCommand
        Dim sSql As String, sErr As String = ""
        Dim myAppoTyUtente As tyUtente
        Dim iMod As Integer

        If Not IsNumeric(txtGgViewErrori.Text) Then
            sErr = sErr & "Compilare correttamente il campo '" & lblGgViewErrori.Text & "' <br /> "

        ElseIf CInt(Val(txtGgViewErrori.Text)) <> txtGgViewErrori.Text Then
            sErr = sErr & "Compilare correttamente il campo '" & lblGgViewErrori.Text & "' <br /> "

        ElseIf CInt(txtGgViewErrori.Text) <= 0 Then
            sErr = sErr & "Compilare correttamente il campo '" & lblGgViewErrori.Text & "' <br /> "

        End If

        If Not IsNumeric(txtGgStoricoVerificaNum.Text) Then
            sErr = sErr & "Compilare correttamente il campo '" & lblGgStoricoVerificaNum.Text & "' <br /> "

        ElseIf CInt(Val(txtGgStoricoVerificaNum.Text)) <> txtGgStoricoVerificaNum.Text Then
            sErr = sErr & "Compilare correttamente il campo '" & lblGgStoricoVerificaNum.Text & "' <br /> "

        ElseIf CInt(txtGgStoricoVerificaNum.Text) < 7 Then
            txtGgStoricoVerificaNum.Text = 7
            sErr = sErr & "Compilare correttamente il campo '" & lblGgStoricoVerificaNum.Text & "'. Il campo non può contenere un valore inveriore a 7. <br /> "
        End If

        If sErr <> "" Then
            lblErrore.Text = sErr
        Else
            txtGgViewErrori.Text = CInt(Val(txtGgViewErrori.Text))
            If ServerSMS_ApriConnessione(myCn) Then
                myAppoTyUtente = CType(Session("MeSIS_SMS_myTyUtente"), tyUtente)

                sSql = " UPDATE [Utenti]" & _
                       " SET GG_View_Errori = " & CInt(txtGgViewErrori.Text) & _
                       " , GG_Storico_Verifica_Num = " & CInt(txtGgStoricoVerificaNum.Text) & _
                       " , Notifica_Invio_Pacchetti = " & CInt(cboNotificaInvioPacchetti.SelectedIndex) & _
                       " WHERE " & mSQL.fnWhere("Utente", enum_Confronto.CLAUSE_EQUALS, myAppoTyUtente.sUtente)
                rsCmdEx = New SqlClient.SqlCommand
                rsCmdEx = myCn.CreateCommand()
                rsCmdEx.CommandText = sSql
                iMod = rsCmdEx.ExecuteNonQuery()
                rsCmdEx.Dispose()

                If iMod = 1 Then
                    sErr = ""
                    Call FaiAccesso(myAppoTyUtente.sUtente, "", sErr, myAppoTyUtente, False, False)
                    If sErr = "" Then
                        Session("MeSIS_SMS_myTyUtente") = myAppoTyUtente
                        Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_View_Errori)
                    Else
                        sErr = "Errore, impossibile proseguire ! "
                        Session("MeSIS_SMS_myTyUtente") = tyUtente_Azzera()
                        Session.Abandon()
                        FormsAuthentication.SignOut()
                    End If
                Else
                    sErr = "Errore, impossibile proseguire ! "
                    Session("MeSIS_SMS_myTyUtente") = tyUtente_Azzera()
                    Session.Abandon()
                    FormsAuthentication.SignOut()
                End If
            Else
                lblErrore.Text = "Errore di connessione, impossibile proseguire ! "
            End If
            Call ServerSMS_ChiudiConnessione(myCn)

            If sErr <> "" Then
                lblErrore.Text = sErr
            Else
                Response.Redirect("~/SMS_Private/CercaSpedizioni.aspx")
            End If
        End If
    End Sub
End Class