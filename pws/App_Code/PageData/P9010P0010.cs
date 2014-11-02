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
    /// 管理中心查询
    /// </summary>
    /// <remarks>
    /// 2008/09/10  李博
    /// </remarks>
    public class P9010P0010 : PageDataBase
    {

        public P9010P0010() : base()
        {
            
        }

        public DataTable GetCompanyInfo(DataTable dt)
        {
             DBConnect conn = this.GetDbConnect();
             MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
             DataRow row = dt.Rows[0];
             DataTable dtCompany = ma_cominfo.GetEnterprisesInfo(
                                  row["VIPID"].ToString(), row["COMPANYNAME"].ToString(), row["PHONE"].ToString(),
                                  row["MAIL"].ToString(), row["CONTACTS"].ToString(), row["CORPORATE"].ToString(),
                                  row["REGBEGIN"].ToString(), row["REGEND"].ToString(), row["EXPBEGIN"].ToString(),
                                  row["EXPEND"].ToString(), row["SERVICESDATE"].ToString(), row["SERVICESMONTH"].ToString(),
                                  row["COUNTRYID"].ToString(), row["PROVINCEID"].ToString(), row["CITYID"].ToString(),
                                  row["BOROUGHID"].ToString(), row["MA_CALLINGCLASSID"].ToString(), row["LANG"].ToString(),
                                  row["PAYSTATE"].ToString());


        
            conn.close();
            
            return dtCompany;
            
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            throw new System.Exception("The method or operation is not implemented.");
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
        /// 清空用户数据库数据
        /// </summary>
        /// <param name="userid">用户ID</param>
        public void Remove(String userid)
        {
            DBConnect conn = this.GetDbConnect();

            //删除公司表
            (new MA_COMINFO(conn)).DeleteCominfo(userid);
            //删除注册表
            (new MA_REGISTER(conn)).DeleteRegister(userid);
            //删除网站语言表
            (new BS_WEBLANG(conn)).DelWebLang(userid);
            //删除用户帐户表
            (new MA_ACCOUNT(conn)).DeleteAccount(userid);
            (new MA_BANKLIST(conn)).DeleteBankList(userid);
            //删除用户服务表
            (new MA_MEMBER(conn)).DelMember(userid);
            //删除招聘表
            (new MA_POST(conn)).DeleteJob(userid);
            //删除产品表
            (new MA_PRODUCT(conn)).DeleteProd(userid);
            //删除用户XML
            (new MA_USERXML(conn)).DeleteXml(userid);
            //删除点击率表
            (new MA_VIEWCOUNT(conn)).DeleteViewCount(userid);

            conn.Commit();
            conn.close();
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
        /// 用户名 
        /// </summary>
        public String RegistId
        {
            set;
            get;
        }
    }
}