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

namespace Seika.COO.DBA.BS
{
    public class BS_TASKMANAGE : Seika.COO.DBA.DateBaseAccess
    {
        public BS_TASKMANAGE(DBConnect sql)
            : base(sql)
	    {
		  
	    }
        //插入事务
        public bool InsertTask( String taskid, String belongtype,String correlationid,String referuserid,
                                String referstructuserid, String refersdeleteflg, String remark,String slaveid,
                                String acceptuserid,String acceptstuctuserid,String acceptdeleteflg,String dealstate,
                                String attestationflg, String createuserid, ref String ma_id)
        {

            StringBuilder orgsql = new StringBuilder();
            ma_id = GetNextSeqNumber();
            orgsql.AppendLine("  insert into BS_TASKMANAGE");
            orgsql.AppendLine("  (BS_TASKMANAGEID,MA_ONLINE_TASKID,BELONGTYPE,CORRELATIONID,REFERUSERID,REFERSTRUCTUREID,");
            orgsql.AppendLine("  REFERDELETEFLG,REMARK,SLAVEID,ACCEPTUSERID,ACCEPTSTRUCTUREID,ACCEPTDELETEFLG,");
            orgsql.AppendLine("  DEALSTATE,ATTESTATIONFLG,UPDATENO,CREATEUSERID,CREATEDATE)");
            orgsql.AppendLine("  values");
            orgsql.AppendFormat("  ('{0}','{1}','{2}','{3}','{4}','{5}',", ma_id, taskid, belongtype, correlationid, referuserid, referstructuserid);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}','{5}',", refersdeleteflg, remark, slaveid, acceptuserid, acceptstuctuserid, acceptdeleteflg);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}',GetDate())", dealstate, attestationflg, '1', createuserid);          
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新事务       
        public bool UpdateTask(String taskmanageid,String taskid, String belongtype, String correlationid, String referuserid,
                                 String referstructuserid, String refersdeleteflg, String remark, String slaveid,
                                 String acceptuserid, String acceptstuctuserid, String acceptdeleteflg, String dealstate,
                                 String attestationflg, String updateno, String updateuseid)
        {

            StringBuilder orgsql = new StringBuilder();          
            orgsql.AppendLine("  update BS_TASKMANAGE");
            orgsql.AppendLine("  set");
            orgsql.AppendFormat("  MA_ONLINE_TASKID = '{0}'", taskid);
            orgsql.AppendFormat("  ,BELONGTYPE = '{0}'", belongtype);
            orgsql.AppendFormat("  ,CORRELATIONID = '{0}'", correlationid);
            orgsql.AppendFormat("  ,REFERUSERID = '{0}'", referuserid);
            orgsql.AppendFormat("  ,REFERSTRUCTUREID = '{0}'", referstructuserid);
            orgsql.AppendFormat("  ,REFERDELETEFLG = '{0}'", refersdeleteflg);
            orgsql.AppendFormat("  ,REMARK = '{0}'", remark);
            orgsql.AppendFormat("  ,SLAVEID = '{0}'", slaveid);
            orgsql.AppendFormat("  ,ACCEPTUSERID = '{0}'", acceptuserid);
            orgsql.AppendFormat("  ,ACCEPTSTRUCTUREID = '{0}'", acceptstuctuserid);
            orgsql.AppendFormat("  ,ACCEPTDELETEFLG = '{0}'", acceptdeleteflg);
            orgsql.AppendFormat("  ,DEALSTATE = '{0}'", dealstate);
            orgsql.AppendFormat("  ,ATTESTATIONFLG = '{0}'", attestationflg);
            orgsql.AppendFormat("  ,UPDATENO = '{0}'", GetUpdateNo(updateno));
            orgsql.AppendFormat("  ,UPDATEUSERID = '{0}'", updateuseid);
            orgsql.AppendLine("  ,UPDATEDATE = GetDate()");
            orgsql.AppendFormat("where BS_TASKMANAGEID = '{0}'", taskmanageid);
            orgsql.AppendFormat("and UPDATENO = '{0}'", updateno);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}