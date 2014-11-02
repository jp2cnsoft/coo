<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuSetHomeControl.ascx.cs" Inherits="Control_MenuSetHomeControl" %>
<div class="Submenu">
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="Image1" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="LinkButton1" runat="server" meta:resourcekey="R00010" OnClick="LinkButton1_Click"></asp:LinkButton></div>
    </div>
</div>