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
using Seika.COO.DBA.BS;
using Seika.Api;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 服务详细信息
    /// </summary>
    /// <remarks>
    /// 2008/04/15  于作伟  新规作成
    /// </remarks>
    public class P4000P0030 : PageDataBase
    {
        static Hashtable dts = new Hashtable();
        String serviceId = String.Empty;

        public P4000P0030()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            String ltype = String.Empty;
            //从数据库中取得服务一览数据
            DataTable dt = (new MA_SERVICE(conn)).GetServiceDetail(serviceId);
            foreach (DataRow row in dt.Rows) 
            {
                ltype = row["EXTENDTYPE"].ToString();
            }
            dts["ServiceDetail"] = dt;

            if (!String.IsNullOrEmpty(ltype))
            {
                //域名后缀初始化
                dt = (new MA_CODE(conn)).GetDomain(ltype);
                dts["Domain"] = dt;
            }
            conn.close();
        }

        public bool CheckServiceId(String registId,String serviceId) 
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new BS_SERVICE(conn)).GetServiceId(registId,serviceId);
            conn.close();

            if (dt.Rows.Count > 0)
                return true;
            return false;
        }

        public bool CheckServiceIdHost(String registId, String remark)
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new BS_SERVICE(conn)).GetServiceIdHost(registId, serviceId);
            conn.close();

            if (dt.Rows.Count > 0)
                return true;
            return false; 
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
        /// 查询条件
        /// </summary>
        public String ServiceId
        {
            set
            {
                serviceId = value;
            }
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        public DataTable ServiceDetail
        {
            get 
            {
                return (DataTable)dts["ServiceDetail"];
            }
        }
        /// <summary>
        /// 返回国际域名
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
