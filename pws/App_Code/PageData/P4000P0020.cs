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
    /// 我的服务
    /// </summary>
    /// <remarks>
    /// 2008/04/16  杨李
    /// </remarks>
    public class P4000P0020 : PageDataBase
    {
        String registId = String.Empty;
        String remark = String.Empty;
        String balance = String.Empty;
        String startdate = String.Empty;
        String enddate = String.Empty;
        String num = String.Empty;
        String accountid = String.Empty;
        String serviceid = String.Empty;
        String outmoneytotal = String.Empty;
        //会员服务到期日，如果购买会员服务，该值写入MA_REGISTER表
        String endServiceDate = String.Empty;
        String type = String.Empty;

        //String maServiceid = String.Empty;
        //String maAccountid = String.Empty;
        //String charge = String.Empty;
        //String inmoneytotal = String.Empty;
        static Hashtable dts = new Hashtable();

        public P4000P0020()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            DataTable dt = (new MA_SERVICE(conn)).GetServiceMy(registId);
            dt.Columns.Add("CHARGESUM");
            foreach (DataRow row in dt.Rows)
            {
                if (row["TYPE"].ToString() == "30" && !String.IsNullOrEmpty(row["REMARK"].ToString()))
                {
                    string type = row["REMARK"].ToString();
                    row["REMARK"] = "www." + type;
                }
                if (!String.IsNullOrEmpty(row["STARTDATE"].ToString()))
                {
                    string startdate = row["STARTDATE"].ToString();
                    row["STARTDATE"] = startdate.Substring(0, 4) + "-" + startdate.Substring(4, 2) + "-" + startdate.Substring(6, 2);
                }
                if (!String.IsNullOrEmpty(row["ENDDATE"].ToString()))
                {
                    string enddate = row["ENDDATE"].ToString();
                    row["ENDDATE"] = enddate.Substring(0, 4) + "-" + enddate.Substring(4, 2) + "-" + enddate.Substring(6, 2);
                }
                row["CHARGESUM"] = row["CHARGE"].ToString();
                if (!String.IsNullOrEmpty(row["SENDBACK"].ToString()))
                {
                    string charge = row["CHARGE"].ToString();
                    string sendback = row["SENDBACK"].ToString();
                    row["CHARGE"] = Convert.ToString(Convert.ToDecimal(charge) - Convert.ToDecimal(sendback));
                }

                //if (!String.IsNullOrEmpty(row["MA_SERVICEID"].ToString()))
                //{
                //    maServiceid = row["MA_SERVICEID"].ToString();
                //}
                //if (!String.IsNullOrEmpty(row["MA_ACCOUNTID"].ToString()))
                //{
                //    maAccountid = row["MA_ACCOUNTID"].ToString();
                //}
                //if (!String.IsNullOrEmpty(row["IN_MONEYTOTAL"].ToString()))
                //{
                //    inmoneytotal = row["IN_MONEYTOTAL"].ToString();
                //}
                //if (!String.IsNullOrEmpty(row["CHARGE"].ToString()))
                //{
                //    charge = row["CHARGE"].ToString();
                //}
            }
            if (!dts.ContainsKey("ServiceMy")) { dts.Add("ServiceMy", dt); } else { dts["ServiceMy"] = dt; }
            conn.close();
        }

        /// <summary>
        /// 数据清空
        /// </summary>
        public override void Remove()
        {
            DBConnect conn = this.GetDbConnect();
            BS_SERVICE bs_service = new BS_SERVICE(conn);
            bs_service.Delservice(serviceid);
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        public override void Load()
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new MA_ACCOUNT(conn)).GetBalance(registId);
            foreach (DataRow row in dt.Rows)
            {
                balance = row["BALANCE"].ToString();
                accountid = row["MA_ACCOUNTID"].ToString();
                outmoneytotal = row["OUT_MONEYTOTAL"].ToString();

            }
            conn.close();
        }

        //保存提交数据
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            //BS_MANAGE bs_manage = new BS_MANAGE(conn);
            //传递提交信息
            if (!(new MA_SERVICE(conn)).Startservice(serviceid,startdate,enddate))
            {
                throw new System.Exception("ED01000380");
            }
            if (!(new MA_ACCOUNT(conn)).Updatebalance(accountid,outmoneytotal,balance))
            {
                throw new System.Exception("ED01000380");
            }

            //如果购买会员服务，服务到期日写入MA_REGISTER表
            if (type == "20")
            {
                if (!(new MA_REGISTER(conn)).SetServiceDate(registId, endServiceDate))
                {
                    throw new System.Exception("ED01000380");
                }
            }

            ////添加消费记录
            //String deBalance = Convert.ToString(Convert.ToDecimal(balance) + Convert.ToDecimal(charge));
            //String deOutmoneyTotal = Convert.ToString(Convert.ToDecimal(outmoneytotal) - Convert.ToDecimal(charge));
            //if (!(new DE_SERVICE(conn)).AddService(maAccountid,maServiceid,inmoneytotal,deOutmoneyTotal,deBalance))
            //{
            //    throw new System.Exception("ED01000380");
            //}
            //提交数据
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ニューズリストデータテーブル
        /// </summary>
        public DataTable ServiceMy
        {
            get 
            {
                return (DataTable)dts["ServiceMy"];
            }
        }
        //传递ID值
        public String RegistId
        {
            set { registId = value; }
            get { return registId; }
        }
        public String Balance
        {
            set { balance = value; }
            get { return balance; }
        }
        public String Startdate
        {
            set { startdate = value; }
            get { return startdate; }
        }
        public String Enddate
        {
            set { enddate = value; }
            get { return enddate; }
        }
        public String Num
        {
            set { num = value; }
            get { return num; }
        }
        public String Accountid
        {
            set { accountid = value; }
            get { return accountid; }
        }
        public String Serviceid
        {
            set { serviceid = value; }
            get { return serviceid; }
        }
        public String Outmoneytotal
        {
            set { outmoneytotal = value; }
            get { return outmoneytotal; }
        }
        public String EndServiceDate
        {
            set { endServiceDate = value; }
            get { return endServiceDate; }
        }
        public String Type
        {
            set { type = value; }
            get { return type; }
        }

        //public String MaServiceid
        //{
        //    set { maServiceid = value; }
        //    get { return maServiceid; }
        //}
        //public String MaAccountid
        //{
        //    set { maAccountid = value; }
        //    get { return maAccountid; }
        //}
        //public String Charge
        //{
        //    set { charge = value; }
        //    get { return charge; }
        //}
        //public String Inmoneytotal
        //{
        //    set { inmoneytotal = value; }
        //    get { return inmoneytotal; }
        //}
    }
}
