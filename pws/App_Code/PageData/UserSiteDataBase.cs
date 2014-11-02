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
using System.Collections.Generic;


namespace Seika.COO.PageData
{
    /// <summary>
    /// 创建网站
    /// </summary>
    /// <remarks>
    /// 2008/10/20  李博
    /// </remarks>
    public class UserSiteDataBase : PageDataBase
    {
        public UserSiteDataBase()
            : base()
        {
            
        }

        public DataTable GetLanguageList(String registId)
        {
            DBConnect conn = this.GetDbConnect();
            MA_LANGUAGE ma_language = new MA_LANGUAGE(conn);
            DataTable dt = ma_language.GetRegisterLanguage(registId);
            conn.close();
            return dt;
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