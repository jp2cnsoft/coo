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
using Seika.COO.DBA.BS;
using Seika.COO.DBA.SV;
using Seika.Api;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 帐户充值,购买服务
    /// </summary>
    /// <remarks>
    /// 2008/04/16  于作伟  新规作成
    /// </remarks>
    public class P9000P0030 : PageDataBase
    {
        Hashtable serviceAccount = new Hashtable();
        DataTable serviceList = null;
        DBConnect conn = null;

        public P9000P0030()
            : base()
        {
            conn = this.GetDbConnect();
        }

        //保存提交数据
        public override void Save()
        {
            String endDate = String.Empty;
            
            MA_MEMBER ma_member = new MA_MEMBER(conn);
            MA_REGISTER ma_register = new MA_REGISTER(conn);
            DataTable dt = ma_member.GetServiceEndDate(RegistId);
            if (dt != null && dt.Rows.Count > 0)
            {
                endDate = dt.Rows[0]["ENDDATE"].ToString();
                //更改用户付费状态
                ma_member.UpdateMember(RegistId);
                //更改用户服务到期日
                ma_register.SetServiceDate(RegistId, endDate);

                conn.Commit();
            }

            
            conn.close();
        }

        //保存数据库用户帐户
        public void UpdateAccount(String register, String money) 
        {
            MA_ACCOUNT con = new MA_ACCOUNT(conn);
            //更新数据库
            con.UpdateAccount(register, money);
        }

        //更新服务列表
        public void UpdateService(String register) 
        {
            String accountId = String.Empty;
            //查找用户帐户ID
            DataTable dta = (new MA_ACCOUNT(conn)).GetAccount(register);
            foreach (DataRow row in dta.Rows)
            {
                accountId = row["MA_ACCOUNTID"].ToString();
            }
            BS_SERVICE bs_service = new BS_SERVICE(conn);
            //购买服务
            if (ServiceList != null)
            {
                DataTable dt = ServiceList;
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    String type = row["TYPE"].ToString();
                    String ma_serviceid = row["MA_SERVICEID"].ToString();
                    String serviceName = row["SERVICENAME"].ToString();
                    String pricenum = row["PRICENUM"].ToString();
                    String remark = row["REMARK"].ToString();
                    String startdate = "";
                    String enddate = "";
                    //未激活状态
                    String state = "2";
                    if (!String.IsNullOrEmpty(accountId))
                        bs_service.AddService(bs_service.GetNextSeqNumber() + i.ToString(), accountId, ma_serviceid, pricenum, startdate, enddate, state, remark);
                    i++;
                }
            }
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 设置数据集
        /// </summary>
        public DataTable ServiceList
        {
            set { serviceList = value; }
            get { return serviceList; }
        }

        /// <summary>
        /// 设置数据集
        /// </summary>
        public Hashtable ServiceAccount
        {
            set { serviceAccount = value; }
            get { return serviceAccount; }
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
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

        public String RegistId
        {
            set;
            get;
        }

    }
}
