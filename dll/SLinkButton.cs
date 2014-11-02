using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seika.ServicesCol
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SLinkButton runat=server></{0}:SLinkButton>")]
    public class SLinkButton : LinkButton
    {
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            string clickHandler = "var Count = document.getElementById('linkbutton_count'); if (Count.value > 0) return false; Count.value = Count.value + 1;";
            writer.AddAttribute("onclick", clickHandler);
            writer.Write("<input id=\"linkbutton_count\" type=\"hidden\" value=\"0\" />");
            base.AddAttributesToRender(writer);

        }
    }
}
