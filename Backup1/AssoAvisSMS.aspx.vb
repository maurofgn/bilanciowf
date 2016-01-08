Public Class AssoAvisSMS
    Inherits System.Web.UI.Page

    Const CONST_LISTA_OPERAZIONI = "|SMSMESIS|SMSCREDITI|"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim myTyUtente As tyUtente = tyUtente_Azzera()
        Dim sChiamata As String, arrChiamata() As String, sErr As String = "", sRiepilogo As String = ""
        Dim iCreditiResidui As Integer

        pnlOutput.Controls.Clear()
        If Not Page.IsPostBack Then
            'Operazione	ddMMyyyy	hh	utente	psw	parametri

            sChiamata = Request.QueryString("ID")
            sChiamata = Encrypt_Hex("D", sChiamata, 0)

            arrChiamata = Split(sChiamata, Chr(9))

            If UBound(arrChiamata) < 4 Then
                sErr = "Chiamata non valida. Contattare la MeSIS !"

            ElseIf Math.Abs(DateDiff("n", (Mid(arrChiamata(1), 1, 2) & "/" & _
                                            Mid(arrChiamata(1), 3, 2) & "/" & _
                                            Mid(arrChiamata(1), 5, 4) & " " & _
                                            arrChiamata(2) & ":00"), _
                                            Now())) > 60 Then
                sErr = "Ora incongruente con il Server !"

            ElseIf InStr(CONST_LISTA_OPERAZIONI, "|" & UCase(arrChiamata(0)) & "|") > 0 Then

                Call FaiAccesso(arrChiamata(3), arrChiamata(4), sErr, myTyUtente)

                If sErr = "" Then
                    FormsAuthentication.SetAuthCookie(arrChiamata(3), False)
                    Session("MeSIS_SMS_myTyUtente") = myTyUtente
                    Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_View_Errori)

                    Select Case UCase(arrChiamata(0))
                        Case "SMSMESIS" '*** View dettaglio spedizione ***
                            If UBound(arrChiamata) <> 5 Then
                                sErr = "Numero parametri errato. Contattare la MeSIS !"
                            Else 'parametri => IDAssoAvisSMS
                                Response.Redirect("~/SMS_Private/DettaglioSpedizione.aspx?AssoAvis_SMS=" & arrChiamata(5))
                            End If

                        Case "SMSCREDITI" '*** Output situazione crediti ***
                            If UBound(arrChiamata) <> 4 Then
                                sErr = "Numero parametri errato. Contattare la MeSIS !"
                            Else 'parametri =>
                                If GetCreditiResidui(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                                                     iCreditiResidui, sRiepilogo) Then

                                    pnlOutput.Controls.Add(New LiteralControl("|<" & iCreditiResidui & ">|" & _
                                                                              "|<" & sRiepilogo & ">|"))
                                End If

                            End If

                            'Case "SETVERIFICACEL" '*** Avvio verifica numero cellulare ***
                            '    If UBound(arrChiamata) <> 5 Then
                            '        sErr = "Numero parametri errato. Contattare la MeSIS !"
                            '    Else 'parametri => cellulare
                            '        lAppo = InsVerificaNumeri(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                            '                                  CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_Storico_Verifica_Num, _
                            '                                  arrChiamata(5))
                            '        If lAppo > 0 Then
                            '            pnlOutput.Controls.Add(New LiteralControl("|<" & lAppo & ">|"))
                            '        Else
                            '            pnlOutput.Controls.Add(New LiteralControl("|<ERR>|"))
                            '        End If
                            '    End If
                            'Case "GETVERIFICACEL" '*** Output stato verifica numero cellulare ***
                            '    If UBound(arrChiamata) <> 6 Then
                            '        sErr = "Numero parametri errato. Contattare la MeSIS !"
                            '    Else 'parametri => cellulare    IDVerifica
                            '        Call GetEsitoVerificaNumeri(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                            '                                    Val(arrChiamata(6)), _
                            '                                    arrChiamata(5), _
                            '                                    sRiepilogo)
                            '        If sRiepilogo <> "" Then
                            '            pnlOutput.Controls.Add(New LiteralControl(sRiepilogo))
                            '        Else
                            '            pnlOutput.Controls.Add(New LiteralControl("|<ERR>|"))
                            '        End If
                            '    End If
                    End Select
                End If
            Else
                sErr = "Comando non riconosciuto ! "
            End If

            If sErr <> "" Then
                lblErrore.Text = "<br><br><br>" & sErr & "<br><br><br>"
                lblErrore.Font.Size = FontUnit.Point(18)
                lblErrore.Font.Bold = True
                lblErrore.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

End Class