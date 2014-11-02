using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Seika.COO.Web.PG;

public partial class Control_SeoKeyWordControl : System.Web.UI.UserControl
{
    // 详情编号
    private string m_detailId;

    public string DetailId
    {
        get { return m_detailId; }
        set { m_detailId = value; }
    }

    // 关键字
    private string m_seoKeyWord;

    public string SeoKeyWord
    {
        get { return m_seoKeyWord; }
        set { m_seoKeyWord = value; }
    }

    // 是否存在关键字
    private bool m_has;

    public bool Has
    {
        get { return m_has; }
        set { m_has = value; }
    }

    // 语言
    private string m_language;

    public string Language
    {
        get { return m_language; }
        set { m_language = value; }
    }

    // 页码
    private string m_currPage;

    public string CurrPage
    {
        get { return m_currPage; }
        set { m_currPage = value; }
    }

    // 用户编号
    private string m_registId;

    public string RegistId
    {
        get { return m_registId; }
        set { m_registId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager m_sessionManager = new SessionManager(Session);
        String language = m_sessionManager.Page_UICultureID;
        String registId = m_sessionManager.PageLogin_RegistId;
        if (!Page.IsPostBack)
        {
            Seika.COO.PageData.SeoKeyWordCountrol ci = new Seika.COO.PageData.SeoKeyWordCountrol();

            DataTable dt = ci.FindSeoKeyWordById(DetailId, language, CurrPage, registId);

            if (dt.Rows.Count == 1)
            {
                SeoKeyWord = ci.FindSeoKeyWordById(DetailId, language, CurrPage, registId).Rows[0]["EXPLAIN"].ToString();
                Has = true;
            }
            else
            {
                Has = false;
            }
        }
        else if (this.Visible)
        {
            DetailId = null == Request["DetailId"] ? ViewState["DetailId"].ToString() : Request["DetailId"];
            SeoKeyWord = Request["SeoKeyWord"];
            Has = Convert.ToBoolean(Request["Has"]);

            Seika.COO.PageData.SeoKeyWordCountrol ci = new Seika.COO.PageData.SeoKeyWordCountrol();
            if (Has)
            {
                ci.UpdateSeoKeyWord(DetailId, language, CurrPage, registId, SeoKeyWord);
            }
        }
    }


    public void AddSeoKeyWord(XmlDocMangContent xdmc, string detailId)
    {
        if (!Convert.ToBoolean(Request["Has"]))
        {
            Seika.COO.PageData.SeoKeyWordCountrol ci = new Seika.COO.PageData.SeoKeyWordCountrol();
            //ci.AddSeoKeyWord(detailId, RegistId, GetGlobalResourceObject("HtmlMenu", CurrPage).ToString(), SeoKeyWord, Language);
            ci.AddSeoKeyWord(detailId, RegistId, CurrPage, SeoKeyWord, Language);
            Has = true;
        }
        //更新集合中的关键字
        DataTable dt = xdmc.SeoKeyword;
        foreach (DataRow r in dt.Rows) 
        {
            if (detailId == r["DETAILID"].ToString() && CurrPage == r["PAGEID"].ToString())
            {
                r["DETAILID"] = SeoKeyWord;
                break;
            }
        }
    }
}
