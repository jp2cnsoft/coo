using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Xml.XPath;
using Seika.CooException;

namespace Seika 
{
    public class XmlDocManage
    {
        String xmlLocalPath;
        public XmlDocument xml;
        private StringFilter stringFilter = new StringFilter();
        String sAttribute = "id";

        public String SAttribute
        {
            get { return sAttribute; }
            set { sAttribute = value; }
        }

        /// <summary>
        /// 构造XML文档
        /// </summary>
        /// <param name="xmlLocalPath">XML文档路径</param>
        public XmlDocManage(String xmlLocalPath)
        {
            //判断文件是否存在
            //if (!System.IO.File.Exists(xmlLocalPath))
            //    throw new SysException("ED00000410");
            this.xmlLocalPath = xmlLocalPath;
            //构造空文档
            xml = new XmlDocument();
            //文档是否存在
            if (File.Exists(xmlLocalPath))
            {   //文档调用
                xml.Load(xmlLocalPath);
            }
            else
            {   //建立文档
                XmlDeclaration xdec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
                //添加默认内容节点
                xml.AppendChild(xdec);
            }
        }

        /// <summary>
        /// 构造XML文档
        /// </summary>
        public XmlDocManage()
        {
            //构造空文档
            xml = new XmlDocument();
            //建立文档
            XmlDeclaration xdec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            //添加默认内容节点
            xml.AppendChild(xdec);
        }

        /// <summary>
        /// 构造XML文档
        /// </summary>
        /// <param name="xmlLocalPath">XML数据流</param>
        public XmlDocManage(Stream xmlStream)
        {
            //构造空文档
            xml = new XmlDocument();
            //调入xml数据流
            xml.Load(xmlStream);
        }

        /// <summary>
        /// 构造XML文档
        /// </summary>
        /// <param name="xmlLocalPath">XML数据流</param>
        public XmlDocManage(XmlReader xmlReader)
        {
            //构造空文档
            xml = new XmlDocument();
            //调入xml数据流
            xml.Load(xmlReader);
        }

        /// <summary>
        /// 调入xml
        /// </summary>
        /// <param name="xml"></param>
        public void LoadXml(String xml) 
        {
            this.xml.LoadXml(xml);
        }

        /// <summary>
        /// 建立文档节点
        /// </summary>
        /// <param name="path">节点路径</param>
        public void CreateNode(String path)
        {
            String sub = "";
            int pos;
            XmlNode fnode = xml;
            //存在节点关系
            while ((pos = path.IndexOf('/')) > 0)
            {
                //取得该节点父级路径
                sub += path.Substring(0, pos);
                //取得该节点
                XmlNode xmlNode = xml.SelectSingleNode(sub);
                //该节点为空
                if (xmlNode == null)
                {
                    //建立该节点
                    xmlNode = xml.CreateElement(path.Substring(0, pos));
                    //追加该节点
                    fnode.AppendChild(xmlNode);                
                }
                //更新节点
                fnode = xmlNode;
                //取得该节点子节点路径
                path = path.Substring(pos + 1);
                sub += "/";
            }
        }

        /// <summary>
        /// 追加新节点
        /// </summary>
        /// <param name="fnodepath"></param>
        /// <param name="xmlNodePath"></param>
        /// <param name="data"></param>
        /// <param name="Node"></param>
        /// <param name="Attribute"></param>
        public void AddNewNode(String fnodepath, String xmlNodePath, DataRow data, ArrayList Node, ArrayList Attribute)
        {
            int i;
            //取得追加节点父级节点
            XmlNode fnode = xml.SelectSingleNode(fnodepath);
            //追加节点
            XmlNode xmlNode = xml.CreateElement(xmlNodePath);
            //追加属性
            if (Attribute != null)
            {
                for (i = 0; i < Attribute.Count; i++)
                {
                    //建立属性
                    XmlAttribute xatb = xml.CreateAttribute(Attribute[i].ToString());
                    //属性值
                    String nodeattr = data[Attribute[i].ToString()].ToString();
                    //追加属性
                    if (CheckXmlTextState(nodeattr)) { xatb.InnerXml = nodeattr; } else { xatb.InnerText = stringFilter.ReplaceBadChar(nodeattr); }
                    //追加该属性
                    xmlNode.Attributes.Append(xatb);
                }
            }
            //将属性追加到节点
            fnode.AppendChild(xmlNode);
            //追加节点内容
            if (Node != null)
            {
                fnode = xmlNode;
                //追加内容
                for (i = 0; i < Node.Count; i++)
                {
                    //建立内容
                    xmlNode = xml.CreateElement(Node[i].ToString());
                    //取得节点内容
                    String nodec = data[Node[i].ToString()].ToString();
                    //判断节点内容是否包含格式化显示部分,存在以Xml形式追加,不存在以文本形式追加
                    if (CheckXmlTextState(nodec)) { xmlNode.InnerXml = nodec; } else { xmlNode.InnerText = stringFilter.ReplaceBadChar(nodec); }
                    //追加该节点
                    fnode.AppendChild(xmlNode);
                }
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xmlNodePath">节点路径</param>
        private void RemoveChild(String xmlNodePath)
        {
            //取得删除节点
            XmlNodeList nodeList = xml.SelectNodes(xmlNodePath);
            foreach (XmlNode node in nodeList)
            {   //删除该节点
                node.ParentNode.RemoveChild(node);
            }
        }

        /// <summary>
        /// 带属性id删除节点
        /// </summary>
        /// <param name="xmlNodePath">节点路径</param>
        /// <param name="id">属性id</param>
        private void RemoveChild(String xmlNodePath, String id)
        {   //取得删除节点
            XmlNodeList nodeList = xml.SelectNodes(xmlNodePath);
            foreach (XmlNode node in nodeList)
            {   //判断属性id节点
                if (node.Attributes[SAttribute].Value.ToString() == id)
                {   //删除该节点
                    node.ParentNode.RemoveChild(node);
                    return;
                }
            }
        }

        /// <summary>
        /// 带组属性id删除节点
        /// </summary>
        /// <param name="xmlNodePath">节点路径</param>
        /// <param name="id">属性id</param>
        private void RemoveChild(String xmlNodePath, ArrayList id)
        {   //取得删除节点
            XmlNodeList nodeList = xml.SelectNodes(xmlNodePath);
            foreach (XmlNode node in nodeList)
            {   //判断属性id节点
                if (id.Contains(node.Attributes[SAttribute].Value.ToString()))
                {   //删除该节点
                    node.ParentNode.RemoveChild(node);
                }
            }
        }

        /// <summary>
        /// 带组属性id删除修改节点
        /// </summary>
        /// <param name="xmlNodePath">节点路径</param>
        /// <param name="id">属性id</param>
        private void RemoveModifyNode(String xmlNodePath, ArrayList id)
        {   //取得删除节点
            XmlNodeList nodeList = xml.SelectNodes(xmlNodePath);
            foreach (XmlNode node in nodeList)
            {   //判断属性id节点
                if (!id.Contains(node.Attributes[SAttribute].Value.ToString()))
                {   //删除该节点
                    node.ParentNode.RemoveChild(node);
                }
            }
        }

        /// <summary>
        /// 根据属性id取得节点
        /// </summary>
        /// <param name="xmlNodePath">节点路径</param>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        private XmlNode GetNode(String xmlNodePath, String id)
        {
            XmlNode res = null;
            //取得该节点
            XmlNodeList nodeList = xml.SelectNodes(xmlNodePath);
            foreach (XmlNode node in nodeList)
            {   //判断取得该节点的属性
                if (node.Attributes[SAttribute].Value.ToString() == id)
                {   //取得该节点
                    res = node;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// 修改节点文本
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="data">修改数据</param>
        /// <param name="Node">节点名称集合</param>
        /// <param name="Attribute">属性</param>
        private void ModifyNodeText(XmlNode node, DataRow data, ArrayList Node, ArrayList Attribute)
        {
            int i;
            for (i = 0; i < Attribute.Count; i++)
            {   //追加属性节点
                node.Attributes[Attribute[i].ToString()].InnerText = data[Attribute[i].ToString()].ToString();
            }
            if (Node != null)
            {   //取得该节点子节点集合
                XmlNodeList childListNode = node.ChildNodes;
                foreach (XmlNode childXmlNode in childListNode)
                {   //修改子节点内容
                    for (i = 0; i < Node.Count; i++)
                    {   //要修改节点的名称
                        if (childXmlNode.Name.ToString() == Node[i].ToString())
                        {   //修改成节点的名称
                            String nodec = data[Node[i].ToString()].ToString();
                            //判断节点内容是否包含格式化显示部分,存在以Xml形式追加,不存在以文本形式追加
                            if (CheckXmlTextState(nodec)) { childXmlNode.InnerXml = nodec; } else { childXmlNode.InnerText = stringFilter.ReplaceBadChar(nodec); }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 节点修改
        /// </summary>
        /// <param name="fnodepath"></param>
        /// <param name="xmlNodePath"></param>
        /// <param name="data"></param>
        /// <param name="Node"></param>
        /// <param name="Attribute"></param>
        /// <param name="mtype"></param>
        private void ModifyNode(String fnodepath, String xmlNodePath, DataRow data, ArrayList Node, ArrayList Attribute, String mtype)
        {
            switch (data[mtype].ToString())
            {
                //节点修改
                case "M": 
                    ModifyNodeText(GetNode(fnodepath + "/" + xmlNodePath, data[SAttribute].ToString()), data, Node, Attribute);
                    break;
                //节点追加
                case "N": 
                    //移除节点
                    RemoveChild(fnodepath + "/" + xmlNodePath, data[SAttribute].ToString());
                    //添加新节点
                    AddNewNode(fnodepath, xmlNodePath, data, Node, Attribute);
                    break;
                case "O":  
                    break;
            }   
        }

        /// <summary>
        /// 更新XML文档
        /// </summary>
        /// <param name="xmlNodePath">文档路径</param>
        /// <param name="dt">生成XML所用表</param>
        /// <param name="Attribute">属性集合</param>
        /// <param name="leaf"></param>
        /// <param name="mtype"></param>
        public void UpdateLocalXml(String xmlNodePath, DataTable dt, ArrayList Attribute, bool leaf, String [] mtype)
        {   
            int Count = dt.Columns.Count;
            String NodeName = dt.TableName;
            ArrayList Node = new ArrayList();
            ArrayList id = new ArrayList();
  
            if (!String.IsNullOrEmpty(xmlNodePath) && dt != null)
            {   //追加XML属性
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if ((Attribute == null || !Attribute.Contains(dt.Columns[i].ColumnName)) && ( mtype == null || dt.Columns[i].ColumnName != mtype[0]))
                    {   //文档属性
                        Node.Add(dt.Columns[i].ColumnName);
                    }
                }
                //建立根节点
                CreateNode(xmlNodePath + "/");

                if (leaf)
                {   //删除节点
                    RemoveChild(xmlNodePath + "/" + NodeName);
                    //追加节点
                    foreach (DataRow dr in dt.Rows)
                    {                  
                        AddNewNode(xmlNodePath ,NodeName, dr, Node, Attribute);
                    }
                }
                else
                {   //修改节点
                    foreach (DataRow dr in dt.Rows)
                    {
                        id.Add(dr[SAttribute]);
                        ModifyNode(xmlNodePath ,NodeName, dr, Node, Attribute, mtype[0]);
                    }
                    //删除节点
                    if (mtype[1] == "0")
                    {
                        RemoveModifyNode(xmlNodePath + "/" + NodeName, id);
                    }   
                }
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xpath">xpath路径</param>
        public void RemoveNode(String xpath)
        {
            XmlNode xmlNode = xml.SelectSingleNode(xpath);
            if (xmlNode != null) 
            {
                xmlNode.ParentNode.RemoveChild(xmlNode);
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        /// <param name="xpath">xpath路径</param>
        /// <param name="content">节点内容</param>
        public void ModifyNode(String xpath,String content)
        {
            XmlNode xmlNode = xml.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                xmlNode.InnerXml = content;
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xmlNodePath">文档路径</param>
        /// <param name="id">移除的属性id</param>
        public void RemoveNode(String xmlNodePath, ArrayList id)
        {
            RemoveChild(xmlNodePath, id);
        }

        /// <summary>
        /// 追加文档路径整个文档
        /// </summary>
        /// <param name="xmlPath"></param>
        public void AppendXml(String xmlPath)
        {   //建立可追加文档对象
            XPathNavigator xmlNav = xml.CreateNavigator();
            //删除整个内容节点
            xmlNav.MoveToChild("chxml", "");
            //新建空文档
            XmlDocument xmlDoc = new XmlDocument();
            //调用路径下文档
            xmlDoc.Load(xmlPath);
            //追加文档
            xmlNav.AppendChild(xmlDoc.DocumentElement.InnerXml.ToString());
            //移动文档到根节点
            xmlNav.MoveToRoot();           
        }

        /// <summary>
        /// 追加文档对象
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void AppendXml(XmlDocument xmlDoc)
        {
            //建立可追加文档对象
            XPathNavigator xmlNav = xml.CreateNavigator();
            //删除整个内容节点
            xmlNav.MoveToChild("chxml", "");
            //追加文档
            xmlNav.AppendChild(xmlDoc.DocumentElement.InnerXml.ToString());
            //移动文档到根节点
            xmlNav.MoveToRoot();
        }

        /// <summary>
        /// 追加节点文档
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="node"></param>
        public void AppendXmlNode(String xpath,XmlNode node)
        {
            if (node == null) return;
            //按节点文档输出内容追加
            AppedNavigatorNode(xpath, node.OuterXml.ToString());
        }

        /// <summary>
        /// 追加节点文档
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="nodeOuterXml"></param>
        public void AppendXmlNode(String xpath, String nodeOuterXml)
        {
            //按字符串文档内容追加
            AppedNavigatorNode(xpath, nodeOuterXml);
        }

        /// <summary>
        /// 追加文档
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="nodeOuterXml"></param>
        private void AppedNavigatorNode(String xpath, String nodeOuterXml)
        {
            int pos;
            String sub = "";
            if ((pos = xpath.LastIndexOf('/')) > 0)
            {
                sub += xpath.Substring(0, pos);
                XmlNode xmlNode = xml.SelectSingleNode(sub);
                xmlNode.InnerXml = nodeOuterXml;
                /*用该方法追加的节点会自动去掉格式显示部分
                 * XPathNavigator xmlNav = xmlNode.CreateNavigator();
                xmlNav.AppendChild(node.OuterXml.ToString());
                 */
            }
        }

        /// <summary>
        /// 节点追加CDATA
        /// </summary>
        /// <param name="xpath"></param>
        public void AppendCDATANavigatorNode(String xpath)
        {
            XmlNode xmlNode = xml.SelectSingleNode(xpath);
            String cdataText = xmlNode.InnerXml;
            xmlNode.InnerXml = "";
            xmlNode.AppendChild(xml.CreateCDataSection(cdataText));
        }
        /// <summary>
        /// 取得指定路径节点
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XmlNode GetXmlNode(String xpath) 
        {
            return xml.SelectSingleNode(xpath);
        }
        /// <summary>
        /// 取得指定路径节点
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public String GetXmlNodes(String xpath)
        {
            StringBuilder _x = new StringBuilder(); 
            XmlNodeList xmls = xml.SelectNodes(xpath);
            foreach (XmlNode x in xmls) 
            {
                _x.Append(x.OuterXml);
            }
            return _x.ToString();
        }
        /// <summary>
        /// 检查节点内容是否带格式化文本
        /// </summary>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        private bool CheckXmlTextState(String nodeText) 
        {
            return nodeText.IndexOf(@"<![CDATA[") == 0 ? true : false;
        }
        /// <summary>
        /// 按构造函数路径保存文档
        /// </summary>
        public void WriteLocalXml()
        {
            xml.Save(xmlLocalPath);
        }

        /// <summary>
        /// 按构造函数路径保存文档
        /// </summary>
        public void WriteLocalXml(String xmlLocalPath)
        {
            xml.Save(xmlLocalPath);
        }

        /// <summary>
        /// 读取xml内存流
        /// </summary>
        public MemoryStream GetXmlDocStream() 
        {
            MemoryStream ms = new MemoryStream();
            //保存成无bom格式utf8
            XmlWriterSettings xmlws = new XmlWriterSettings();
            xmlws.Encoding = new System.Text.UTF8Encoding(false);
            xmlws.CloseOutput = true;
            xmlws.Indent = true;

            XmlWriter xmlwr = XmlWriter.Create(ms, xmlws);
            try
            {
                xml.Save(xmlwr);
                xmlwr.Flush();
            }
            catch
            {
                xmlwr.Close();
                ms.Close();
            }
            return ms;
        }

        public override string ToString()
        {
            return xml.InnerXml.ToString();
        }

        public XmlTextReader ToXmlReader() 
        {
            XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.Default);
            //xml文档包含非头内容
            if (!String.IsNullOrEmpty(xml.OuterXml) && (xml.LastChild.LastChild != null))
            {
                return new XmlTextReader(xml.OuterXml, XmlNodeType.Document, context);
            }
            return null;
        }
        
    }
}
