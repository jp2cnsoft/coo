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
using Seika.Api;


namespace Seika.COO.PageData
{
    /// <summary>
    /// 产品点击率统计查询
    /// </summary>
    /// <remarks>
    /// 2008/12/22  李博  新规作成
    /// </remarks>
    public class P3020P0920 : PageDataBase
    {
        public P3020P0920()
            : base()
        {
            
        }

        /// <summary>
        /// 获得语言列表并取得注册日期
        /// </summary>
        public DataTable GetLangAndRegistDate()
        {
            DBConnect conn = this.GetDbConnect();

            MA_LANGUAGE ma_language = new MA_LANGUAGE(conn);
            DataTable dt = ma_language.GetLang(RegisterId, WebLangID);

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable dtDate = ma_cominfo.GetRegistDate(RegisterId);

            conn.close();

            if (dtDate != null && dtDate.Rows.Count > 0)
            {
                RegistDate = dtDate.Rows[0]["CREATEDATE"].ToString();
            }

            return dt;
        }

        /// <summary>
        /// 获得产品点击率
        /// </summary>
        public DataTable GetProdCount()
        {
            DBConnect conn = this.GetDbConnect();
            MA_VIEWCOUNT ma_viewcount = new MA_VIEWCOUNT(conn);
            DataTable dt = ma_viewcount.GetProdCount(RegisterId, WebLang, Date, DateType);
            conn.close();
            return dt;
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            throw new System.Exception("The method or operation is not implemented.");
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
        /// 语言
        /// </summary>
        public String WebLang
        {
            set;
            get;
        }

        /// <summary>
        /// 页面语言
        /// </summary>
        public String WebLangID
        {
            set;
            get;
        }

        /// <summary>
        /// 日期
        /// </summary>
        public String Date
        {
            set;
            get;
        }

        /// <summary>
        /// 日期类型
        /// </summary>
        public String DateType
        {
            set;
            get;
        }

        /// <summary>
        /// 注册日期
        /// </summary>
        public String RegistDate
        {
            set;
            get;
        }

    }
}

public class ViewDateType
{
    /// <summary>
    /// 年
    /// </summary>
    public const String YEAR = "year";
    /// <summary>
    /// 月
    /// </summary>
    public const String MONTH = "month";
    /// <summary>
    /// 周
    /// </summary>
    public const String WEEK = "week";
    /// <summary>
    /// 日
    /// </summary>
    public const String DAY = "day";
}