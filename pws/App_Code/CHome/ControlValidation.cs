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

/// <summary>
/// 控件验证
/// </summary>
/// 
namespace Seika.COO.Web.PG
{
    public class ControlValidation
    {
        public ControlValidation(){}
        
        /// <summary>
        /// 日期验证 格式为 2006/06/12
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsBirthdate(String strNumber)
        {
            string samll = strNumber.Substring(0, 4);
            
            if (Convert.ToInt16(samll) < 1900 || Convert.ToInt16(samll) > 2037)
            {
                return false;
            }
                Regex objNotWholePattern = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})/(0[13578]|1[02])/(0[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})/(0[13456789]|1[012])/(0[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})/02/(0[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))/02/29))$");
            
            return objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 手机号验证
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsMobilePhone(String strNumber)
        {
            Regex objNotWholePattern = new Regex("^[0-9]{1,}$");
            return objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 邮编验证
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsPostalCode(String strNumber)
        {
            Regex objNotWholePattern = new Regex("^([1-9]\\d{5})|([1-9-]\\d{6,11})$");
            return objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Email验证
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsEmail(String strNumber)
        {
            Regex objNotWholePattern = new Regex("^([A-Z0-9a-z_]+[-|\\.]?)+[A-Z0-9a-z_]@([A-Z0-9a-z]+(-[A-Z0-9a-z]+)?\\.)+[A-Za-z]{2,}$");
            return objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 金钱验证
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsMoney(String str)
        {
            //数字及按三位分的逗号
            Regex objNotWholePattern = new Regex("^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$");
            Regex objNotWholePattern_2 = new Regex("^[0-9]+(.[0-9]{1,2})?$");
            return (objNotWholePattern.IsMatch(str) || objNotWholePattern_2.IsMatch(str));
        }

        /// <summary>
        /// 判断日期型 格式:20080102
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsDate(String str)
        {
            if (!ISNumber(str)) return false;
            if (str.Length < 6 || str.Length > 8 ) return false;
            string year = str.Substring(0, 4);
            if (Convert.ToInt16(year) < 1900 || Convert.ToInt16(year) > 2037)
            {
                return false;
            }
            Regex objNotWholePattern = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})(0[13578]|1[02])(0[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})(0[13456789]|1[012])(0[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})02(0[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))0229))$");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 判断自然数值型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsNaturalNumber(String strNumber, int Num)
        {
            if (strNumber.Length > Num)
                return false;
            string str = @"^\+?[1-9][0-9]*$";
            Regex objNotWholePattern = new Regex(str);                                                
            return objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 只允许字母或数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ISLatterOrNumber(String str)
        {
            Regex objNotWholePattern = new Regex("^[0-9a-zA-Z]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }
        /// <summary>
        /// 只允数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ISNumber(String str)
        {
            Regex objNotWholePattern = new Regex("^[0-9]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 只允许字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ISLatter(String str)
        {
            Regex objNotWholePattern = new Regex("^[a-zA-Z]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 只允许字母和空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ISLatterSpace(String str)
        {
            Regex objNotWholePattern = new Regex("^[a-zA-Z\\s().]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        public bool ISNum(String str)
        {
            Regex objNotWholePattern = new Regex("^[0-9]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        

        public bool ISChinese(String str)
        {
            Regex objNotWholePattern = new Regex("^[\u4e00-\u9fa5()\\s]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 只允许英文数字及下划线
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsLatterNumberLine(String str)
        {
            Regex objNotWholePattern = new Regex("^[0-9a-zA-Z-]{1,}$");
            return objNotWholePattern.IsMatch(str);
        }

        public bool IsStringOrNumBer(string str)
        {
            Regex objNotWholePattern = new Regex("^([A-Z0-9a-z]+[_|-|\\.]?){1,18}$");
            return objNotWholePattern.IsMatch(str);
        }

        public bool IsNumeric(string str)
        {
            if (str == "0")
                return false;
            Regex objNotWholePattern = new Regex("^(-?[0-9]*[.]*[0-9]{0,3})$");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 主页地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsHttp(string str)
        {
            Regex objNotWholePattern = new Regex(@"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return objNotWholePattern.IsMatch(str);
        }

        /// <summary>
        /// 域名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsDomain(string str) 
        {
            Regex objNotWholePattern = new Regex(@"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$");
            return objNotWholePattern.IsMatch(str);
        }

        public bool IsICP(string str)
        {
            Regex objNotWholePattern = new Regex(@"^[京|浙|津|皖|沪|闽|渝|赣|港|鲁|澳|豫|内蒙古|鄂|新|湘|宁|粤|藏|海口|桂|川|蜀|冀|贵|黔|晋|云|滇|陕|秦|吉|甘|陇|黑|青|苏|台|辽]+ICP备[\d]{8}号$");
            return objNotWholePattern.IsMatch(str);
        }
    }
}
