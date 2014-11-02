<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuServicePageControl.ascx.cs" Inherits="Control_MenuServicePageControl" %>
<div id="leftmenu" style="background-color:#F5F5F5;">
    <div style="width:148px;*width:auto;" onmouseover="this.className='div_over'" onmouseout="this.className='div_out'">
        <asp:Image ID="imgServiceList" width="13px" height="13px" runat="server" />&nbsp;
	    <asp:LinkButton ID="lbtnServiceList" runat="server" meta:resourcekey="R00010" OnClick="lbtnServiceList_Click"></asp:LinkButton>
    </div>
    <div style="width:165px;*width:auto;" onmouseover="this.className='div_over'" onmouseout="this.className='div_out'">
        <asp:Image ID="imgServiceDrive" width="13px" height="13px" runat="server" />&nbsp;
	    <asp:HyperLink ID="hlinkServiceDrive" runat="server" meta:resourcekey="R00020"></asp:HyperLink>
<%--	    <asp:LinkButton ID="lbtnServiceDrive" runat="server" meta:resourcekey="R00020" OnClick="lbtnServiceDrive_Click" ></asp:LinkButton>
--%>    </div>
    <div style="width:148px;*width:auto;" onmouseover="this.className='div_over'" onmouseout="this.className='div_out'">
        <asp:Image ID="imgServiceMy" width="13px" height="13px" runat="server" />&nbsp;
	    <asp:LinkButton ID="lbtnServiceMy" runat="server" meta:resourcekey="R00030" OnClick="lbtnServiceMy_Click"></asp:LinkButton>
    </div>
</div>