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
    /// 购买服务
    /// </summary>
    /// <remarks>
    /// 2009/01/13  李博  新规作成
    /// </remarks>
    public class P4000P0060 : PageDataBase
    {
        public P4000P0060()
            : base()
        {
           
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            MA_SERVICE ma_service = new MA_SERVICE(conn);
            DataTable sdt = ma_service.GetServiceCharge(ServiceType, Lang);
            Charge = sdt;

            

            MA_MEMBER ma_member = new MA_MEMBER(conn);
            DataTable dt = ma_member.GetServices(RegistId);
            Services = dt;

            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            String endDate = String.Empty;
            DBConnect conn = this.GetDbConnect();

            MA_MEMBER ma_member = new MA_MEMBER(conn);
            DataTable mdt = ma_member.GetSTATEFLG(RegistId);
            if (mdt != null && mdt.Rows.Count > 0)
            {
                //如果存在未付款记录，删除该记录
                ma_member.DeleteMember(RegistId);
            }

            //服务到期日
            MA_REGISTER ma_register = new MA_REGISTER(conn);
            DataTable edt = ma_register.GetServicesEndDate(RegistId);

            if (edt != null && edt.Rows.Count > 0)
            {
                endDate = edt.Rows[0]["SERVICEENDDATE"].ToString();
                //如果超出赠送服务日，则按当前日期开始服务
                if (m_objStrTool.DateCompare(DateTime.Now, Convert.ToDateTime(endDate)))
                {
                    endDate = DateTime.Now.ToString();
                }
            }

            DataTable dt = Services;
            if (dt != null && dt.Rows.Count > 0)
            {
                ma_member.InsertMember(  dt.Rows[0]["MA_MEMBERID"].ToString()
                                       , RegistId
                                       , endDate
                                       , dt.Rows[0]["NUMBER"].ToString()
                                       , dt.Rows[0]["PAYMONEY"].ToString()
                                       , dt.Rows[0]["STATEFLG"].ToString()
                                       , dt.Rows[0]["MODEFLG"].ToString());
                
            }
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

        /// <summary>
        /// 服务信息
        /// </summary>
        public DataTable Services
        {
            set;
            get;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public String RegistId
        {
            set;
            get;
        }

        /// <summary>
        /// 语言
        /// </summary>
        public String Lang
        {
            set;
            get;
        }

        /// <summary>
        /// 服务类别
        /// </summary>
        public String ServiceType
        {
            set;
            get;
        }

        /// <summary>
        /// 服务价格
        /// </summary>
        public DataTable Charge
        {
            set;
            get;
        }
       
    }
}