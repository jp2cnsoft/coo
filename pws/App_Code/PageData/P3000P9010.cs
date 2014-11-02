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
using Seika.COO.DBA.BS;
using Seika.Api;
using Seika.COO.PageData;


namespace Seika.COO.PageData
{
    /// <summary>
    /// 在线联系信息处理
    /// </summary>
    /// <remarks>
    /// 2008/02/19  范召俊
    /// </remarks>
    public class P3000P9010 : PageDataBase
    {
        Hashtable dts = new Hashtable();
        String ltype = String.Empty;
        String title = String.Empty;
        String depict = String.Empty;
        String name = String.Empty;
        String email = String.Empty;
        String state = "1";
        String typeid = String.Empty;
        String img1 = String.Empty;
        String img2= String.Empty;
        public P3000P9010()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            ltype = "custommail";
            DBConnect conn = this.GetDbConnect();
            //取得指定条件数据
            DataTable dt = (new MA_COMINFO(conn)).GetStyle(ltype);
            Resid2Name("CodeManage", ref dt, "NAME");
            //问题类型信息
            dts.Add("MA_CODE", dt);
            conn.close();
            // throw new System.Exception("The method or operation is not implemented.");
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
        }
        //保存提交数据
        public override void Save()
        {            
            DBConnect conn = this.GetDbConnect();
            BS_MANAGE bs_manage = new BS_MANAGE(conn);
            //传递提交信息
            if (!(new MA_COMINFO(conn)).AddCusInfo(bs_manage.GetNextSeqNumber(), typeid, title, depict, name, email, state, img1, img2))
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
        /// <summary>
        /// 简要标题
        /// </summary>
        public String Title
        {
            set {title = value; }
            get { return title; }
        }
        /// <summary>
        /// 详细描述
        /// </summary>
        public String Depict
        {
            set { depict = value; }
            get { return depict; }
        }
        /// <summary>
        /// 称谓
        /// </summary>
        public String Name
        {
            set { name = value; }
            get { return name; }
        }
        /// <summary>
        /// 回复邮箱
        /// </summary>
        public String Email
        {
            set { email = value; }
            get { return email; }
        }
        /// <summary>
        /// 问题类型
        /// </summary>
        public String Typeid
        {
            set { typeid = value; }
            get { return typeid; }
        }
        /// <summary>
        /// 截取图片名1
        /// </summary>
        public String Img1
        {
            set { img1 = value; }
            get { return img1; }
        }
        /// <summary>
        /// 截取图片名2
        /// </summary>
        public String Img2
        {
            set { img2 = value; }
            get { return img2; }
        }
        //问题类型信息
        public DataTable MA_CODE
        {
            get
            {
                return (DataTable)dts["MA_CODE"];
            }
        }
    }
}
