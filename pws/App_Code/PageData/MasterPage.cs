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
    /// 网站设置模板
    /// </summary>
    /// <remarks>
    /// 2009/05/12  李博
    /// </remarks>
    public class MasterPage : PageDataBase
    {
        public MasterPage()
            : base()
        {
            
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            //DBConnect conn = this.GetDbConnect();
            //MA_SEOKEYWORD ma_seokeyword = new MA_SEOKEYWORD(conn);
            //DataTable dt = ma_seokeyword.GetSeoKeyWord();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    Explain = dt.Rows[0]["EXPLAIN"].ToString();
            //    //修改
            //    State = "update";
            //}
            //else
            //{
            //    //追加
            //    State = "add";
            //}

            //conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            //DBConnect conn = this.GetDbConnect();
            //MA_SEOKEYWORD ma_seokeyword = new MA_SEOKEYWORD(conn);
            
            //switch (State)
            //{
            //        //追加
            //    case "add": ma_seokeyword.SaveSeoKeyWord(m_objStrTool.GetNextSeqNumber(), RegistId, PageId, Explain, Lang);
            //        conn.Commit();
            //        break;
            //        //修改
            //    case "update": ma_seokeyword.UpdateSeoKeyWord(RegistId, Lang, PageId, Explain);
            //        conn.Commit();
            //        break;
            //    default: break;
            //}
            //conn.close();
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
        /// 用户ID
        /// </summary>
        public String RegistId
        {
            set;
            get;
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
        /// 页面ID
        /// </summary>
        public String PageId
        {
            set;
            get;
        }
        /// <summary>
        /// 关键字详细
        /// </summary>
        public String Explain
        {
            set;
            get;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public String State
        {
            set;
            get;
        }
    }
}