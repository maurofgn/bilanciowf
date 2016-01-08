Public Class CercaSpedizioni
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim myTyAppoCerca As tyCercaSpedizioni
            'myTyAppoCerca = CType(Session("MeSIS_SMS_myTyCercaSpedizioni"), tyCercaSpedizioni)
            'optSMS.Checked = myTyAppoCerca.bCercaSMS
            'optVer.Checked = Not (myTyAppoCerca.bCercaSMS)
            Call ImpostaPnlRicerca()
        End If
    End Sub

    Protected Sub cmdCerca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCerca.Click
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Lista As SqlClient.SqlDataAdapter = Nothing, rsDS_Lista As DataSet = Nothing, row_Lista As DataRow
        Dim sSql As String, sSqlTot As String = "", sLink As String = ""
        Dim myRiga As TableRow, lkSpedizione As HyperLink, myHeadRiga As TableHeaderRow, myHeadCol As TableHeaderCell
        Dim myTyAppoCerca As tyCercaSpedizioni

        With myTyAppoCerca
            .bCercaSMS = True
            .bEvasa = chkEvase.Checked
            .bErrori = chkErrori.Checked
            If Not myIsDate(txtDallaData.Text) Then txtDallaData.Text = ""
            .sDallaData = txtDallaData.Text
            If Not myIsDate(txtAllaData.Text) Then txtAllaData.Text = ""
            .sAllaData = txtAllaData.Text
        End With

        tblSpedizioni.Rows.Clear()

        myHeadRiga = New System.Web.UI.WebControls.TableHeaderRow
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Accettazione" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Totale SMS" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "SMS Inviati" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "SMS con Errori" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Crediti Insufficienti" : myHeadRiga.Cells.Add(myHeadCol)
        tblSpedizioni.Rows.Add(myHeadRiga)

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT ID_Spedizione, Data_Ricezione, Totale_SMS " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_SMS " & _
                   "   WHERE Lista_SMS.Data_Spedizione Is Not Null " & _
                   "     AND Lista_SMS.Esito Is Not Null" & _
                   "     AND Lista_SMS.Esito = 'InvioCorretto' " & _
                   "     AND Lista_SMS.ID_Spedizione = Spedizione.ID_Spedizione) As NumSpeditiOK " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_SMS " & _
                   "   WHERE Lista_SMS.Data_Spedizione Is Not Null " & _
                   "     AND Lista_SMS.Esito Is Not Null" & _
                   "     AND Lista_SMS.Esito <> 'InvioCorretto' " & _
                   "     AND Lista_SMS.Esito <> 'NoCreditiMeSIS' " & _
                   "     AND Lista_SMS.ID_Spedizione = Spedizione.ID_Spedizione) As NumErrori " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_SMS " & _
                   "   WHERE Lista_SMS.Data_Spedizione Is Not Null " & _
                   "     AND Lista_SMS.Esito Is Not Null" & _
                   "     AND Lista_SMS.Esito = 'NoCreditiMeSIS' " & _
                   "     AND Lista_SMS.ID_Spedizione = Spedizione.ID_Spedizione) As NumErrCrediti " & _
                   " FROM Spedizione" & _
                   " WHERE " & mSQL.fnWhere("Spedizione.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis)

            sSqlTot = sSqlTot & _
                      " UNION " & _
                      sSql & _
                      SQL_AND & mSQL.fnWhere("Spedizione.Evaso", enum_Confronto.CLAUSE_LESSTHAN, 2)

            If myIsDate(myTyAppoCerca.sDallaData) Then
                sSql = sSql & SQL_AND & mSQL.fnWhere("Spedizione.Data_Ricezione", enum_Confronto.CLAUSE_GREATERTHANOREQUAL, CDate(myTyAppoCerca.sDallaData), False)
            End If
            If myIsDate(myTyAppoCerca.sAllaData) Then
                sSql = sSql & SQL_AND & mSQL.fnWhere("Spedizione.Data_Ricezione", enum_Confronto.CLAUSE_LESSTHAN, DateAdd(DateInterval.Day, 1, CDate(myTyAppoCerca.sAllaData)), False)
            End If

            If myTyAppoCerca.bEvasa = True Then
                sSqlTot = sSqlTot & _
                          " UNION " & _
                          sSql & _
                          SQL_AND & mSQL.fnWhere("Spedizione.Evaso", enum_Confronto.CLAUSE_EQUALS, 2)
            End If

            If myTyAppoCerca.bErrori = True Then
                sSqlTot = sSqlTot & _
                          " UNION " & _
                          sSql & _
                          SQL_AND & " Spedizione.ID_Spedizione IN " & _
                                       "     (SELECT DISTINCT Lista_SMS.ID_Spedizione" & _
                                       "      FROM Lista_SMS INNER JOIN Spedizione ON Lista_SMS.ID_Spedizione = Spedizione.ID_Spedizione" & _
                                       "      WHERE " & mSQL.fnWhere("Lista_SMS.Esito", enum_Confronto.CLAUSE_DOESNOTEQUAL, "InvioCorretto") & _
                                               SQL_AND & mSQL.fnWhere("Spedizione.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis) & _
                                       "     ) "
            End If

            sSqlTot = Mid(sSqlTot, 7) & _
                      " ORDER BY Spedizione.Data_Ricezione DESC"

            If Apri_Rs_DataSet(myCn, rsDa_Lista, rsDS_Lista, sSqlTot) = "" Then
                For Each row_Lista In rsDS_Lista.Tables(0).Rows
                    sLink = "DettaglioSpedizione.aspx?Err=|<ERR>|&Sped=" & row_Lista("ID_Spedizione")

                    myRiga = New System.Web.UI.WebControls.TableRow

                    lkSpedizione = New HyperLink
                    lkSpedizione.Text = FormattaData_2Str(row_Lista("Data_Ricezione"), "")
                    lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "")
                    Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Center)

                    Call Table_AggiungiColonna(myRiga, row_Lista("Totale_SMS"), HorizontalAlign.Right)
                    Call Table_AggiungiColonna(myRiga, IIf((row_Lista("NumSpeditiOK") = 0), "", row_Lista("NumSpeditiOK")), HorizontalAlign.Right)

                    lkSpedizione = New HyperLink
                    If row_Lista("NumErrori") <> 0 Then
                        lkSpedizione.Text = row_Lista("NumErrori")
                        lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "1")
                    Else
                        lkSpedizione.Text = ""
                        lkSpedizione.NavigateUrl = ""
                    End If
                    Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)

                    lkSpedizione = New HyperLink
                    If row_Lista("NumErrCrediti") <> 0 Then
                        lkSpedizione.Text = row_Lista("NumErrCrediti")
                        lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "2")
                    Else
                        lkSpedizione.Text = ""
                        lkSpedizione.NavigateUrl = ""
                    End If
                    Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)

                    tblSpedizioni.Rows.Add(myRiga)
                Next
            End If
            Call Chiudi_Rs_DataSet(rsDS_Lista, rsDa_Lista)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)

        Session("MeSIS_SMS_myTyCercaSpedizioni") = myTyAppoCerca
    End Sub

    Protected Sub cmdCercaVerifiche_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCercaVerifiche.Click
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Lista As SqlClient.SqlDataAdapter = Nothing, rsDS_Lista As DataSet = Nothing, row_Lista As DataRow
        Dim sSql As String, sSqlTot As String = "", sLink As String = "", sAppo As String
        Dim myRiga As TableRow, lkSpedizione As HyperLink, myHeadRiga As TableHeaderRow, myHeadCol As TableHeaderCell
        Dim myTyAppoCerca As tyCercaSpedizioni

        With myTyAppoCerca
            .bCercaSMS = False
            '.bEvasa = chkEvase.Checked
            '.bNonEvasa = chkNonEvase.Checked
            '.bErrori = chkErrori.Checked
            'If Val(txtGiorniErrori.Text) = 0 Then txtGiorniErrori.Text = 5
            '.iGgErrori = Val(txtGiorniErrori.Text)
            If Not myIsDate(txtVerDallaData.Text) Then txtVerDallaData.Text = ""
            .sDallaData = txtVerDallaData.Text
            If Not myIsDate(txtVerAllaData.Text) Then txtVerAllaData.Text = ""
            .sAllaData = txtVerAllaData.Text
        End With

        tblSpedizioni.Rows.Clear()

        myHeadRiga = New System.Web.UI.WebControls.TableHeaderRow
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Data Ricezione / Evaso" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Da Verificare" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Saltati" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Confermati" : myHeadRiga.Cells.Add(myHeadCol)
        myHeadCol = New TableHeaderCell
        myHeadCol.CssClass = "thIntestazione" : myHeadCol.Text = "Non Confermati" : myHeadRiga.Cells.Add(myHeadCol)
        tblSpedizioni.Rows.Add(myHeadRiga)

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT ID_Spedizione, Data_Ricezione, Data_Chiusura " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_Verifiche_Numeri " & _
                   "   WHERE Lista_Verifiche_Numeri.Stato_Invio <= " & CInt(enum_Verifiche_StatoInivio.VER_Inviato) & _
                   "    AND Lista_Verifiche_Numeri.ID_Spedizione = Spedizione_Verifiche.ID_Spedizione) As DaVerificare " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_Verifiche_Numeri " & _
                   "   WHERE Lista_Verifiche_Numeri.Stato_Invio IN (" & CInt(enum_Verifiche_StatoInivio.VER_Errore) & ") " & _
                   "    AND Lista_Verifiche_Numeri.ID_Spedizione = Spedizione_Verifiche.ID_Spedizione) As NumSaltati " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_Verifiche_Numeri " & _
                   "     Inner Join Verifiche_Numeri_Esito On Verifiche_Numeri_Esito.ID_Provider = Lista_Verifiche_Numeri.ID_Provider" & _
                   "     Inner Join (SELECT * " & _
                   "                 FROM PopUp " & _
                   "                 WHERE TipoPopUp = 'EsitoVerificaNumeri') As Lista_Esiti " & _
                   "         On Lista_Esiti.Provider_SMS = Lista_Verifiche_Numeri.Provider_SMS " & _
                   "        And Lista_Esiti.Valore = Verifiche_Numeri_Esito.Stato " & _
                   "   WHERE Lista_Verifiche_Numeri.Stato_Invio IN (" & CInt(enum_Verifiche_StatoInivio.VER_RicevutoReport) & ") " & _
                   "    AND Lista_Esiti.Is_Errore = 0 " & _
                   "    AND Lista_Verifiche_Numeri.ID_Spedizione = Spedizione_Verifiche.ID_Spedizione) As NumConfermati " & _
                   ", (SELECT Count(*) " & _
                   "   FROM Lista_Verifiche_Numeri " & _
                   "     Inner Join Verifiche_Numeri_Esito On Verifiche_Numeri_Esito.ID_Provider = Lista_Verifiche_Numeri.ID_Provider" & _
                   "     Inner Join (SELECT * " & _
                   "                 FROM PopUp " & _
                   "                 WHERE TipoPopUp = 'EsitoVerificaNumeri') As Lista_Esiti " & _
                   "         On Lista_Esiti.Provider_SMS = Lista_Verifiche_Numeri.Provider_SMS " & _
                   "        And Lista_Esiti.Valore = Verifiche_Numeri_Esito.Stato " & _
                   "   WHERE Lista_Verifiche_Numeri.Stato_Invio IN (" & CInt(enum_Verifiche_StatoInivio.VER_RicevutoReport) & ") " & _
                   "    AND Lista_Esiti.Is_Errore <> 0 " & _
                   "    AND Lista_Verifiche_Numeri.ID_Spedizione = Spedizione_Verifiche.ID_Spedizione) As NumNonConfermati " & _
                   " FROM Spedizione_Verifiche" & _
                   " WHERE " & mSQL.fnWhere("Spedizione_Verifiche.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis)

            If myIsDate(myTyAppoCerca.sDallaData) Then
                sSql = sSql & SQL_AND & mSQL.fnWhere("Spedizione_Verifiche.Data_Ricezione", enum_Confronto.CLAUSE_GREATERTHANOREQUAL, CDate(myTyAppoCerca.sDallaData), False)
            End If
            If myIsDate(myTyAppoCerca.sAllaData) Then
                sSql = sSql & SQL_AND & mSQL.fnWhere("Spedizione_Verifiche.Data_Ricezione", enum_Confronto.CLAUSE_LESSTHAN, DateAdd(DateInterval.Day, 1, CDate(myTyAppoCerca.sAllaData)), False)
            End If

            sSqlTot = sSql & _
                      " ORDER BY Spedizione_Verifiche.Data_Ricezione DESC"

            If Apri_Rs_DataSet(myCn, rsDa_Lista, rsDS_Lista, sSqlTot) = "" Then
                For Each row_Lista In rsDS_Lista.Tables(0).Rows
                    sLink = "DettaglioVerifiche.aspx?Err=|<ERR>|&Sped=" & row_Lista("ID_Spedizione")

                    myRiga = New System.Web.UI.WebControls.TableRow

                    sAppo = FormattaData_2Str(row_Lista("Data_Ricezione"), "")
                    If ("" & row_Lista("Data_Chiusura")) <> "" Then
                        sAppo = sAppo & " - " & FormattaData_2Str(row_Lista("Data_Chiusura"), "")
                    End If

                    If ("" & row_Lista("Data_Chiusura")) = "" Then 'E' una richiesta ancora aperta 
                        Call Table_AggiungiColonna(myRiga, sAppo, HorizontalAlign.Left)
                        Call Table_AggiungiColonna(myRiga, row_Lista("DaVerificare"), HorizontalAlign.Right)
                        If row_Lista("NumSaltati") <> 0 Then
                            Call Table_AggiungiColonna(myRiga, row_Lista("NumSaltati"), HorizontalAlign.Right)
                        Else
                            Call Table_AggiungiColonna(myRiga, "", HorizontalAlign.Right)
                        End If
                        Call Table_AggiungiColonna(myRiga, "", HorizontalAlign.Right)
                        Call Table_AggiungiColonna(myRiga, "", HorizontalAlign.Right)
                    Else
                        lkSpedizione = New HyperLink
                        lkSpedizione.Text = sAppo
                        lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "")
                        Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Left)

                        lkSpedizione = New HyperLink
                        If row_Lista("DaVerificare") <> 0 Then
                            lkSpedizione.Text = row_Lista("DaVerificare")
                            lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "1")
                        Else
                            lkSpedizione.Text = ""
                            lkSpedizione.NavigateUrl = ""
                        End If
                        Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)

                        lkSpedizione = New HyperLink
                        If row_Lista("NumSaltati") <> 0 Then
                            lkSpedizione.Text = row_Lista("NumSaltati")
                            lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "2")
                        Else
                            lkSpedizione.Text = ""
                            lkSpedizione.NavigateUrl = ""
                        End If
                        Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)

                        lkSpedizione = New HyperLink
                        If row_Lista("NumConfermati") <> 0 Then
                            lkSpedizione.Text = row_Lista("NumConfermati")
                            lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "4")
                        Else
                            lkSpedizione.Text = ""
                            lkSpedizione.NavigateUrl = ""
                        End If
                        Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)

                        lkSpedizione = New HyperLink
                        If row_Lista("NumNonConfermati") <> 0 Then
                            lkSpedizione.Text = row_Lista("NumNonConfermati")
                            lkSpedizione.NavigateUrl = Replace(sLink, "|<ERR>|", "8")
                        Else
                            lkSpedizione.Text = ""
                            lkSpedizione.NavigateUrl = ""
                        End If
                        Call Table_AggiungiColonna(myRiga, lkSpedizione, HorizontalAlign.Right)
                    End If
                    tblSpedizioni.Rows.Add(myRiga)
                Next
            End If
            Call Chiudi_Rs_DataSet(rsDS_Lista, rsDa_Lista)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)

        Session("MeSIS_SMS_myTyCercaSpedizioni") = myTyAppoCerca
    End Sub

    Private Sub ImpostaPnlRicerca()
        Dim myTyAppoCerca As tyCercaSpedizioni

        myTyAppoCerca = CType(Session("MeSIS_SMS_myTyCercaSpedizioni"), tyCercaSpedizioni)

        pnlRicercaSMS.Visible = myTyAppoCerca.bCercaSMS
        pnlRicercaVer.Visible = Not (myTyAppoCerca.bCercaSMS)

        cmdSms.Enabled = Not (myTyAppoCerca.bCercaSMS)
        cmdVer.Enabled = (myTyAppoCerca.bCercaSMS)

        If myTyAppoCerca.bCercaSMS Then
            lblTitolo.Text = "Gestione SMS"

            chkEvase.Checked = myTyAppoCerca.bEvasa
            chkErrori.Checked = myTyAppoCerca.bErrori
            txtDallaData.Text = myTyAppoCerca.sDallaData
            txtAllaData.Text = myTyAppoCerca.sAllaData

            Call cmdCerca_Click(cmdCerca, New EventArgs())
        Else
            lblTitolo.Text = "Verifica Numeri"

            txtVerDallaData.Text = myTyAppoCerca.sDallaData
            txtVerAllaData.Text = myTyAppoCerca.sAllaData

            Call cmdCercaVerifiche_Click(cmdCercaVerifiche, New EventArgs())
        End If
    End Sub

    Protected Sub cmdSms_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSms.Click
        Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_View_Errori, _
                                                                            True)
        Call ImpostaPnlRicerca()
    End Sub

    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdVer.Click
        Session("MeSIS_SMS_myTyCercaSpedizioni") = tyCercaSpedizioni_Azzera(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).iGG_View_Errori, _
                                                                            False)
        Call ImpostaPnlRicerca()
    End Sub

End Class