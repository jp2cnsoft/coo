using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections;
using Seika;
using System.Xml;

/// <summary>
/// 转换生成前台文件
/// </summary>
/// <remarks>
/// 2008/10/17  于作伟  新规作成
namespace Seika.COO.Web.PG
{
    public class UserSiteTransFile
    {
        public UserSiteTransFile(XmlDocManage xmlDocMang)
        {
            this.XmlDocMang = xmlDocMang;
        }

        public MemoryStream GetStream(String xpath, String type, TransValue transValue)
        {
            //新建xml路径
            String createXpath = "uscurrency/configure";
            //新建xml文档
            XmlDocManage xml = new XmlDocManage();
            //建立新节点
            xml.CreateNode(createXpath);
            //取得xml节点
            XmlNode node = XmlDocMang.GetXmlNode(xpath);
            switch (type)
            {
                case TransType.PHP:
                    node = ToReplacePhp(node, transValue);
                    break;
                case TransType.JS:
                    node = ToReplaceJs(node, transValue);
                    break;
                case TransType.USCSS:
                    node = ToReplaceUscss(node, transValue);
                    break;
                case TransType.USJS_FLASH:
                    node = ToReplaceFlash(node, transValue);
                    break;
                case TransType.USJS_NONE_FLASH:
                    node = ToReplaceNoneFlash(node, transValue);
                    break;
            }

            //在新建节点中添加节点
            xml.AppendXmlNode(createXpath, node);

            return xml.GetXmlDocStream();
        }

        private XmlNode ToReplacePhp(XmlNode node, TransValue transValue)
        {
            if (transValue != null)
            {
                node.InnerXml = node.InnerXml.Replace(TransKey.PHP_ACTIVE_LANGUAGE_KEY, (String)transValue[TransKey.PHP_ACTIVE_LANGUAGE_KEY]);
                node.InnerXml = node.InnerXml.Replace(TransKey.PHP_DEFAULT_LANGUAGE_KEY, (String)transValue[TransKey.PHP_DEFAULT_LANGUAGE_KEY]);
            }
            return node;
        }

        private XmlNode ToReplaceJs(XmlNode node, TransValue transValue)
        {
            StringBuilder _js = new StringBuilder();
            if (transValue != null)
            {
                IEnumerator names = transValue.v.GetEnumerator();

                while (names.MoveNext())
                {
                    String key = ((System.Collections.DictionaryEntry)names.Current).Key.ToString();
                    String value = transValue.v[key].ToString();
                    _js.Append(String.Format(TransValue.LANGUAGE_LIST, key, value));
                }
            }
            node.InnerXml = node.InnerXml.Replace(TransKey.JS_LANGUAGE_LIST, _js.ToString());
            return node;
        }

        private XmlNode ToReplaceUscss(XmlNode node, TransValue transValue)
        {
            if (transValue != null)
            {
                node.InnerXml = node.InnerXml.Replace(TransKey.USCSS_HEAD_BACKGROUND_IMG_KEY, (String)transValue[TransKey.USCSS_HEAD_BACKGROUND_IMG_KEY]);
                node.InnerXml = node.InnerXml.Replace(TransKey.USCSS_HEAD_BACKGROUND_HEIGHT_KEY, (String)transValue[TransKey.USCSS_HEAD_BACKGROUND_HEIGHT_KEY]);
                node.InnerXml = node.InnerXml.Replace(TransKey.USCSS_BODY_BACKGROUND_COLOR_KEY, (String)transValue[TransKey.USCSS_BODY_BACKGROUND_COLOR_KEY]);
                node.InnerXml = node.InnerXml.Replace(TransKey.USCSS_BODY_BACKGROUND_IMG_KEY, (String)transValue[TransKey.USCSS_BODY_BACKGROUND_IMG_KEY]);
                node.InnerXml = node.InnerXml.Replace(TransKey.USCSS_BODY_BACKGROUND_REPEAT_KEY, (String)transValue[TransKey.USCSS_BODY_BACKGROUND_REPEAT_KEY]);
            }
            return node;
        }

        private XmlNode ToReplaceFlash(XmlNode node, TransValue transValue)
        {
            if (transValue != null)
            {
                node.InnerXml = node.InnerXml.Replace(TransKey.FLASH_FLASHNAME, (String)transValue[TransKey.FLASH_FLASHNAME]);
                node.InnerXml = node.InnerXml.Replace(TransKey.FLASH_FLASH_HEIGHT, (String)transValue[TransKey.FLASH_FLASH_HEIGHT]);
            }
            return node;
        }

        private XmlNode ToReplaceNoneFlash(XmlNode node, TransValue transValue)
        {
            return node;
        }

        private XmlDocManage XmlDocMang
        {
            get;
            set;
        }
    }

    public class TransType
    {
        public const String PHP = "php";
        public const String JS = "js";
        public const String USCSS = "uscss";
        public const String USJS_FLASH = "usjs_flash";
        public const String USJS_NONE_FLASH = "usjs_none_flash";

    }

    public class TransKey
    {
        public const String PHP_ACTIVE_LANGUAGE_KEY = "{ACTIVE_LANGUAGE}";
        public const String PHP_DEFAULT_LANGUAGE_KEY = "{DEFAULT_LANGUAGE}";

        public const String USCSS_HEAD_BACKGROUND_IMG_KEY = "{HEAD_BACKGROUND_IMG}";
        public const String USCSS_HEAD_BACKGROUND_HEIGHT_KEY = "{HEAD_BACKGROUND_IMG_HEIGHT}";
        public const String USCSS_BODY_BACKGROUND_COLOR_KEY = "{BODY_BACKGROUND_COLOR}";
        public const String USCSS_BODY_BACKGROUND_IMG_KEY = "{BODY_BACKGROUND_IMG}";
        public const String USCSS_BODY_BACKGROUND_REPEAT_KEY = "{BODY_BACKGROUND_REPEAT}";

        public const String FLASH_FLASHNAME = "{FLASHNAME}";
        public const String FLASH_FLASH_HEIGHT = "{FLASH_HEIGHT}";

        public const String JS_LANGUAGE_LIST = "{LANGUAGE_LIST}";

    }

    public class TransValue
    {
        //语言JS模板
        public const String LANGUAGE_LIST = "document.write(\"<div class='langSelect'><a href='../../{0}/pc'>{1}</a></div>\");";
        //public const String LANGUAGE_LIST = "document.write(\"<div class='langSelect'><img src='../STYLE/COMMON/RESOURCES/IMG/{0}.png' /><a href='{0}'>{1}</a></div>\");";

        public Hashtable v = new Hashtable();

        public String this[string name]
        {
            get { return (String)v[name]; }
            set { v[name] = value; }
        }
    }
}