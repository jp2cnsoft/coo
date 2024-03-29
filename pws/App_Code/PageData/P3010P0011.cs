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
    /// 公司注册成功显示
    /// </summary>
    /// <remarks>
    /// 2008/02/26  于作伟  新规作成
    /// </remarks>
    public class P3010P0011 : PageDataBase
    {
        String register = String.Empty;
        String password = String.Empty;
        String serverHost = String.Empty;
        String serverEmail = String.Empty;

        public P3010P0011()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();
            serverHost = @"http://" + register + @"." + m_hpDomain;
            //从数据库中取得域名数据
            //DataTable dt = (new SV_SERVER(conn)).GetServerInfo(register);
            //foreach (DataRow row in dt.Rows)
            //{
            //    //公司帐号
            //    serverHost = @"http://" + register + @"." + row["SERVERHOST"].ToString();
            //    //公司邮箱
            //    serverEmail = register + @"@" + row["SERVERHOST"].ToString();
            //}

            //从数据库中取得公司数据
            DataTable dtp = (new MA_REGISTER(conn)).GetRegisterPassWord(register);
            foreach (DataRow row in dtp.Rows)
            {
                //公司密码
                password = row["PASSWORD"].ToString();
            }

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
        /// 公司帐号
        /// </summary>
        public String Register
        {
            get { return register; }
            set { register = value; }
        }

        /// <summary>
        /// 公司密码
        /// </summary>
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 公司帐号
        /// </summary>
        public String ServerHost
        {
            get { return serverHost; }
            set { serverHost = value; }
        }

        /// <summary>
        /// 公司邮箱
        /// </summary>
        public String ServerEmail
        {
            get { return serverEmail; }
            set { serverEmail = value; }
        }
    }
}
