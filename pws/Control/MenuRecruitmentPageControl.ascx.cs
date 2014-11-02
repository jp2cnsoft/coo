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

public partial class Control_MenuRecruitmentPageControl : System.Web.UI.UserControl
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
        imgManagementPost.ImageUrl = imgRecruitmentMethod.ImageUrl = imagePath;

        switch (currPageIndex)
        {
            case 0:
                imgManagementPost.ImageUrl = selectImagePath;
                break;
            case 1:
                imgRecruitmentMethod.ImageUrl = selectImagePath;
                break;
        }
    }
    protected void lbtnRecruitmentMethod_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("P3060P0050.aspx");
    }

    protected void lbtnManagementPost_Click(object sender, EventArgs e)
    {

        Response.Redirect("P3060P0200.aspx");
    }
}
