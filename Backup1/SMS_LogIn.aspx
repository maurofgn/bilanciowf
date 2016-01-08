<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="SMS_LogIn.aspx.vb" Inherits="ServerSMS.SMS_LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <br /><br /><br />
        <asp:Login ID="LoginSMS" runat="server" DisplayRememberMe="False" 
            LoginButtonType="Link" TitleText="" UserNameLabelText="Utente:" 
            FailureText="">
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Utente:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="UserName" runat="server" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="Il nome utente è obbligatorio." 
                                            ToolTip="Il nome utente è obbligatorio." ValidationGroup="LoginSMS">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" ErrorMessage="La password è obbligatoria." 
                                            ToolTip="La password è obbligatoria." ValidationGroup="LoginSMS">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <br />
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="LoginLinkButton" runat="server" CommandName="Login" 
                                            ValidationGroup="LoginSMS">Accedi</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
    </center>
    <p align="left">
        <br />
        <font face="Verdana" size="2"><em>Segnaliamo che e' stato attivato un servizio di email che segnalera':<br />
        - ad inizio mese gli SMS residui per ogni pacchetto acquistato<br />
        - 10 giorni prima del termine gli SMS in scadenza<br />
        - pacchetti SMS terminati<br />
        - SMS non inviati per insufficienza di crediti<br />
        </em></font>
        <br />
        <br />
    </p>
</asp:Content>
