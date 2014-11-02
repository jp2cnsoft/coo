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
using System.Collections.Generic;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;

/// <summary>
/// PageBase の概要の説明です
/// </summary>
namespace Seika.COO.Web.PG
{
    public class BaseFunction : System.Web.UI.Page
    {
        //基本操作数据
        public String m_common_type = CodeSymbol.m_common_type;

        FileTools m_fileTools = new FileTools();
        
        public void SetControlColor(Control col)
        {
            String controlName = col.GetType().Name.ToString();
            switch (controlName)
            {
                case "TextBox":
                    ((TextBox)col).BackColor = CodeSymbol.color_err;
                    break;
                case "DropDownList":
                    ((DropDownList)col).BackColor = CodeSymbol.color_err;
                    break;
                case "Label":
                    ((Label)col).BackColor = CodeSymbol.color_err;
                    break;
            }
        }

        public void UnSetControlColor(Control col)
        {
            UnSetControlColor(col, System.Drawing.Color.LightYellow);
        }

        public void UnSetControlColor(Control col, System.Drawing.Color color)
        {
            String controlName = col.GetType().Name.ToString();
            switch (controlName)
            {
                case "TextBox":
                    ((TextBox)col).BackColor = color;
                    break;
                case "DropDownList":
                    ((DropDownList)col).BackColor = color;
                    break;
                case "Label":
                    ((Label)col).BackColor = color;
                    break;
            }
        }

        /// <summary>
        /// 错误信息输出到页面
        /// </summary>
        /// <param name="em"></param>
        public void SendAppMsg(ExceptionMess em)
        {
            //过滤重复出错信息
            AppMsgDoFilter(ref em);

            for (int i = 0; i < em.GetData.Count; i++)
            {
                //字符串集合
                StringBuilder _sb = new StringBuilder();
                _sb.Append("<br /><br /><font color=\"red\">" + GetGlobalResourceObject("GPR", "ERRORMESSAGE") + "</font><br /><br /><br /><br />");
                //当前出错集合信息
                for (int j = 0; j < em.GetData[i].Count; j++)
                {
                    _sb.Append("<img src=\"../../Images/dian.png\" />");
                    _sb.Append(em.GetData[i].GetAppException[j].AppMessage.ToString());
                    _sb.Append("<br />");
                    _sb.Append("<br />");
                }
                Label l = (Label)em.GetControl[i];
                l.Text = _sb.ToString();
                if(_sb.Length > 0)
                    l.Visible = true;
            }
        }

        /// <summary>
        /// 错误信息输出到页面
        /// </summary>
        /// <param name="em">异常集合</param>
        /// <param name="result">返回值</param>
        public void SendAppMsg(ExceptionMess em,ref String result)
        {
            //过滤重复出错信息
            AppMsgDoFilter(ref em);

            for (int i = 0; i < em.GetData.Count; i++)
            {
                //字符串集合
                StringBuilder _sb = new StringBuilder();
                //当前出错集合信息
                for (int j = 0; j < em.GetData[i].Count; j++)
                {
                    _sb.Append(em.GetData[i].GetAppException[j].AppMessage.ToString());
                    _sb.Append("<br />");
                }
                result = _sb.ToString();
            }
        }

        //过滤重复出错信息
        private void AppMsgDoFilter(ref ExceptionMess em)
        {
            int datac = em.GetData.Count;
            if (datac < 1) return;
            for (int i = 0; i < em.GetData[0].Count; i++)
            {
                for (int j = i + 1; j < em.GetData[0].Count; j++)
                {
                    if (em.GetData[0].GetAppException[i].AppMessage.ToString() == em.GetData[0].GetAppException[j].AppMessage.ToString())
                    {
                        em.GetData[0].GetAppException.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

        //抛出系统异常,跳转到相应抛错页面
        public void SysExceptionThrow(SessionManager sessionManager,  SysException es)
        {
            sessionManager.SystemExceptionId = es;
            //throw es;
        }
        /// <summary>
        /// 取得本地XML路径(公司)
        /// </summary>
        /// <param name="registId">xml文件子目录</param>
        /// <param name="xml">xml文件</param>
        /// <returns></returns>
        public String LocalXmlPath(String registId, String xml)
        {
            StringBuilder _sb = new StringBuilder();

            String path = String.Format(CodeSymbol.m_dataSourcePath, "{0}", registId);
            _sb.Append(path);
            _sb.Append(@"\");
            _sb.Append(xml);
            return _sb.ToString();
        }

        /// <summary>
        /// 取得本地XML路径(公司)
        /// </summary>
        /// <param name="registId">xml文件子目录</param>
        /// <returns></returns>
        public String LocalXmlPath(String registId)
        {
            StringBuilder _sb = new StringBuilder();

            String path = CodeSymbol.m_dataSourcePath;
            _sb.Append(String.Format(path, "{0}", registId));
            return _sb.ToString();
        }

        /// <summary>
        /// 取得本地XML路径(公司)
        /// </summary>
        /// <returns></returns>
        public String LocalXmlPath()
        {
            return CodeSymbol.m_dataSourcePath;
        }

        //取得预存发送文件信息
        public DataSet GetRegistSendData(String dataPath)
        {
            DataSet sds = new DataSet("TransSet");
            if (!m_fileTools.CheckFile(dataPath))
            {
                m_fileTools.CreateFile(dataPath);
                sds.WriteXml(dataPath);
            }
            else
            {
                sds.ReadXml(dataPath);
            }
            return sds;
        }

        //当前日期时间码
        public String CreateRedonName()
        {
            return System.DateTime.Now.ToString("yyMMddHHmmssfff");
        }
        //当前时间码
        public String CreateRedonNameTime()
        {
            return System.DateTime.Now.ToString("mmssfff");
        }
        public String GetCooCode()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["CooServerName"].ToString();
        }

        /// <summary>
        /// 是否需要邮件验证
        /// </summary>
        public Boolean EmailConfirmation()
        {
            return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("EmailConfirmation"));
        }
    }
}
