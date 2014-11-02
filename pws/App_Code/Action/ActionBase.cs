using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Data;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using Seika.COO.DBA.BS;
using System.Data.SqlClient;
using Seika.Util;
using Seika.Db;
using Seika.CooException;
using Seika.COO.Web.PG;

namespace Seika.COO.Action
{
    public abstract class ActionBase : System.Web.UI.Page
    {
        public abstract DataSet Run(DBConnect sql, DataSet cds, String[] oparms);

        //数据库连接字符串
        public static String strconn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        //action路径
        public static String actionPath = CodeSymbol.m_actionPath;
    }
}