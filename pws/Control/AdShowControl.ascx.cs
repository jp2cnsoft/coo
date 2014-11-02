using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Seika.COO.Web.PG;

public partial class Control_AdShowControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitControl();
    }

    private void InitControl()
    {
        SessionManager sessionManager = new SessionManager(Session);
        imgAd.ImageUrl = String.Format("../Images/{0}_adPic.png", sessionManager.Page_UICultureID);
    }
}
