<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupControl.ascx.cs" Inherits="Control_GroupControl" %>

            <asp:Button id="btnAddGroup" onclick="btnAddGroup_Click" runat="server" meta:resourcekey="R00010"></asp:Button>

            <asp:CheckBoxList id="chklstGroup" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0"></asp:CheckBoxList>
            <asp:Label ID="lblNullGroup" runat="server"  Font-Size="12px" CssClass="leaderFont"></asp:Label>

<asp:Panel id="pnlAddGroup" runat="server" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <asp:TextBox id="txtNewGroup" runat="server" SkinID="SkinRegisterBg" 
                    MaxLength="20" Width="100px"></asp:TextBox>
                <asp:Button id="btnSave" runat="server" OnClick="btnSave_Click" meta:resourcekey="R00020"></asp:Button>
                <asp:Button id="btnGroupCancel" runat="server" OnClick="btnGroupCancel_Click"  meta:resourcekey="R00030"></asp:Button>
                <asp:Label runat="server" ID="lblErrorGroup" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblGroupRemark" meta:resourcekey="R00040" CssClass="leaderFont"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel> 