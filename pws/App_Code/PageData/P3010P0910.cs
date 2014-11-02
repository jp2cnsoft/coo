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
using Seika.Db;
using Seika.COO.Web.PG;
using Seika.COO.DBA.SV;

namespace Seika.COO.PageData
{
     ///<summary>
     ///风格选择页
     ///</summary>
     ///<remarks>
     ///2008/02/26
     ///</remarks>
    public class P3010P0910 : PageDataBase
    {
        Hashtable dts = new Hashtable();
        String registId = String.Empty;
        String lang = String.Empty;
        String currentstyleid = String.Empty;
        String currentstylename = String.Empty;
        String currentexplain = String.Empty;
        String currentpicname = String.Empty;
        String currentserverhost = String.Empty;
        String styleid = String.Empty;
        String stylename = String.Empty;
        String explain = String.Empty;
        String picname = String.Empty;
        String serverhost = String.Empty;
        String styletypeid = String.Empty;
        String rstyleid = String.Empty;
        String cssname = String.Empty;
        String xslname = String.Empty;

        public P3010P0910()            
        {            
        }
        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            m_pageXmlSym = new PageXmlSymbol(); 
            String mastyleid = String.Empty;
            DataTable styledt = (new BS_WEBLANG(conn)).GetWebLang(registId, WebLang);
            if (styledt != null && styledt.Rows.Count > 0)
            {
                mastyleid = styledt.Rows[0]["MA_STYLEID"].ToString();
                Partitionflg = styledt.Rows[0]["PARTITIONFLG"].ToString();  
            }
            //取得当前用户风格数据
            DataTable dt = (new MA_STYLE(conn)).GetCurrentStyle(mastyleid);
            
            foreach (DataRow row in dt.Rows)
            {
                currentstyleid = row["MA_STYLEID"].ToString();
                //currentstylename = row["STYLENAME"].ToString();
                currentexplain = row["EXPLAIN"].ToString();
                currentpicname = row["PICNAME"].ToString();
            }
            //Resid2Name("CodeManage", ref dt, "STYLENAME");
            if (dt.Rows.Count > 0)
            {
                currentstylename = dt.Rows[0]["STYLENAME"].ToString();
            }
            
            //取得公司域名
            DataTable sv = (new SV_SERVER(conn)).GetServerInfo(registId);
            foreach (DataRow row in sv.Rows)
            {
                //currentserverhost = @"http://" + RegistId + @"." + row["SERVERHOST"].ToString() + @"/System/style_" + currentstyleid + @"/" + CodeSymbol.m_resPath + @"/Images_Company/" + currentpicname;
                serverhost = row["SERVERHOST"].ToString();
            }

            //取得所有风格数据
            DataTable dd = (new MA_STYLE(conn)).GetAllStyle(WebLang);
            dd = CloneStyleTable(dd);
            //Resid2Name("CodeManage", ref dd, "STYLENAME");
            dts.Add("MA_STYLE", dd);

            //取得风格类别数据
            DataTable dl = (new MA_STYLE(conn)).GetStyletype();
            Resid2Name("CodeManage", ref dl, "CLASSNAME");
            dts.Add("MA_STYLECLASS", dl);

            //取得行业菜单
            DataTable dh = (new MA_BASICXML(conn)).GetBasicClassList(m_pageXmlSym.Menu, WebLang);
            dts.Add("profession", dh);

            conn.close();
        }

        private DataTable CloneStyleTable(DataTable dt)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add("MA_STYLEID");
            newDt.Columns.Add("PICNAME");
            newDt.Columns.Add("STYLENAME");
            newDt.Columns.Add("EXPLAIN");
            newDt.Columns.Add("REGISTID");
            newDt.Columns.Add("RESID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //MA_STYLE.REGISTID <> '' 时，只取 MA_STYLE.REGISTID = 用户注册ID的值
                if (!String.IsNullOrEmpty(dt.Rows[i]["REGISTID"].ToString()) && dt.Rows[i]["REGISTID"].ToString() == registId)
                {
                    DataRow row = newDt.NewRow();
                    row["MA_STYLEID"] = dt.Rows[i]["MA_STYLEID"].ToString();
                    row["PICNAME"] = dt.Rows[i]["PICNAME"].ToString();
                    row["STYLENAME"] = dt.Rows[i]["STYLENAME"].ToString();
                    row["REGISTID"] = dt.Rows[i]["REGISTID"].ToString();
                    row["EXPLAIN"] = dt.Rows[i]["EXPLAIN"].ToString();
                    row["RESID"] = dt.Rows[i]["RESID"].ToString();
                    newDt.Rows.Add(row);
                }
            }

            if (newDt.Rows.Count > 0)
            {
                return newDt;
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
            DBConnect conn = this.GetDbConnect();
            //取得当前用户风格数据
            //DataTable dd = (new MA_STYLE(conn)).GetChooseStyletype(styletypeid);
            DataTable dd = (new MA_STYLE(conn)).GetStyle(WebLang,styletypeid);
            dts.Add("MA_CHOOSESTYLECLASS", dd);
            conn.close();
        }
        //保存提交数据
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            if (!(new BS_WEBLANG(conn)).UpdateWebLangStyle(rstyleid, ProfessionName, registId, Lang))
            {
                throw new System.Exception("ED01000380");
            }
            //提交数据
            conn.Commit();
            DataTable dt = (new MA_STYLE(conn)).GetChooseStyle(rstyleid);
            foreach (DataRow row in dt.Rows)
            {
                xslname = row["XSLNAME"].ToString();
                cssname = row["CSSNAME"].ToString();
            }
            conn.close();
        }
        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }
        //传递ID值
        public String RegistId
        {
            set { registId = value; }
            get { return registId; }
        }
        //传递语言
        public String Lang
        {
            set { lang = value; }
            get { return lang; }
        }
        //传递风格样式ID值
        public String Styletypeid
        {
            set { styletypeid = value; }
            get { return styletypeid; }
        }
        //传递当前样式ID称值
        public String CurrentStyleid
        {
            set { currentstyleid = value; }
            get { return currentstyleid; }
        }
        //传递当前样式名称值
        public String CurrentStylename
        {
            set { currentstylename = value; }
            get { return currentstylename; }
        }
        //传递当前样式说明值
        public String CurrentExplain
        {
            set { currentexplain = value; }
            get { return currentexplain; }
        }
        //传递当前用户公司域名值
        public String CurrentServerhost
        {
            set { currentserverhost = value; }
            get { return currentserverhost; }
        }
        //传递风格名称
        public String CurrentPicName
        {
            set { currentpicname = value; }
            get { return currentpicname; }
        }
        //用户激活语言行业
        public String Partitionflg
        {
            set;
            get;
        }

        //取得样式表
        public DataTable MA_STYLE
        {
            get
            {
                return (DataTable)dts["MA_STYLE"];
            }
        }

        //取得行业表
        public DataTable Profession
        {
            get
            {
                return (DataTable)dts["profession"];
            }
        }
        //用户选择的行业名
        public String ProfessionName
        {
            get;
            set;
        }

        /// <summary>
        /// 语言信息
        /// </summary>
        public String WebLang
        {
            set;
            get;
        }

        //取得样式类别表
        public DataTable MA_STYLECLASS
        {
            get
            {
                return (DataTable)dts["MA_STYLECLASS"];
            }
        }
        //取得选择的样式类别表
        public DataTable MA_CHOOSESTYLECLASS
        {
            get
            {
                return (DataTable)dts["MA_CHOOSESTYLECLASS"];
            }
        }

        //传递公司域名值
        public String Serverhost
        {
            set { serverhost = value; }
            get { return serverhost; }
        }

        //传递选择样式id值
        public String Rstyleid
        {
            set { rstyleid = value; }
            get { return rstyleid; }
        }
        //传递选择样式名称值
        public String Xslname
        {
            set { xslname = value; }
            get { return xslname; }
        }
        //传递选择样式CSS名称值
        public String Cssname
        {
            set { cssname = value; }
            get { return cssname; }
        }
    }
}