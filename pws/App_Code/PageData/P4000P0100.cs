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
    /// 网站访问统计详细信息
    /// </summary>
    /// <remarks>
    /// 2008/11/17  李博  新规作成
    /// </remarks>
    public class P4000P0100 : PageDataBase
    {
        DBConnect conn = null;

        public P4000P0100()
            : base()
        {
            conn = this.GetDbConnect();
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            String viewCountId = String.Empty;
            int viewNum = 1;
            String viewDate = System.DateTime.Now.ToString("yyyyMMdd");
            MA_VIEWCOUNT ma_viewcount = new MA_VIEWCOUNT(conn);
            DataTable dt = ma_viewcount.GetViewCount(RegisterId,PageId,viewDate,WebLang);

            if (dt != null && dt.Rows.Count > 0)
            {
                //修改访问统计信息
                viewCountId = dt.Rows[0]["MA_VIEWCOUNTID"].ToString();
                viewNum = Convert.ToInt32(dt.Rows[0]["VIEWNUM"].ToString()) + 1;
                ma_viewcount.UpdateViewCount(viewCountId, viewNum);
                conn.Commit();
            }
            else
            { 
                //追加访问统计信息
                viewCountId = m_objStrTool.GetNextSeqNumber();
                ma_viewcount.AddViewCount(viewCountId,RegisterId,PageId,viewNum,viewDate,WebLang);
                conn.Commit();
            }

            //取得统计访问数据
            DataTable dtCount = ma_viewcount.GetViewCountNum(RegisterId, PageId, WebLang);
            if (dtCount != null && dtCount.Rows.Count > 0)
            {
                Count = dtCount.Rows[0]["COUNT"].ToString();
            }

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
        /// 网站语言
        /// </summary>
        public String WebLang
        {
            set;
            get;
        }
        /// <summary>
        /// 被访问画面ID
        /// </summary>
        public String PageId
        {
            set;
            get;
        }
        /// <summary>
        /// 统计访问数据
        /// </summary>
        public String Count
        {
            set;
            get;
        }
    }


}