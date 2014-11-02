using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Xml.XPath;

namespace Seika 
{
    public class StringFilter
    {
        /// <summary>
        /// 特殊字符处理
        /// </summary>
        /// <param name="strChar"></param>
        /// <returns></returns>
        public String ReplaceBadChar(string strChar)
        {
            //if (!String.IsNullOrEmpty(strChar))
            //{
            //    strChar = strChar.Replace("'", ToSBC("'"));
            //    strChar = strChar.Replace("*", ToSBC("*"));
            //    strChar = strChar.Replace("?", ToSBC("?"));
            //    strChar = strChar.Replace("<", ToSBC("<"));
            //    strChar = strChar.Replace("=", ToSBC("="));
            return strChar;
            //}
            //return String.Empty;
        }
        //半角转全角
        public String ToSBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        } 
        //tab键转换成相应空格
        private String ToTabKey(String input) 
        {
            return input.Replace(@" ",@"&nbsp;");
        }
    }
}