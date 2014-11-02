<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuHomePage.ascx.cs" Inherits="Control_MenuHomePage" %>
<div class="width3">

    <div class="leftMenuHead">
        <asp:Label ID="lblRegOrgNone" runat="server" CssClass="leftMenuFont"  meta:resourcekey="R00040"></asp:Label>
	</div>	
	<div  class="leftMenuBorder">
		<div class="width3">
			<asp:HyperLink ID="hlnkReg" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00050"/>
		</div>
		<div class="width3">
		    <asp:HyperLink ID="hlnkOnline" runat="server" CssClass="leftConFont" Visible="false" Target="_blank" meta:resourcekey="R00060"/>
		</div>
	</div>
	
	<div class="clearAll"></div>
    <div class="leftMenuPad"></div>

     <div class="leftMenuHead">
        <asp:Label ID="lblRegOrg" runat="server" CssClass="leftMenuFont"  meta:resourcekey="R00010"></asp:Label>
    </div>
    <div class="leftMenuBorder">
		<div class="width3">
		    <asp:HyperLink ID="hlnkLogin" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00020"/>
		</div>
		<div class="width3">
			<asp:HyperLink ID="hlnkPass" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00030"/>
		</div>
	</div>

    <div class="clearAll"></div>
    <div class="leftMenuPad"></div>

    <div class="leftMenuHead">
        <asp:Label ID="lblService" runat="server" CssClass="leftMenuFont"  meta:resourcekey="R00160"></asp:Label>
	</div>	
	<div  class="leftMenuBorder">
<%--		<div class="width3">
            <a href="../P3010/P3010P0040.aspx" target="_blank"><img src="../../Images/buttonTY_06.png" style="padding-left:16px; padding-top:5px; border:0;"/></a>
		</div>--%>
		<div class="width3">
		    <asp:Label ID="lblServiceLogin" runat="server" meta:resourcekey="R00170"></asp:Label>
		</div>
	</div>
	
	<div class="clearAll"></div>
    <div class="leftMenuPad"></div>
    
	<div class="leftMenuHead">
	    <asp:Label ID="lblCustomer" runat="server" CssClass="leftMenuFont"  meta:resourcekey="R00070"></asp:Label>
	</div>	
	<div  class="leftMenuBorder">
		<div class="width3">
		    <asp:HyperLink ID="hlnkLink" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00080"/>
		</div>
		<div class="width3">
		     <asp:HyperLink ID="hlnkAd" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00090"/>
		</div>
		<div class="width3">
		    <asp:HyperLink ID="hlnkLinkOnline" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00100"/>
		</div>
		<div class="width3">
		    <asp:HyperLink ID="hlnkLinkAsk" runat="server" CssClass="leftConFont" Target="_blank" meta:resourcekey="R00140"/>
		</div>
		<div class="width3">
		    <asp:Label ID="lblTmOnline" runat="server" meta:resourcekey="R00150"></asp:Label>
        </div>
	</div>

    <div class="clearAll"></div> 
    <div class="leftMenuPad"></div>

	<div class="leftMenuHead">
	    <asp:Label ID="lblHostAnno" runat="server" CssClass="leftMenuFont"  meta:resourcekey="R00110"></asp:Label>
	</div>	
	<div class="leftMenuBorder">
		<div class="width3">
		    <asp:Label ID="lblHostAnnoN" runat="server" CssClass="leftConVoid"  meta:resourcekey="R00120"></asp:Label>
		</div>
        <div class="leftMuneFoot"></div>
	</div>
	<div class="leftMapMarg">
	     <img src="../../Images/map001.png" border="0" width="100px"/>
	</div>
	<div class="leftMapMarg">
	     <%--<img src="../../Images/12060.png" border="0" width="100px"/>--%>
        <asp:ImageButton ID="ImageButton1" runat="server" border="0" width="100px" 
             ImageUrl="~/Images/12060.png"/>
	</div>

	<div></div>
</div>