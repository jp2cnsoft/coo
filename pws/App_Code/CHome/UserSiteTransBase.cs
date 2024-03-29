using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;
using System.IO;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.CooException;
using Seika.COO.Util;
using Seika.Common.Net;
using Seika.Transform.Command;
using Seika.Transform.Command.Client;
using Seika.Transform.Command.Enum;


namespace Seika.COO.Web.PG
{
    /// <summary>
    /// Summary description for PageDataBase
    /// </summary>
    public abstract class UserSiteTransBase : System.Web.UI.Page
    {

        public CommandSender m_cs;
        protected FileTools m_fileTools = new FileTools();
        protected BaseFunction m_baseFunction = new BaseFunction();
        protected DataSetManage m_dsManage = new DataSetManage();
        protected ResManager m_rsMansge = new ResManager();
        //生成文件类型
        protected String m_opType = UserSiteTransType.TYPE_HTML;
        //生成文件后缀名
        protected String m_opExt = UserSiteTransExt.EXT_HTML;
        //组织类型
        public String m_type = CodeSymbol.m_common_type;

        ////数据库字符串
        //protected DBConnect conn;
        ///// <summary>
        ///// 连接数据库
        ///// </summary>
        //protected void Open()
        //{
        //    //连接服务
        //    //m_cs.Open("coo", "coo001");
        //    conn = this.GetDbConnect();
        //}
        ///// <summary>
        ///// 关闭数据库
        ///// </summary>
        //protected void Close()
        //{
        //    //m_cs.Close();
        //     try
        //     {
        //         Conn.Commit();
        //     }
        //     catch (SysException es)
        //     {
        //         Conn.Rollback();
        //         throw new SysException("", es);
        //     }
        //     finally
        //     {
        //         Conn.close();
        //     }
        //}
        //private DBConnect GetDbConnect()
        //{
        //    DBConnect sql = DBConnectPool.GetConnect();
        //    sql.open();
        //    return sql;
        //}
        /// <summary>
        /// 取得用户xml列表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="xmlList"></param>
        /// <returns></returns>
        protected XmlDocMangContent GetUserXmlList(String registId, String language, String[] xmlList)
        {
            return UserSiteXmlManager.GetUserXml(registId, language, xmlList);
        }

        /// <summary>
        /// 保存用户xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="xmlName"></param>
        /// <param name="xmlDoc"></param>
        protected void SaveUserXml(String registId, String language, String xmlName,XmlDocManage xmlDoc)
        {
            XmlDocMangContent xmlDocList = new XmlDocMangContent();
            xmlDocList[xmlName] = xmlDoc;
            UserSiteXmlManager.SaveUserXml(registId, language, xmlDocList);
        }

        /// <summary>
        /// 取得本地XML路径(公司)
        /// </summary>
        /// <param name="registId">本地路径</param>
        /// <param name="registId">xml文件子目录</param>
        /// <param name="xml">xml文件</param>
        /// <returns></returns>
        protected String LocalXmlPath(String localPath, String registId, String xml)
        {
            return localPath + String.Format(m_baseFunction.LocalXmlPath(registId, xml), m_baseFunction.m_common_type);
        }

        #region 生成页面命令基本函数
        /// <summary>
        /// 取得xml的xpath属性值
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        protected String GetXpathAttrId(String xpath)
        {
            int lsub;
            if ((lsub = xpath.LastIndexOf('\'')) > 0)
            {
                String fsub = xpath.Substring(0, lsub);
                int llsub;
                if ((llsub = fsub.LastIndexOf('\'')) > 0)
                {
                    return fsub.Substring(llsub + 1, fsub.Length - llsub - 1);
                }
            }
            return "";
        }

        /// <summary>
        /// 克隆xml
        /// </summary>
        /// <param name="m_xdManage"></param>
        /// <param name="xpath"></param>
        /// <param name="dt"></param>
        /// <param name="sort"></param>
        /// <param name="xmlpath"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        protected XmlDocManage CloneXml(XmlDocManage m_xdManage, String xpath, DataTable dt,
            String sort, int start, int len)
        {
            XmlDocManage m_Manage = new XmlDocManage();
            m_Manage.CreateNode(xpath);

            String id = String.Empty;
            StringBuilder _nodeXml = new StringBuilder();
            switch (sort)
            {
                //正序
                case UserSiteTransParam.PARAM_ASC:
                    for (int i = start; i < start + len; i++)
                    {
                        id = dt.Rows[i]["id"].ToString();
                        System.Xml.XmlNode xmlnode = m_xdManage.GetXmlNode(String.Format(xpath, id));
                        if (xmlnode != null)
                        {
                            _nodeXml.Append(xmlnode.OuterXml.ToString());
                        }
                    }
                    break;
                //倒序
                case UserSiteTransParam.PARAM_DESC:
                    for (int i = start; i > start - len; i--)
                    {
                        if (i < 0) break;
                        id = dt.Rows[i]["id"].ToString();
                        System.Xml.XmlNode xmlnode = m_xdManage.GetXmlNode(String.Format(xpath, id));
                        if (xmlnode != null)
                        {
                            _nodeXml.Append(xmlnode.OuterXml.ToString());
                        }
                    }
                    break;
            }
            m_Manage.AppendXmlNode(xpath, _nodeXml.ToString());
            return m_Manage;
        }

        /// <summary>
        /// 类别过滤
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="ddt"></param>
        /// <param name="filterHead"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected bool DataTable2Filter(DataTable sdt, ref DataTable ddt, String filterHead, String filter)
        {

            //处理的为一览页
            if (filter == CodeSymbol.m_noneType)
            {
                ddt = sdt;
                return false;
            }
            //当前页不存在类别
            if (!sdt.Columns.Contains(filterHead))
            {
                return false;
            }
            else
            {
                DataView dv = new DataView(sdt);
                dv.RowFilter = filterHead + @" like " + @"'%" + filter + @"%'";
                ddt = dv.ToTable();
            }
            return true;
        }

        /// <summary>
        /// 过滤数据取得
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected ArrayList String2ArrayFilter(String filter)
        {
            ArrayList al = new ArrayList();
            if (filter == null) return al;
            String[] fs = filter.Split('=');
            al.Add(fs[0].ToString());
            if (fs.Length > 1)
            {
                String[] fd = fs[1].ToString().Split(',');
                for (int i = 0; i < fd.Length; i++)
                {
                    al.Add(fd[i].ToString());
                }
            }
            return al;
        }

        /// <summary>
        /// 取得追加的文档节点数据流
        /// </summary>
        /// <param name="createXpath">新建节点路径</param>
        /// <param name="appendXmlDocMang">追加文档</param>
        /// <param name="appendXpath">追加文档路径</param>
        /// <returns></returns>
        protected MemoryStream GetAppendXmlNodeStream(String createXpath, XmlDocManage appendXmlDocMang, String appendXpath)
        {
            //新建xml文档
            XmlDocManage xml = new XmlDocManage();
            //建立新节点
            xml.CreateNode(createXpath);
            //在新建节点中添加节点
            xml.AppendXmlNode(createXpath, appendXmlDocMang.GetXmlNode(appendXpath));
            //取得xml文档流
            MemoryStream sm = xml.GetXmlDocStream();
            //返回
            return sm;
        }
        #endregion        
    }

    /// <summary>
    /// 设置命令
    /// </summary>
    public abstract class UserSiteTransCommand
    {
        ///<summary>新对象追加</summary>
        public const String COMMAND_NEW_RECORD = "new";
        ///<summary>追加记录</summary>
        public const String COMMAND_ADD_RECORD = "add";
        ///<summary>删除记录</summary>
        public const String COMMAND_DEL_RECORD = "del";
    }

    /// <summary>
    /// 设置参数
    /// </summary>
    public abstract class UserSiteTransParam
    {
        ///<summary>生成单个文件</summary>
        public const String PARAM_SINGLE = "single";
        ///<summary>以降序模式全部生成</summary>
        public const String PARAM_ALL_DESC = "all_desc";
        ///<summary>生成首页</summary>
        public const String PARAM_INDEX = "index";
        ///<summary>升序</summary>
        public const String PARAM_ASC = "asc";
        ///<summary>降序</summary>
        public const String PARAM_DESC = "desc";
        ///<summary>保留</summary>
        public const String PARAM_OTHER = "other";
    }

    /// <summary>
    /// 生成文件类型
    /// </summary>
    public abstract class UserSiteTransType
    {
        ///<summary>html</summary>
        public static String TYPE_HTML = ((int)FILE_TYPE.HTML).ToString();
        ///<summary>php</summary>
        public static String TYPE_PHP = ((int)FILE_TYPE.PHP).ToString();
        ///<summary>pfd</summary>
        public static String TYPE_PFD = ((int)FILE_TYPE.PDF).ToString();
        ///<summary>css</summary>
        public static String TYPE_CSS = ((int)FILE_TYPE.CSS).ToString();
        ///<summary>js</summary>
        public static String TYPE_JS = ((int)FILE_TYPE.JS).ToString();
        ///<summary>other</summary>
        public static String TYPE_OTHER = ((int)FILE_TYPE.OTHER).ToString();
    }

    /// <summary>
    /// 生成文件后缀
    /// </summary>
    public abstract class UserSiteTransExt
    {
        ///<summary>html</summary>
        public const String EXT_HTML = "html";
        ///<summary>php</summary>
        public const String EXT_PHP = "php";
        ///<summary>css</summary>
        public const String EXT_CSS = "css";
        ///<summary>js</summary>
        public const String EXT_JS = "js";
    }
}

