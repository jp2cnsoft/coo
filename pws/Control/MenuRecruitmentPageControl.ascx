<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuRecruitmentPageControl.ascx.cs" Inherits="Control_MenuRecruitmentPageControl" %>
<div class="Submenu">
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="imgManagementPost" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnManagementPost" runat="server" 
            meta:resourcekey="R00010" onclick="lbtnManagementPost_Click"></asp:LinkButton></div>
    </div>
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="imgRecruitmentMethod" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnRecruitmentMethod" runat="server" meta:resourcekey="R00020" OnClick="lbtnRecruitmentMethod_Click"></asp:LinkButton></div>  
    </div>
</div>