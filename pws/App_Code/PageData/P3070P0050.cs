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


namespace Seika.COO.PageData
{

    /// <summary>
    /// 判断用户是否存在
    /// </summary>
    /// <remarks>
    /// 2008/07/23  李博
    /// </remarks>
    public class P3070P0050 : PageDataBase
    {
        DBConnect conn = null;
        String xslName = String.Empty;
        String cssName = String.Empty;

        /// <summary>
        /// XSL
        /// </summary>
        public String XSLNAME
        {
            get { return xslName; }
            set { xslName = value; }
        }

        /// <summary>
        /// CSS
        /// </summary>
        public String CSSNAME
        {
            get { return cssName; }
            set { cssName = value; }
        }

        /// <summary>
        /// EMAIL
        /// </summary>
        public String EMAIL
        {
            get;
            set;
        }

        public P3070P0050() : base()
        {
            conn = this.GetDbConnect();
        }

        /// <summary>
        /// 取得样式
        /// </summary>
        /// <param name="strManager">用户名</param>
        /// <param name="strLanguage">语言</param>
        /// <returns></returns>
        public bool GetComInfo(String strManager, String strLanguage)
        {
            MA_REGISTER ma_registerid = new MA_REGISTER(conn);

            DataTable dt = ma_registerid.GetRegisterStyle(strManager);
            conn.close();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    XSLNAME = row["XSLNAME"].ToString();
                    CSSNAME = row["CSSNAME"].ToString();
                    EMAIL = row["EMAIL"].ToString();
                }
                return true;
            }
            return false;
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

    }
}
