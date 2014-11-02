using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Seika
{
    public class ChangeDataStructFile
    {
        public XmlReader XmlMerge(String mainXml, String[] subXml)
        {
            if( String.IsNullOrEmpty(mainXml) ||
                subXml == null)    return null; 
            XmlDocument docXml = new XmlDocument();
            docXml.LoadXml(mainXml);
            XPathNavigator navXml = docXml.CreateNavigator();
            navXml.MoveToFirstChild();
            foreach (String xml in subXml)
            {
                navXml.AppendChild(xml);       
            }
            navXml.MoveToRoot();
            return navXml.ReadSubtree();
        }

        public XmlReader XslMerge(String mainXsl, String[] subXsl)
        {
            if (String.IsNullOrEmpty(mainXsl) ||
                subXsl == null) return null;
            StringBuilder sbXsl = new StringBuilder();
            sbXsl.AppendFormat(mainXsl, subXsl);
            XmlDocument docXml = new XmlDocument();
            docXml.LoadXml(sbXsl.ToString());
            XPathNavigator navXml = docXml.CreateNavigator();
            navXml.MoveToRoot();
            return navXml.ReadSubtree();
        }

        public String XmlToJson(XPathNavigator navXml)
        {
            if (navXml == null) return "";
            String json = "{";
            bool move = true;
            ArrayList nodelist = new ArrayList();

            while (navXml.MoveToNext())
            {
                nodelist.Add(navXml.Name);
            }

            navXml.MoveToFirst();
            json += "\"" + navXml.Name + "\":";
            if (nodelist.Contains(navXml.Name))
            {
                json += "[";
            }

            while (move)
            {     
                if (navXml.HasAttributes)
                {
                    json += "{\"@Attributes\":{";
                    move = navXml.MoveToFirstAttribute();
                    while (move)
                    {                       
                        json += "\"" + navXml.LocalName + "\":\"" + navXml.InnerXml + "\"";
                        move = navXml.MoveToNextAttribute();
                        if (move) json += ",";
                    }
                    json += "},\"@Text\":";
                    navXml.MoveToParent();
                }
               
                if (navXml.HasChildren)    
                {               
                    navXml.MoveToFirstChild();
                    if (navXml.NodeType == XPathNodeType.Text)
                    {
                        json += "\"" + navXml.Value + "\"";
                        navXml.MoveToParent();
                    }
                    else
                    {
                        json += XmlToJson(navXml);
                    }               
                }
                if (navXml.HasAttributes) json += "}";

                if (nodelist.Contains(navXml.Name))
                {
                    move = navXml.MoveToNext(navXml.Name, "");
                    if (move)
                    {
                        json += ",";
                    }
                    else
                    {
                        json += "],";
                        nodelist.Remove(navXml.Name);
                        if (nodelist.Count > 0)
                        {
                            navXml.MoveToFirst();
                            move = navXml.MoveToNext(nodelist[0].ToString(), "");
                            nodelist.RemoveAt(0);
                            if (move)
                            {
                                json += "\"" + navXml.Name + "\":";
                                if (nodelist.Contains(navXml.Name))
                                {
                                    json += "[";
                                }
                            }
                        }
                        else
                        {
                            move = false;
                        }
                    }                   
                }
                else
                {
                    nodelist.Remove(navXml.Name);
                    if (nodelist.Count > 0)
                    {
                        navXml.MoveToFirst();
                        move = navXml.MoveToNext(nodelist[0].ToString(), "");
                        nodelist.RemoveAt(0);
                        if (move)
                        {
                            json += ",";
                            json += "\"" + navXml.Name + "\":";
                            if (nodelist.Contains(navXml.Name))
                            {
                                json += "[";
                            }
                        }                        
                    }
                    else
                    {
                        move = false;
                    }
                }

            }
            json += "}";
            navXml.MoveToParent();
            return json;
        }

        public XmlDocument JsonToXml(String json)
        {
            if (String.IsNullOrEmpty(json)) return null;
            XmlDocument docXml = new XmlDocument();          
            //
            return docXml;
        }
    }
}
