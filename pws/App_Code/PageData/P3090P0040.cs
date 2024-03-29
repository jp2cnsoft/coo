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
using Seika.COO.DBA.BS;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.Api;
using Seika;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 域名管理
    /// </summary>
    /// <remarks>
    /// 2009/01/13  于作伟  新规作成
    /// </remarks>
    public class P3090P0040 : PageDataBase
    {
        public P3090P0040()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            //从数据库中取得用户域名一览
            DataTable dt = (new BS_DOMAIN(conn)).GetDomainList(RegistId);
            //取得域名
            Domain = dt;

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

        //保存提交数据
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();

            BS_DOMAIN bs_domain = new BS_DOMAIN(conn);
            bs_domain.RemoveDomainList(RegistId);

            DataTable dt = Domain;
            int count = 0;
            foreach (DataRow r in dt.Rows)
            {
                bs_domain.AddDomain(bs_domain.GetNextSeqNumber() + count.ToString(), r["WEBDOMAIN"].ToString(), RegistId);
                count++;
            }
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 域名是否存在数据库
        /// </summary>
        /// <returns></returns>
        public bool IsDomainExist(String registId,String domainName)
        {
            DBConnect conn = this.GetDbConnect();
            BS_DOMAIN bs_domain = new BS_DOMAIN(conn);
            DataTable dt = bs_domain.GetDomain(registId,domainName);
            conn.close();
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 返回域名
        /// </summary>
        public DataTable Domain
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public String RegistId
        {
            get;
            set;
        }
    }
}
