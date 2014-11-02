using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using Seika.COO.DBA.MA;
using Seika.Api;
using Seika.Db;
using System.Collections.Generic;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 语言管理后台
    /// </summary>
    /// <remarks>
    /// 2008/09/25  李博
    /// </remarks>
    public class P3090P1020 : PageDataBase
    {
        public P3090P1020()
            : base()
        {
            
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable dtCompany = ma_cominfo.GetCompanyInfo(RegisterId);
            Company = dtCompany;

            MA_STYLE ma_style = new MA_STYLE(conn);
            DataTable dtMenuStyle = ma_style.GetStyletype();
            Resid2Name("CodeManage", ref dtMenuStyle, "CLASSNAME");
            MenuStyle = dtMenuStyle;

            MA_BASICXML ma_basic = new MA_BASICXML(conn);
            m_pageXmlSym = new Seika.COO.Web.PG.PageXmlSymbol();
            DataTable dtPro = ma_basic.GetBasicClassList(m_pageXmlSym.Menu, WebLang);
            Profession = dtPro;

            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);
            DataTable dt = bs_weblang.GetWebLang(RegisterId);
            if (dt != null && dt.Rows.Count > 0)
            {
                SelectSite = true;
            }
            else
            {
                SelectSite = false;
            }

            conn.close();
        }

        public DataTable GetStyle(String styleClassId)
        { 
            DBConnect conn = this.GetDbConnect();
            MA_STYLE ma_style = new MA_STYLE(conn);
            DataTable dt = ma_style.GetStyle(WebLang, styleClassId);
            conn.close();
            return dt;
        }

        /// <summary>
        /// 设置
        /// </summary>
        public void SetCominfo()
        {
            DBConnect conn = this.GetDbConnect();
            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            bool b = ma_cominfo.UpdataComInfoOpenflg(RegisterId, WebLang);
            conn.Commit();
            conn.close();
        }

        public void SaveWebLang(String defaulflg, String openflg, String weblangId,String profession)
        {
            DBConnect conn = this.GetDbConnect();
            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);
            bool b = bs_weblang.InsertWebLang(RegisterId, WebLang, StyleId, defaulflg, openflg, weblangId, profession);
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            throw new System.Exception("The method or operation is not implemented.");
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

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 注册ID
        /// </summary>
        public String RegisterId
        {
            set;
            get;
        }

        /// <summary>
        /// 语言信息
        /// </summary>
        public String WebLang
        {
            set;
            get;
        }

        /// <summary>
        /// 样式ID
        /// </summary>
        public String StyleId
        {
            set;
            get;
        }

        /// <summary>
        /// 基本数据
        /// </summary>
        public DataTable Company
        {
            set;
            get;
        }

        /// <summary>
        /// 样式菜单
        /// </summary>
        public DataTable MenuStyle
        {
            set;
            get;
        }

        /// <summary>
        /// 行业菜单
        /// </summary>
        public DataTable Profession
        {
            set;
            get;
        }

        public bool SelectSite
        {
            set;
            get;
        }
        
    }
}