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
    public class MA_STYLE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_STYLE(DBConnect sql)
            : base(sql)
	    {
		  
	    }
       
        /// <summary>
        /// 取得默认样式ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetDefaultInfo()
        {
            StringBuilder cmd = new StringBuilder();

            cmd.AppendLine(" SELECT MA_STYLEID ");
            cmd.AppendLine("        ,XSLNAME ");
            cmd.AppendLine("        ,CSSNAME ");
            cmd.AppendLine(" FROM MA_STYLE");
            cmd.AppendLine(" WHERE DEFAULFLG = '1'");
            cmd.AppendLine(" AND DELFLG IS NULL");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得当前用户样式类型信息
        /// </summary>
        public DataTable GetCurrentStyle(String styleId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    MA_STYLEID");
            cmd.AppendLine(" 	    ,PICNAME");
            cmd.AppendLine(" 	    ,STYLENAME");
            cmd.AppendLine(" 	    ,EXPLAIN");
            cmd.AppendLine("        ,RESID");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_STYLE");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" MA_STYLEID = '{0}'", styleId);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得风格信息
        /// </summary>
        public DataTable GetAllStyle(String lang)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    a.MA_STYLEID");
            cmd.AppendLine(" 	    ,a.PICNAME");
            cmd.AppendLine(" 	    ,a.STYLENAME");
            cmd.AppendLine(" 	    ,a.EXPLAIN");
            cmd.AppendLine("        ,a.RESID");
            cmd.AppendLine("        ,a.REGISTID");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_STYLE a");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" a.MA_LANGUAGEID = '{0}'", lang);
            cmd.AppendLine(" and  isnull(a.DELFLG,'') <> '1'");
            cmd.AppendLine("    ORDER BY CREATEDATETIME DESC");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得风格类别信息
        /// </summary>
        public DataTable GetStyletype()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    CLASSNAME");
            cmd.AppendLine("        ,RESID");
            cmd.AppendLine(" 	    ,MA_STYLECLASSID");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_STYLECLASS");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得风格类别信息
        /// </summary>
        public DataTable GetChooseStyletype(String styletypeid)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    b.MA_STYLEID");
            cmd.AppendLine(" 	    ,b.STYLENAME");
            cmd.AppendLine(" 	    ,b.EXPLAIN");
            cmd.AppendLine(" 	    ,b.PICNAME");
            cmd.AppendLine(" 	   FROM BS_STYLE a");
            cmd.AppendLine(" 	    LEFT JOIN MA_STYLE b");
            cmd.AppendLine(" 	    ON a.MA_STYLEID = b.MA_STYLEID");
            cmd.AppendLine("   WHERE");
            cmd.AppendFormat("    MA_STYLECLASSID = '{0}'", styletypeid);
            cmd.AppendLine("    ORDER BY CREATEDATETIME DESC");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 保存风格类别信息
        /// </summary>
        public bool Updatestyle(String rstyleid,String registid)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    UPDATE");
            cmd.AppendLine(" 	    MA_REGISTER");
            cmd.AppendLine(" 	    SET");
            cmd.AppendFormat(" 	    MA_STYLEID = '{0}'", rstyleid);
            cmd.AppendLine(" 	    WHERE");
            cmd.AppendFormat(" 	   REGISTID = '{0}'", registid);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
        /// <summary>
        /// 取得风格信息
        /// </summary>
        public DataTable GetChooseStyle(String rstyleid)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    XSLNAME");
            cmd.AppendLine(" 	    ,CSSNAME");
            cmd.AppendLine(" 	FROM MA_STYLE");
            cmd.AppendLine("   WHERE");
            cmd.AppendFormat("    MA_STYLEID = '{0}'", rstyleid);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 激活网站取得风格信息
        /// </summary>
        public DataTable GetStyle(String lang, String styleClassId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    a.MA_STYLEID");
            cmd.AppendLine(" 	    ,a.PICNAME");
            cmd.AppendLine("        ,a.STYLENAME");
            cmd.AppendLine("        ,a.XSLNAME");
            cmd.AppendLine("        ,a.REGISTID");
            cmd.AppendLine("        ,a.EXPLAIN");
            cmd.AppendLine("        ,a.RESID");
            cmd.AppendLine(" 	   FROM MA_STYLE a");
            cmd.AppendLine("  LEFT OUTER JOIN ");
            cmd.AppendLine("        BS_STYLE b");
            cmd.AppendLine("  ON ");
            cmd.AppendLine("        b.MA_STYLEID = a.MA_STYLEID");
            cmd.AppendLine("   WHERE");
            cmd.AppendLine("    isnull(a.DELFLG,'') <> '1'");
            if (styleClassId != "")
            {
                cmd.AppendFormat("   AND b.MA_STYLECLASSID = '{0}'  ", styleClassId);
            }
            cmd.AppendFormat("   AND a.MA_LANGUAGEID = '{0}'", lang);
            cmd.AppendLine("    ORDER BY COMMENDFLG DESC, CREATEDATETIME DESC, a.DEFAULFLG DESC");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 风格上传取得风格信息
        /// </summary>
        public DataTable GetStyle(String lang)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT");
            cmd.AppendLine(" 	    a.MA_STYLEID");
            cmd.AppendLine(" 	    ,a.STYLENAME");
            cmd.AppendLine(" 	    ,a.XSLNAME");
            cmd.AppendLine(" 	    ,a.PICNAME");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_STYLE a");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" a.MA_LANGUAGEID = '{0}'", lang);
            cmd.AppendLine(" and  isnull(a.DELFLG,'') <> '1'");
            cmd.AppendLine("    ORDER BY MA_STYLEID DESC");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 风格上传保存风格信息
        /// </summary>
        public bool SaveStyle(String styleid, String stylename, String xslname, String lang, String picname)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    INSERT INTO MA_STYLE (MA_STYLEID, STYLENAME, XSLNAME, CSSNAME, PICNAME, RESID, CREATEDATETIME, STYLELANG, MA_LANGUAGEID)");
            
            cmd.AppendLine(" 	    VALUES");
            cmd.AppendFormat(" 	   ('{0}','{1}','{2}','style','{3}','R001001000',getdate(),'{4}','{5}')", styleid, stylename, xslname, picname, lang, lang);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 风格上传更新风格信息
        /// </summary>
        public bool UpdateStyle(String styleid, String stylename, String xslname, String picname)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    UPDATE");
            cmd.AppendLine(" 	    MA_STYLE");
            cmd.AppendLine(" 	    SET");
            cmd.AppendFormat(" 	    STYLENAME = '{0}'", stylename);
            cmd.AppendFormat(" 	,    XSLNAME = '{0}'", xslname);
            cmd.AppendFormat(" 	,    PICNAME = '{0}'", picname);
            cmd.AppendLine(" 	    WHERE");
            cmd.AppendFormat(" 	   MA_STYLEID = '{0}'", styleid);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
    }
}