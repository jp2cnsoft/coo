using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Seika.Db;
using System.Configuration;
using System.Web.Configuration;

namespace Seika.COO.COOS
{
    /// <summary>
    /// Summary description for CooWebService
    /// </summary>
    public class COOWebService : System.Web.Services.WebService
    {
        public COOWebService()
            : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected DBConnect GetDbConnect()
        {
            Configuration rootWebConfig =
                WebConfigurationManager.OpenWebConfiguration("/coos");

            ConnectionStringSettings connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings["MSSQLDB"];

            DBConnect conn = new DBConnect(connString.ConnectionString);
            conn.open();

            return conn;
        }

        protected void CloseDbConnect(DBConnect conn)
        {
            if (conn != null)
            {
                conn.close();
            }
        }
    }
}
