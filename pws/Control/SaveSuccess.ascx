<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SaveSuccess.ascx.cs" Inherits="Control_SaveSuccess" %>
<asp:Panel  runat="server" id="saveSuccess" Height="56px" meta:resourcekey="saveSuccessResource1">
    <table width="100%" cellspacing="8">
        <tr>
            <td align="left" valign="middle" height="40px" style="border-width:1px;border-color:black;background-color:lightyellow;border:2px solid #CCCCCC;" >                      
                <asp:Image ID="imgRemark" BorderColor="Black" style="padding-left:10px;" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/saveok.png"></asp:Image>
                <asp:Label ID="lblRemark"  Font-Size="14px" CssClass="leaderFont" runat="server"></asp:Label> 
            </td>
        </tr>
    </table> 
</asp:Panel>  