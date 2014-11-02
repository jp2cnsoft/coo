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
    /// 管理中心管理
    /// </summary>
    /// <remarks>
    /// 2009/03/26  李博
    /// </remarks>
    public class P9010P0060 : PageDataBase
    {
        public P9010P0060() : base()
        {

        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();
            MA_REGISTER ma_register = new MA_REGISTER(conn);
            DataTable dt = ma_register.GetServicesEndDate(RegistId);
            if (dt != null && dt.Rows.Count > 0)
            {
                ServiceEndDate = dt.Rows[0]["SERVICEENDDATE"].ToString();
            }

            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            MA_REGISTER ma_register = new MA_REGISTER(conn);
            ma_register.SetServiceDate(RegistId, ServiceEndDate);

            conn.Commit();
            conn.close();
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
        //用户ID 
        public String RegistId
        {
            set;
            get;
        }
        //服务到期日
        public String ServiceEndDate
        {
            set;
            get;
        }
    }
}