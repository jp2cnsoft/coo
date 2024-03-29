using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.Common.Net;
//using Seika.Api;

namespace Seika.COO.PageData
{
    /// <summary>
    /// Summary description for PageDataBase
    /// </summary>
    public abstract class PageDataBase : PageBase
    {
        protected SessionManager sm = null;

        public PageDataBase()
        {
        }

        public PageDataBase(SessionManager sm)
        {
            this.sm = sm;
        }

        /// <summary>
        /// ページデータ初期処理
        /// </summary>
        public abstract void Init();
        
        /// <summary>
        /// ページデータ削除
        /// </summary>
        public abstract void Remove();
        
        /// <summary>
        /// ページデータ取得
        /// </summary>
        public abstract void Load();
        
        /// <summary>
        /// ページデータ保存
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// ページデータ捨てる
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// フラグ更新
        /// </summary>
       // public ReloadDataFlag reloadFlag = new ReloadDataFlag();

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static PageDataBase GetInstance(PageBase cls)
        {
            String clsName = cls.GetType().BaseType.Name;

            if (clsName.IndexOf("_") > 0)
            {
                clsName = clsName.Substring(clsName.LastIndexOf("_") + 1);
            }

            return GetInstance(clsName);
        }

        public static PageDataBase GetInstance(String clsName)
        {
            Type type = System.Type.GetType(
                String.Format("Seika.COO.PageData.{0}_PageData", clsName));

            return (PageDataBase)Activator.CreateInstance(type);
        }
        */

        protected DBConnect GetDbConnect ()
        {
            DBConnect sql = DBConnectPool.GetConnect();
            sql.open();
            return sql;
        }

        protected String StringToFilter(String str)
        {
            ObjectStringTool objectStringTool = new ObjectStringTool();
            return objectStringTool.StringToFilter(str);
        }

        protected void Resid2Name(String globalResName, ref DataTable dt, String strName)
        {
            ResManager rsMansge = new ResManager();
            int count = dt.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                String resid = dt.Rows[i]["RESID"].ToString();
                String name = rsMansge.GetGlobalRes(globalResName, resid);
                if (!String.IsNullOrEmpty(name))
                {
                    dt.Rows[i][strName] = name;
                }
                else
                {
                    dt.Rows.RemoveAt(i);
                }
            }
        }
    }
}

