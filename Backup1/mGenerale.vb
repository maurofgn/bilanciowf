Module mGenerale

    Public Structure tyApplicazione
        Dim sInitialCatalog As String
        Dim sDataSource As String
        Dim sUtenteSA As String
        Dim sPswSA As String
        'Dim sProvider As String
    End Structure

    Public g_tyApplicazione As tyApplicazione

    Public Function EseguiScript(ByVal NomeScript As String) As String
        EseguiScript = "<script language='javascript'> " & _
                            NomeScript & ";" & _
                       "</script>"
    End Function

    Private Function EsaTODec(ByVal ESA As String) As Integer
        Select Case UCase(ESA)
            Case "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" : EsaTODec = 0 + ESA
            Case "A" : EsaTODec = 10
            Case "B" : EsaTODec = 11
            Case "C" : EsaTODec = 12
            Case "D" : EsaTODec = 13
            Case "E" : EsaTODec = 14
            Case "F" : EsaTODec = 15
            Case Else : EsaTODec = 0
        End Select
    End Function

    Public Function Encrypt_Hex(ByVal sAction As String, _
                                ByVal sSrc As String, _
                                Optional ByVal lOffSet As Long = 0) As String
        Dim sDest As String, sKey As String, appo1 As String
        Dim lKeyPos As Long, lKeyLen As Long, lSrcAsc As Long, lSrcPos As Long, lTmpSrcAsc As Long

        sKey = "hj4hHdtPay5gdSmkgi5Sx74UogiipsxrNSREs5Kx"

        lKeyPos = 0
        sDest = ""
        Encrypt_Hex = ""

        lKeyLen = Len(sKey)
        If Len(sSrc) = 0 Then
            Encrypt_Hex = ""
            Exit Function
        End If

        If sAction = "E" Then
            Randomize()
            If lOffSet = 0 Then lOffSet = (Rnd() * 10000 Mod 255) + 1
            sDest = Right("  " & Hex(lOffSet), 2)
            For lSrcPos = 1 To Len(sSrc)
                lSrcAsc = (Asc(Mid(sSrc, lSrcPos, 1)) + lOffSet) Mod 255
                If lKeyPos < lKeyLen Then
                    lKeyPos = lKeyPos + 1
                Else
                    lKeyPos = 1
                End If
                lSrcAsc = lSrcAsc Xor Asc(Mid(sKey, lKeyPos, 1))
                sDest = sDest & Right("  " & Hex(lSrcAsc), 2)
                lOffSet = lSrcAsc
            Next
            Encrypt_Hex = Replace(sDest, " ", "0")

        ElseIf sAction = "D" Then
            lOffSet = EsaTODec(Mid(sSrc, 2, 1)) + (EsaTODec(Mid(sSrc, 1, 1)) * 16)
            For lSrcPos = 3 To Len(sSrc) Step 2
                appo1 = Mid(sSrc, lSrcPos, 2)
                lSrcAsc = (EsaTODec(Mid(appo1, 2, 1)) + (EsaTODec(Mid(appo1, 1, 1)) * 16))

                If lKeyPos < lKeyLen Then
                    lKeyPos = lKeyPos + 1
                Else
                    lKeyPos = 1
                End If
                lTmpSrcAsc = lSrcAsc Xor Asc(Mid(sKey, lKeyPos, 1))
                If lTmpSrcAsc <= lOffSet Then
                    lTmpSrcAsc = 255 + lTmpSrcAsc - lOffSet
                Else
                    lTmpSrcAsc = lTmpSrcAsc - lOffSet
                End If
                sDest = sDest & Chr(lTmpSrcAsc)
                lOffSet = lSrcAsc
            Next
            Encrypt_Hex = sDest
        End If
    End Function

    Public Sub InizializzaApplicazione()
        g_tyApplicazione.sInitialCatalog = "ServerSMS"
        g_tyApplicazione.sDataSource = "192.200.200.37"
        g_tyApplicazione.sUtenteSA = "provasms"
        g_tyApplicazione.sPswSA = "provasms"
    End Sub

    Public Function IsBounded(ByVal vntArray As Object, _
                              Optional ByVal iQualeDimensione As Integer = -1) As Boolean
        'note: the application in the IDE will stop
        'at this line when first run if the IDE error
        'mode is not set to "Break on Unhandled Errors"
        '(Tools/Options/General/Error Trapping)

        Try
            If iQualeDimensione = -1 Then
                IsBounded = IsNumeric(UBound(vntArray))
                If IsBounded Then
                    If UBound(vntArray) < 0 Then
                        IsBounded = False
                    End If
                End If
            Else
                IsBounded = IsNumeric(UBound(vntArray(iQualeDimensione)))
                If IsBounded Then
                    If UBound(vntArray(iQualeDimensione)) < 0 Then
                        IsBounded = False
                    End If
                End If
            End If
        Catch ex As Exception
            IsBounded = False
        End Try
    End Function

    Public Function Find_In_Array_String(ByVal sValore As String, _
                                         ByRef arrValori()() As String, _
                                         ByVal iQualeDimensione As Integer) As Long
        Dim l As Long

        Find_In_Array_String = -1
        If IsBounded(arrValori, iQualeDimensione) Then
            For l = LBound(arrValori(iQualeDimensione)) To UBound(arrValori(iQualeDimensione))
                If StrComp(sValore, arrValori(iQualeDimensione)(l), CompareMethod.Text) = 0 Then
                    Find_In_Array_String = l
                    Exit For
                End If
            Next l
        End If
    End Function

    Public Function Find_In_Array_String(ByVal sValore As String, _
                                         ByRef arrValori() As String) As Long
        Dim l As Long

        Find_In_Array_String = -1
        If IsBounded(arrValori) Then
            For l = LBound(arrValori) To UBound(arrValori)
                If StrComp(sValore, arrValori(l), CompareMethod.Text) = 0 Then
                    Find_In_Array_String = l
                    Exit For
                End If
            Next l
        End If
    End Function

    Public Sub Table_AggiungiColonna(ByRef myRiga As TableRow, _
                                     ByVal sTesto As String, _
                                     ByVal myAlign As HorizontalAlign, _
                                     Optional ByVal sCssClass As String = "tdStandard", _
                                     Optional ByVal iBold As Integer = -1, _
                                     Optional ByVal sToolTipText As String = "")

        Dim myCol As System.Web.UI.WebControls.TableCell

        myCol = New System.Web.UI.WebControls.TableCell
        myCol.CssClass = sCssClass

        If iBold <> -1 Then
            myCol.Font.Bold = IIf((iBold = 0), False, True)
        End If

        myCol.HorizontalAlign = myAlign
        myCol.Text = sTesto
        myCol.ToolTip = sToolTipText
        myRiga.Cells.Add(myCol)
    End Sub

    Public Sub Table_AggiungiColonna(ByRef myRiga As TableRow, _
                                     ByVal myControl As Control, _
                                     ByVal myAlign As HorizontalAlign, _
                                     Optional ByVal sCssClass As String = "tdStandard")

        Dim myCol As System.Web.UI.WebControls.TableCell

        myCol = New System.Web.UI.WebControls.TableCell
        myCol.CssClass = sCssClass
        myCol.HorizontalAlign = myAlign
        myCol.Controls.Add(myControl)
        myRiga.Cells.Add(myCol)
    End Sub

    Public Sub ComboBox_CreaListItem(ByRef myCbo As System.Web.UI.WebControls.DropDownList, _
                                     ByVal sIndice As String, _
                                     ByVal sValore As String, _
                                     ByVal sIndiceSelezionato As String)

        Dim myLi As System.Web.UI.WebControls.ListItem

        myLi = New System.Web.UI.WebControls.ListItem
        myLi.Text = sValore
        myLi.Value = sIndice
        If sIndiceSelezionato = sIndice Then
            myLi.Selected = True
        Else
            myLi.Selected = False
        End If
        myCbo.Items.Add(myLi)
    End Sub
End Module
