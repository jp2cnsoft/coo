<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuControl.ascx.cs" Inherits="Control_MenuControl" %>
<div class="Submenu">
    <asp:Repeater ID="repMenu" runat="server">
        <ItemTemplate>
            <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
                <asp:HiddenField ID="hfdLink" Value='<%# Eval("managelink") %>' runat="server" />
                <div class="Submenuimg"><asp:Image ID="imgHead" ImageUrl="~/Images/book01.png" width="13px" height="13px" runat="server"/></div>
                <div class="Submenucon"><asp:HyperLink ID="hlkName" Text='<%# Eval("content") %>' NavigateUrl='<%# "../Pages/" + Eval("managelink") %>' runat="server"></asp:HyperLink></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>