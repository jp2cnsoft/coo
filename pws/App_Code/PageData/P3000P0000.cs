using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Seika.COO.DBA.MA;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.COO.DBA.SV;
using Seika.COO.DBA.DE;
using Seika.Api;
using Seika;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 主页显示数据取得及保持处理
    /// </summary>
    /// <remarks>
    /// 2008/01/02  金光哲  新规作成
    /// </remarks>
    public class P3000P0000 : PageDataBase
    {
        static Hashtable dts = new Hashtable();
        String xmlNewsPath = String.Empty;

        public P3000P0000() : base ()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            //新闻一览数据不存在时
            //if (!dts.ContainsKey("NewsList") || ReloadDataFlag.PUBLISH_HOMEPAGE_NEWS)
            //{
            //    //新闻静态页详细ID
            //    String newsHtmlDetail = CodeSymbol.m_newsHtmlId + @"_";
            //    //从XML中取得固定公司一览数据
            //    DataTable dt = GetNewsList(1,5);
            //    //新闻客户端服务器信息
            //    ClientServerManager csm = new ClientServerManager();
            //    String newsServerHost =  String.Format(csm.GetClientServerHttp(CodeSymbol.m_DefaultRegistId),CodeSymbol.m_DefaultRegistId) + @"/";
            //    if (!dts.ContainsKey("NewsServerHost")) { dts.Add("NewsServerHost", newsServerHost); } else { dts["NewsServerHost"] = newsServerHost; }
            //    //设置初始状态
            //    ReloadDataFlag.PUBLISH_HOMEPAGE_NEWS = false;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        //新闻标题
            //        //String name = row["title"].ToString();
            //        //if (!String.IsNullOrEmpty(name))
            //        //{
            //        //    if (name.Length > 22)
            //        //    {
            //        //        row["title"] = name.Substring(0, 20) + "...";
            //        //    }
            //        //}
            //        row["SERVERHOST"] = newsServerHost
            //            + newsHtmlDetail + row["ID"].ToString() + CodeSymbol.m_destinatExt;
            //    }
            //    //从XML取得新闻一览数据
            //    if (!dts.ContainsKey("NewsList")) { dts.Add("NewsList", dt); } else { dts["NewsList"] = dt; }
            //}

            //公司一览数据不存在时
            if (!dts.ContainsKey("CompanyList") || ReloadDataFlag.PUBLISH_HOMEPAGE_COMPANY)
            {
                //记录取得完整数据数
                int resultCount = 0;
                sm = new SessionManager(Session);
                //从数据库中取得公司一览数据
                DataTable dt = (new MA_COMINFO(conn)).GetCompanyList(1, 20, sm.Page_UICultureID);
                foreach (DataRow row in dt.Rows)
                {
                    //公司帐号
                    String registId = row["REGISTID"].ToString();
                    //公司名称
                    String name = row["NAME"].ToString();
                    if (!String.IsNullOrEmpty(name))
                    {
                        if (name.Length > 30)
                        {
                            row["NAME"] = name.Substring(0, 28) + "...";
                        }
                    }
                    //公司客户端服务器链接
                    String serverHost = row["SERVERHOST"].ToString();
                    if (!String.IsNullOrEmpty(serverHost))
                    {
                        row["SERVERHOST"] = @"http://" + registId + @"." + serverHost + @"/";
                        resultCount++;
                    }
                }
                //域名后缀初始化
                dts["Domain"] = (new MA_CODE(conn)).GetDomain();

                if (!dts.ContainsKey("CompanyList")) { dts.Add("CompanyList", dt); } else { dts["CompanyList"] = dt; }
                //设置初始状态
                if (dt.Rows.Count == resultCount) { ReloadDataFlag.PUBLISH_HOMEPAGE_COMPANY = false; }
            }

            //产品一览数据不存在时
            //if (!dts.ContainsKey("ProductList") || ReloadDataFlag.PUBLISH_HOMEPAGE_PRODUCT)
            //{
            //    //记录取得完整数据数
            //    int resultCount = 0;
            //    //产品静态页详细ID
            //    String proHtmlDetail = CodeSymbol.m_proHtmlId + @"_";
            //    //从数据库中取得公司一览数据
            //    DataTable dt = (new MA_PRODUCT(conn)).GetProductList(1, 20);
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        //公司帐号
            //        String registId = row["REGISTID"].ToString();
            //        //产品名称
            //        String name = row["NAME"].ToString();
            //        if (!String.IsNullOrEmpty(name))
            //        {
            //            if (name.Length > 15)
            //            {
            //                row["NAME"] = name.Substring(0, 13) + "...";
            //            }
            //        }
            //        //产品客户端服务器
            //        String serverHost = row["SERVERHOST"].ToString();
            //        if (!String.IsNullOrEmpty(serverHost))
            //        {
            //            row["SERVERHOST"] = @"http://" + registId + @"." + serverHost + @"/"
            //                + proHtmlDetail + row["MA_PRODID"].ToString().Trim() + CodeSymbol.m_destinatExt;
            //            resultCount++;
            //        }
            //    }
            //    if (!dts.ContainsKey("ProductList")) { dts.Add("ProductList", dt); } else { dts["ProductList"] = dt; }
            //    //设置初始状态
            //    if (dt.Rows.Count == resultCount) { ReloadDataFlag.PUBLISH_HOMEPAGE_PRODUCT = false; }
            //}

            conn.close();

        }

        /// <summary>
        /// 取得新闻一览
        /// </summary>
        /// <returns></returns>
        private DataTable GetNewsList(int startIdx, int count) 
        {
            //实力化DataSetManage
            m_dsManage = new DataSetManage();
            DataTable dt = new DataTable();
            if (m_fileTools.CheckFile(XmlNewsPath))
            {
                //读取XML内容转为DataSet
                m_dsManage.ReadLocalXml2DataSet("chxml", XmlNewsPath);
                if (m_dsManage.Get.Tables.Contains("news"))
                {
                    DataView dv = m_dsManage.Get.Tables["news"].DefaultView;
                    //ID倒序
                    dv.Sort = "id Desc";
                    int nCount = 0;
                    //取得指定条件数据
                    dt = m_dsManage.GetCloneTable(dv.ToTable(), "news", startIdx - 1, count, ref nCount);
                    //客户端服务器信息
                    dt.Columns.Add("SERVERHOST");
                    return dt;
                }
            }
            return dt;
        }

        /// <summary>
        /// 数据清空
        /// </summary>
        public override void Remove()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        public override void Load()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        //保存提交数据
        public override void Save()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 操作新闻XML路径
        /// </summary>
        public String XmlNewsPath
        {
            set { xmlNewsPath = value; }
            get { return xmlNewsPath; }
        }

        /// <summary>
        /// 新闻客户端服务器信息
        /// </summary>
        public String NewsServerHost
        {
            get
            {
                return (String)dts["NewsServerHost"];
            }
        }

        /// <summary>
        /// ニューズリストデータテーブル
        /// </summary>
        public DataTable NewsList
        {
            get 
            {
                return (DataTable)dts["NewsList"];
            }
        }

        /// <summary>
        /// 会社リストデータテーブル
        /// </summary>
        public DataTable CompanyList
        {
            get
            {
                return (DataTable)dts["CompanyList"];
            }
        }

        /// <summary>
        /// 製品リストデータテーブル
        /// </summary>
        public DataTable ProductList
        {
            get
            {
                return (DataTable)dts["ProductList"];
            }
        }

        /// <summary>
        /// 返回域名
        /// </summary>
        public DataTable Domain
        {
            get
            {
                return (DataTable)dts["Domain"];
            }
        }
    }
}
