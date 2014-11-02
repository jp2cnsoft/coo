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

namespace Seika.COO.DBA.MA
{
    public class MA_BANKLIST : Seika.COO.DBA.DateBaseAccess
    {
        public MA_BANKLIST(DBConnect sql)
            : base(sql)
        {
        }

        public bool AddBank(String ma_banklistid, String registid, String paymoney, String state)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" MA_BANKLIST ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       MA_BANKLISTID");
            cmd.AppendLine("       ,REGISTID");
            cmd.AppendLine("       ,PAYMONEY");
            cmd.AppendLine("       ,STATE");
            cmd.AppendLine("       ,CREATEDATE)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}',GetDate())",
                ma_banklistid, registid, paymoney, state);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 删除用户帐户
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteBankList(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_BANKLIST");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}