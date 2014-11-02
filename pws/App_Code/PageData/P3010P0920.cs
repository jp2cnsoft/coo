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
using Seika.Db;
using Seika.COO.Web.PG;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 密码修改页密码取得及修改处理
    /// </summary>
    /// <remarks>
    /// 2008/02/19  范召俊
    /// </remarks>
    public class P3010P0920 : PageDataBase
    {
        Hashtable dts = new Hashtable();
        String registId = String.Empty;
        String newpassword = String.Empty;
        public P3010P0920()            
        {            
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
            DBConnect conn = this.GetDbConnect();
            //从数据库中取得公司密码数据
            DataTable dt = (new MA_REGISTER(conn)).GetRegisterPassWord(registId);            
            dts.Add("MA_COMINFO", dt);
            conn.close();
        }
        //保存提交数据
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            //传递提交的ID和新密码
            if (!(new MA_COMINFO(conn)).SetCompanyPassWord(registId, newpassword))
            {
                throw new System.Exception("ED01000380");
            }
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
        //传递ID值
        public String RegistId
        {
            set { registId = value; }
            get { return registId; }
        }
        //传递新密码的值
        public String Newpassword
        {
            set { newpassword = value; }
            get { return newpassword; }
        }
        //传递从数据库中取得公司密码数据 
        public DataTable MA_COMINFO
        {
            get
            {
                return (DataTable)dts["MA_COMINFO"];
            }
        }
        
    }
}