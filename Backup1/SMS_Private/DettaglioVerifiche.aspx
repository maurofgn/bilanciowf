<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="DettaglioVerifiche.aspx.vb" Inherits="ServerSMS.DettaglioVerifiche" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hhIDSpedizione" runat="server" />
    <center>
    <table style="width:70%;" class="tblStandard" border="1">
        <tr>
            <td class="tdIntestazione" align="left">Dati della spedizione:</td>
        </tr>
        <tr>
            <td align="left"><table style="width:100%;" class="tblStandard" border="0">
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Data e ora creazione:&nbsp;<asp:Label ID="lblCreazione" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Data e ora apertura:&nbsp;<asp:Label ID="lblApertura" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Data e ora chiusura:&nbsp;<asp:Label ID="lblChiusura" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table></td>
        </tr>
    </table>
    <br />
    <asp:CheckBox ID="chkSaltati" runat="server" TextAlign="Left" Text="Saltati" />  
    &nbsp;&nbsp;&nbsp;&nbsp;   
    <asp:CheckBox ID="chkConfermati" runat="server" TextAlign="Left" Text="Confermati" />
    &nbsp;&nbsp;&nbsp;&nbsp;   
    <asp:CheckBox ID="chkNonConfermati" runat="server" TextAlign="Left" Text="Non Confermati" />
    &nbsp;&nbsp;&nbsp;&nbsp;   
    <asp:LinkButton ID="cmdCerca" runat="server">Cerca</asp:LinkButton>
    </center>
    <br />
    <asp:Table ID="tblSpedizioni" runat="server" Width="100%" BorderStyle="Solid" 
        CssClass="tblStandard" BorderWidth="1" BorderColor="Black">
        <asp:TableHeaderRow ID="trIntestazione">
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Telefono"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Data Verifica"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Esito"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <asp:HyperLink ID="lkIndietro" runat="server" 
        NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Indietro</asp:HyperLink>    
</asp:Content>
