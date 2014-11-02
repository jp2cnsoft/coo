using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using System.IO;
using System.Data.SqlClient;
using Seika.Db;
using Seika.Util;
using Seika.CooException;
using Seika.COO.DBA.BS;
using Seika.COO.Web.PG;
namespace Seika.COO.Action
{
    /// <summary>
    /// Summary description for OrgCode
    /// </summary>
    public class OrgCode : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            res = new DataSet();
            cmdt = new DataSetManage();
            ma_code = new MA_CODE(sql);
            string str = null;
            for(int i=0;i<oparms.Length;i++)
            {
                if (oparms[i] != null || oparms[i].Equals(""))
                {
                    str = oparms[i];
                }
            }
            switch (str) 
            {
                    case "SEX":
                    res.Tables.Add(cmdt.GetCloneTable(ma_code.GetSex(), "SEX"));
                        break;
                    case "SCHOOLLEVEL":
                        res.Tables.Add(cmdt.GetCloneTable(ma_code.GetSchoollevel(), "SCHOOLLEVEL"));
                        break;
                    case "WORKKIND":
                        res.Tables.Add(cmdt.GetCloneTable(ma_code.GetWorkkind(), "WORKKIND"));
                        break;
                    case "ISSUEDATE":
                        res.Tables.Add(cmdt.GetCloneTable(ma_code.GetIssueDate(), "ISSUEDATE"));
                        break;
                    case "AGE":
                        res.Tables.Add(cmdt.GetCloneTable(ma_code.GetAge(), "AGE"));
                        break;
                    case "EXPERIENE":
                        res.Tables.Add(cmdt.GetCloneTable(ma_code.GetExperiene(), "EXPERIENE"));
                        break;
                }
          
            //返回结果集
            return res;
            
        }
    }
}
