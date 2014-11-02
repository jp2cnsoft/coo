using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Seika.Common.Xml
{
    /// <summary>
    /// Summary description for XmlDataSet
    /// </summary>
    public class XmlDataSet : DataSet
    {
        public XmlDataSet() : base ()
        {
        }

        public bool LoadXmlTable(string tableName, string path)
        {
            return this.LoadXmlTable(path, "/");
        }

        public bool LoadXmlTable(string tableName, string path, string xpath)
        {
            if (System.IO.File.Exists(path))
            {
                this.ReadXml(path);
            }
            return false;
        }
    }
}
