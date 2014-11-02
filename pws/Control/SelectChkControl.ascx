<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectChkControl.ascx.cs" Inherits="Control_SelectChkControl" %>

<asp:LinkButton ID="lbtnAllSelect" runat="server" OnClick="lbtnAllSelect_Click" Visible="false" meta:resourcekey="R00010"></asp:LinkButton>
<asp:Button ID="btnSelect" runat="server" meta:resourcekey="R00020" OnClick="btnSelect_Click" />
<asp:Repeater ID="selectTitleList" runat="server" 
    OnItemCommand="selectTitleList_ItemCommand" 
    meta:resourcekey="callingTitleListResource1" 
    onitemdatabound="selectTitleList_ItemDataBound">
    <ItemTemplate>
        <asp:HiddenField ID="hfSelectID" runat="server" Value='<%# Eval("MA_ORDERID") %>' />
         <asp:Label ID="lblSelectLink" Text=">" Visible="false" runat="server"></asp:Label><asp:LinkButton ID="lbtnSelectLink" runat="server" Text='<%# Eval("NAME") %>'></asp:LinkButton>
    </ItemTemplate>
</asp:Repeater>
<%--<asp:DataList ID="dlSelect" runat="server" RepeatColumns="6" 
    RepeatDirection="Horizontal" OnItemCommand="dlSelect_ItemCommand" 
    OnItemDataBound="dlSelect_ItemDataBound" 
    meta:resourcekey="dlSelectResource1">
    <ItemTemplate>
        <td align="left">
            <asp:HiddenField ID="hfSelectID" runat="server" Value='<%# Eval("MA_ORDERID") %>' />
            <asp:HiddenField ID="hfSelectText" runat="server" Value='<%# Eval("NAME") %>' />
            <asp:LinkButton ID="lbtnSelectLink" runat="server" Text='<%# Eval("NAME") %>' 
                meta:resourcekey="lbtnSelectLinkResource2"></asp:LinkButton>
        </td>
        <td>
        </td>
    </ItemTemplate>
</asp:DataList>--%>
<table>
    <tr>
        <td>
            <asp:Repeater ID="rptSelect" runat="server" 
                onitemdatabound="rptSelect_ItemDataBound" 
                onitemcommand="rptSelect_ItemCommand">
                <ItemTemplate>
                    <asp:HiddenField ID="hfSelectID" runat="server" Value='<%# Eval("MA_ORDERID") %>' />
                    <asp:HiddenField ID="hfSelectText" runat="server" Value='<%# Eval("NAME") %>' />
                    <asp:Label ID="lblCallingLine" Text="|" Visible="false" runat="server"></asp:Label>
                    <asp:LinkButton ID="lbtnSelectLink" runat="server" Text='<%# Eval("NAME") %>'></asp:LinkButton>
                    
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
<asp:CheckBoxList ID="chklstSelectChk" runat="server" RepeatColumns="2" 
    RepeatDirection="Horizontal" meta:resourcekey="chklstSelectChkResource1"></asp:CheckBoxList>