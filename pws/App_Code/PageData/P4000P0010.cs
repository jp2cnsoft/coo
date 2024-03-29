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
    /// 服务一览
    /// </summary>
    /// <remarks>
    /// 2008/04/15  于作伟  新规作成
    /// </remarks>
    public class P4000P0010 : PageDataBase
    {
        static Hashtable dts = new Hashtable();

        public P4000P0010()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            ////服务一览数据不存在时
            //if (!dts.ContainsKey("ServiceList"))
            //{
                //从数据库中取得服务一览数据
                DataTable dt = (new MA_SERVICE(conn)).GetServiceList(RegisterId);
                foreach (DataRow row in dt.Rows)
                {
                    //说明
                    String readme = row["README"].ToString();
                    if (!String.IsNullOrEmpty(readme))
                    {
                        if (readme.Length > 36)
                        {
                            row["README"] = readme.Substring(0, 34) + "...";
                        }
                    }
                }
                if (!dts.ContainsKey("ServiceList")) { dts.Add("ServiceList", dt); } else { dts["ServiceList"] = dt; }
            //}

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
        /// ニューズリストデータテーブル
        /// </summary>
        public DataTable ServiceList
        {
            get 
            {
                return (DataTable)dts["ServiceList"];
            }
        }

        /// <summary>
        /// 注册ID
        /// </summary>
        public String RegisterId
        {
            set;
            get;
        }
    }
}
