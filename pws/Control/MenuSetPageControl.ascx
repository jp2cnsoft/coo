<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuSetPageControl.ascx.cs" Inherits="Control_MenuSetPageControl" %>
<%--<div class="Submenu">
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="imgIndexSet" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnIndexSet" runat="server" meta:resourcekey="R00110" OnClick="lbtnIndexSet_Click" ></asp:LinkButton></div>
    </div>
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="Image2" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="LinkButton1" runat="server" meta:resourcekey="R00010" OnClick="LinkButton1_Click"></asp:LinkButton></div>
    </div>
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="Image5" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="LinkButton6" runat="server" meta:resourcekey="R00060" OnClick="LinkButton6_Click"></asp:LinkButton></div>
    </div>    
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="Image9" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnStyle" runat="server" 
                meta:resourcekey="R00120" onclick="lbtnStyle_Click"></asp:LinkButton></div>
    </div>
    <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
        <div class="Submenuimg"><asp:Image ID="Image7" width="13px" height="13px" runat="server" /></div>
	    <div class="Submenucon"><asp:LinkButton ID="lbtnRePage" runat="server" meta:resourcekey="R00080" OnClick="lbtnRePage_Click"></asp:LinkButton></div>
    </div>
    
</div>--%>
<div class="Submenu">
    <asp:Repeater ID="repList" runat="server" 
        onitemdatabound="repList_ItemDataBound">
        <ItemTemplate>
            <div onmouseover="this.className='Sub_over'" onmouseout="this.className='Sub_out'">
                <div class="Submenuimg"><asp:Image ID="imgIndexSet" ImageUrl="~/Images/book01.png" width="13px" height="13px" runat="server" /></div>
	            <div class="Submenucon"><asp:HyperLink ID="hlkIndexSet" Text='<%# Eval("showtext") %>' NavigateUrl='<%# Eval("url") %>' runat="server"></asp:HyperLink></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>