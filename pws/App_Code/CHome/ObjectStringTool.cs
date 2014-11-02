 using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Seika;

/// <summary>
/// 系统常用处理工具
/// </summary>
namespace Seika.COO.Web.PG
{
    public class ObjectStringTool
    {
        StringFilter stringFilter = new StringFilter();

        public ObjectStringTool(){}
        /// <summary>
        /// 检查字符真实占位数
        /// </summary>
        /// <param name="str">检查的字符</param>
        /// <param name="count">符合的数值</param>
        /// <returns></returns>
        public bool CheckTextCount(string str, int count)
        {
            int retCount = 0;
            retCount = GetStringLen(str); 
            if (retCount == 0) return true;
            if (retCount > count)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得字符真实长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int GetStringLen(string str) 
        {
            //int retCount = 0;
            //for (int i = 0; i < str.Length; i++)
            //{
            //    int tempCount = UTF8Encoding.GetEncoding("utf-8").GetBytes(str[i].ToString()).Length;
            //    switch (tempCount)
            //    {
            //        case 1:
            //            retCount++;
            //            break;
            //        case 2:
            //            retCount = retCount + 2;
            //            break;
            //        case 3:
            //            retCount = retCount + 2;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //return retCount;
            return str.Length;
        }

        /// <summary>
        /// 替换成标准日期格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public string GetStandardDate(string date)
        {
            string line = "/";
            StringBuilder _str = new StringBuilder();
            if (!string.IsNullOrEmpty(date)) 
            {
                DateTime dDate = Convert.ToDateTime(date);
                _str.Append(dDate.Year.ToString());
                _str.Append(line);
                _str.Append(SetStringZero(dDate.Month.ToString(), 2));
                _str.Append(line);
                _str.Append(SetStringZero(dDate.Day.ToString(), 2));
            }
            return _str.ToString() ;
        }

        /// <summary>
        /// 替成标准日时格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public string GetStandardDateTime(string datetime)
        {
            string line = "/";
            string timeline = ":";
            StringBuilder _str = new StringBuilder();
            if (!string.IsNullOrEmpty(datetime))
            {
                DateTime dDatetime = Convert.ToDateTime(datetime);
                _str.Append(dDatetime.Year.ToString());
                _str.Append(line);
                _str.Append(SetStringZero(dDatetime.Month.ToString(), 2));
                _str.Append(line);
                _str.Append(SetStringZero(dDatetime.Day.ToString(), 2));
                _str.Append(" ");
                _str.Append(SetStringZero(dDatetime.Hour.ToString(), 2));
                _str.Append(timeline);
                _str.Append(SetStringZero(dDatetime.Minute.ToString(), 2));
                _str.Append(timeline);
                _str.Append(SetStringZero(dDatetime.Second.ToString(), 2));
            }
            return _str.ToString();
        }

        /// <summary>
        /// 对字符进行前置０操作
        /// </summary>
        /// <returns></returns>
        public string SetStringZero(string str, int len)
        {
            if (!string.IsNullOrEmpty(str))
            {
                while(str.Length < len)
                {
                     str = "0" + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 取得指定字串的日期格式字符
        /// </summary>
        /// <param name="datetime">日期字串</param>
        /// <returns></returns>
        public string GetDataTime(string datetime)
        {
            if (string.IsNullOrEmpty(datetime))
            {
                return null;
            }
            ArrayList al = new ArrayList();
            if (!string.IsNullOrEmpty(datetime))
            {
                for (int i = 0; i < datetime.Length; i++)
                {
                    al.Add(datetime[i].ToString());
                    if (i > 2)
                    {
                        if ((i + 1) % 2 == 0 && i != (datetime.Length - 1))
                        {
                            al.Add("/");
                        }
                    }
                }
            }
            return ArrayListToString(al);
        }

        /// <summary>
        /// 取得指定字串的日期格式字符
        /// </summary>
        /// <param name="dateheader">日期前缀</param>
        /// <param name="datetime">日期字串</param>
        /// <returns></returns>
        public string GetDataTime(string dateheader, string datetime)
        {
            if (string.IsNullOrEmpty(datetime))
            {
                return null;
            }
            ArrayList al = new ArrayList();
            if (!string.IsNullOrEmpty(datetime))
            {
                for (int i = 0; i < datetime.Length; i++)
                {
                    al.Add(datetime[i].ToString());
                    if ((i + 1) % 2 == 0 && i != (datetime.Length - 1))
                    {
                        al.Add("-");
                    }
                }
            }
            return dateheader + ArrayListToString(al);
        }

        /// <summary>
        /// 格式化长字符串
        /// </summary>
        /// <returns></returns>
        public string FormatLongString(string str,int len) 
        {
            string fstr = "..";
            if (!string.IsNullOrEmpty(str)) 
            {
                if (str.Length > len)
                {
                    string st = str.Substring(0, len) + fstr;
                    return st;
                }
                else 
                {
                    return str;
                }
            }
            return null;
        }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        /// <param name="Drop"></param>
        /// <param name="sr"></param>
        public void DropDownListSelect(Object Drop, string sr)
        {
            DropDownList dl = (DropDownList)Drop;

            for (int i = 0; i < dl.Items.Count; i++)
            {
                if (dl.Items[i].Value == sr)
                {
                    dl.Items[i].Selected = true;
                }
            }
        }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        /// <param name="Drop"></param>
        /// <param name="sr"></param>
        public void DropDownListTextSelect(Object Drop, string sr)
        {
            DropDownList dl = (DropDownList)Drop;

            for (int i = 0; i < dl.Items.Count; i++)
            {
                if (dl.Items[i].Text == sr)
                {
                    dl.Items[i].Selected = true;
                }
            }
        }


        /// <summary>
        /// 数组转换字符串
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public String ArrayListToString(ArrayList al)
        {
            StringBuilder sb = new StringBuilder();
            if (al != null)
            {
                for (int j = 0; j < al.Count; j++)
                {
                    sb.Append(al[j]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 数组转换字符串
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public String ArrayListToString(ArrayList al, String separator)
        {
            StringBuilder sb = new StringBuilder();
            int alen = al.Count;
            if (al == null || alen < 1) return "";
            for (int j = 0; j < alen; j++)
            {
                if (j == (alen - 1))
                {
                    sb.Append(al[j]);
                }
                else
                {
                    sb.Append(al[j] + separator);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 数组转换字符串
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public ArrayList StringToArrayList(String str, String separator)
        {
            ArrayList al = new ArrayList();
            if (String.IsNullOrEmpty(str)) return al;
            String[] astr = str.Split(separator.ToCharArray());
            int slen = astr.Length;
            for (int j = 0; j < slen; j++)
            {
                al.Add(astr[j]);
            }
            return al;
        }
        /// <summary>
        /// 字符串转数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int stringToInt(String str) {
            int temp = 0;
            temp = Convert.ToInt32(str);
            return temp;
        }
        /// <summary>
        /// 字符串转数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns></returns>
        public ArrayList StringToArrayList(string str, int len) 
        {
            ArrayList array = new ArrayList();
            if (!string.IsNullOrEmpty(str)) 
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i == 0 || i % len == 0)
                    {
                        array.Add(str.Substring(i,  len));
                    }
                }
            }
            return array;
        }

        //算ID
        public String GetNextSeqNumber()
        {
            Random rnd = new Random();
            return System.DateTime.Now.ToString("yyMMddHHmmssfff") + rnd.Next(0, 10000).ToString();
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public string RandomNum() 
        {
            Random dom = new Random();
            return dom.Next(1000, 9999).ToString();
        }

        /// <summary>
        /// 生成随机字母数字字符串
        /// </summary>
        /// <returns></returns>
        public string GenerateRandCode(int len)
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            return checkCode;
        }

        /// <summary>
        /// 格式化换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Format2HtmlString(string str) 
        {
            //return str.Replace("\r\n", "<br/>").Replace(" ","&nbsp;");
            return str.Replace("\r\n", "<br/>");
        }

        /// <summary>
        /// 格式化换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string HtmlString2Format(string str)
        {
            //return str.Replace("<br/>", "\r\n").Replace("&nbsp;", " ");
            return str.Replace("<br/>", "\r\n");
        }

        /// <summary>
        /// 超过一定长度字符用...表示
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetOverrideString(string str, int num)
        {
            string str1;
            if (str.Length > num)
            {
                str1 = str.Substring(0, num) + "...";
            }
            else
            {
                str1 = str;
            }
            return str1;
        }


        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string SubString(string str,int len) 
        {
            if (!string.IsNullOrEmpty(str) && str.Length>len) 
            {
                return str.Substring(len, str.Length - len);
            }
            return "";
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>sDate大于dDate返回true; 否则返回false</returns>
        public bool DateCompare(DateTime sDate,DateTime dDate) 
        {
            int res = DateTime.Compare(sDate, dDate);
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得checkbox选择的value字符串
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string CheckBoxGetString(CheckBoxList c) 
        {
            string str = "";
            for (int i = 0; i < c.Items.Count; i++)
            {
                if (c.Items[i].Selected == true)
                {
                    str += c.Items[i].Value.Trim();
                }
            }
            return str;
        }
        ///
        ///checkboxlist初使化
        ///
        ///
        public void CheckBoxListSelect(CheckBoxList cbl,String value) 
        {
            for (int i = 0; i < cbl.Items.Count; i++)  //遍历复选框组
            {
                if (cbl.Items[i].Value.Equals( value))
                {
                    cbl.Items[i].Selected = true; //选中复选框
                }
            }
        }

        

        /// <summary>
        /// checkbox初始化
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        public void CheckBoxInit(CheckBoxList c, string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int id = Convert.ToInt16(str[i].ToString()) - 1;
                c.Items[id].Selected = true;
            }
        }

        /// <summary>
        /// 字符串转CDATA格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String String2CDATAString(String str) 
        {
            if (String.IsNullOrEmpty(str)) 
            {
                return String.Empty;
            }
            //所有字符串转换成大写
            str = stringFilter.ReplaceBadChar(str);
            int cdbLen;
            int cdeLen;
            while ((cdbLen = str.IndexOf("<![CDATA[")) != -1 || (cdeLen = str.IndexOf("]]>")) != -1) 
            {
                str = str.Replace("<![CDATA[", stringFilter.ToSBC("<![CDATA[")).Replace("]]>", stringFilter.ToSBC("]]>"));
            }
            return String.Concat(HttpUtility.HtmlDecode("<![CDATA["), HttpUtility.HtmlDecode(Format2HtmlString(str)), HttpUtility.HtmlDecode("]]>"));
        }
        /// <summary>
        /// HTML字符转换CDATA格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String HtmlString2CDATAString(String str) 
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }
            int cdbLen;
            int cdeLen;
            while ((cdbLen = str.IndexOf("<![CDATA[")) != -1 || (cdeLen = str.IndexOf("]]>")) != -1)
            {
                str = str.Replace("<![CDATA[", stringFilter.ToSBC("<![CDATA[")).Replace("]]>", stringFilter.ToSBC("]]>"));
            }
            return String.Concat(HttpUtility.HtmlDecode("<![CDATA["), str, HttpUtility.HtmlDecode("]]>"));
        }

        /// <summary>
        /// CDATA格式字符串转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String CDATAString2String(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }
            String fStr = HtmlString2Format(str);
            fStr = fStr.Replace(@"<![CDATA[", stringFilter.ToSBC("<![CDATA[")).Replace(@"]]>", stringFilter.ToSBC("]]>"));
            return fStr;
        }

        /// <summary>
        /// CDATA格式字符串转HTML字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String CDATAString2HtmlString(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }
            return str.Replace(@"<![CDATA[", stringFilter.ToSBC("<![CDATA[")).Replace(@"]]>", stringFilter.ToSBC("]]>"));
        }

        /// <summary>
        /// 特殊字符串格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String StringToFilter(String str)
        {
            return stringFilter.ReplaceBadChar(str);
        }

        /// <summary>
        /// 字符串金钱型转带分隔符金钱型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String StringToMoney(String str) 
        {
            if (String.IsNullOrEmpty(str)) return "";
            int len = 3;
            String c = @",";
            int strLen = str.Length;
            String rdstr = String.Empty;
            int l = str.IndexOf('.');
            if (l != -1)
            {
                rdstr = str.Substring(l, strLen - l);
                str = str.Substring(0, l);
            }
            int rstrLen = str.Length / len + str.Length + (str.Length % len == 0 ? -1 : 0);
            String[] rstr = new String[rstrLen];
            rstrLen--;
            int il = 1;
            for (int i = str.Length - 1; i >= 0; i--) 
            {
                rstr[rstrLen] = Convert.ToString(str[i]);
                rstrLen--;
                if (rstrLen == -1) break;
                if (i != str.Length - 1 && il % len == 0)
                { 
                    rstr[rstrLen] = c; 
                    rstrLen--;
                }
                il++;
            }
            StringBuilder _sb = new StringBuilder();
            for (int i = 0; i < rstr.Length; i++)
                _sb.Append(rstr[i].ToString());
            return _sb.ToString() + rdstr;
        }
        /// <summary>
        /// 去掉金钱分隔符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String MoneyClearChar(String str) 
        {
            return str.Replace(@",", "");
        }
        
        /// <summary>
        /// 文本框可以输入tab键
        /// </summary>
        /// <param name="tb"></param>
        public void EnableTabType(TextBox tb)
        {
            tb.Attributes.Add("onkeydown",
            "if(event.which || event.keyCode){if ((event.which == 9)" +
            "|| (event.keyCode == 9)) {document.getElementById('" +
            tb.ClientID + "').selection = document.selection.createRange();" +
            tb.ClientID + ".selection.text = String.fromCharCode(9);return false;}} else {return true}; ");
        }
        /// <summary>
        /// 字符串是否由数字组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool stringToNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 去除html标记
        /// </summary>
        /// <param name="html"></param>
        /// <param name="allowHarmlessTags"></param>
        /// <returns></returns>
        public String StripHtml(String html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;

            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);

            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }
     }
}
