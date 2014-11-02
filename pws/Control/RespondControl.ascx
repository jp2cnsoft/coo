<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RespondControl.ascx.cs" Inherits="Control_RespondControl" %>
<%@ Register Src="ValidateCodeControl.ascx" TagName="ValidateCodeControl"
    TagPrefix="uc2" %>
<div class="top15 width100p">
    <table width="100%">
        <tr>
            <td class="tdButtomLineB">
                <table width="100%">
                    <tr>
                        <td width="15%" align="right">
                            <asp:Label ID="lbtitle" runat="server" meta:resourcekey="R00010"></asp:Label>:
                        </td>
                        <td align="left"  colspan="2">
                            <asp:TextBox ID="txtTitle" runat="server" SkinID="SkinRegisterBg" Width="70%" meta:resourcekey="txtTitleResource1"></asp:TextBox><asp:Label ID="remarkTitle" CssClass="leaderFont" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                         <td align="right" rowspan="2" valign="top" width="15%">
                            <asp:Label ID="lbcontent" runat="server" meta:resourcekey="R00020"></asp:Label>:
                         </td>
                        <td align="left">
                            <asp:Label ID="remarkContent" CssClass="leaderFont" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 164px">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server"  Rows="10" Width="90%" Height="160px" meta:resourcekey="txtContentResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <asp:Label ID="lblValiCode" runat="server" meta:resourcekey="R00030"></asp:Label>:
                        </td>
                        <td align="left" colspan="2" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtValidateCode" SkinID="SkinRegisterBg" runat="server" Width="50px" meta:resourcekey="txtValidateCodeResource1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <uc2:ValidateCodeControl ID="validateCodeCol" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="errValidateCode" runat="server" CssClass="errFont" meta:resourcekey="errValidateCodeResource1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>