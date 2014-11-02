using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Seika.Transform.Command.Data;

namespace Seika.Transform.Command.Data
{
    public class CommandParam : CmdSendData
    {
        public const String PRM_FILE_NAME = "filename";
        public const String PRM_DIR_NAME = "dirname";
        public const String PRM_SEND_TO_TARGER = "sendtotarger";
        public const String PRM_FILE_SIZE = "filesize";
        public const String PRM_USERID = "userid";
        public const String PRM_PASSWD = "passwd";
        public const String PRM_LANGUAGE = "language";
        public const String PRM_SITETYPE = "sitetype";
        public const String PRM_STYLE = "style";
        public const String PRM_PATH = "path";
        public const String PRM_DATA_SIZE = "datasize";
        public const String PRM_POSITION = "position";
        public const String PRM_BACKUPID = "backupid";
        public const String PRM_DOMAIN = "domain";
        
        

        public static String RESULT_OK = "OK";
        public static String RESULT_NG = "NG";
        public static String RESULT_WA = "WA";

        //操作类型(html,js,css)
        public const String PRM_OPTYPE = "optype";
        //生成文件后缀名(.html,.js,.css)
        public const String PRM_OPTYPEEXT = "opext";

        public const char DIV = '&';
        private Hashtable args = new Hashtable();

        public String this[string name]
        {
            get { return (String)args[name]; }
            set { args[name] = value; }
        }

        public String LogMessage()
        {
            StringBuilder cmdBuf = new StringBuilder();
            IEnumerator enumNames = args.Keys.GetEnumerator();

            int count = 0;
            while (enumNames.MoveNext())
            {
                String name = (String)enumNames.Current;
                cmdBuf.Append(name);
                cmdBuf.Append("\t= ");
                cmdBuf.Append(args[name]);
                cmdBuf.Append("\n");
            }

            return cmdBuf.ToString();
        }

        /// <summary>
        /// 命令字符串转换为命令对象
        /// </summary>
        /// <param name="cmdStr"></param>
        public override void Import(String param)
        {
            char[] div = { DIV };
            String[] nameValues = param.Split(div);

            for (int i = 0; i < nameValues.Length; i++)
            {
                String nameValue = nameValues[i];

                String name = nameValue.Substring(0, nameValue.IndexOf('=')).Trim();
                String value = nameValue.Substring(nameValue.IndexOf('=') + 1).Trim();

                args[name] = value;

                Console.WriteLine(name + "=" + value);
            }
        }

        /// <summary>
        /// 命令对象转换为字符串
        /// </summary>
        /// <returns></returns>
        public override String Export()
        {
            StringBuilder cmdBuf = new StringBuilder();
            IEnumerator enumNames = args.Keys.GetEnumerator();

            int count = 0;
            while (enumNames.MoveNext())
            {
                if (count++ > 0) cmdBuf.Append("&");
                String name = (String)enumNames.Current;
                cmdBuf.Append(name);
                cmdBuf.Append("=");
                cmdBuf.Append(args[name]);
            }           

            return cmdBuf.ToString();
        }
    }

}
