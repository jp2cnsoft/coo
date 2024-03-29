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
using Seika.COO.DBA.DE;
using Seika.Api;
using Seika;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 主页显示数据取得及保持处理
    /// </summary>
    /// <remarks>
    /// 2008/01/02  金光哲  新规作成
    /// </remarks>
    public class SeoKeyWordCountrol : PageDataBase
    {
        public SeoKeyWordCountrol()
            : base()
        {
        }

        // 添加关键字
        public void AddSeoKeyWord(String id, String registid, String pageId, String explain, String lang)
        {
            MA_SEOKEYWORD ms = new MA_SEOKEYWORD(GetDbConnect());
            ms.SaveSeoKeyWord(id, registid, pageId, explain, lang);
        }

        // 更新关键字
        public void UpdateSeoKeyWord(String detailId, String lang, String pageId, String registId, String explain)
        {
            MA_SEOKEYWORD ms = new MA_SEOKEYWORD(GetDbConnect());
            ms.UpdateSeoKeyWord(detailId, lang, pageId, registId, explain);
        }

        public DataTable FindSeoKeyWordById(String detailId, String lang, String pageId, String registId)
        {
            MA_SEOKEYWORD ms = new MA_SEOKEYWORD(GetDbConnect());
            return ms.GetSeoKeyWord(detailId, lang, pageId, registId);
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {

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

        
    }
}
