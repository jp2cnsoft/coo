using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Seika.Common.Net;

namespace Seika.ServicesCol
{
    [DefaultProperty("PageControl")]
    [ToolboxData("<{0}:PageControl runat=server></{0}:PageControl>")]
    public class PageControl : WebControl, IPostBackEventHandler 
    {
        private String _total = String.Empty;
        private String _piece = String.Empty;
        private String _showLimit = String.Empty;
        private String _firstPage = String.Empty;
        private String _provPage = String.Empty;
        private String _nextPage = String.Empty;
        private String _lastPage = String.Empty;

        //承载总记录数标题
        private Label lblTotal = new Label();
        //总记录数
        private Label lblShowTotal = new Label();
        //承载显示数单位
        private Label lblPiece = new Label();
        //承载显示数单位
        private Label lblPiece2 = new Label();
        //承载显示数区间
        private Label lblShowLimit = new Label();
        //第一页链接
        private LinkButton lbtnFirst = new LinkButton();
        //上一页链接
        private LinkButton lbtnProv = new LinkButton();
        //下一页链接
        private LinkButton lbtnNext = new LinkButton();
        //最后页链接
        private LinkButton lbtnLast = new LinkButton();
        //循环页码
        private Literal litPage = new Literal();

        public event EventHandler PageIndexChange;

        public PageControl()
        {
            ResManager resMag = new ResManager();
            _showLimit = 1 + "～" + 1;
            _piece = resMag.GetGlobalPageResource("PAGE_PIECE");
            _total = resMag.GetGlobalPageResource("PAGE_TOTAL");
            _firstPage = resMag.GetGlobalPageResource("PAGE_FIRST");
            _provPage = resMag.GetGlobalPageResource("PAGE_PROV"); 
            _nextPage = resMag.GetGlobalPageResource("PAGE_NEXT"); 
            _lastPage = resMag.GetGlobalPageResource("PAGE_LAST"); 

            lbtnFirst.Click += new EventHandler(lbtnFirst_Click);
            lbtnProv.Click += new EventHandler(lbtnProv_Click);
            lbtnNext.Click += new EventHandler(lbtnNext_Click);
            lbtnLast.Click += new EventHandler(lbtnLast_Click);

            this.Controls.Add(lbtnFirst);
            this.Controls.Add(lbtnProv);
            this.Controls.Add(lbtnNext);
            this.Controls.Add(lbtnLast);
        }

        [Bindable(true)]
        [Category("每页显示数")]
        [DefaultValue("")]
        [Localizable(true)]
        public int PageNum
        {
            set 
            {
                ViewState["PageNum"] = value; 
            }
            get 
            {
                if (ViewState["PageNum"] == null)
                {
                    return 0;
                }
                return (int)ViewState["PageNum"];
            }
        }

        [Bindable(true)]
        [Category("当前页")]
        [DefaultValue("")]
        [Localizable(true)]
        public int CurrPage
        {
            set 
            {
                ViewState["CurrPage"] = value; 
            }
            get 
            {
                if (ViewState["CurrPage"] == null)
                {
                    return 0;
                }
                return (int)ViewState["CurrPage"];
            }
        }

        [Bindable(true)]
        [Category("总页数")]
        [DefaultValue("")]
        [Localizable(true)]
        public int PageTotal
        {
            set 
            {
                ViewState["PageTotal"] = value; 
            }
            get 
            {
                if (ViewState["PageTotal"] == null)
                {
                    return 0;
                }
                return (int)ViewState["PageTotal"];
            }
        }

        [Bindable(true)]
        [Category("总记录数标题")]
        [DefaultValue("")]
        [Localizable(true)]
        public String Total
        {
            set 
            {
                ViewState["Total"] = value; 
            }
            get 
            {
                if (ViewState["Total"] == null)
                {
                    return String.Empty;
                }
                return (String)ViewState["Total"];
            }
        }

        [Bindable(true)]
        [Category("总记录数")]
        [DefaultValue("5")]
        [Localizable(true)]
        public int ShowTotal
        {
             set 
            {
                ViewState["ShowTotal"] = value; 
            }
            get 
            {
                if (ViewState["ShowTotal"] == null)
                {
                    return 0;
                }
                return (int)ViewState["ShowTotal"];
            }
        }

        [Bindable(true)]
        [Category("显示数单位")]
        [DefaultValue("件")]
        [Localizable(true)]
        public String Piece
        {
            set 
            {
                ViewState["Piece"] = value; 
            }
            get 
            {
                if (ViewState["Piece"] == null)
                {
                    return String.Empty;
                }
                return (String)ViewState["Piece"];
            }
        }

        [Bindable(true)]
        [Category("最前页显示文本")]
        [DefaultValue("开始页")]
        [Localizable(true)]
        public String FirstPage
        {
            set 
            {
                ViewState["FirstPage"] = value; 
            }
            get 
            {
                if (ViewState["FirstPage"] == null)
                {
                    return String.Empty;
                }
                return (String)ViewState["FirstPage"];
            }
        }

        [Bindable(true)]
        [Category("当前总页数")]
        [DefaultValue("0")]
        [Localizable(true)]
        public int ShowPageTotal
        {
            set
            {
                ViewState["showPageTotal"] = value;
            }
            get
            {
                if (ViewState["ShowTotal"] != null && ViewState["PageNum"] != null)
                {
                    ViewState["showPageTotal"] = (int)Convert.ToInt32(ShowTotal % PageNum) == 0 ? Convert.ToInt32(ShowTotal / PageNum) : Convert.ToInt32(ShowTotal / PageNum) + 1;
                    return (int)ViewState["showPageTotal"];
                }
                return 0;
            }
        }

        protected void RaisePostDataChangedEvent(System.EventArgs e)
        {
            //委托给加载控件页的PageIndexChange事件
            if (PageIndexChange != null)
            {
                PageIndexChange(this, e);
            }
        }

        public void RaisePostDataChangedEvent()
        {
            if (PageIndexChange != null)
            {
                PageIndexChange(this, System.EventArgs.Empty);
            }
        }

        public void RaisePostBackEvent(String e)
        {
            CurrPage = Convert.ToInt32(e);
            if (PageIndexChange != null)
            {
                PageIndexChange(this, System.EventArgs.Empty);
            }
        }

        private void lbtnFirst_Click(object sender, EventArgs e) 
        {
            CurrPage = 0;
            if (PageIndexChange != null)
            {
                PageIndexChange(this, e);
            }
        }

        private void lbtnProv_Click(object sender, EventArgs e)
        {
            CurrPage = CurrPage - 1;
            if (PageIndexChange != null)
            {
                PageIndexChange(this, e);
            }
        }

        private void lbtnNext_Click(object sender, EventArgs e)
        {
            CurrPage = CurrPage + 1;
            if (PageIndexChange != null)
            {
                PageIndexChange(this, e);
            }
        }

        private void lbtnLast_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ShowTotal % PageNum) == 0)
            {
                CurrPage = Convert.ToInt32(ShowTotal / PageNum) - 1;
            }
            else 
            {
                CurrPage = Convert.ToInt32(ShowTotal / PageNum);
            }
            if (PageIndexChange != null)
            {
                PageIndexChange(this, e);
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Width, "100%");
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);

            output.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Style, "text-align:right");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            //在表格中添加Label控件
            this.lblTotal.Text = this._total.ToString();
            this.lblTotal.RenderControl(output);

            this.lblShowTotal.Text = this.ShowTotal.ToString();
            this.lblShowTotal.RenderControl(output);

            this.lblPiece.Text = this._piece.ToString();
            this.lblPiece.RenderControl(output);

            output.WriteEncodedText("  ");

            //当前件状态显示
            int limitBegin = CurrPage * PageNum + 1;
            int limitEnd = CurrPage * PageNum + ((CurrPage + 1 == ShowPageTotal) ? (ShowTotal % PageNum == 0 ? PageNum : ShowTotal % PageNum) : PageNum);
            if (ShowTotal == 0 || limitBegin > ShowTotal)
            {
                this.lblShowLimit.Text = "0";
            }
            else
            {
                this.lblShowLimit.Text = limitBegin.ToString() + "～" + limitEnd.ToString();
            }
            this.lblShowLimit.RenderControl(output);

            this.lblPiece2.Text = this._piece.ToString();
            this.lblPiece2.RenderControl(output);

            output.WriteEncodedText("  ");

            if (ShowTotal == 0)
            {
                lbtnFirst.Visible = lbtnProv.Visible = lbtnNext.Visible = lbtnLast.Visible = false;
            }
            else
            {
                if (CurrPage == 0)
                {
                    lbtnFirst.Visible = lbtnProv.Visible = false;
                }
                else
                {
                    lbtnFirst.Visible = lbtnProv.Visible = true;
                }

                if (CurrPage == ShowPageTotal - 1)
                {
                    lbtnNext.Visible = lbtnLast.Visible = false;
                }
                else
                {
                    lbtnNext.Visible = lbtnLast.Visible = true;
                }
            }

            this.lbtnFirst.Text = this._firstPage.ToString();
            this.lbtnFirst.RenderControl(output);

            output.WriteEncodedText(" ");

            this.lbtnProv.Text = this._provPage.ToString();
            this.lbtnProv.RenderControl(output);

            output.WriteEncodedText(" ");

            //生成循环页链接
            StringBuilder _html = new StringBuilder();

            int startPageNo = CurrPage - 5;
            int endPageNo = startPageNo + 10;

            if (startPageNo < 0)
            {
                endPageNo += (startPageNo * -1);
                startPageNo = 0;
            }

            if (endPageNo > ShowPageTotal)
            {
                startPageNo -= (endPageNo - ShowPageTotal);
                endPageNo = ShowPageTotal;
            }

            if (startPageNo < 0) startPageNo = 0;


            for (int i = startPageNo; i < endPageNo; i++)
            {
                String showPage = Convert.ToString(i + 1);
                if (CurrPage == i)
                {
                    _html.Append(showPage.ToString());
                    _html.Append("&nbsp;");
                }
                else
                {
                    _html.Append("<a href =\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, i.ToString()) + "\">" + showPage.ToString() + "</a>");
                    _html.Append("&nbsp;");
                }
            }


            output.Write(_html.ToString());

            output.WriteEncodedText(" ");

            this.lbtnNext.Text = this._nextPage.ToString();
            this.lbtnNext.RenderControl(output);

            output.WriteEncodedText(" ");

            this.lbtnLast.Text = this._lastPage.ToString();
            this.lbtnLast.RenderControl(output);

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

        }

    }
}
