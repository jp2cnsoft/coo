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
using Seika.COO.Web.PG;

namespace Seika.COO.DBA
{
    public class DateBaseAccess
    {
        public DBConnect sql = null;
        private ObjectStringTool m_objStrTol = new ObjectStringTool();
        public DateBaseAccess(DBConnect sql)
        {
            this.sql = sql;
        }
        //算ID
        public String GetNextSeqNumber()
        {
            return m_objStrTol.GetNextSeqNumber();
        }

        //算UPDATENO
        public String GetUpdateNo(String no)
        {
            UInt64 tempno = Convert.ToUInt64(no); 
            tempno = tempno + 1;
            if (tempno > 999)
            {
                tempno = 1;
            }
            return tempno.ToString();
        }

        //public override string ToString()
        //{
        //    return ;
        //}   
    }
}
