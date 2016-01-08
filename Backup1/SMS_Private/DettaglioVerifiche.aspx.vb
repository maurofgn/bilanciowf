Public Class DettaglioVerifiche
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lIDSpedizione As Long
        Dim iErrori As Integer

        If Not Page.IsPostBack Then
            lIDSpedizione = Val(Request.QueryString("Sped"))

            iErrori = Val(Request.QueryString("Err"))
            If iErrori = 0 Then iErrori = 15

            If (iErrori And 2) = 2 Then chkSaltati.Checked = True
            If (iErrori And 4) = 4 Then chkConfermati.Checked = True
            If (iErrori And 8) = 8 Then chkNonConfermati.Checked = True

            Call CercaVerifiche(lIDSpedizione)
        End If
    End Sub

    Private Sub CercaVerifiche(ByVal lIDSpedizione As Long)
        Dim sSql As String, sSqltot As String, sFont As String, sEsito As String, sData As String
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Dett As SqlClient.SqlDataAdapter = Nothing, rsDs_Dett As DataSet = Nothing
        Dim myRiga As TableRow, row_Lista As DataRow
        Dim iErrori As Integer = 0

        If chkSaltati.Checked Then iErrori = iErrori Or 2
        If chkConfermati.Checked Then iErrori = iErrori Or 4
        If chkNonConfermati.Checked Then iErrori = iErrori Or 8

        If ServerSMS_ApriConnessione(myCn) Then
            sSqltot = ""

            sSql = " SELECT Spedizione_Verifiche.ID_Spedizione, Spedizione_Verifiche.Data_Ricezione " & _
                   ", Spedizione_Verifiche.Data_Apertura, Spedizione_Verifiche.Data_Chiusura" & _
                   ", Lista_Verifiche_Numeri.ID" & _
                   ", Verifiche_Numeri_Esito.Data_Importazione, Lista_Verifiche_Numeri.Data_Invio_Provider " & _
                   ", Lista_Verifiche_Numeri.Esito_Mesis, Lista_Verifiche_Numeri.Stato_Invio" & _
                   ", Lista_Verifiche_Numeri.Telefono" & _
                   ", Errori_Provider.Descrizione_WEB AS Descr_Esito, Errori_Provider.Font_WEB " & _
                   ", Descr_NumValido = Case IsNull(ListaStatiNumero.Descrizione_Web, '') WHEN '' THEN ListaStatiNumero.Descrizione_Provider ELSE ListaStatiNumero.Descrizione_Web END " & _
                   ", ListaStatiNumero.IS_Errore" & _
                   " FROM Spedizione_Verifiche " & _
                   "   INNER JOIN Lista_Verifiche_Numeri ON Spedizione_Verifiche.ID_Spedizione = Lista_Verifiche_Numeri.ID_Spedizione " & _
                   "   LEFT JOIN Verifiche_Numeri_Esito ON Verifiche_Numeri_Esito.Telefono = Lista_Verifiche_Numeri.Telefono " & _
                   "                                   AND Verifiche_Numeri_Esito.ID_Provider = Lista_Verifiche_Numeri.ID_Provider " & _
                   "   LEFT JOIN Errori_Provider ON Errori_Provider.Codice_Errore_provider = Lista_Verifiche_Numeri.Esito_MeSIS" & _
                   "   LEFT JOIN (SELECT * " & _
                   "              FROM PopUp " & _
                   "              WHERE " & mSQL.fnWhere("PopUp.TipoPopUp", enum_Confronto.CLAUSE_EQUALS, "EsitoVerificaNumeri") & _
                   "             ) As ListaStatiNumero ON ListaStatiNumero.Provider_SMS = Lista_Verifiche_Numeri.Provider_SMS " & _
                   "                                  AND ListaStatiNumero.Valore = Verifiche_Numeri_Esito.Stato" & _
                   " WHERE " & mSQL.fnWhere("Spedizione_Verifiche.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis) & _
                     SQL_AND & mSQL.fnWhere("Spedizione_Verifiche.ID_Spedizione", enum_Confronto.CLAUSE_EQUALS, lIDSpedizione)

            If iErrori > 0 Then
                If (iErrori And 1) = 1 Then
                    sSqltot = sSqltot & " UNION " & _
                              sSql & SQL_AND & mSQL.fnWhere("Lista_Verifiche_Numeri.Stato_Invio", enum_Confronto.CLAUSE_LESSTHANOREQUAL, CInt(enum_Verifiche_StatoInivio.VER_Inviato))
                End If
                If (iErrori And 2) = 2 Then
                    sSqltot = sSqltot & " UNION " & _
                              sSql & SQL_AND & mSQL.fnWhere("Lista_Verifiche_Numeri.Stato_Invio", enum_Confronto.CLAUSE_EQUALS, CInt(enum_Verifiche_StatoInivio.VER_Errore))
                End If
                If (iErrori And 4) = 4 Then
                    sSqltot = sSqltot & " UNION " & _
                              sSql & SQL_AND & mSQL.fnWhere("Lista_Verifiche_Numeri.Stato_Invio", enum_Confronto.CLAUSE_EQUALS, CInt(enum_Verifiche_StatoInivio.VER_RicevutoReport)) & _
                                        SQL_AND & mSQL.fnWhere("ListaStatiNumero.Is_Errore", enum_Confronto.CLAUSE_EQUALS, 0)
                End If
                If (iErrori And 8) = 8 Then
                    sSqltot = sSqltot & " UNION " & _
                              sSql & SQL_AND & mSQL.fnWhere("Lista_Verifiche_Numeri.Stato_Invio", enum_Confronto.CLAUSE_EQUALS, CInt(enum_Verifiche_StatoInivio.VER_RicevutoReport)) & _
                                        SQL_AND & mSQL.fnWhere("ListaStatiNumero.Is_Errore", enum_Confronto.CLAUSE_DOESNOTEQUAL, 0)
                End If
            Else
                sSqltot = " UNION " & _
                          sSql
            End If

            sSqltot = Mid(sSqltot, 7) & _
                     " ORDER BY Lista_Verifiche_Numeri.Telefono "

            If Apri_Rs_DataSet(myCn, rsDa_Dett, rsDs_Dett, sSqltot) = "" Then
                If rsDs_Dett.Tables(0).Rows.Count > 0 Then
                    hhIDSpedizione.Value = rsDs_Dett.Tables(0).Rows(0)("ID_Spedizione")

                    lblCreazione.Text = FormattaData_2Str("" & rsDs_Dett.Tables(0).Rows(0)("Data_Ricezione"), "", FORMATO_DATA1)
                    lblApertura.Text = FormattaData_2Str(rsDs_Dett.Tables(0).Rows(0)("Data_Apertura"), "", FORMATO_DATA1)
                    lblChiusura.Text = FormattaData_2Str(rsDs_Dett.Tables(0).Rows(0)("Data_Chiusura"), "", FORMATO_DATA1)

                    For Each row_Lista In rsDs_Dett.Tables(0).Rows
                        sData = FormattaData_2Str("" & row_Lista("Data_Invio_Provider"), "")
                        sFont = "tdStandard"
                        If row_Lista("Stato_Invio") = CInt(enum_Verifiche_StatoInivio.VER_RicevutoReport) Then 'Inviato al provider
                            sEsito = "" & row_Lista("Descr_NumValido")
                            If row_Lista("Is_Errore") <> 0 Then
                                sFont = "tdErrore"
                            End If

                        ElseIf row_Lista("Stato_Invio") = CInt(enum_Verifiche_StatoInivio.VER_Errore) Then 'errore
                            If ("" & row_Lista("Descr_Esito")) = "" Then
                                sEsito = "" & row_Lista("Esito_Mesis")
                            Else
                                sEsito = "" & row_Lista("Descr_Esito")
                            End If
                            If "" & row_Lista("Font_WEB") <> "" Then
                                sFont = "" & row_Lista("Font_WEB")
                            ElseIf ("" & row_Lista("Esito_Mesis")) = "NoCreditiMeSIS" Then
                                sFont = "tdErroreBanale"
                            ElseIf ("" & row_Lista("Esito_Mesis")) = "NonVerificato" Then
                                sFont = "tdGrigio"
                            Else
                                sFont = "tdErrore"
                            End If
                        Else
                            If ("" & row_Lista("Esito_Mesis") = "InvioCorretto") And ("" & row_Lista("Data_Importazione") = "") Then
                                sData = ""
                            End If
                            sEsito = ""
                        End If

                        myRiga = New System.Web.UI.WebControls.TableRow
                        Call Table_AggiungiColonna(myRiga, row_Lista("Telefono"), HorizontalAlign.Left, sFont)
                        Call Table_AggiungiColonna(myRiga, sData, HorizontalAlign.Center, sFont)
                        Call Table_AggiungiColonna(myRiga, sEsito, HorizontalAlign.Left, sFont)

                        tblSpedizioni.Rows.Add(myRiga)
                    Next
                End If
            End If
            Call Chiudi_Rs_DataSet(rsDs_Dett, rsDa_Dett)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)
    End Sub

    Protected Sub cmdCerca_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCerca.Click
        Call CercaVerifiche(Val(hhIDSpedizione.Value))
    End Sub
End Class