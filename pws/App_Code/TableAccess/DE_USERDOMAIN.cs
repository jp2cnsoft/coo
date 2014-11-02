﻿using System;
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

namespace Seika.COO.DBA.DE
{
    public class DE_USERDOMAIN : Seika.COO.DBA.DateBaseAccess
    {
        public DE_USERDOMAIN(DBConnect sql)
            : base(sql)
        {
        }

        public bool AddUserDomain(String bs_serviceId, String webdomain, String userdomain)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" DE_USERDOMAIN ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       DE_USERDOMAINID");
            cmd.AppendLine("       ,BS_SERVICEID");
            cmd.AppendLine("       ,WEBDOMAIN");
            cmd.AppendLine("       ,USERDOMAIN)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}')",
                GetNextSeqNumber(), bs_serviceId, webdomain, userdomain);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        public DataTable GetUserDomain()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT  ");
            cmd.AppendLine("       DE_USERDOMAINID  ");
            cmd.AppendLine("       ,BS_SERVICEID  ");
            cmd.AppendLine("       ,WEBDOMAIN  ");
            cmd.AppendLine("       ,USERDOMAIN  ");
            cmd.AppendLine(" FROM DE_USERDOMAIN ");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
    }
}