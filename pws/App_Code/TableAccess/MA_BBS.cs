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
    public class MA_BBS : Seika.COO.DBA.DateBaseAccess
    {
        public MA_BBS(DBConnect sql)
            : base(sql)
	    {
		  
	    }     
    }
}