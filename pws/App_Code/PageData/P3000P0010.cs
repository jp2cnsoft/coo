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
    /// 域名查询结果
    /// </summary>
    /// <remarks>
    /// 2008/05/16  于作伟  新规作成
    /// </remarks>
    public class P3000P0010 : PageDataBase
    {
        String domain_name = null;
        ArrayList domain_ext = null;
        DataTable domain = null;

        public P3000P0010()
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

            domain = CreatePayTable();

            //符合条件域名一览
            XinNetServer xinnet = new XinNetServer();
            DataTable dom = xinnet.DomainSearch(domain_name, domain_ext);
            foreach (DataRow row in dom.Rows) 
            {
                String dname = row["DNAME"].ToString();
                DataTable dt = ma_service.GetServiceByDomain(dname.Replace(domain_name, ""));
                foreach(DataRow dr in dt.Rows)
                {
                    DataRow r = domain.NewRow();
                    r["ID"] = GetMaxId(domain) + 1;
                    r["TYPE"] = dr["TYPE"].ToString();
                    r["MA_SERVICEID"] = dr["MA_SERVICEID"].ToString();
                    r["SERVICENAME"] = dr["SERVICENAME"].ToString();
                    r["PRICE"] = dr["CHARGE"].ToString();
                    r["PRICENUM"] = "1";
                    r["PRICEUNIT"] = dr["SERVICEUNIT"].ToString();
                    r["PRICETOTAL"] = Convert.ToDecimal(dr["CHARGE"].ToString());
                    r["REMARK"] = dname;
                    domain.Rows.Add(r);
                }
            }

        }

        private DataTable CreatePayTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TYPE");
            dt.Columns.Add("MA_SERVICEID");
            dt.Columns.Add("SERVICENAME");
            dt.Columns.Add("PRICE");
            dt.Columns.Add("PRICENUM");
            dt.Columns.Add("PRICEUNIT");
            dt.Columns.Add("PRICETOTAL", typeof(Decimal));
            dt.Columns.Add("REMARK");
            return dt;
        }

        private int GetMaxId(DataTable dt)
        {
            int cId = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                cId = Convert.ToInt32(dt.Compute("MAX(ID)", "").ToString());
            }
            return cId;
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
        /// 域名查询条件
        /// </summary>
        public String Domain_name
        {
            get { return domain_name; }
            set { domain_name = value; }
        }

        /// <summary>
        /// 域名查询后缀
        /// </summary>
        public ArrayList Domain_ext
        {
            get { return domain_ext; }
            set { domain_ext = value; }
        }

        /// <summary>
        /// 域名查询结果
        /// </summary>
        public DataTable Domain
        {
            get { return domain; }
            set { domain = value; }
        }

    }
}
