<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ValidateCodeControl.ascx.cs" Inherits="Control_ValidateCodeControl" %>
<script type="text/javascript">
function CallServer(inputcontrol,context)
{
　<% = Page.ClientScript.GetCallbackEventReference(this, "", "ReceiveServerData", "")%>; 
}
function ReceiveServerData(result)
{
    document.getElementById("imgShow").src ="../SYS/ShowImage.aspx?" + result;
}
</script>
<img src="../SYS/ShowImage.aspx" id="imgShow" /><asp:Literal ID="ltlImgShow" runat="server"></asp:Literal>
