using System;
using System.IO;
using System.Data;
using System.Text;

/// <summary>
/// 资源文件管理
/// </summary>
namespace Seika.Common.Net
{
    public class ResManager : System.Web.UI.Page
    {
        public ResManager(){}

        /// <summary>
        /// 信息显示用资源串取得
        /// </summary>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public String MsgBoxGlobal(String key)
        {
            String msg = GetGlobalResMess(key);
            return Msg(msg);
        }

        /// <summary>
        /// 资源取得共通函数
        /// </summary>
        /// <param name="resfn">资源文件名</param>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public string GetGlobalRes(String resfn, String key)
        {
            return (string)GetGlobalResourceObject(resfn, key);
        }

        /// <summary>
        /// CODE名定义资源信息取得
        /// </summary>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public string GetGlobalResCode(String key)
        {
            return (string)GetGlobalResourceObject("CodeManage", key);
        }

        /// <summary>
        /// 错误信息，提示信息等资源串定义取得
        /// </summary>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public string GetGlobalResMess(String key)
        {
            return (string)GetGlobalResourceObject("Message", key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public string GetGlobalResUserXmlMang(String key)
        {
            return (string)GetGlobalResourceObject("UserXmlManage", key);
        }

        /// <summary>
        /// 全局显示用共通资源字符串取得
        /// </summary>
        /// <param name="key">资源KEY</param>
        /// <returns>资源值字符串</returns>
        public string GetGlobalPageResource(String key)
        {
            return (string)GetGlobalResourceObject("GPR", key);
        }

        /// <summary>
        /// 资源消息定义取得
        /// </summary>
        /// <param name="msg">消息ID</param>
        /// <returns></returns>
        public String Msg(String msg)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("<script lenguage=\"javscript\">");
            _sb.Append(" alert('" + msg + "')");
            _sb.Append("</script>");
            return _sb.ToString();
        }

    }
}
