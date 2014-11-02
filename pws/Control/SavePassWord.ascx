<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SavePassWord.ascx.cs" Inherits="Control_SavePassWord" %>
<asp:Panel  runat="server" id="saveSuccess" Height="56px" meta:resourcekey="saveSuccessResource1">
    <table width="100%" cellspacing="8">
        <tr>
            <td align="left" valign="middle" height="40px" class="saveSuccess" style="border:2px solid #CCCCCC;background-color:lightyellow;" >                      
                <asp:Image ID="imgRemark" BorderColor="Black" CssClass="saveSuccess_img" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/saveok.png"></asp:Image>
                <asp:Label ID="lblRemark"  Font-Size="13px" CssClass="leaderFont" runat="server" meta:resourcekey="R00010" Font-Bold="true"></asp:Label> 
                <asp:Label ID="Label1"  Font-Size="13px" CssClass="leaderFont" runat="server" meta:resourcekey="R00020"></asp:Label> 
            </td>
        </tr>
    </table> 
</asp:Panel>  