<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuMessagePageControl.ascx.cs" Inherits="Control_MenuMessagePageControl" %>
<div class="Submenu">
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="imgAllMessage" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnAllMessage" runat="server" 
            Text="<%$ Resources:GPR, ALLMESSAGE %>" onclick="lbtnAllMessage_Click"></asp:LinkButton></div>
    </div>
</div>