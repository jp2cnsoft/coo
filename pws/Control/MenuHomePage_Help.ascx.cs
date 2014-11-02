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
using Seika.COO.Web.PG;

public partial class Control_MenuHomePage_Help : System.Web.UI.UserControl
{
    /// <summary>
    /// 显示类型 HELP;LINK
    /// </summary>
    public String ShowType
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (ShowType.ToUpper())
        {
            //帮助可见
            case "HELP":
                pnlHelp.Visible = true;
                break;
            //联系我们可见
            case "LINK":
                pnlAbout.Visible = true;
                break;

        }
        //如果当前语言为中文则显示TM链接
        SessionManager session = new SessionManager(Session);
        if (session.Page_UICultureID == "CHINA")
            pnlTm.Visible = true;

        //背景图片多国语
        //leftMenuHead.Style["background"] = String.Format(@"url('../../Images/{0}_menuHead.png') no-repeat ", session.Page_UICultureID);
        //leftMenuHeadLink.Style["background"] = String.Format(@"url('../../Images/{0}_menuHeadLink.png') no-repeat ", session.Page_UICultureID);
    }
}
