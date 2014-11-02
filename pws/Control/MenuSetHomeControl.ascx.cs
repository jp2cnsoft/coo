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
using Seika;
using Seika.COO.Util;
using Seika.Api;
using Seika.ServicesCol;
using Seika.COO.Action;

public partial class Control_MenuSetHomeControl : ControlBase
{
    private int currPageIndex = 0;

    public int CurrPageIndex
    {
        set { currPageIndex = value; }
        get { return currPageIndex; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        String selectImagePath = "~/Images/book02.png";
        String imagePath = "~/Images/book01.png";

        //默认显示图片
        Image1.ImageUrl = imagePath;

        switch (currPageIndex)
        {
            case 0:
                Image1.ImageUrl = selectImagePath;
                break;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/P3010/P3010P0940.aspx");
    }
}
