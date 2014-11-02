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
    /// 管理中心 风格上传
    /// </summary>
    /// <remarks>
    /// 2009/03/23  李博
    /// </remarks>
    public class P9010P0050 : PageDataBase
    {
        public P9010P0050()
            : base()
        {
            
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();
            MA_STYLE ma_style = new MA_STYLE(conn);
            DataTable dt = ma_style.GetStyle(Lang);

            if (dt != null && dt.Rows.Count > 0)
            {
                StyleMaxId = dt.Rows[0]["MA_STYLEID"].ToString();
            }

            conn.close();
            
            Style = dt;
            
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();
            MA_STYLE ma_style = new MA_STYLE(conn);
            //追加
            if (Type == "M")
            {
                ma_style.SaveStyle(StyleId, StyleName, XslName, Lang, PicName);
                conn.Commit();
            }
            else if (Type == "N")
            //更新
            {
                ma_style.UpdateStyle(StyleId, StyleName, XslName, PicName);
                conn.Commit();
            }
            DataTable dt = ma_style.GetStyle(Lang);

            if (dt != null && dt.Rows.Count > 0)
            {
                StyleMaxId = dt.Rows[0]["MA_STYLEID"].ToString();
            }

            Style = dt;

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
        /// 语言
        /// </summary>
        public String Lang
        {
            set;
            get;
        }

        /// <summary>
        /// 风格
        /// </summary>
        public DataTable Style
        {
            set;
            get;
        }

        //保存类型
        public String Type
        {
            set;
            get;
        }

        //风格ID
        public String StyleId
        {
            set;
            get;
        }

        //风格名
        public String StyleName
        {
            set;
            get;
        }

        //XSL名
        public String XslName
        {
            set;
            get;
        }

        //图片名
        public String PicName
        {
            set;
            get;
        } 

        //最大ID
        public String StyleMaxId
        {
            set;
            get;
        }

    }
}