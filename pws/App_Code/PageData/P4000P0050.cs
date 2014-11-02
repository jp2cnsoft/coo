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
    /// 发票申请
    /// </summary>
    /// <remarks>
    /// 2009/01/09  李博  新规作成
    /// </remarks>
    public class P4000P0050 : PageDataBase
    {
        public P4000P0050()
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
            DataTable dt = ma_cominfo.GetRecCompanyInfo(RegistId);
            Company = dt;

            MA_MEMBER ma_member = new MA_MEMBER(conn);
            MA_INVOICE ma_invoice = new MA_INVOICE(conn);
            DataTable dtInvoice = ma_invoice.GetInvoiceBalance(RegistId);
            DataTable dtMember = ma_member.GetMemberBalance(RegistId);
            if (dtMember != null && dtMember.Rows.Count > 0)
            {
                Invoice = Convert.ToDecimal(dtMember.Rows[0]["MEMBERBALANCE"].ToString()) - Convert.ToDecimal(dtInvoice.Rows[0]["INVOICEBALANCE"].ToString());
            }

            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            MA_INVOICE ma_invoice = new MA_INVOICE(conn);
            if (InvoiceInfo != null && InvoiceInfo.Rows.Count > 0)
            {
                ma_invoice.UpdateInvoice( InvoiceInfo.Rows[0]["ID"].ToString()
                                        , RegistId
                                        , InvoiceInfo.Rows[0]["REQUISITIONMONEY"].ToString()
                                        , InvoiceInfo.Rows[0]["POSTCODE"].ToString()
                                        , InvoiceInfo.Rows[0]["POSTADDRESS"].ToString()
                                        , InvoiceInfo.Rows[0]["POSTNAME"].ToString()
                                        , InvoiceInfo.Rows[0]["COMNAME"].ToString()
                                        , InvoiceInfo.Rows[0]["SENDFLG"].ToString());
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
        /// 企业信息
        /// </summary>
        public DataTable Company
        {
            set;
            get;
        }

        /// <summary>
        /// 添加发票数据
        /// </summary>
        public DataTable InvoiceInfo
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
        /// 待开发票金额
        /// </summary>
        public Decimal Invoice
        {
            set;
            get;
        }
    }
}