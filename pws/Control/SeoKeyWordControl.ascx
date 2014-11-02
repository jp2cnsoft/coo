<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SeoKeyWordControl.ascx.cs" Inherits="Control_SeoKeyWordControl" %>
<div id="ShowEditer" style="position: absolute; width: 200px; height: 130px; display: none;">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
      <tr>
        <td height="29"><div style="position: absolute; left:25px; top:25px; height:10px; z-index:1;"><asp:Literal ID="Label2" runat="server" Text="<%$ Resources:GPR, SEOKEYWORD %>"></asp:Literal></div><img src="../../Images/two_01.png" height="29" border="0" usemap="#Map"></td></tr>
      <tr>
        <td align="center" background="../../Images/two_02.png"><table border="0" cellpadding="0" cellspacing="0" style="width: 80%; height: 100%">
                <tr>
            	    <td><textarea cols="20" rows="3" id="SeoKeyWord" name="SeoKeyWord"><%=SeoKeyWord %></textarea></td>
                </tr>
                <tr>
            	    <td align="right"><input id="Button1" type="button" runat="server" onclick='EditSeoKeyWord()' value="<%$ Resources:GPR, CONFIRM %>"></td>
                </tr></table>
        </td>
      </tr>
    </table>
    <map name="Map"><area shape="rect" coords="195,5,200,15" href="javascript:void(0)" onclick="EditSeoKeyWord()"></map>
</div>


[<a href='javascript:void(0)' onclick='EditSeoKeyWord()' runat="server" title="<%$ Resources:GPR, SEOKEYWORD %>"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:GPR, SEOKEYWORD %>"></asp:Label></a>]

<input id='DetailId' name='DetailId' type="hidden" value="<%=DetailId %>" />
<input id='Has' name='Has' type="hidden" value="<%=Has %>" />

<script type="text/javascript" src="../../Res/jquery/jquery-1.3.1.js"></script>

<script type="text/javascript">
    function EditSeoKeyWord() {
        $('#ShowEditer').toggle();
    }
</script>
<%-- <asp:Button ID="Button1" runat="server" Text="Button" /> --%>
