using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.Db;

namespace Seika.COO.DBA.MA
{
    public class MA_SEOKEYWORD : Seika.COO.DBA.DateBaseAccess
    {
        public MA_SEOKEYWORD(DBConnect sql)
            : base(sql)
        {
            
        }

        /// <summary>
        /// 取得关键字信息
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="lang"></param>
        /// <param name="pageId"></param>
        /// <param name="registId"></param>
        /// <returns></returns>
        public DataTable GetSeoKeyWord(String detailId, String lang, String pageId, String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    DETAILID ");
            cmd.AppendLine(" 	    ,EXPLAIN ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_SEOKEYWORD ");
            cmd.AppendFormat("  WHERE DETAILID = '{0}' ", detailId);
            cmd.AppendFormat("  AND LANG = '{0}' ", lang);
            cmd.AppendFormat("  AND PAGEID = '{0}' ", pageId);
            cmd.AppendFormat("  AND REGISTID = '{0}' ", registId);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得当前用户当前语言的seo关键字
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetSeoKeyWord(String registId,String language)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    DETAILID ");
            cmd.AppendLine(" 	    ,PAGEID ");
            cmd.AppendLine(" 	    ,EXPLAIN ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_SEOKEYWORD ");
            cmd.AppendFormat("  WHERE REGISTID = '{0}' ", registId);
            cmd.AppendFormat("  AND LANG = '{0}' ", language);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="registid">用户ID</param>
        /// <param name="pageid">页面ID</param>
        /// <param name="explain">关键字详细</param>
        /// <param name="lang">语言</param>
        /// <returns></returns>
        public void SaveSeoKeyWord(String id, String registid, String pageid, String explain, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("insert into MA_SEOKEYWORD");
            orgsql.AppendLine("  (DETAILID, REGISTID, LANG, PAGEID, EXPLAIN, UPDATEDATE) values");
            orgsql.AppendLine("('" + id + "','" + registid + "','" + lang + "','" + pageid + "','" + explain + "',getdate())");
            sql.ExecuteStrQuery(orgsql.ToString()); 
            sql.Commit();
        }

        /// <summary>
        /// 更新关键字信息
        /// </summary>
        /// <param name="registid">用户ID</param>
        /// <param name="lang">语言</param>
        /// <param name="pageid">页面ID</param>
        /// <param name="explain">关键字详细</param>
        /// <returns></returns>
        public void UpdateSeoKeyWord(String detailId,String lang, String pageId, String registId, String explain)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  UPDATE MA_SEOKEYWORD");
            orgsql.AppendFormat("  SET    EXPLAIN = '{0}'", explain);
            orgsql.AppendLine("  ,UPDATEDATE = getdate()");
            orgsql.AppendLine("  WHERE");
            orgsql.AppendFormat("    DETAILID = '{0}'", detailId);
            orgsql.AppendLine("  AND");
            orgsql.AppendFormat("    LANG = '{0}'", lang);
            orgsql.AppendLine("  AND");
            orgsql.AppendFormat("    PAGEID = '{0}'", pageId);
            orgsql.AppendLine("  AND");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            sql.ExecuteStrQuery(orgsql.ToString());
            sql.Commit();
        }

        /// <summary>
        /// 删除关键字信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteSeoKeyWord(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_SEOKEYWORD");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}