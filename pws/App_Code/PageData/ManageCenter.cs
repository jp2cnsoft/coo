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
    /// 管理中心模板
    /// </summary>
    /// <remarks>
    /// 2008/09/16  于作伟
    /// </remarks>
    public class ManageCenter : PageDataBase
    {
        static Hashtable dts = new Hashtable();
        public ManageCenter()
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
            DataTable dtCompany = ma_cominfo.GetComInfo(RegisterId);
            if (dtCompany != null || dtCompany.Rows.Count > 0) 
            {
                CominfoName = dtCompany.Rows[0]["NAME"].ToString();
            }

            MA_REGISTER ma_register = new MA_REGISTER(conn);
            DataTable dt = ma_register.GetServicesEndDate(RegisterId);
            if (dt != null && dt.Rows.Count > 0)
            {
                ServicesEndDate = dt.Rows[0]["SERVICEENDDATE"].ToString();
            }

            conn.close();
        }

        public void Isact()
        {
            DBConnect conn = this.GetDbConnect();

            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);
            DataTable wdt = bs_weblang.GetLang(RegisterId);
            if (wdt != null && wdt.Rows.Count > 0)
            {
                IsAct = true;
            }
            else
            {
                IsAct = false;
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
            set 
            {
                dts["RegisterId"] = value;
            }
            get
            {
                return (String)dts["RegisterId"];
            }
        }

        /// <summary>
        /// 公司名
        /// </summary>
        public String CominfoName
        {
            set
            {
                dts["CominfoName"] = value;
            }
            get
            {
                return (String)dts["CominfoName"];
            }
        }

        /// <summary>
        /// 服务到期日
        /// </summary>
        public String ServicesEndDate
        {
            set;
            get;
        }

        /// <summary>
        /// 是否激活网站
        /// </summary>
        public bool IsAct
        {
            set;
            get;
        }
    }
}