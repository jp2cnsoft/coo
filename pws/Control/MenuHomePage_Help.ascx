<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuHomePage_Help.ascx.cs" Inherits="Control_MenuHomePage_Help" %>
<div id="leftMu">
    <asp:Panel ID="pnlHelp" Visible="false" runat="server">
	    <%--<div class="leftMuH" id="leftMenuHead" runat="server"></div>
	    <div class="leftMuC">--%>
	        <div class="side_question"><asp:HyperLink ID="hlkQuestion"  NavigateUrl="~/Pages/P3000/P3000P0130.aspx" runat="server"></asp:HyperLink></div>
<%--	        <div class="side_ly"><asp:HyperLink ID="hlkMess"  NavigateUrl="~/gbook/index.aspx" runat="server" Target="_blank"></asp:HyperLink></div>
--%>	        <%--<div class="side_ad"><asp:HyperLink ID="hlkAd"  NavigateUrl="~/Pages/P3000/P3000P0132.aspx" runat="server"></asp:HyperLink></div>--%>
	    <%--</div>--%>
	</asp:Panel>
	
	<asp:Panel ID="pnlAbout" Visible="false" runat="server">
			<%--<div class="leftMuL" id="leftMenuHeadLink" runat="server"></div>
			<div class="leftMuC">--%>
				<div class="side_lianxi"><asp:HyperLink ID="hlkLink"  NavigateUrl="~/Pages/P3000/P3000P0150.aspx" runat="server"></asp:HyperLink></div>
	            <div class="side_online"><asp:HyperLink ID="hlkOnlineLink" Target="_blank" NavigateUrl="~/Pages/P3000/P3000P9010.aspx" runat="server"></asp:HyperLink></div>
			<%--</div>--%>
	</asp:Panel>
	
	<asp:Panel ID="pnlTm" Visible="false" runat="server">
	    <div class="side_tm">
		    <a href="tencent://message/?uin=429219375">
		        <%--<asp:ImageButton ID="ibtnTm" ImageUrl="~/Images/menuLef_02.png" AlternateText="<%$ Resources:GPR, LINKME %>" runat="server" />--%>
		    </a>
	    </div>
	</asp:Panel>
</div>