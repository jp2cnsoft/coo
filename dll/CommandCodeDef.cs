using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Command
{
    /// <summary>
    /// 命令CODE定义（值必须为小写）
    /// </summary>
    public class CommandCodeDef
    {
        public const String Transform = "transform";
        public const String Copy = "copy";
        public const String Move = "move";
        public const String Rename = "rename";
        public const String Del = "del";
        public const String PutFile = "putfile";
        public const String GetFile = "getfile";
        public const String PubStyle = "pubstyle";
        public const String Login = "login";
        public const String NewSite = "newsite";
        public const String AddSiteLang = "addsitelang";
        public const String UpCommonFile = "upcommonfile";
        
    }
}
