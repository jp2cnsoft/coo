using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Seika.COO.Web.PG
{
    /// <summary>
    /// Summary description for CodeCategorySymbol
    /// </summary>
    public class CodeSymbol
    {
        //用户XML所在根目录
        public static String m_common_type = "Company";
        //action名路径
        public static String m_actionPath = "Seika.COO.Action";
        //XML数据相对目录
        public static String m_dataSourcePath = @"WebRoot/XData/{0}/{1}/";
        //上传数据相对目录
        public static String m_dataSourceCusPath = @"WebRoot\XData\System\Web\CustomQA\";
        //数据源后缀
        public static String m_dataSourceExt = ".xml";
        //临时转换数据目录
        public static String m_transFilePath = @"TransFile\";
        //默认信息用户ID
        public static String m_DefaultRegistId = @"SEIKA";
        //权限分类-种类ID
        public static System.Drawing.Color color_err = System.Drawing.Color.Red;
        //动态导航条xml的id
        public static String m_naviId = "navi";
        ////系统xml的id/友情链接
        //public static String[] m_system = new String[] { "System", "P3010P0940" };
        ////需要嵌套xml
        //public static String[] m_linkXml = new String[] { "ad" };
        //无类别状态生成类别字符串
        public static String m_noneType = "000";
        //每页显示数
        public static int m_pageNum = 30;
        //数据源样式后缀
        public static String m_dataStyleExt = ".xsl";
        //生成文件后缀
        public static String m_destinatExt = ".html";
        //新闻静态页ID
        public static String m_newsHtmlId = "P3040P0030";
        //产品静态页ID
        public static String m_proHtmlId = "P3050P0030";

        //客户端域名配置文件夹名称
        public static String domain_name = "h";
        //客户端F域名配置文件路径
        public static String domain_host = "documentXml/userdomain.xml";

        //服务到期提示时间设置
        public static int m_servicesDate = 15;

        //临时文件目录
        public static String m_tempFileFolder = "Temp/";
        //客户浏览范域名
        public static String m_clientHost =
            "http://{0}." + System.Configuration.ConfigurationManager.AppSettings["DomainName"];
        //客户浏览范域名
        public static String m_clientHostJp = 
            "http://www." + System.Configuration.ConfigurationManager.AppSettings["DomainName"];

    }
}