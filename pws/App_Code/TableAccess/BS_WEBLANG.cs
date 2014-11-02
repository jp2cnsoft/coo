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
    public class BS_WEBLANG : Seika.COO.DBA.DateBaseAccess
    {
        public BS_WEBLANG(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        /// <summary>
        /// 取得语言信息
        /// </summary>
        public DataTable GetWebLang(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    BS_WEBLANGID ");
            cmd.AppendLine(" 	    ,REGISTID ");
            //cmd.AppendLine(" 	    ,LANGRES ");
            cmd.AppendLine(" 	    ,MA_STYLEID ");
            cmd.AppendLine(" 	    ,DEFAULFLG ");
            cmd.AppendLine(" 	    ,OPENFLG ");
            cmd.AppendLine(" 	    ,UPDATEDATE ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        BS_WEBLANG ");
            cmd.AppendFormat("  WHERE REGISTID = '{0}' ", register);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得风格ID
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public DataTable GetWebLang(String register,String lang)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    MA_STYLEID ");
            cmd.AppendLine(" 	    ,PARTITIONFLG ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        BS_WEBLANG ");
            cmd.AppendFormat("  WHERE REGISTID = '{0}' ", register);
            cmd.AppendFormat("  and MA_LANGUAGEID = '{0}'", lang);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        //添加语言信息
        public bool InsertWebLang(String registId, String languageId, String stytleId, String defaulflg, 
            String openflg, String weblangId,String profession)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("insert into BS_WEBLANG");
            orgsql.AppendLine("  (BS_WEBLANGID,REGISTID,MA_LANGUAGEID,MA_STYLEID,PARTITIONFLG,DEFAULFLG,OPENFLG,CREATEDATE,UPDATEDATE) values");
            orgsql.AppendLine("('" + weblangId + "','" + registId + "','" + languageId + "','" + stytleId + "','" + profession + "','" + defaulflg + "','" + openflg + "',getdate() ,getdate())");
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

       /// <summary>
       /// 关闭网站更新
       /// </summary>
       /// <param name="registId"></param>
       /// <param name="lang"></param>
       /// <param name="openFlg"></param>
       /// <returns></returns>
       public bool UpdataCloseFlg(String registId, String lang,String openFlg)
       {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  UPDATE BS_WEBLANG");
            orgsql.AppendFormat("  SET    OPENFLG = '{0}'", openFlg);
            orgsql.AppendLine("  WHERE");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            orgsql.AppendLine("  AND");
            orgsql.AppendFormat("    MA_LANGUAGEID = '{0}'", lang);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 设置更新默认语言
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public bool UpdataDefaulFlg(String registId, String lang)
        {
           StringBuilder orgsql = new StringBuilder();
           orgsql.AppendLine("  UPDATE BS_WEBLANG");
           orgsql.AppendLine("  SET    DEFAULFLG = '1'");
           orgsql.AppendLine("  WHERE");
           orgsql.AppendFormat("    REGISTID = '{0}'", registId);
           orgsql.AppendLine("  AND");
           orgsql.AppendFormat("    MA_LANGUAGEID = '{0}'", lang);
           return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 设置更新为未默认语言
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>
        public bool UpdataNoneDefaulFlg(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  UPDATE BS_WEBLANG");
            orgsql.AppendLine("  SET    DEFAULFLG = ''");
            orgsql.AppendLine("  WHERE");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

       /// <summary>
       /// 删除网站信息
       /// </summary>
       /// <param name="registId"></param>
       /// <param name="lang"></param>
       /// <returns></returns>
       public bool DeleteWebLang(String registId, String lang)
       {
           StringBuilder orgsql = new StringBuilder();
           orgsql.AppendLine("  DELETE ");
           orgsql.AppendLine("      FROM    BS_WEBLANG");
           orgsql.AppendLine("  WHERE");
           orgsql.AppendFormat("    REGISTID = '{0}'", registId);
           orgsql.AppendLine("  AND");
           orgsql.AppendFormat("    MA_LANGUAGEID = '{0}'", lang);
           return sql.ExecuteStrQuery(orgsql.ToString());
       }

       /// <summary>
       /// 改变风格
       /// </summary>
       /// <param name="registId"></param>
       /// <param name="lang"></param>
       /// <returns></returns>
       public bool UpdateWebLangStyle(String styleId,String partition,String registId, String lang)
       {
           StringBuilder orgsql = new StringBuilder();
           orgsql.AppendLine("  UPDATE  BS_WEBLANG ");
           orgsql.AppendFormat("      SET    MA_STYLEID = '{0}'", styleId);
           orgsql.AppendFormat("      ,      PARTITIONFLG = '{0}'", partition);
           orgsql.AppendLine("  WHERE");
           orgsql.AppendFormat("    REGISTID = '{0}'", registId);
           orgsql.AppendLine("  AND");
           orgsql.AppendFormat("    MA_LANGUAGEID = '{0}'", lang);
           return sql.ExecuteStrQuery(orgsql.ToString());
       }

       /// <summary>
       /// 取得激活语言信息
       /// </summary>
       public DataTable GetLang(String register)
       {
           StringBuilder cmd = new StringBuilder();
           cmd.AppendLine("    SELECT ");
           cmd.AppendLine(" 	    MA_LANGUAGEID  AS  LANG");
           cmd.AppendLine("    FROM");
           cmd.AppendLine("        BS_WEBLANG ");
           cmd.AppendFormat("  WHERE REGISTID = '{0}' ", register);

           return sql.ExecuteDataset(cmd.ToString()).Tables[0];
       }

       /// <summary>
       /// 删除网站语言
       /// </summary>
       /// <param name="registerId"></param>
       /// <returns></returns>
       public bool DelWebLang(String registerId)
       {
           StringBuilder orgsql = new StringBuilder();
           orgsql.AppendLine("   DELETE");
           orgsql.AppendLine("        FROM BS_WEBLANG");
           orgsql.AppendLine("   WHERE");
           orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

           return sql.ExecuteStrQuery(orgsql.ToString());
       }
    }
}