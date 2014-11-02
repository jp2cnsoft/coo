using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Seika.Transform.Command;
using Seika.Transform.Command.Client;
using Seika.Transform.Command.Data;
using Seika.Transform.Exception;
using Seika.Transform.Command.Enum;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SendCommand_Click(object sender, EventArgs e)
    {
        String srvPath = this.FileUpload1.PostedFile.FileName.Substring(this.FileUpload1.PostedFile.FileName.LastIndexOf('\\') + 1);
        CommandSender cs = new CommandSender();
        try
        {
            cs.Open("sender", "123456");
            String cmdtxt = cmdSelect.Text.Substring(2);
            CMD_IDS cmd = (CMD_IDS)int.Parse(cmdtxt, NumberStyles.AllowHexSpecifier);

            switch (cmd)
            {
                case CMD_IDS.CMD_SEND_FILE:
                    cs.SendFile(this.FileUpload1.PostedFile.InputStream, srvPath, COMMAND_POSITION.LOCAL);
                    break;
                case CMD_IDS.CMD_ADD_STYLE:
                    cs.AddStyle(this.FileUpload1.PostedFile.InputStream);
                    break;
                case CMD_IDS.CMD_TRANSFORM_FS:

                    cs.Transform_fs(
                        this.FileUpload1.PostedFile.InputStream,
                        txtUserId.Text,
                        txtLanguage.Text,
                        FILE_TYPE.HTML,
                        "",
                        txtStyleId.Text,
                        txtXslpath.Text + ".xsl",
                        txtTargetFileName.Text,
                        "", "", ""
                        );
                    break;
                case CMD_IDS.CMD_ADD_LANGUAGE:
                    cs.AddUserLanguage(txtUserId.Text, txtLanguage.Text);
                    break;
                case CMD_IDS.CMD_SITE_LANGUAGE_OPEN:
                    cs.SetUserLanguageOpen(txtUserId.Text, txtLanguage.Text);
                    break;
                case CMD_IDS.CMD_SITE_LANGUAGE_CLOSE:
                    cs.SetUserLanguageClose(txtUserId.Text, txtLanguage.Text);
                    break;
                case CMD_IDS.CMD_DEL_USER_LANGUAGE:
                    cs.DeleteUserLanguage(txtUserId.Text, txtLanguage.Text);
                    break;
                case CMD_IDS.CMD_SET_COMMON_FILES:
                    cs.SetSiteCommonFiles(this.FileUpload1.PostedFile.InputStream);
                    break;
                case CMD_IDS.CMD_SET_USER_STYLE:
                    cs.SetUserStyle(txtUserId.Text, txtLanguage.Text, txtStyleId.Text, true);
                    break;
                case CMD_IDS.CMD_ADD_USER_SITE:
                    cs.AddUser(this.txtUserId.Text);
                    break;
                case CMD_IDS.CMD_DEL_USER_FILES:
                    cs.DeleteUserFiles(this.txtUserId.Text, txtLanguage.Text, SITE_TYPE.PC, txtTargetFileName.Text, false);
                    break;
                case CMD_IDS.CMD_GET_FILE_LIST:
                    cs.GetFileList(this.txtUserId.Text, txtLanguage.Text, "*");
                    break;
                case CMD_IDS.CMD_DEL_USER_SITE:
                    cs.DeleteUserSite(this.txtUserId.Text);
                    break;
                case CMD_IDS.CMD_GET_FILE_STREAM:
                    cs.GetDefaultStyleConfigXml(txtLanguage.Text, txtStyleId.Text);
                    break;
                case CMD_IDS.CMD_SEND_PICTURE_FILE:
                    cs.SendPictureToUserSite(txtUserId.Text, txtLanguage.Text, FileUpload1.PostedFile.InputStream, txtTargetFileName.Text, true);
                    break;
                case CMD_IDS.CMD_SEND_MYSTYLE_FILE:
                    //cs.SendMyStyleFileToUserSite(txtUserId.Text, txtLanguage.Text, FileUpload1.PostedFile.InputStream, txtTargetFileName.Text);
                    cs.SendFileToUserSite(FileUpload1.PostedFile.InputStream, txtUserId.Text, txtTargetFileName.Text);
                    break;
                case CMD_IDS.CMD_EXIST_DIRECT:
                    cs.ExistUser(txtUserId.Text);
                    break;
                default:
                    break;
            }
            cs.Close();
        }
        catch (TransformException te)
        {
            labMessage.Text = te.MessageId + ":" + te.Message;
        }
    }
}
