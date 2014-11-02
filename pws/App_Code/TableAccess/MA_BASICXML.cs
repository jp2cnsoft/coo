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
    public class MA_BASICXML : Seika.COO.DBA.DateBaseAccess
    {
        public MA_BASICXML(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        /// <summary>
        /// 取得基础xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetBasicXmlList(String language,String havingFlg,String xmlName)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT XMLCON");
            cmd.AppendLine("       ,XMLNAME");
            cmd.AppendLine("   FROM MA_BASICXML");
            cmd.AppendFormat(" WHERE LANG = '{0}'", language);
            cmd.AppendFormat(" AND HAVINGFLG = '{0}'", havingFlg);
            cmd.AppendFormat(" AND XMLNAME = '{0}'", xmlName);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得行业基础xml
        /// </summary>
        /// <param name="language"></param>
        /// <param name="partitionflg"></param>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        public DataTable GetBasicXmlListPart(String language, String partitionflg, String xmlName)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT XMLCON");
            cmd.AppendLine("       ,XMLNAME");
            cmd.AppendLine("   FROM MA_BASICXML");
            cmd.AppendFormat(" WHERE LANG = '{0}'", language);
            cmd.AppendFormat(" AND PARTITIONFLG = '{0}'", partitionflg);
            cmd.AppendFormat(" AND XMLNAME = '{0}'", xmlName);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得基础xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetBasicXmlList(String language, String havingFlg)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT XMLCON");
            cmd.AppendLine("       ,XMLNAME");
            cmd.AppendLine("   FROM MA_BASICXML");
            cmd.AppendFormat(" WHERE LANG = '{0}'", language);
            cmd.AppendFormat(" AND HAVINGFLG = '{0}'", havingFlg);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 取得基础xml行业类别列表
        /// </summary>
        /// <param name="xmlName"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetBasicClassList(String xmlName, String language)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT PARTITIONFLG");
            cmd.AppendLine("       ,EXPLAIN");
            cmd.AppendLine("       ,DEFAULTFLG");
            cmd.AppendLine("   FROM MA_BASICXML");
            cmd.AppendFormat(" WHERE LANG = '{0}'", language);
            cmd.AppendFormat(" AND XMLNAME = '{0}'", xmlName);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得基本website的xml
        /// </summary>
        /// <param name="havingFlg"></param>
        /// <returns></returns>
        public DataTable GetBasicWebSiteList(String havingFlg)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT ");
            cmd.AppendLine("       XMLNAME");
            cmd.AppendLine("       ,XMLCON");
            cmd.AppendLine("       ,LANG");
            cmd.AppendLine("   FROM MA_BASICXML");
            cmd.AppendFormat(" WHERE HAVINGFLG = '{0}'", havingFlg);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
    }
}