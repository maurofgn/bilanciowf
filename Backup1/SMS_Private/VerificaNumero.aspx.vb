Public Class VerificaNumero
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cmdCercaVerifiche_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCercaVerifiche.Click
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsCmdEx As SqlClient.SqlCommand
        Dim arrAppo() As String, arrAppoOK() As String, sSql As String
        Dim i As Integer, j As Integer, iAppoPos As Integer
        Dim bErrore As Boolean
        Dim lIdSpedizione As Long

        lblErrore.Text = ""

        If Trim(txtListaNumeri.Text) = "" Then
            lblErrore.Text = "Impossibile proseguire, non e' stato specificato nessun numero !"
            Exit Sub
        End If

        arrAppo = Split(Trim(txtListaNumeri.Text), ",")
        j = -1
        For i = 0 To UBound(arrAppo)
            If Trim(arrAppo(i)) <> "" Then
                iAppoPos = Find_In_Array_String(Trim(arrAppo(i)), arrAppoOK)
                If iAppoPos = -1 Then
                    j = j + 1
                    ReDim Preserve arrAppoOK(j)
                    arrAppoOK(j) = Trim(arrAppo(i))
                End If
            End If
        Next
        If j = -1 Then
            lblErrore.Text = "Impossibile proseguire, compilare la casella in modo corretto !"
            Exit Sub
        End If

        If ServerSMS_ApriConnessione(myCn) Then
            lIdSpedizione = au_Spedizione_Verifiche_Crea(0, myCn, _
                                                         "-" & CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                                                         "", _
                                                         #1/1/1900#, _
                                                         CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_Storico_Verifica_Num)
            If lIdSpedizione > 0 Then
                bErrore = False
                For i = 0 To UBound(arrAppoOK)
                    If au_ListaVerificheNumeri_Crea(0, myCn, lIdSpedizione, arrAppoOK(i)) = 0 Then
                        bErrore = True
                    End If
                Next
                If bErrore Then
                    lblErrore.Text = "<br /><br />ATTENZIONE !!! <br />" & _
                                     "Problemi durante l'inserimento dei numeri. <br />" & _
                                     "Contattare la MeSIS !<br /><br />"
                Else
                    sSql = " UPDATE Spedizione_Verifiche" & _
                           " SET Codice_Avis = '" & CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis & "'" & _
                           ", Data_Chiusura = Null" & _
                           " WHERE Codice_Avis = '-" & CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis & "'" & _
                           "   AND ID_Spedizione = " & lIdSpedizione & _
                           "   AND Data_Chiusura Is Not Null"
                    rsCmdEx = New SqlClient.SqlCommand
                    rsCmdEx = myCn.CreateCommand()
                    rsCmdEx.CommandText = sSql
                    rsCmdEx.ExecuteNonQuery()
                    rsCmdEx.Dispose()

                    Call ServerSMS_ChiudiConnessione(myCn)
                    Response.Redirect("CercaSpedizioni.aspx")

                End If
            Else
                lblErrore.Text = "<br /><br />ATTENZIONE !!! <br />" & _
                                 "Problemi durante l'inserimento dei dati. <br />" & _
                                 "Contattare la MeSIS !<br /><br />"
            End If
        End If
        Call ServerSMS_ChiudiConnessione(myCn)
    End Sub
End Class