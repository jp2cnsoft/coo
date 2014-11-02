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
    public class MA_FILEMANAGE : Seika.COO.DBA.DateBaseAccess
    {
	    public MA_FILEMANAGE(DBConnect sql):base(sql)
	    {
		  
	    }

        //插入组织徽标
        public bool InsertOrgImg(String title, String type, byte[] bata, String explan, String cuserid, String id, String odr)
        {           
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_FILEMANAGE");
            orgsql.AppendLine("  (MA_FILEMANAGEID,ORDERID,FILENAME,FILETYPE,FILES,EXPLAIN,");
            orgsql.AppendLine("  UPDATENO,CREATEUSERID,CREATEDATE)");
            orgsql.AppendLine("  values");
            orgsql.AppendFormat("  ('{0}','{1}','{2}','{3}',@FILES,", id, odr, title, type);
            orgsql.AppendFormat("  '{0}','{1}','{2}',GetDate())", explan, 1, cuserid);
            return sql.ExecuteImgQuery(orgsql.ToString(), "@FILES", bata);
        }

        //插入URL
        public bool InsertURL(String title, String type, String url, String explan, String cuserid, String id, String odr)
        {           
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_FILEMANAGE");
            orgsql.AppendLine("  (MA_FILEMANAGEID,ORDERID,FILENAME,FILETYPE,URL,EXPLAIN,");
            orgsql.AppendLine("  UPDATENO,CREATEUSERID,CREATEDATE)");
            orgsql.AppendLine("  values");
            orgsql.AppendFormat("  ('{0}','{1}','{2}','{3}','{4}',", id, odr, title, type, url);
            orgsql.AppendFormat("  '{0}','{1}','{2}',GetDate())", explan, 1, cuserid);          
            return sql.ExecuteStrQuery(orgsql.ToString());
        }             

        //更新图片文件
        public bool UpdateImg(byte[] bata,String explan, String updateno, String uuserid, String id, String odr)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_FILEMANAGE");
            orgsql.AppendLine("  set");
            orgsql.AppendLine("  FILES = @FILES'");    
            orgsql.AppendFormat("  EXPLAIN = '{0}'", explan);
            orgsql.AppendFormat("  ,UPDATENO = '{0}'", GetUpdateNo(updateno));
            orgsql.AppendFormat("  ,UPDATEUSERID = '{0}'", uuserid);
            orgsql.AppendLine("  ,UPDATEDATE = GetDate()");
            orgsql.AppendFormat("where (MA_FILEMANAGEID = '{0}'", id);
            orgsql.AppendFormat("and ORDERID = '{0}'", odr);
            orgsql.AppendFormat("and UPDATENO = '{0}')", updateno);
            return sql.ExecuteImgQuery(orgsql.ToString(), "@FILES", bata);
        }        

        //更新文件
        public bool UpdateFile(String explan, String updateno, String uuserid, String id, String odr)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_FILEMANAGE");
            orgsql.AppendLine("  set");          
            orgsql.AppendFormat("  EXPLAIN = '{0}'", explan);
            orgsql.AppendFormat("  ,UPDATENO = '{0}'", GetUpdateNo(updateno));
            orgsql.AppendFormat("  ,UPDATEUSERID = '{0}'", uuserid);
            orgsql.AppendLine("  ,UPDATEDATE = GetDate()");
            orgsql.AppendFormat("where (MA_FILEMANAGEID = '{0}'", id);
            orgsql.AppendFormat("and ORDERID = '{0}'", odr);
            orgsql.AppendFormat("and UPDATENO = '{0}')", updateno);       
            return sql.ExecuteStrQuery(orgsql.ToString());
        }        

        //删除文件
        public bool DelFile(String id, String odr)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  delete MA_FILEMANAGE");          
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  (MA_FILEMANAGEID = '{0}' and ORDERID = '{1}')", id, odr);
            return sql.ExecuteStrQuery(orgsql.ToString());        
        }        

        //获得图片
        public DataTable GetFiles(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select  * from MA_FILEMANAGE");
            orgsql.AppendFormat("  where MA_FILEMANAGEID = '{0}'", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}