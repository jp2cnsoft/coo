using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Sockets;
using Seika.CooException;
using System.IO;
using Seika.Transform.Command.Data;
using Seika.Common.Net;
using Seika.Transform.Exception;
using Seika.Transform.Command.Enum;

namespace Seika.Transform.Command.Client
{
    public class CommandSender : CommandSenderBase
    {
        public CommandSender() : base()
        {
        }

        /// <summary>
        /// 发送命令执行时所需要的文件附件
        /// </summary>
        /// <param name="s">文件流</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public void SendFile(Stream s, String filename, COMMAND_POSITION to)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_SEND_TO_TARGER] = ((byte)to).ToString();
            args[CommandParam.PRM_FILE_NAME] = filename;
            args[CommandParam.PRM_FILE_SIZE] = s.Length.ToString();

            SendCommand(CMD_IDS.CMD_SEND_FILE, args, filename, s);
        }

        /// <summary>
        /// 上传文件到用户站点 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="userId"></param>
        /// <param name="upPath"></param>
        public void SendFileToUserSite(Stream s, String userId, String upPath)
        {
            SendFileToUserSite(s, userId, "", upPath);
        }

        public void SendFileToUserSite(Stream s, String userId, String language, String path)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_SEND_TO_TARGER] = ((byte)COMMAND_POSITION.USERSITE).ToString();
            args[CommandParam.PRM_FILE_NAME] = path;
            args[CommandParam.PRM_FILE_SIZE] = s.Length.ToString();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;

            SendCommand(CMD_IDS.CMD_SEND_FILE, args, path, s);
        }

        /// <summary>
        /// 发送图片到用户站点的指定语言目录下
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="language"></param>
        /// <param name="s"></param>
        /// <param name="upPath"></param>
        /// <param name="isThumbnail"></param>
        public void SendPictureToUserSite(String userId, String language, Stream s, String filename, bool isThumbnail)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_FILE_NAME] = filename;
            args[CommandParam.PRM_SEND_TO_TARGER] = ((byte)COMMAND_POSITION.USERSITE).ToString();
            args["ISTHUMBNAIL"] = (isThumbnail ? "1" : "0");

            SendCommand(CMD_IDS.CMD_SEND_PICTURE_FILE, args, filename, s);
        }

        /// <summary>
        /// 送自定义风格文件到该用户目录下。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="language"></param>
        /// <param name="s"></param>
        /// <param name="fileName"></param>
        public void SendMyStyleFileToUserSite(String userId, String language, Stream s, String fileName)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_FILE_NAME] = fileName;
            args[CommandParam.PRM_SEND_TO_TARGER] = ((byte)COMMAND_POSITION.USERSITE).ToString();

            SendCommand(CMD_IDS.CMD_SEND_MYSTYLE_FILE, args, fileName, s);
        }

        /// <summary>
        /// 验证用户目录是否存在
        /// </summary>
        /// <param name="userId">用户ID</param>
        public bool ExistUser(String userId)
        {
            CommandParam args = new CommandParam();

            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = "";
            args[CommandParam.PRM_DIR_NAME] = "";

            SendCommand(CMD_IDS.CMD_EXIST_DIRECT, args);

            return (this.Result.RecordCd == CMD_RESULT.SUCCESS);
        }

        /// <summary>
        /// 追加指定语言的新风格所需ZIP资源
        /// </summary>
        /// <param name="lang">语言ID</param>
        /// <param name="styleId">风格ID</param>
        /// <param name="sm">风格打包ZIP文件名</param>
        public void AddStyle(Stream sm)
        {
            String filename = GetTmpFileName("zip");
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_FILE_NAME] = filename;

            SendCommand(CMD_IDS.CMD_ADD_STYLE, args, filename, sm);
        }

        /// <summary>
        /// 上传用户网站根目录以下共通文件压缩包
        /// </summary>
        /// <param name="sm"></param>
        public void SetSiteCommonFiles(Stream sm)
        {
            String filename = GetTmpFileName("zip");

            CommandParam args = new CommandParam();
            args[CommandParam.PRM_FILE_NAME] = filename;

            SendCommand(CMD_IDS.CMD_SET_COMMON_FILES, args, filename, sm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="userId"></param>
        /// <param name="language"></param>
        /// <param name="optype"></param>
        /// <param name="opext"></param>
        /// <param name="styleId"></param>
        /// <param name="xslPath"></param>
        /// <param name="targetFilename"></param>
        /// <param name="prevPageUrl"></param>
        /// <param name="nextPageUrl"></param>
        /// <param name="category"></param>
        public void Transform_fs(Stream sm,
                                 String userId,
                                 String language,
                                 FILE_TYPE optype,
                                 String opext,
                                 String styleId,
                                 String xslPath,
                                 String targetFilename,
                                 String prevPageUrl, String nextPageUrl,
                                 String category)
        {
            String xmlFilename = this.GetTmpFileName("xml");

            CommandParam args = new CommandParam();

            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_OPTYPE] = ((byte)optype).ToString(); ;
            args[CommandParam.PRM_OPTYPEEXT] = opext;

            args["STYLEID"] = styleId;
            args["XSL_FILENAME"] = xslPath;
            args["XML_FILENAME"] = xmlFilename;
            args["PERVPAGE"] = prevPageUrl;
            args["NEXTPAGE"] = nextPageUrl;
            args["GROUPCATAGORY"] = category;
            args["TARGER_FILENAME"] = targetFilename;

            SendCommand(CMD_IDS.CMD_TRANSFORM_FS, args, xmlFilename, sm);
        }

        /// <summary>
        /// 更新设置用户指定语言网站的风格
        /// </summary>
        /// <param name="language"></param>
        /// <param name="styleId"></param>
        public void SetUserStyle(String userId, String language, String styleId, bool delMyStyle)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_STYLE] = styleId;
            args[CommandParam.PRM_USERID] = userId;
            args["DELETE_MY_STYLE"] = (delMyStyle ? "1" : "0");
            SendCommand(CMD_IDS.CMD_SET_USER_STYLE, args);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files"></param>
        public void DeleteFiles(String files, bool delDir)
        {
            DeleteUserFiles("", "", files, delDir);
        }

        /// <summary>
        /// 删除指定用户目录下的文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="files"></param>
        public void DeleteUserFiles(String userId, String files, bool delDir)
        {
            DeleteUserFiles(userId, "", files, delDir);
        }

        /// <summary>
        /// 删除用户指定语言下的文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="language"></param>
        /// <param name="files"></param>
        public void DeleteUserFiles(String userId, String language, String files, bool delDir)
        {
            DeleteUserFiles(userId, language, SITE_TYPE.NONE, files, delDir);
        }

        /// <summary>
        /// 删除用户转换文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="language"></param>
        /// <param name="files"></param>
        public void DeleteUserTransFiles(String userId, String language, String files, bool delDir)
        {
            DeleteUserFiles(userId, language, SITE_TYPE.PC, files, delDir);
            DeleteUserFiles(userId, language, SITE_TYPE.WAP, files, delDir);
        }

        public void DeleteUserFiles(String userId, String language, SITE_TYPE typ, String files, bool delDir)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_SITETYPE] = ((byte)typ).ToString();
            args[CommandParam.PRM_FILE_NAME] = files;
            args["DELETE_DIR"] = (delDir ? "1" : "0");
            SendCommand(CMD_IDS.CMD_DEL_USER_FILES, args);
        }

        public String GetTmpFileName(String ext)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + ext;
        }

        /// <summary>
        /// 追加用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        public void AddUser(String userId)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            SendCommand(CMD_IDS.CMD_ADD_USER_SITE, args);
        }

        /// <summary>
        /// 追加用户不同语言站点 ---修改参数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="language">追加语言</param>
        /// <param name="sm">生成php用xml数据流</param>
        public void AddUserLanguage(String userId, String language)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;

            SendCommand(CMD_IDS.CMD_ADD_LANGUAGE, args);
        }

        /// <summary>
        /// 更新设置用户指定语言网站的风格
        /// </summary>
        /// <param name="userId">语言ID</param>
        /// <param name="language">指定语言</param>
        /// <param name="styleId">更新样式ID</param>
        public void SetUserLanguageStyle(String userId, String language, String styleId, bool delMyStyle)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_STYLE] = styleId;
            args["DELETE_MY_STYLE"] = (delMyStyle ? "1" : "0");
            SendCommand(CMD_IDS.CMD_SET_USER_STYLE, args);
        }

        /// <summary>
        /// 关闭当前语言版本网站
        /// </summary>
        /// <param name="userId">语言ID</param>
        /// <param name="language">指定语言</param>
        public void SetUserLanguageClose(String userId, String language) 
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;

            SendCommand(CMD_IDS.CMD_SITE_LANGUAGE_CLOSE, args);
        }

        /// <summary>
        /// 公开当前语言版本网站
        /// </summary>
        /// <param name="userId">语言ID</param>
        /// <param name="language">指定语言</param>
        public void SetUserLanguageOpen(String userId, String language) 
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_USERID] = userId;

            SendCommand(CMD_IDS.CMD_SITE_LANGUAGE_OPEN, args);
        }

        /// <summary>
        /// 删除当前语言版本网站
        /// </summary>
        /// <param name="userId">语言ID</param>
        /// <param name="language">指定语言</param>
        /// <param name="xml_php">生成php用xml数据流</param>
        /// <param name="xml_js">生成js用xml数据流</param>
        public void DeleteUserLanguage(String userId, String language) 
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;

            SendCommand(CMD_IDS.CMD_DEL_USER_LANGUAGE, args);
        }

        public Stream GetFileStream(String userId, String language, String styleId, String path, COMMAND_POSITION pos)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_POSITION] = ((byte)pos).ToString();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_FILE_NAME] = path;
            args[CommandParam.PRM_STYLE] = styleId;

            SendCommand(CMD_IDS.CMD_GET_FILE_STREAM, args);

            return Result.GetResultFileStream(path);
        }

        public FtpFileInfo[] GetFileList(String userId, String language, String path)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_LANGUAGE] = language;
            args[CommandParam.PRM_FILE_NAME] = path;

            SendCommand(CMD_IDS.CMD_GET_FILE_LIST, args);

            if (Result.RecordCd == CMD_RESULT.SUCCESS)
            {
                CmdDataTable tab = new CmdDataTable(new String[] { "Filename", "Size", "CreateDate", "IsDir" });
                tab.Import(Encoding.UTF8.GetString(Result.Data));

                FtpFileInfo[] fs = new FtpFileInfo[tab.Count];

                for (int i = 0; i < tab.Count; i++)
                {
                    CmdDataRow row = tab[i];
                    fs[i] = new FtpFileInfo();

                    fs[i].Filename = row["Filename"].StringValue;
                    fs[i].FileSize = row["Size"].LongValue;
                    fs[i].CreateDate = row["CreateDate"].DateTimeValue;
                    fs[i].IsDir = row["IsDir"].BoolValue;
                }
                
                return fs;
            }
            
            return null;
        }

        public Stream GetDefaultStyleConfigXml(String language, String styleId)
        {
            return GetFileStream("", language, styleId, "style.xml", COMMAND_POSITION.LOCAL);
            //return GetFileStream("", language, styleId, "resources\\mystyle\\headFlash.swf", COMMAND_POSITION.LOCAL);
            
        }

        /// <summary>
        /// 删除用户网站
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void DeleteUserSite(String userId)
        {
            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;

            SendCommand(CMD_IDS.CMD_DEL_USER_SITE, args);
        }
    }
}
