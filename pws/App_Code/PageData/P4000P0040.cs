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
    /// 服务详细信息
    /// </summary>
    /// <remarks>
    /// 2008/04/16  于作伟  新规作成
    /// </remarks>
    public class P4000P0040 : PageDataBase
    {
        Hashtable serviceBank = new Hashtable();

        public P4000P0040()
            : base()
        {
        }

        //判断帐户中金钱是否足够支付 
        public bool CheckAccount() 
        {
            String registId = serviceBank["REGISTID"].ToString();
            decimal payMoney = Convert.ToDecimal(serviceBank["PAYMONEY"].ToString());
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new MA_ACCOUNT(conn)).GetAccount(registId);
            decimal balance = 0;
            foreach (DataRow row in dt.Rows) 
            {
                balance = Convert.ToDecimal(row["BALANCE"].ToString());
            }
            if (payMoney > balance) 
            {
                return false;
            }
            return true;
        }

        //保存提交数据
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();

            //将数据保存到数据库
            (new MA_BANKLIST(conn)).AddBank(serviceBank["MA_BANKLISTID"].ToString(), serviceBank["REGISTID"].ToString()
                , serviceBank["PAYMONEY"].ToString(), serviceBank["STATE"].ToString());

            conn.Commit();
            conn.close();

        }


        /// <summary>
        /// 设置数据集
        /// </summary>
        public Hashtable ServiceBank
        {
            set
            {
                serviceBank = value;
            }
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

    }
}
