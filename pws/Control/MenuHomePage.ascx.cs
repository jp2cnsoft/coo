using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Control_MenuHomePage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ImageButton1.Attributes.Add("onclick", "window.open('http://www.chinabank.com.cn');");
        }

        hlnkLink.NavigateUrl =
            "http://www.{domainname}/P3000P0150.htm".Replace("{domainname}", 
                System.Configuration.ConfigurationManager.AppSettings["DomainName"]);

        hlnkAd.NavigateUrl =
            "http://seika.{domainname}/System/html_WebPage/P3000P8030.html".Replace("{domainname}",
                System.Configuration.ConfigurationManager.AppSettings["DomainName"]);

    }
}
