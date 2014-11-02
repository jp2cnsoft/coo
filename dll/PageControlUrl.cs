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
    [DefaultProperty("PageControlUrl")]
    [ToolboxData("<{0}:PageControlUrl runat=server></{0}:PageControlUrl>")]
    public class PageControlUrl : WebControl
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
        Literal ltlFirst = new Literal();
        //上一页链接
        Literal ltlProv = new Literal();
        //下一页链接
        Literal ltlNext = new Literal();
        //最后页链接
        Literal ltlLast = new Literal();
        //循环页码
        private Literal litPage = new Literal();

        public PageControlUrl()
        {
            ResManager resMag = new ResManager();
            _showLimit = 1 + "～" + 1;
            _piece = resMag.GetGlobalPageResource("PAGE_PIECE");
            _total = resMag.GetGlobalPageResource("PAGE_TOTAL");
            _firstPage = resMag.GetGlobalPageResource("PAGE_FIRST");
            _provPage = resMag.GetGlobalPageResource("PAGE_PROV"); 
            _nextPage = resMag.GetGlobalPageResource("PAGE_NEXT"); 
            _lastPage = resMag.GetGlobalPageResource("PAGE_LAST");

            this.Controls.Add(ltlFirst);
            this.Controls.Add(ltlProv);
            this.Controls.Add(ltlNext);
            this.Controls.Add(ltlLast);
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
                ltlFirst.Visible = ltlProv.Visible = ltlNext.Visible = ltlLast.Visible = false;
            }
            else
            {
                if (CurrPage == 0)
                {
                    ltlFirst.Visible = ltlProv.Visible = false;
                }
                else
                {
                    ltlFirst.Visible = ltlProv.Visible = true;
                }

                if (CurrPage == ShowPageTotal - 1)
                {
                    ltlNext.Visible = ltlLast.Visible = false;
                }
                else
                {
                    ltlNext.Visible = ltlLast.Visible = true;
                }
            }

            this.ltlFirst.Text = "<a href =\"?page=0\">" + this._firstPage.ToString() + "</a>";
            this.ltlFirst.RenderControl(output);

            output.WriteEncodedText(" ");

            this.ltlProv.Text = "<a href =\"?page=" + Convert.ToString(CurrPage - 1) + "\">" + this._provPage.ToString() + "</a>";
            this.ltlProv.RenderControl(output);

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
                    _html.Append("<a href =\"?page=" + i.ToString()+ "\">" + showPage.ToString() + "</a>");
                    _html.Append("&nbsp;");
                }
            }


            output.Write(_html.ToString());

            output.WriteEncodedText(" ");

            this.ltlNext.Text = "<a href =\"?page=" + Convert.ToString(CurrPage + 1) + "\">" + this._nextPage.ToString() + "</a>";
            this.ltlNext.RenderControl(output);

            output.WriteEncodedText(" ");

            this.ltlLast.Text = "<a href =\"?page=" + Convert.ToString(endPageNo - 1) + "\">" + this._lastPage.ToString() + "</a>";
            this.ltlLast.RenderControl(output);

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
        }
    }
}
