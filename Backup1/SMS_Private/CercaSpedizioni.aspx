<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="CercaSpedizioni.aspx.vb" Inherits="ServerSMS.CercaSpedizioni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center><asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label><br /><br /></center>
    <table style="border: 1px solid #000000; width:100%;" class="tblStandard">
        <tr>
            <td class="tdIntestazione" align="left" colspan="2">Criteri di ricerca:</td>
            <td rowspan="2" align="right" valign="top" 
                style="width: 170px; border-left-style: solid; border-left-width: 1px; border-left-color: #000000; line-height: 15pt;">

                <asp:LinkButton ID="cmdSms" runat="server" Font-Bold="True">Gestione SMS</asp:LinkButton>
                <br />
                <asp:LinkButton ID="cmdVer" runat="server" Font-Bold="True">Verifica Numeri</asp:LinkButton>
                <br />
                <hr size="1" style="height: 1px" />
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SMS_Private/Configurazioni.aspx">Config. servizio</asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/SMS_Private/Info.aspx">Info servizio</asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SMS_Private/Crediti.aspx">Situazione Crediti</asp:HyperLink>
            </td>
         </tr>
        <tr>
            <td align="left" valign="top" >
                <br />
                <asp:Panel ID="pnlRicercaSMS" runat="server" Width="580px" Height="70px">
                    <table style="width:100%;" class="tblStandard" border="0">
                        <tr>
                            <td align="right" style="width: 220px;">
                                Dalla Data:&nbsp;<asp:TextBox ID="txtDallaData" runat="server" Width="118px"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 170px;">
                                <asp:CheckBox ID="chkEvase" runat="server" TextAlign="Left" Text="Evasa" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                Alla Data:&nbsp;<asp:TextBox ID="txtAllaData" runat="server" Width="118px"></asp:TextBox>
                            </td>
                            <td align="right" >
                                <asp:CheckBox ID="chkErrori" runat="server" TextAlign="Left" Text="Con Errori" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="font-size: 8pt"><br /><i>&nbsp;&nbsp;(*) Le richieste ancora aperte vengono sempre visualizzate.</i></td>
                            <td align="center">
                                &nbsp;&nbsp;<asp:LinkButton ID="cmdCerca" runat="server">Cerca</asp:LinkButton>
                            </td>
                        </tr>
                    </table> 
                </asp:Panel>
                <asp:Panel ID="pnlRicercaVer" runat="server" Width="580px" Height="70px">
                    <table style="width:100%;" class="tblStandard" border="0">
                        <tr>
                            <td align="right" style="width: 220px;">
                                Dalla Data:&nbsp;<asp:TextBox ID="txtVerDallaData" runat="server" Width="118px"></asp:TextBox>
                            </td>
                            <td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td >&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 220px;">
                                Alla Data:&nbsp;<asp:TextBox ID="txtVerAllaData" runat="server" Width="118px"></asp:TextBox>
                            </td>
                            <td >&nbsp;</td>
                            <td align="left">
                                &nbsp;&nbsp;<asp:LinkButton ID="cmdCercaVerifiche" runat="server">Cerca</asp:LinkButton>
                            </td>
                        </tr>      
                        <tr>
                            <td colspan="2" align="right">&nbsp;</td>
                            <td align="center">
                                &nbsp;&nbsp;
                                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/SMS_Private/VerificaNumero.aspx">Verifica Numero</asp:HyperLink>
                            </td>
                        </tr> 
                    </table> 
                </asp:Panel> 
            </td>
        </tr>
    </table>
    <br />
    <asp:Table ID="tblSpedizioni" runat="server" Width="100%" BorderStyle="Solid" 
        CssClass="tblStandard" BorderWidth="1" BorderColor="Black">

    </asp:Table>
</asp:Content>
