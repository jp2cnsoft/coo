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
    public class MA_USERXML : Seika.COO.DBA.DateBaseAccess
    {
        public MA_USERXML(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        /// <summary>
        /// 清理全部基础XML数据
        /// </summary>
        public bool DeleteBasicUserXml(String registId, String language)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("  DELETE FROM MA_USERXML");
            cmd.AppendFormat("       WHERE LANG = '{0}'", language);
            cmd.AppendFormat("       AND REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
       
        /// <summary>
        /// 插入全部基础XML数据
        /// </summary>
        public bool InsertBasicUserXml(String registId, String language,String tradeName, String havingFlg)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("  INSERT INTO MA_USERXML");
            cmd.AppendLine(" 	   SELECT XMLNAME  ");
            cmd.AppendFormat(" 	          ,'{0}' ", registId);
            cmd.AppendLine("              ,LANG");
            cmd.AppendLine("              ,XMLCON");
            cmd.AppendLine("              ,getdate()");
            cmd.AppendLine("       FROM MA_BASICXML");
            cmd.AppendFormat("       WHERE LANG = '{0}'", language);
            cmd.AppendFormat("       AND HAVINGFLG = '{0}'", havingFlg);
            cmd.AppendFormat("       AND (PARTITIONFLG = '{0}'", tradeName);
            cmd.AppendLine("         OR PARTITIONFLG IS NULL)");

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 取得全部用户xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetUserXmlList(String registId, String language)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT XMLCON");
            cmd.AppendLine("       ,MA_USERXMLID");
            cmd.AppendLine("   FROM MA_USERXML");
            cmd.AppendFormat(" WHERE REGISTID = '{0}'", registId);
            cmd.AppendFormat(" AND LANG = '{0}'", language);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得单个xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="xmlId"></param>
        /// <returns></returns>
        public DataTable GetUserXml(String registId, String language,String xmlId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT XMLCON");
            cmd.AppendLine("       ,MA_USERXMLID");
            cmd.AppendLine("   FROM MA_USERXML");
            cmd.AppendFormat(" WHERE REGISTID = '{0}'", registId);
            cmd.AppendFormat(" AND LANG = '{0}'", language);
            cmd.AppendFormat(" AND MA_USERXMLID = '{0}'", xmlId);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 更新用户xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="xmlId"></param>
        /// <param name="xmlCon"></param>
        public bool UpdateUserXml(String registId, String language,String xmlId,String xmlCon)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" 	   UPDATE MA_USERXML  ");
            cmd.AppendLine(" 	          SET  ");
            cmd.AppendFormat("                  XMLCON = N'{0}'", xmlCon);
            cmd.AppendFormat("            WHERE REGISTID = '{0}'", registId);
            cmd.AppendFormat("                  AND   LANG = '{0}'", language);
            cmd.AppendFormat("                  AND   MA_USERXMLID = '{0}'", xmlId);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        public bool DeleteUserXml(String registId, String language)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" 	   DELETE FROM MA_USERXML  ");
            cmd.AppendFormat("            WHERE REGISTID = '{0}'", registId);
            cmd.AppendFormat("                  AND   LANG = '{0}'", language);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 删除用户XML表
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteXml(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_USERXML");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}