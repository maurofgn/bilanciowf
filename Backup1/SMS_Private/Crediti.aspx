<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="Crediti.aspx.vb" Inherits="ServerSMS.Crediti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <table style="width:90%;" class="tblStandard" border="1">
        <tr>
            <td class="tdIntestazione" align="left">Situazione Crediti:</td>
        </tr>
        <tr>
            <td align="left"><table style="width:100%;" class="tblStandard" border="0">
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Crediti residui:&nbsp;<asp:Label 
                            ID="lblCreditiResidui" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRiepilogo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table></td>
        </tr>
    </table> 
    <br />
    <asp:Table ID="tblCrediti" runat="server" Width="90%" BorderStyle="Solid" 
        CssClass="tblStandard" BorderWidth="1" BorderColor="Black">
        <asp:TableHeaderRow ID="trIntestazione">
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Data Attivazione" ></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Data Scadenza" ></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Crediti Acquistati"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Crediti Residui"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Indietro</asp:HyperLink>
    </center>
</asp:Content>
