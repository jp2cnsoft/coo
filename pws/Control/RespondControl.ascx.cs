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
using System.Xml;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;

public partial class Control_RespondControl : ControlBase
{
    /// <summary>
    /// 操作xml
    /// </summary>
    public String ClientXml
    {
        set
        {
            ViewState["clientXml"] = value;
        }
        get
        {
            if (ViewState["clientXml"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["clientXml"];
        }
    }

    /// <summary>
    /// 操作回复id
    /// </summary>
    public String MessageTypeId
    {
        set
        {
            ViewState["messagetypeid"] = value;
        }
        get
        {
            if (ViewState["messagetypeid"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["messagetypeid"];
        }
    }

    /// <summary>
    /// 所回复的楼层号
    /// </summary>
    public String ReNum
    {
        set
        {
            ViewState["renum"] = value;
        }
        get
        {
            if (ViewState["renum"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["renum"];
        }
    }

    /// <summary>
    /// 所回复的标题
    /// </summary>
    public String ReHead
    {
        set
        {
            ViewState["rehead"] = value;
        }
        get
        {
            if (ViewState["rehead"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["rehead"];
        }
    }

    /// <summary>
    /// 标题注释
    /// </summary>
    public String RemarkTitle
    {
        set
        {
            ViewState["remarkTitle"] = value;
        }
        get
        {
            if (ViewState["remarkTitle"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["remarkTitle"];
        }
    }

    /// <summary>
    /// 标题内容
    /// </summary>
    public String RemarkContent
    {
        set
        {
            ViewState["remarkContent"] = value;
        }
        get
        {
            if (ViewState["remarkContent"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["remarkContent"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            remarkTitle.Text = RemarkTitle;
            remarkContent.Text = RemarkContent;
        }
    }

    public void Save()
    {
        m_xmlManage = new XmlDocManage(ClientXml);

        DataTable dt = new DataTable("message");
        dt.Columns.Add("id");
        dt.Columns.Add("messagetypeid");
        dt.Columns.Add("num");
        dt.Columns.Add("userid");
        dt.Columns.Add("puuser");
        dt.Columns.Add("comid");
        dt.Columns.Add("pucom");
        dt.Columns.Add("puyear");
        dt.Columns.Add("pumonth");
        dt.Columns.Add("pudata");
        dt.Columns.Add("putime");
        dt.Columns.Add("renum");
        dt.Columns.Add("rehead");
        dt.Columns.Add("title");
        dt.Columns.Add("revote");
        dt.Columns.Add("states");

        DataRow dr = dt.NewRow();
        dr["id"] = m_objStrTool.SetStringZero(GetMaxId("MAX(id)", "").ToString(), 3);
        dr["messagetypeid"] = MessageTypeId;
        dr["num"] = GetMaxId("MAX(num)", "messagetypeid=" + MessageTypeId).ToString();
        dr["userid"] = "";
        dr["puuser"] = "";
        dr["comid"] = "";
        dr["pucom"] = "";
        dr["puyear"] = DateTime.Now.Year.ToString();
        dr["pumonth"] = DateTime.Now.Month.ToString();
        dr["pudata"] = DateTime.Now.Day.ToString();
        dr["putime"] = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        dr["renum"] = ReNum;
        dr["rehead"] = ReHead;
        dr["title"] = txtTitle.Text.Trim();
        dr["revote"] = m_objStrTool.String2CDATAString(txtContent.Text.Trim());
        dr["states"] = "N";

        dt.Rows.Add(dr);

        ArrayList propert = new ArrayList();
        propert.Add("id");
        m_xmlManage.UpdateLocalXml("chxml", dt, propert, false, new String[] { "states", "1" });

        m_xmlManage.WriteLocalXml();
    }

    //取得最大id
    private int GetMaxId(String expression, String filter)
    {
        m_dsManage = new DataSetManage();
        //读取XML内容转为DataSet
        m_dsManage.ReadLocalXml2DataSet("chxml", ClientXml);
        DataTable dt = m_dsManage.Get.Tables["message"];
        int maxId = 0;
        //取得最大id
        if (dt.Rows.Count > 0)
        {
            object filterCon = dt.Compute(expression, filter);
            if (!String.IsNullOrEmpty(filterCon.ToString()))
            {
                maxId = Convert.ToInt32(filterCon) + 1;
            }
        }
        return maxId;
    }

    public void CheckInput()
    {
        m_epList = new ExceptionMess();
        //错误状态
        bool errState = false;
        //验证标题数据长度
        UnSetControlColor(remarkTitle, System.Drawing.Color.White);
        if (m_objStrTool.GetStringLen(txtTitle.Text.Trim()) < 10 || m_objStrTool.GetStringLen(txtTitle.Text.Trim()) > 60)
        {
            SetControlColor(remarkTitle);
            errState = true;
        }
        //验证内容数据长度
        UnSetControlColor(remarkContent, System.Drawing.Color.White);
        if (m_objStrTool.GetStringLen(txtContent.Text.Trim()) > 1000)
        {
            SetControlColor(remarkContent);
            errState = true;
        }
        //验证码验证
        errValidateCode.Text = "";
        if (m_objStrTool.GetStringLen(txtValidateCode.Text.Trim()) == 0)
        {
            m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000040"), new String[] { GetResource("R00110.Text") })), errValidateCode);
        }
        if (Page.Session["validatecode"].ToString().ToLower() != txtValidateCode.Text.Trim().ToLower())
        {
            m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000230"), new String[] { GetResource("R00030.Text") })), errValidateCode);
        }

        //抛出异常
        if (m_epList.Count > 0 || errState)
        {
            throw (m_epList);
        }
    }
}
