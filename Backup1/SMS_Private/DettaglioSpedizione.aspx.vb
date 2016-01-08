Public Class DettaglioSpedizione
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lIDSpedizione As Long
        Dim iErrori As Integer
        Dim sIdAssoAvisSMS As String

        lblErrore.Text = ""
        If Not Page.IsPostBack Then
            lIDSpedizione = Val(Request.QueryString("Sped"))
            iErrori = Val(Request.QueryString("Err"))
            sIdAssoAvisSMS = "" & Request.QueryString("AssoAvis_SMS")

            Call CercaSpedizione(lIDSpedizione, iErrori, sIdAssoAvisSMS)
        End If
    End Sub

    Private Sub CercaSpedizione(ByVal lIDSpedizione As Long, _
                                ByVal iErrori As Integer, _
                                ByVal sIdAssoAvisSMS As String)

        Dim sSql As String, sFont As String, sEsito As String
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Dett As SqlClient.SqlDataAdapter = Nothing, rsDs_Dett As DataSet = Nothing
        Dim myRiga As TableRow, chkRiattiva As CheckBox, row_Lista As DataRow
        Dim bRiattiva As Boolean


        hhIDAssoAvis.Value = sIdAssoAvisSMS

        bRiattiva = IIf((iErrori = 2), True, False)
        CType(tblSpedizioni.FindControl("trintestazione").FindControl("tdRiattiva"), TableHeaderCell).Visible = bRiattiva
        plhRiattivazione.Visible = bRiattiva

        'If sIdAssoAvisSMS <> "" Then
        '    pnlIndietro.Visible = False
        'Else
        '    pnlIndietro.Visible = True
        'End If

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT Spedizione.ID_Spedizione, Spedizione.Data_Ricezione, Spedizione.DataModifica_File " & _
                   ", Spedizione.Totale_SMS, Spedizione.Totale_Spediti, Spedizione.SMS_NoSpediti" & _
                   ", Lista_SMS.ID, Lista_SMS.Id_Assoavis, Lista_SMS.Data_spedizione, isnull(Lista_SMS.Esito,'') as Esito, Lista_SMS.Testo" & _
                   ", Lista_SMS.Con_Ricevuta, Lista_SMS.Mittente " & _
                   ", DaSpedire = Case IsNull(Lista_SMS.Data_DaSped, '') When '' Then 'Immed.' Else dbo.fDataIta(Lista_SMS.Data_DaSped) End " & _
                   ", Errori_Provider.Descrizione_WEB AS Descr_Esito, Errori_Provider.Font_WEB " & _
                   ", ListaReport.ID_SMS_Provider, ListaReport.Status, ListaReport.TimeStamp_Report, ListaReport.Descriz_Report, ListaReport.Is_Errore " & _
                   ", Provider.Descrizione As DescrProv, Lista_SMS.ID_SMS_Provider AS IDProvInvio " & _
                   " FROM Spedizione " & _
                   "   INNER JOIN Lista_SMS ON Spedizione.ID_Spedizione = Lista_SMS.ID_Spedizione " & _
                   "   LEFT JOIN Provider ON Provider.ID_Provider = Lista_SMS.Provider_SMS " & _
                   "   LEFT JOIN Errori_Provider ON Errori_Provider.Codice_Errore_provider = Lista_SMS.Esito" & _
                   "   LEFT JOIN (SELECT Report_Invii.*, ListaEsiti.Is_Errore " & _
                   "              , Descriz_Report = CASE IsNull(ListaEsiti.Descrizione_Web, '') WHEN '' THEN ListaEsiti.Descrizione_Provider ELSE ListaEsiti.Descrizione_Web END" & _
                   "              FROM (SELECT AA.ID_SMS_Provider, MAX(AA.ID) AS UltimoReport, SUBSTRING(AA.ID_SMS_Provider, 1, 1) AS AppoIDProvider " & _
                   "                    FROM Report_Invii as AA" & _
                   "                    GROUP BY AA.ID_SMS_Provider, SUBSTRING(AA.ID_SMS_Provider, 1, 1)) ListaMaxReport" & _
                   "                Inner Join Report_Invii On Report_Invii.ID_SMS_Provider = ListaMaxReport.ID_SMS_Provider " & _
                   "                                       AND Report_Invii.ID = ListaMaxReport.UltimoReport" & _
                   "                Inner Join (SELECT *" & _
                   "                            FROM PopUp " & _
                   "                            WHERE TipoPopUp = 'EsitoInvioSMS') ListaEsiti" & _
                   "             		On ListaEsiti.Provider_SMS = ListaMaxReport.AppoIDProvider" & _
                   "             	   AND ListaEsiti.Valore = Report_Invii.Status " & _
                   "             ) ListaReport ON ListaReport.ID_SMS_Provider = Lista_SMS.ID_SMS_Provider " & _
                   "                          AND ListaReport.Telefono = Lista_SMS.Telefono " & _
                   "  WHERE " & mSQL.fnWhere("Spedizione.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis)

            If sIdAssoAvisSMS <> "" Then
                sSql = sSql & _
                       SQL_AND & " Lista_SMS.ID_Spedizione IN " & _
                                 "                (SELECT TOP 1 Lista_SMS.ID_Spedizione " & _
                                 "                 FROM Lista_SMS" & _
                                 "                 WHERE " & mSQL.fnWhere("ID_Assoavis", enum_Confronto.CLAUSE_EQUALS, CStr(sIdAssoAvisSMS)) & _
                                 "                 ORDER BY ID Desc" & _
                                 "                ) "
            Else
                sSql = sSql & _
                       SQL_AND & mSQL.fnWhere("Spedizione.ID_Spedizione", enum_Confronto.CLAUSE_EQUALS, lIDSpedizione)
            End If

            If iErrori = 1 Then
                sSql = sSql & _
                        SQL_AND & mSQL.fnWhere("Lista_SMS.Esito", enum_Confronto.CLAUSE_DOESNOTEQUAL, "InvioCorretto") & _
                        SQL_AND & mSQL.fnWhere("Lista_SMS.Esito", enum_Confronto.CLAUSE_DOESNOTEQUAL, "NoCreditiMeSIS")
            End If

            If iErrori = 2 Then
                sSql = sSql & _
                        SQL_AND & mSQL.fnWhere("Lista_SMS.Esito", enum_Confronto.CLAUSE_EQUALS, "NoCreditiMeSIS")
            End If

            sSql = sSql & _
                   " ORDER BY Lista_SMS.Id_Assoavis "

            If Apri_Rs_DataSet(myCn, rsDa_Dett, rsDs_Dett, sSql) = "" Then
                If rsDs_Dett.Tables(0).Rows.Count > 0 Then
                    hhIDSpedizione.Value = rsDs_Dett.Tables(0).Rows(0)("ID_Spedizione")

                    lblCreazione.Text = FormattaData_2Str("" & rsDs_Dett.Tables(0).Rows(0)("DataModifica_File"), "", FORMATO_DATA1)
                    lblAccettazione.Text = FormattaData_2Str(rsDs_Dett.Tables(0).Rows(0)("Data_Ricezione"), "", FORMATO_DATA1)
                    lblTotErrori.Text = rsDs_Dett.Tables(0).Rows(0)("SMS_NoSpediti")
                    lblTotOK.Text = (rsDs_Dett.Tables(0).Rows(0)("Totale_Spediti") - rsDs_Dett.Tables(0).Rows(0)("SMS_NoSpediti"))
                    lblTotSMS.Text = rsDs_Dett.Tables(0).Rows(0)("Totale_SMS")
                    For Each row_Lista In rsDs_Dett.Tables(0).Rows
                        If "" & row_Lista("Font_WEB") <> "" Then
                            sFont = "" & row_Lista("Font_WEB")
                        ElseIf row_Lista("Esito") = "InvioCorretto" Or row_Lista("Esito") = "" Then
                            sFont = "tdStandard"
                        ElseIf row_Lista("Esito") = "NoCreditiMeSIS" Then
                            sFont = "tdErroreBanale"
                        Else
                            sFont = "tdErrore"
                        End If

                        If row_Lista("Esito") = "InvioCorretto" Then
                            If ("" & row_Lista("Descr_Esito")) = "" Then
                                sEsito = "" & row_Lista("Esito")
                            Else
                                sEsito = "" & row_Lista("Descr_Esito")
                            End If

                            If "" & row_Lista("ID_SMS_Provider") <> "" Then
                                If row_Lista("Is_Errore") = 1 Then
                                    sFont = "tdErrore"
                                    sEsito = "" & row_Lista("Descriz_Report")
                                Else
                                    sEsito = Replace("" & row_Lista("Descriz_Report"), _
                                                     "|<TIMESTAMP>|", _
                                                     row_Lista("TimeStamp_Report"))
                                End If
                            End If
                        Else
                            If ("" & row_Lista("Descr_Esito")) = "" Then
                                sEsito = "" & row_Lista("Esito")
                            Else
                                sEsito = "" & row_Lista("Descr_Esito")
                            End If
                        End If

                        myRiga = New System.Web.UI.WebControls.TableRow

                        If row_Lista("Con_Ricevuta") = 1 Then
                            Call Table_AggiungiColonna(myRiga, "+Ric.", HorizontalAlign.Center, sFont, 1)
                        ElseIf "" & row_Lista("Mittente") <> "" Then
                            Call Table_AggiungiColonna(myRiga, "Mit.", HorizontalAlign.Center, sFont)
                        Else
                            Call Table_AggiungiColonna(myRiga, "&nbsp;", HorizontalAlign.Center, sFont)
                        End If
                        Call Table_AggiungiColonna(myRiga, row_Lista("Id_Assoavis"), HorizontalAlign.Center, sFont, , _
                                                   "" & row_Lista("DescrProv") & ":" & Mid("" & row_Lista("IDProvInvio"), 3))
                        If (row_Lista("Data_spedizione") & "" = "") Then 'Luigi 01/01/2014
                            Call Table_AggiungiColonna(myRiga, "", HorizontalAlign.Center, sFont)
                            If (sEsito = "") Then
                                sEsito = "=> Da spedire"
                            End If
                        Else
                            Call Table_AggiungiColonna(myRiga, FormattaData_2Str(row_Lista("Data_spedizione"), ""), HorizontalAlign.Center, sFont)
                        End If
                        Call Table_AggiungiColonna(myRiga, sEsito, HorizontalAlign.Left, sFont)
                        Call Table_AggiungiColonna(myRiga, row_Lista("Testo"), HorizontalAlign.Left, sFont)
                        Call Table_AggiungiColonna(myRiga, row_Lista("DaSpedire"), HorizontalAlign.Center, sFont)

                        If bRiattiva Then
                            chkRiattiva = New CheckBox
                            chkRiattiva.Text = ""
                            chkRiattiva.ID = "chkRiattiva_" & row_Lista("ID")
                            chkRiattiva.Checked = False
                            Call Table_AggiungiColonna(myRiga, chkRiattiva, HorizontalAlign.Center, sFont)
                        End If

                        tblSpedizioni.Rows.Add(myRiga)
                    Next
                End If
            End If
            Call Chiudi_Rs_DataSet(rsDs_Dett, rsDa_Dett)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)
    End Sub

    Private Sub lkRiattiva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkRiattiva.Click
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim arrListaID() As String, sLista As String = "", sListaAppo As String = ""
        Dim sLista1 As String = "", sLista2 As String = "", sLista3 As String = "", sLista4 As String = ""
        Dim lSpedizione As Long, lPos As Long, lContaSMS As Long = 0
        Dim i As Integer
        Dim bAzzera As Boolean, bErrore As Boolean

        lSpedizione = Val(hhIDSpedizione.Value)
        arrListaID = Split(Replace("" & hhRiattivaID.Value, "chkRiattiva_", ""), "|")
        bErrore = False
        bAzzera = False
        For i = LBound(arrListaID) To UBound(arrListaID)
            If Trim(arrListaID(i)) <> "" Then
                sLista = sLista & "," & Trim(arrListaID(i))
            End If

            If Len(sLista) = 7600 Then
                sLista = Mid(sLista, 2)
                bAzzera = True
            ElseIf Len(sLista) > 7600 Then
                lPos = InStrRev(sLista, ",")
                sListaAppo = Mid(sLista, lPos)
                sLista = Mid(sLista, 2, lPos - 2)
                bAzzera = True
            End If
            If bAzzera Then
                If sLista1 = "" Then
                    sLista1 = sLista
                ElseIf sLista2 = "" Then
                    sLista2 = sLista
                ElseIf sLista3 = "" Then
                    sLista3 = sLista
                ElseIf sLista4 = "" Then
                    sLista4 = sLista
                Else
                    bErrore = True
                    Exit For
                End If
                sLista = sListaAppo
                sListaAppo = ""
                bAzzera = False
            End If
        Next
        If Len(sLista) > 0 Then 'Sono uscita dal For con Len<7600 or Len>7600
            sLista = Mid(sLista, 2)
            If sLista1 = "" Then
                sLista1 = sLista
            ElseIf sLista2 = "" Then
                sLista2 = sLista
            ElseIf sLista3 = "" Then
                sLista3 = sLista
            ElseIf sLista4 = "" Then
                sLista4 = sLista
            Else
                bErrore = True
            End If
        End If

        If bErrore = True Then
            lblErrore.Text = "<br /><br />ATTENZIONE !!! <br />" & _
                             "Problemi nella riattivazione degli SMS. <br />" & _
                             "Contattare la MeSIS !<br /><br />"
            lblErrore.CssClass = "failureNotification"

        ElseIf sLista1 <> "" Then
            If ServerSMS_ApriConnessione(myCn) Then
                lContaSMS = au_RiattivaSMS(myCn, lSpedizione, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                                           CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sUtente, _
                                           sLista1, sLista2, sLista3, sLista4)
                If lContaSMS = 0 Then
                    lblErrore.Text = "<br /><br />ATTENZIONE !!! <br />" & _
                                     "Problemi durante la procedura di riattivazione degli SMS. <br />" & _
                                     "Contattare la MeSIS !<br /><br />"
                    lblErrore.CssClass = "failureNotification"
                Else
                    lblErrore.Text = "<br /><br />Sono stati riattivati " & lContaSMS & " SMS !<br /><br />"
                    lblErrore.CssClass = ""
                    lblErrore.Font.Bold = True
                End If
            End If
            Call ServerSMS_ChiudiConnessione(myCn)
        End If
        Call CercaSpedizione(lSpedizione, 2, hhIDAssoAvis.Value)
    End Sub
End Class