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
    public class MA_CODE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_CODE(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        //获得钱币类型   
        public DataTable GetMoney()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");           
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'BANKROLLUNIT' and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得组织人数
        public DataTable GetOrgScale()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'EMPLOYEENUM' and  DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得组织性质
        public DataTable GetOrgCharacter()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'CHARACTER' and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得经营形式
        public DataTable GetManageMode()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'MANAGEMODE' and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得机构类别
        public DataTable GetStructueClass()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'STRUCTURECLASS' and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得域名列表
        public DataTable GetDomain(String ltype)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  (LTYPE = '{0}' and DELETEFLG is null)", ltype);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得域名列表
        public DataTable GetDomain()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'WEBDOMAIN' OR LTYPE = 'CHINADOMAIN' and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
     
        //性别
        public DataTable GetSex()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'SEX'  and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //学历
        public DataTable GetSchoollevel()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID,EXPLAIN from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'SCHOOLLEVEL'  and DELETEFLG is null ) order by ORDERID");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //工作经验
        public DataTable GetExperiene()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID,EXPLAIN from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'EXPERIENCE'  and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //年龄
        public DataTable GetAge()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID,EXPLAIN from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'AGE'  and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //发布日期
        public DataTable GetIssueDate()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID,EXPLAIN from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'ISSUEDATE'  and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //工作性质
        public DataTable GetWorkkind()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CODEID,NAME,RESID from MA_CODE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("  (LTYPE = 'WORKKIND'  and DELETEFLG is null)");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 按条件取得CODE一览
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="values">值数组</param>
        /// <returns></returns>
        public DataTable GetCodeList(String field,String[] values)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT ");
            orgsql.AppendLine("        MA_CODEID");
            orgsql.AppendLine("       ,NAME");
            orgsql.AppendLine("       ,LTYPE");
            orgsql.AppendLine("  FROM MA_CODE");
            orgsql.AppendLine("  WHERE");
            foreach (String v in values)
            {
                orgsql.AppendFormat("      {0} = '{1}'", field, v);
                if (v != values[values.Length - 1]) { orgsql.AppendFormat(" OR "); }
            }
            orgsql.AppendLine(" AND      DELETEFLG IS NULL");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}