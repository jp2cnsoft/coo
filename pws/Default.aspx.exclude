<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="labMessage" runat="server" Text=""></asp:Label>
    <div>
        <br />
        <asp:RadioButtonList ID="cmdSelect" runat="server">
            <asp:ListItem Value="0x05">CMD_SET_COMMON_FILES</asp:ListItem>
            <asp:ListItem Value="0x04">CMD_ADD_STYLE</asp:ListItem>
            <asp:ListItem Value="0x07">CMD_ADD_USER_SITE</asp:ListItem>
            <asp:ListItem Value="0x08">CMD_ADD_LANGUAGE</asp:ListItem>
            <asp:ListItem Value="0x0B">CMD_SET_USER_STYLE</asp:ListItem>
            <asp:ListItem Value="0x06">CMD_TRANSFORM_FS</asp:ListItem>
            <asp:ListItem Value="0x0E">CMD_DEL_USER_LANGUAGE</asp:ListItem>
            <asp:ListItem Value="0x0F">CMD_DEL_USER_FILES</asp:ListItem>
            <asp:ListItem Value="0x10">CMD_GET_FILE_LIST</asp:ListItem>
            <asp:ListItem Value="0x11">CMD_DEL_USER_SITE</asp:ListItem>
            <asp:ListItem Value="0x12">CMD_GET_FILE_STREAM</asp:ListItem>
            <asp:ListItem Value="CMD_SITE_LANGUAGE_OPEN">CMD_SITE_LANGUAGE_OPEN</asp:ListItem>
            <asp:ListItem Value="CMD_SITE_LANGUAGE_CLOSE">CMD_SITE_LANGUAGE_CLOSE</asp:ListItem>
            <asp:ListItem Value="0x03">CMD_SEND_FILE</asp:ListItem>
            <asp:ListItem Value="0x0C">CMD_SEND_PICTURE_FILE</asp:ListItem>
            <asp:ListItem Value="0x0D">CMD_SEND_MYSTYLE_FILE</asp:ListItem>
            <asp:ListItem Value="0x13">CMD_EXIST_DIRECTORY</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        Language<asp:TextBox ID="txtLanguage" runat="server">china</asp:TextBox>
        <br />
        Style<asp:TextBox ID="txtStyleId" runat="server">standard</asp:TextBox>
        <br />
        UserId
        <asp:TextBox ID="txtUserId" runat="server">test</asp:TextBox>
        <br />
        optype HTML,CSS,JS,PHP
        <asp:TextBox ID="txtOptype" runat="server">HTML</asp:TextBox>
        <br />
        xslPath
        <asp:TextBox ID="txtXslpath" runat="server">P3010P0310</asp:TextBox>
        <br />
        targetFileName
        <asp:TextBox ID="txtTargetFileName" runat="server">P3010P0310.html</asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SendCommand" runat="server" onclick="SendCommand_Click" 
            Text="Send Command" Width="226px" />
        <br />
    </div>
    </form>
</body>
</html>
