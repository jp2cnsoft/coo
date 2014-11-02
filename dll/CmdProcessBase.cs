using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Resources;
using System.Reflection;
using Seika.Common.Net;
using Seika.Transform.Command.Data;
using Seika.Transform.Command.Enum;
using System.Collections;

namespace Seika.Transform.Command
{
    public abstract class CmdProcessBase
    {
        //public NetStream ns = null;

        static ResourceManager msg = new ResourceManager("MessageResource", Assembly.GetExecutingAssembly());

        protected static String FTP_Host = ConfigurationSettings.AppSettings["FTP_Host"];
        protected static String FTP_Port = ConfigurationSettings.AppSettings["FTP_Port"];
        protected static String FTP_Username = ConfigurationSettings.AppSettings["FTP_Username"];
        protected static String FTP_Password = ConfigurationSettings.AppSettings["FTP_Password"];
        protected static String FTP_BaseDir = ConfigurationSettings.AppSettings["FTP_BaseDir"];

        public static String COMMON_FILE_DIR = "style/common";

        public abstract CMD_RESULT Execute(CooCommand cmd, CommandParam args);

        public static String STYLE_DIR { get { return System.AppDomain.CurrentDomain.BaseDirectory + "style";} }

        public byte[] Data { get; set; }

        public CmdProcessBase()
        {
            Recvice = new CommandData();
        }

        public String GetMessage(String messageId)
        {
            return messageId + ":" + msg.GetString(messageId);
        }

        public static String GetTmpFileName(String ext)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + ext;
        }

        public void MakeDirectory(String path)
        {
            String[] dirs = path.Replace('/', '\\').Split('\\');
            if (dirs.Length > 0)
            {
                String theDir = dirs[0];
                for (int i = 1; i < dirs.Length; i++)
                {
                    theDir += "\\" + dirs[i];
                    if (!Directory.Exists(theDir))
                    {
                        Directory.CreateDirectory(theDir);
                    }
                }
            }
        }

        public static String Success = "!OK";

        protected FtpMethods FtpServer = new FtpMethods(FTP_Host, FTP_Port, FTP_Username, FTP_Password);

        /// <summary>
        /// 上传本地目录
        /// </summary>
        /// <param name="localPath">本地路径</param>
        /// <param name="userFtpDir">FTP服务器上传路径</param>
        protected void CopyDir2UserSite(String localPath, String userFtpDir, bool checkExist)
        {
            String[] files = Directory.GetFiles(localPath + "\\");

            for (int i = 0; i < files.Length; i++)
            {
                String filename = System.IO.Path.GetFileName(files[i]);
                FtpServer.PutFile(files[i], userFtpDir + "/" + filename);
            }

            String[] dirs = Directory.GetDirectories(localPath);

            for (int i = 0; i < dirs.Length; i++)
            {
                String dirname = dirs[i].Substring(dirs[i].LastIndexOf('\\') + 1);
                FtpServer.MakeDirectory(userFtpDir + "/" + dirname);

                String subLocalPath = localPath + "\\" + dirname;
                String subFtpPath = userFtpDir + "/" + dirname;

                CopyDir2UserSite(subLocalPath, subFtpPath, checkExist);
            }
        }

        protected String GetTmpPath()
        {
            String dir = System.AppDomain.CurrentDomain.BaseDirectory + @"\Temp";
            if (!Directory.Exists(dir)) MakeDirectory(dir);
            return dir;
        }

        protected String GetAccessPath(String userId, String language, String subpath)
        {
            return GetAccessPath(userId, language, SITE_TYPE.NONE, subpath);
        }

        protected String GetAccessPath(String userId, String language, SITE_TYPE typ, String subpath)
        {
            String path = System.Configuration.ConfigurationSettings.AppSettings["FTP_BaseDir"]; 

            if (!String.IsNullOrEmpty(userId))
            {
                path += "/" + userId;
            }

            if (!String.IsNullOrEmpty(language))
            {
                path += "/" + language;
            }

            switch (typ)
            {
                case SITE_TYPE.PC:
                    path += "/pc";
                    break;
                case SITE_TYPE.WAP:
                    path += "/wap";
                    break;
            }

            if (!String.IsNullOrEmpty(subpath))
            {
                path += "/" + subpath;
            }
            return path;
        }

        protected String GetLocalAccessPath(String language, String style, String subpath)
        {
            String path = STYLE_DIR;

            if (!String.IsNullOrEmpty(language))
            {
                path += "/" + language;
            }
            if (!String.IsNullOrEmpty(style))
            {
                path += "/" + style;
            }

            path += "/" + subpath;
            return path;
        }

        public Hashtable ParamFileTable { get; set; }

        public CommandData Recvice { get; set; }

        public Stream GetParamFileStream(String filename)
        {
            return (Stream)ParamFileTable[filename];
        }
        public void AddParamFileStream(String filename, Stream sm)
        {
            this.ParamFileTable.Add(filename, sm);
        }
    }
}


