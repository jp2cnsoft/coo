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

namespace Seika.COO.DBA.SV
{
    public class SV_SERVER : Seika.COO.DBA.DateBaseAccess
    {
        public SV_SERVER(DBConnect sql)
            : base(sql)
        {
        }

        public DataTable GetServerInfo(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" SELECT SV_SERVERID ");
            cmd.AppendLine("        ,CASE WHEN HTTP_SV_DOMAIN IS NULL THEN HTTP_SV_IP ELSE HTTP_SV_DOMAIN END AS SERVERHOST");
            cmd.AppendLine("        ,REGCMP_SV_COUNT ");
            cmd.AppendLine(" FROM   SV_SERVER");
            cmd.AppendLine(" WHERE ");
            cmd.AppendLine("        SV_SERVERID = ");
            cmd.AppendLine(" (");
            cmd.AppendLine("       SELECT SV_SERVERID");
            cmd.AppendLine("       FROM   SV_BSSERVER");
            cmd.AppendFormat("       WHERE REGISTID = '{0}'", registId);
            cmd.AppendLine("  )");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
    }
}