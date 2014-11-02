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

public partial class Control_MenuServicePageControl : ControlBase
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
        imgServiceList.ImageUrl = imgServiceDrive.ImageUrl = imgServiceMy.ImageUrl = imagePath;

        switch (currPageIndex) 
        {
            case 0:
                imgServiceList.ImageUrl = selectImagePath;
                break;
            case 1:
                imgServiceDrive.ImageUrl = selectImagePath;
                break;
            case 2:
                imgServiceMy.ImageUrl = selectImagePath;
                break;
        }
    }

    protected void lbtnServiceList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/P4000/P4000P0010.aspx");
    }
    //protected void lbtnServiceDrive_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Pages/P4000/P4000P0040.aspx");
    //}
    protected void lbtnServiceMy_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/P4000/P4000P0020.aspx");
    }
}
