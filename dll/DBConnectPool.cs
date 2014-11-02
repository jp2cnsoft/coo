using System;
using System.Collections.Generic;
using System.Text;

namespace Seika.Db
{
    public class DBConnectPool
    {
        static string connStr = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");

        public static DBConnect GetConnect()
        {
            return new DBConnect(connStr);
        }
    }
}
