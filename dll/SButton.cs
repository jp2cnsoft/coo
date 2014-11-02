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
    [ToolboxData("<{0}:SButton runat=server></{0}:SButton>")]
    public class SButton : Button
    {
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            string clickHandler = string.Format(
                "document.body.style.cursor = 'wait'; this.disabled = true; {0};",
                Page.ClientScript.GetPostBackEventReference(this, string.Empty));
            writer.AddAttribute("onclick", clickHandler);
            base.AddAttributesToRender(writer);

        }

    }
}
