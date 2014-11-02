using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Seika.COO.Web.PG;
using System.Collections;

/// <summary>
/// Summary description for Question
/// </summary>
namespace Seika.COO.Web.PG
{
    public class Question
    {
        String _qname;
        String _answer;
        String _answer_1;
        String _qnumber;
        ArrayList checkqname = new ArrayList();

        public String Qname
        {
            set { _qname = value; }
            get { return _qname; }
        }
        public String Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        public String Answer_1
        {
            set { _answer_1 = value; }
            get { return _answer_1; }
        }
        public String Qnumber
        {
            set { _qnumber = value; }
            get { return _qnumber; }
        }
        public ArrayList Canswer
        {
            set { checkqname = value; }
            get { return checkqname; }
        }
    }
}