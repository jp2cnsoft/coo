using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;
using Seika.Db;
using Seika.COO.DBA.MA;
using Seika.Common.Net;
using Seika.Transform.Command;
using Seika.Transform.Command.Enum;

/// <summary>
/// 生前台前HTML操作(生成临时XML及发送生成命令)
/// </summary>
/// <remarks>
/// 2008/01/02  于作伟  新规作成
/// 2008/08/15          接口函数修改,追加 tableRowFilter 用于xml表中记录过滤
/// 2008/09/25          不保存临时文件
///                     使用数据流生成文件
///                     生成接口
/// 2008/12/26          读取xml方式改为数据库
namespace Seika.COO.Web.PG
{
    public class UserSiteTrans : UserSiteTransBase
    {
        /// <summary>
        /// 生成发送数据
        /// </summary>
        /// <param name="ddType">生成类型</param>
        /// <param name="typeParam">在追加模式下的操作参数</param>
        /// <param name="xmlId">要操作的xml的ID</param>
        /// <param name="tableName">主xml中的操作的数据表名</param>
        /// <param name="filter">检索条件</param>
        /// <param name="xpath">xpath路径</param>
        /// <param name="sXsl">xsl的ID</param>
        /// <param name="opType">生成文件的类型</param>
        /// <param name="opExt">生成文件的后缀</param>
        /// <param name="xslName">生成文件所需要调用的样式名称</param>
        /// <param name="htmlId">生成文件的id</param>
        public void UpdateUserSiteTrans(String registId, String language, String ddType, String typeParam, XmlDocMangContent xmlDocCont, 
            String[] xmlId,String tableName, String filter, String xpath, String sXsl,
            String opType, String opExt, String xslName, String htmlId)
        {
            CreateUserSiteTrans(registId, language, ddType, typeParam, xmlDocCont, xmlId, 
                tableName, null, filter, xpath, sXsl, opType, opExt, xslName, htmlId);
        }

        /// <summary>
        /// 生成发送数据
        /// </summary>
        /// <param name="ddType">生成类型</param>
        /// <param name="typeParam">在追加模式下的操作参数</param>
        /// <param name="xmlId">要操作的xml的ID</param>
        /// <param name="tableName">主xml中的操作的数据表名</param>
        /// <param name="tableRowFilter">主xml中的操作的数据表行过滤表达式</param>
        /// <param name="filter">检索条件</param>
        /// <param name="xpath">xpath路径</param>
        /// <param name="sXsl">xsl的ID</param>
        /// <param name="opType">生成文件的类型</param>
        /// <param name="opExt">生成文件的后缀</param>
        /// <param name="xslName">生成文件所需要调用的样式名称</param>
        /// <param name="htmlId">生成文件的id</param>
        public void UpdateUserSiteTrans(String registId, String language, String ddType, String typeParam, XmlDocMangContent xmlDocCont, 
            String[] xmlId,String tableName, String tableRowFilter, String filter, String xpath, 
            String sXsl,String opType, String opExt, String xslName, String htmlId)
        {
            CreateUserSiteTrans(registId, language, ddType, typeParam, xmlDocCont, xmlId, 
                tableName, tableRowFilter, filter, xpath, sXsl, opType, opExt, xslName, htmlId);
        }

        /// <summary>
        /// 转换XML数据流(生成单个文件到语言下任何目录)
        /// </summary>
        /// <param name="language">语言</param>
        /// <param name="xmlDoc">xml数据流</param>
        /// <param name="xslName">生成文件所需要调用的样式名称</param>
        /// <param name="sXsl">xsl的ID</param>
        /// <param name="opType">生成文件的类型</param>
        /// <param name="opExt">生成文件的后缀</param>
        /// <param name="targetFileName">目标文件名称(可带路径)</param>
        /// <param name="targetFileName">是否添加共通XML</param>
        public void UpdateUserSiteTrans(String registId, String language, Stream xmlDoc, XmlDocMangContent xmlDocCont,String xslName, String sXsl,
            String opType, String opExt, String targetFileName, bool addStaticXml)
        {
            String[] xmlId = InitXmlId(null);
            SetSendData(registId, language, xmlDoc, xmlDocCont, sXsl, opType, 
                opExt, xslName, targetFileName, addStaticXml);
        }

        /// <summary>
        /// 生成发送数据
        /// </summary>
        /// <param name="ddType">生成类型</param>
        /// <param name="typeParam">在追加模式下的操作参数</param>
        /// <param name="xmlId">要操作的xml的ID</param>
        /// <param name="tableName">主xml中的操作的数据表名</param>
        /// <param name="tableRowFilter">主xml中的操作的数据表行过滤表达式</param>
        /// <param name="filter">检索条件</param>
        /// <param name="xpath">xpath路径</param>
        /// <param name="sXsl">xsl的ID</param>
        /// <param name="opType">生成文件的类型</param>
        /// <param name="opExt">生成文件的后缀</param>
        /// <param name="xslName">生成文件所需要调用的样式名称</param>
        /// <param name="htmlId">生成文件的id</param>
        private void CreateUserSiteTrans(String registId, String language, String ddType, String typeParam, XmlDocMangContent xmlDocCont, 
            String[] xmlId,String tableName, String tableRowFilter,String filter, String xpath, 
            String sXsl,String opType,String opExt, String xslName, String htmlId)
        {
            //过程调用
            switch (ddType)
            {
                //新对象追加
                case UserSiteTransCommand.COMMAND_NEW_RECORD:
                    break;
                //追加模式
                case UserSiteTransCommand.COMMAND_ADD_RECORD:
                    //按语言更新xml名称
                    String[] rxmlId = InitXmlId(xmlId);
                    ////取得操作xml的类集合
                    //XmlDocMangContent xmlDocCont = GetUserXmlList(registId, language, rxmlId);
                    //主xml管理类
                    m_xdManage = xmlDocCont[rxmlId[0]];
                    //取得主xml表
                    DataTable dt = InitUserSiteTrans(registId, m_xdManage, tableName, tableRowFilter);
                    switch (typeParam)
                    {
                        //升序
                        case UserSiteTransParam.PARAM_ASC:
                            break;
                        //降序
                        case UserSiteTransParam.PARAM_DESC:
                            PageLimitMult(registId, language, dt, filter, UserSiteTransParam.PARAM_DESC,
                                xmlId, xmlDocCont, xpath, sXsl, opType, 
                                opExt, xslName, htmlId);
                            break;
                        //生成单个文件
                        case UserSiteTransParam.PARAM_SINGLE:
                            PageSingle(registId, language, xmlId, xmlDocCont,xpath
                                ,sXsl, opType, opExt, xslName, htmlId);
                            break;
                        //降序生成全部
                        case UserSiteTransParam.PARAM_ALL_DESC:
                            PageMult(registId, language, dt, filter, UserSiteTransParam.PARAM_DESC,
                                xmlDocCont, xmlId, xpath, sXsl, opType, 
                                opExt, xslName, htmlId);
                            break;
                        //生成首页
                        case UserSiteTransParam.PARAM_INDEX:
                            PageSingleIndex(registId, language, xmlId, xmlDocCont, xpath,
                                sXsl, opType, opExt, xslName, htmlId);
                            break;
                        default:
                            break;
                    }
                    break;
                //删除模式
                case UserSiteTransCommand.COMMAND_DEL_RECORD:
                    if (m_cs != null)
                        m_cs.DeleteUserTransFiles(registId, language, htmlId, false);
                    break;
                default:
                    break;
            }
        }

        #region 初期化数据
        //基础数据
        PageXmlSymbol m_pageXmlSym = new PageXmlSymbol();
        //主xml文档
        XmlDocManage m_xdManage;
        //固定转换值
        int staticMaxPage = 0;
        //网站url地址
        String m_manageUrl = String.Empty;
        //生成文件后缀
        String m_pageExt = String.Empty;
        //生成使用html的id
        String m_htmlId = String.Empty;
        //过滤字符串
        String m_filter = String.Empty;

        public UserSiteTrans() { }

        //初期化参数
        protected void Init(String serverUrl) 
        {
            //生成发送到生成html服务的数据集url路径
            m_manageUrl = serverUrl;
            //生成文件生成的页面后缀
            m_pageExt = CodeSymbol.m_destinatExt;
        }

        /// <summary>
        /// 初期化基本值
        /// </summary>
        /// <param name="cXml">操作XML的ID</param>
        /// <param name="tableName">取得的表名</param>
        /// <param name="tableRowFilter">过滤表表达式</param>
        /// <returns></returns>
        private DataTable InitUserSiteTrans(String registId, XmlDocManage xmlDoc, String tableName, String tableRowFilter)
        {
            //读取系统xml
            m_dsManage = new DataSetManage(xmlDoc);
            //取得XML表
            DataTable dt = m_dsManage.Get.Tables[tableName];
            //过滤表行数据
            if (!String.IsNullOrEmpty(tableRowFilter))
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = tableRowFilter;
                dt = dv.ToTable();
            }
            return dt;
        }

        //按当前语言初期化当前XML名称
        private String[] InitXmlId(String[] xmlId)
        {
            List<String> _list = new List<String>();
            if (xmlId != null)
            {
                foreach (String s in xmlId)
                {
                    _list.Add(s);
                }
            }
            String[] staticXml = new String[] { m_pageXmlSym.P3010P0940,m_pageXmlSym.Menu };
            foreach (String s in staticXml)
            {
                _list.Add(s);
            }
            return _list.ToArray();
        }
        #endregion 

        #region 生成具体操作
        //首页生成处理命令
        private void PageSingleIndex(String registId,String language, String[] xmlId, XmlDocMangContent xmlList, String xpath,
            String sXsl, String opType, String opExt,String xslName, String htmlId)
        {
            XmlDocManage m_Manage = new XmlDocManage();
            m_Manage.CreateNode(xpath);
            m_Manage.AppendXmlNode(xpath, m_xdManage.GetXmlNode(xpath));
            AppenAndSaveIndexXml(registId, language, m_Manage, xmlList, xmlId, htmlId);
            //XML内存流
            MemoryStream ms = m_Manage.GetXmlDocStream();
            ms.Seek(0, SeekOrigin.Begin);
            //生成单个文件
            SendDataSingle(registId, language, ms, sXsl, opType, opExt,xslName, htmlId, "");
            //关闭流
            ms.Close();
        }

        //单个处理命令
        private void PageSingle(String registId, String language, String[] xmlId, XmlDocMangContent xmlList
            , String xpath,String sXsl, String opType,String opExt, String xslName, String htmlId)
        {
            XmlDocManage m_Manage = new XmlDocManage();
            m_Manage.CreateNode(xpath);
            m_Manage.AppendXmlNode(xpath, m_xdManage.GetXmlNode(xpath));
            String htmlPara = GetXpathAttrId(xpath);
            AppenAndSaveXml(registId, language, m_Manage,
                xmlList, xmlId, (!String.IsNullOrEmpty(htmlPara) ? htmlId + "_" + htmlPara : htmlId));
            //XML内存流
            MemoryStream ms = m_Manage.GetXmlDocStream();
            ms.Seek(0, SeekOrigin.Begin);
            //生成单个文件
            SendDataSingle(registId, language, ms, sXsl, 
                opType, opExt, xslName, htmlId, htmlPara);
            //关闭流
            ms.Close();
        }

        //多页追加命令
        private void PageLimitMult(String registId,String language,DataTable sdt, String sFilter, String sort, 
            String[] xmlId, XmlDocMangContent xmlList, String xpath,String sXsl,
            String opType, String opExt, String xslName, String htmlId)
        {
            if (xmlId == null || xmlId.Length < 1) return;
            String pageXmlName = xmlId[1];
            //取得每页显示数
            int pageSize = CodeSymbol.m_pageNum;
            ArrayList filter = String2ArrayFilter(sFilter);
            for (int i = 1; i < filter.Count; i++)
            {
                //类别取得
                String typeId = filter[i].ToString();
                DataTable dt = new DataTable();
                bool typestate = DataTable2Filter(sdt, ref dt, filter[0].ToString(), typeId);
                //记录总数
                int count = dt.Rows.Count;
                if (count > 0)
                {
                    int tempcount = count - 1;
                    //第一页起止行
                    int firstPageStart = 0;
                    int firstPageLen = 0;
                    //第二页起止行
                    int secondPageStart = 0;
                    int secondPageLen = 0;
                    //第三页起止行
                    int threePageStart = 0;
                    int threePageLen = 0;

                    //少于一页
                    if (count < pageSize * 2)
                    {
                        firstPageStart = tempcount;
                        firstPageLen = count;
                    }
                    //一页
                    else if (count == pageSize * 2)
                    {
                        firstPageStart = tempcount;
                        secondPageStart = tempcount - pageSize;
                        secondPageLen = firstPageLen = pageSize;
                    }
                    //多于一页
                    else
                    {
                        firstPageStart = tempcount;
                        firstPageLen = count - (count / pageSize - 1) * pageSize;

                        //分出一页
                        if (count % pageSize == 0)
                        {
                            secondPageStart = (count / pageSize - 1) * pageSize - 1;
                            secondPageLen = pageSize;
                            //设置第二分页
                            if (count / pageSize > 2)
                            {
                                threePageStart = (count / pageSize - 2) * pageSize - 1;
                                threePageLen = pageSize;
                            }
                        }
                    }

                    List<Stream> xmlDocCont = new List<Stream>();
                    XmlDocManage m_oxdManage = null;
                    if (firstPageLen > 0)
                    {
                        //克隆保留的记录
                        m_oxdManage = CloneXml(m_xdManage, xpath, dt, sort, firstPageStart, firstPageLen);
                        AppenAndSaveXml(registId, language, m_oxdManage, xmlList, xmlId, htmlId + "_" + typeId);
                        MemoryStream memStream = m_oxdManage.GetXmlDocStream();
                        memStream.Seek(0, SeekOrigin.Begin);
                        xmlDocCont.Add(memStream);
                    }

                    bool newState = false;
                    if (secondPageLen > 0)
                    {
                        //克隆分出的记录
                        m_oxdManage = CloneXml(m_xdManage, xpath, dt, sort, secondPageStart, secondPageLen);
                        AppenAndSaveXml(registId, language, m_oxdManage, xmlList, xmlId, htmlId + "_" + typeId);
                        //更新分页xml
                        XmlDocManage pXmlMang = xmlList[pageXmlName];
                        UpdateCurrPageNo(registId, language, ref pXmlMang, pageXmlName, typestate, typeId, count / pageSize - 1);
                        xmlList[pageXmlName] = pXmlMang;
                        SaveUserXml(registId, language, pageXmlName, pXmlMang);

                        MemoryStream memStream = m_oxdManage.GetXmlDocStream();
                        memStream.Seek(0, SeekOrigin.Begin);
                        xmlDocCont.Add(memStream);
                        newState = true;
                    }

                    if (threePageLen > 0)
                    {
                        //克隆分出的记录
                        m_oxdManage = CloneXml(m_xdManage, xpath, dt, sort, threePageStart, threePageLen);
                        AppenAndSaveXml(registId, language, m_oxdManage, xmlList, xmlId, htmlId + "_" + typeId);
                        MemoryStream memStream = m_oxdManage.GetXmlDocStream();
                        memStream.Seek(0, SeekOrigin.Begin);
                        xmlDocCont.Insert(1,memStream);
                    }
                    SendDataDesc(registId, language, xmlDocCont, newState, typeId, 
                        sXsl, opType, opExt,xslName, htmlId,
                        GetCurrPageNo(registId, xmlList,pageXmlName, typestate, typeId));
                    //关闭集合中数据流
                    foreach (Stream s in xmlDocCont) s.Close();
                }
            }
        }
        //多页xml生成
        private void PageMult(String registId, String language, DataTable sdt, String sFilter, String sort, 
            XmlDocMangContent xmlList, String[] xmlId, String xpath, String sXsl, 
            String opType, String opExt, String xslName, String htmlId)
        {
            if (xmlId[1] == null) return;
            //分页XML路径
            String sXml = xmlId[1].ToString();
            //取得每页显示数
            int pageSize = CodeSymbol.m_pageNum;

            ArrayList filter = String2ArrayFilter(sFilter);
            //分页xml
            XmlDocManage pXmlMang = xmlList[sXml];

            for (int fi = 0; fi < filter.Count; fi++)
            {
                String typeId = filter[fi].ToString();
                bool blankPageState = false;
                //清空页号
                if (sdt == null)
                {
                    if (fi > 0)
                        blankPageState = true;
                    UpdateCurrPageNo(registId, language, ref pXmlMang, sXml, ((CodeSymbol.m_noneType == typeId) ? false : true), typeId, 0);
                    xmlList[sXml] = pXmlMang;
                }
                //修改操作
                else
                {
                    DataTable dt = new DataTable();
                    bool typestate = DataTable2Filter(sdt, ref dt, filter[0].ToString(), typeId);

                    //记录总数
                    int count = dt.Rows.Count;
                    if (count > 0)
                    {
                        //临时XML数据流集合
                        List<Stream> xmlDocCont = new List<Stream>();
                        for (int i = count; i > 0; )
                        {
                            //当前条数
                            int pSize = (((i % pageSize) == 0) ? pageSize : (i % pageSize));
                            //如果第一页不满双倍规定条数
                            if (pSize < pageSize) { pSize = pSize + pageSize; }
                            XmlDocManage nxdManage = CloneXml(m_xdManage, xpath, dt, sort, i - 1, pSize);
                            AppenAndSaveXml(registId, language, nxdManage,xmlList, xmlId, htmlId + "_" + typeId);
                            MemoryStream memStream = nxdManage.GetXmlDocStream();
                            memStream.Seek(0, SeekOrigin.Begin);
                            xmlDocCont.Insert(0, memStream);
                            i -= pSize;
                        }
                        //更新分页xml
                        UpdateCurrPageNo(registId, language, ref pXmlMang, sXml, typestate, typeId, (count < pageSize * 2) ? 0 : (count / pageSize - 1));
                        xmlList[sXml] = pXmlMang;

                        SendDataMult(registId, language, xmlDocCont, typeId, sXsl, opType, opExt,xslName, htmlId);
                        
                        //关闭集合中数据流
                        foreach (Stream s in xmlDocCont) s.Close();
                    }
                    if (count < 1 && fi > 0)
                    {
                        blankPageState = true;
                    }
                }
                //一览生成空白页
                if (blankPageState)
                {
                    //临时XML数据流集合
                    List<Stream> xmlDocContBlank = new List<Stream>();
                    XmlDocManage nxdManage = new XmlDocManage();
                    nxdManage.CreateNode(xpath);
                    AppenAndSaveXml(registId, language, nxdManage, xmlList,xmlId, htmlId + "_" + typeId);
                    MemoryStream memStream = nxdManage.GetXmlDocStream();
                    memStream.Seek(0, SeekOrigin.Begin);
                    xmlDocContBlank.Add(memStream);
                    SendDataBlankMult(registId, language, xmlDocContBlank, typeId, sXsl, opType, opExt, xslName, htmlId);
                    memStream.Close();
                }
            }
            //更新分页xml到数据库
            SaveUserXml(registId, language, sXml, pXmlMang);
        }
        //生成单件命令
        private void SendDataSingle(String registId, String language, Stream xmlDoc, String sXsl, String opType
            , String opExt, String xslName, String htmlId, String htmlPara)
        {
            //生成的html
            String[] sendHtml = new String[] { htmlId + ((!String.IsNullOrEmpty(htmlPara)) ? ("_" + htmlPara) : "") + m_pageExt };

            List<Stream> xmlDocCont = new List<Stream>();
            xmlDocCont.Add(xmlDoc);

            SetSendData(registId, language, xmlDocCont, sXsl, opType, 
                opExt, xslName, sendHtml, "", null, null);
        }
        //倒序生成命令
        private void SendDataDesc(String registId, String language, List<Stream> xmlDocCont, bool newState, String typeId, 
            String sXsl, String opType, String opExt, String xslName, String htmlId, 
            int nextPageNo)
        {

            //生成的html
            String[] sendHtml = null;
            //前一页
            String[] pervPage = null;
            //后一页
            String[] nextPage = null;

            String xmlHtml = htmlId + "_" + typeId + "_";
            //单页处理
            if (!newState)
            {

                sendHtml = new String[] { xmlHtml + staticMaxPage + m_pageExt };
                pervPage = new String[] { "" };

                if (nextPageNo == staticMaxPage)
                {
                    nextPage = new String[] { "" };
                }
                else
                {
                    nextPage = new String[] { xmlHtml + nextPageNo.ToString() };
                }
            }
            //多页处理
            else
            {
                //只存在一个分页
                if (nextPageNo == 1)
                {
                    String nxmlhtml = xmlHtml + nextPageNo;
                    sendHtml = new String[] { xmlHtml + staticMaxPage + m_pageExt, nxmlhtml + m_pageExt };
                    pervPage = new String[] { "", xmlHtml + staticMaxPage };
                    nextPage = new String[] { nxmlhtml, "" };
                }
                else if (nextPageNo > 1)
                {
                    String nxmlHtml = xmlHtml + nextPageNo;
                    String nnxmlHtml = xmlHtml + Convert.ToString(nextPageNo - 1);
                    String nnnxmlHtml = String.Empty;
                    if (nextPageNo > 2)
                    {
                        nnnxmlHtml = xmlHtml + Convert.ToString(nextPageNo - 2);
                    }

                    sendHtml = new String[] { xmlHtml + staticMaxPage + m_pageExt, nnxmlHtml + m_pageExt, nxmlHtml + m_pageExt };
                    pervPage = new String[] { "", nxmlHtml, xmlHtml + staticMaxPage };
                    nextPage = new String[] { nxmlHtml, nnnxmlHtml, nnxmlHtml };
                }
            }
            //发送设置数据到服务
            SetSendData(registId, language, xmlDocCont, sXsl, opType, 
                opExt, xslName, sendHtml, typeId, pervPage, nextPage);
        }
        //生成空页命令
        private void SendDataBlankMult(String registId, String language, List<Stream> xmlDocCont, String typeId, String sXsl, String opType, String opExt,
            String xslName, String htmlId)
        {
            String[] sendHtml = new String[] { htmlId + "_" + typeId + "_0" + m_pageExt };
            String[] eEmpty = new String[] { "" };
            SetSendData(registId, language, xmlDocCont, sXsl, opType, opExt, xslName, sendHtml, typeId, eEmpty, eEmpty);
        }
        //生成多条发送命令
        private void SendDataMult(String registId,String language,List<Stream> xmlDocCont, String typeId, String sXsl, String opType, String opExt,
            String xslName, String htmlId)
        {
            //临时XML文档数
            int docCount = xmlDocCont.Count;
            //生成的html
            String[] sendHtml = new String[docCount];
            //前一页
            String[] pervPage = new String[docCount];
            //后一页
            String[] nextPage = new String[docCount];

            String xmlHtml = htmlId + "_" + typeId + "_";

            int cbegin = docCount - 1;
            for (int i = cbegin; i >= 0; i--)
            {
                sendHtml[i] = xmlHtml + ((i == cbegin) ? "0" : Convert.ToString(i + 1)) + m_pageExt;
                pervPage[i] = ((i == cbegin) ? "" : xmlHtml + Convert.ToString((i + 1 == cbegin) ? 0 : (i + 2)));
                nextPage[i] = (i == 0 ? "" : xmlHtml + i.ToString());
            }

            //发送设置数据到服务
            SetSendData(registId, language, xmlDocCont, sXsl, opType, opExt, xslName, sendHtml, typeId, pervPage, nextPage);
        }
        #endregion   

        #region 页号处理
        //取得当前页号
        private int GetCurrPageNo(String registId, XmlDocMangContent xmlList, String sXml, bool typestate, String typeId)
        {
            String pageTableName = "";
            String pageField = "maxpage";
            DataSetManage m_cuManage = new DataSetManage(xmlList[sXml]);
            if (typestate)
            {
                pageTableName = "name";
                DataTable dt = m_cuManage.Get.Tables[pageTableName];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "typeid=" + typeId;
                dt = dv.ToTable();
                if (dt.Rows.Count == 1)
                {
                    return Convert.ToInt32(dt.Rows[0][pageField].ToString());
                }
            }
            //取得页号
            else
            {
                pageTableName = "alltype";
                DataTable dt = m_cuManage.Get.Tables[pageTableName];
                return Convert.ToInt32(dt.Rows[0][pageField].ToString());
            }
            return -1;
        }

        //更新当前页号
        private void UpdateCurrPageNo(String registId, String language, ref XmlDocManage xmlDoc, String xmlid, bool typestate, String typeId, int pageNo)
        {
            String pageTableName = "";
            String pageField = "maxpage";
            DataSetManage m_cuManage = new DataSetManage(xmlDoc);
            if (typestate)
            {
                pageTableName = "name";
                DataTable dt = m_cuManage.Get.Tables[pageTableName];
                if (dt == null) return;
                foreach (DataRow r in dt.Rows)
                {
                    if (r["typeid"].ToString() == typeId)
                    {
                        r[pageField] = pageNo;
                    }
                }
            }
            else
            {
                pageTableName = "alltype";
                DataTable dt = m_cuManage.Get.Tables[pageTableName];
                dt.Rows[0][pageField] = pageNo;
            }
            m_cuManage.WriteLocalDataSet2XmlStream();
            xmlDoc = m_cuManage.Xml;
        }
        #endregion
        
        #region 主页处理
        //追加保存主页xml
        private void AppenAndSaveIndexXml(String registId,String language,XmlDocManage xmlDocManage, XmlDocMangContent xmlList
            , String[] xmlId, String htmlId)
        {
            //系统xml追加
            if (xmlId == null) return;
            String[] snum = GetIndexNum(registId, language, xmlList);
            for (int i = 1; i < xmlId.Length; i++)
            {
                XmlDocManage m_xdManage = xmlList[xmlId[i]];
                XmlDocManage m_nxdManage = null;
                DataSetManage dsm = new DataSetManage(m_xdManage);
                if (xmlId[i] == m_pageXmlSym.P3050P0020)
                {
                    DataTable dt = dsm.Get.Tables["prod"];
                    if (dt != null)
                        m_nxdManage = CloneXml(m_xdManage, "chxml/content/prod[@id='{0}']", dt, UserSiteTransParam.PARAM_DESC, dt.Rows.Count - 1, Convert.ToInt32(snum[0]));
                }
                if (xmlId[i] == m_pageXmlSym.P3040P0020)
                {
                    DataTable dt = dsm.Get.Tables["news"];
                    if (dt != null)
                        m_nxdManage = CloneXml(m_xdManage, "chxml/content/news[@id='{0}']", dt, UserSiteTransParam.PARAM_DESC, dt.Rows.Count - 1, Convert.ToInt32(snum[1]));
                }
                if (xmlId[i] == m_pageXmlSym.P3010P0340)
                {
                    DataTable dt = dsm.Get.Tables["method"];
                    if (dt != null)
                        m_nxdManage = CloneXml(m_xdManage, "chxml/content/method[@id='{0}']", dt, UserSiteTransParam.PARAM_ASC, 0, 1);
                }
                if (m_nxdManage != null)
                    xmlDocManage.AppendXml(m_nxdManage.xml);
            }
            AppendStaticXml(registId,language,xmlDocManage,xmlList, htmlId,true);
        }

        //取得首页产品新闻显示条数
        private String[] GetIndexNum(String registId,String language,XmlDocMangContent xmlList)
        {
            String[] num = new String[] { "5", "5" };
            DataSetManage m_cuManage = new DataSetManage(xmlList[m_pageXmlSym.P3010P0950]);
            DataTable dt = m_cuManage.Get.Tables["name"];
            if (dt == null) return num;
            num[0] = dt.Rows[0]["shownum"].ToString();
            num[1] = dt.Rows[1]["shownum"].ToString();
            return num;
        }
        #endregion

        #region XML处理
        //追加保存xml
        private void AppenAndSaveXml(String registId,String language,XmlDocManage xmlDocManage, XmlDocMangContent xmlList
            ,String[] xmlId, String htmlId)
        {
            //添加配置性xml
            AppenXml(registId, xmlDocManage, xmlList, xmlId, 1);
            //添加固定节点xml
            AppendStaticXml(registId, language, xmlDocManage, xmlList, htmlId,true);
        }

        //保存xml
        protected void AppendStaticXml(String registId, String language, XmlDocManage xmlDocManage, XmlDocMangContent xmlList
            ,String htmlId,bool siteMapSta)
        {
            //添加友情链接xml
            AppenXml(registId, xmlDocManage, xmlList, new String[] { m_pageXmlSym.P3010P0940 }, 0);
            //添加system的xml
            xmlDocManage.AppendXml(FormatSystemXml(registId, language, xmlList, htmlId).xml);
            //添加navi的xml
            xmlDocManage.AppendXml(FormatMenuXml(registId, language, xmlList[m_pageXmlSym.Menu], m_manageUrl, htmlId).xml);
            //添加tree菜单的xml
            UserSiteSiteMap treemenu = new UserSiteSiteMap(registId, language, "treemenu", xmlList);
            xmlDocManage.AppendXml(treemenu.GetXmlDoc().xml);
            if (siteMapSta)
            {
                //添加sitemap
                UserSiteSiteMap sitmap = new UserSiteSiteMap(registId, language, "sitemap",xmlList);
                xmlDocManage.AppendXml(sitmap.GetXmlDoc().xml);
            }
        }

        //添加用户目录下xml
        private void AppenXml(String registId, XmlDocManage xml, XmlDocMangContent xmlList, String[] xmlId, int start)
        {
            //系统xml追加
            if (xmlId == null) return;
            for (int i = start; i < xmlId.Length; i++)
            {
                xml.AppendXml(xmlList[xmlId[i]].xml);
            }
        }
        #endregion

        #region 格式化数据处理
        //对调用的共通System.xml文件进行格式化,添加语言及页面信息
        private XmlDocManage FormatSystemXml(String registId, String language, XmlDocMangContent xmlList, String htmlId)
        {
            DataTable seoDt = xmlList.SeoKeyword;

            DataSetManage dsManager = new DataSetManage(xmlList[m_pageXmlSym.System]);

            //更新header节点下用户名
            DataTable dt = dsManager.Get.Tables["header"];
            //添加seo关键字节点
            dt.Columns.Add("seokeyword");

            //当前Parameters节点下当前页面
            DataTable pdt = dsManager.Get.Tables["Parameters"];
            m_rsMansge = new ResManager();
            //取得html页面编号,按下划线截取
            String[] shtml = htmlId.Split('_');
            //页面编号初始化
            String fhtml = String.Empty;
            //页面详细页id初始化
            String dhtml = String.Empty;
            //取得html的id
            if (shtml.Length > 0)
            {
                fhtml = shtml[0];
            }
            //取得html的详细页id
            if (shtml.Length > 1)
            {
                dhtml = shtml[1];
            }

            //用户名和seo关键字初始化
            foreach (DataRow r in dt.Rows)
            {
                r["registerid"] = registId;
                DataView seodv = seoDt.DefaultView;
                seodv.RowFilter = String.Format(" DETAILID = '{0}' and PAGEID = '{1}'",dhtml, fhtml );
                DataTable seoddv = seodv.ToTable();
                //数据库中存在SEO关键字
                if (seoddv.Rows.Count > 0)
                {
                    r["seokeyword"] = "<meta name=\"keywords\" content=\"" + seoddv.Rows[0]["EXPLAIN"].ToString() + "\" />";
                }
                //不存在关键字则在资源文件中读取默认值
                else
                {
                    r["seokeyword"] = "<meta name=\"keywords\" content=\"" + m_rsMansge.GetGlobalRes("PageSeo", fhtml + ".Keyword") + "\" />";
                }
            }
           
            //页面编号不为空执行取其父菜单操作
            if (!String.IsNullOrEmpty(fhtml))
            {
                foreach (DataRow r in pdt.Rows)
                {
                    //主菜单设置
                    r["mainmenuurl"] = m_rsMansge.GetGlobalRes("PageMenu", fhtml + ".Menu");
                    //子菜单设置
                    r["submenu"] = fhtml;
                }
            }
            dsManager.WriteLocalDataSet2XmlStream();
            return dsManager.Xml;
        }

        //对导航菜单进行格式化设置
        private XmlDocManage FormatMenuXml(String registId,String language,XmlDocManage xmlDoc, String url, String htmlId)
        {
            DataSetManage dsManager = new DataSetManage(xmlDoc);
            DataTable dt = dsManager.Get.Tables["mainitem"];
            DataTable ndt = new DataTable("mainitem");
            ndt.Columns.Add("id");
            ndt.Columns.Add("url");
            ndt.Columns.Add("menushow");
            ndt.Columns.Add("manageshow");
            ndt.Columns.Add("content");
            ndt.Columns.Add("managelink");
            
            ndt.Columns.Add("status");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = ndt.NewRow();
                r["id"] = dt.Rows[i]["id"].ToString();
                r["menushow"] = dt.Rows[i]["menushow"].ToString();
                r["manageshow"] = dt.Rows[i]["manageshow"].ToString();
                r["content"] = dt.Rows[i]["content"].ToString();
                r["managelink"] = dt.Rows[i]["managelink"].ToString();
                r["status"] = "M";
                if (dt.Rows[i]["id"].ToString() == "manage")
                {
                    r["url"] = url + "Pages/P3010/P3010P0040.aspx?uId=" + registId + "&htmlId=" + htmlId;
                }
                else
                {
                    r["url"] = dt.Rows[i]["url"].ToString();
                }
                ndt.Rows.Add(r);
            }
            ArrayList tPropert = new ArrayList();
            tPropert.Add("id");
            tPropert.Add("url");
            tPropert.Add("menushow");
            xmlDoc.UpdateLocalXml("chxml/mainmenu", ndt, tPropert, false, new String[] { "status", "1" });
            return xmlDoc;
        }
        #endregion

        #region 发送数据到服务
        //发送设置数据到服务(源数据流)
        private void SetSendData(String registId, String language, Stream xmlDoc, XmlDocMangContent xmlList, String sXsl
            , String opType, String opExt, String xslName, String dFileName,bool addStaticXml)
        {
            //允许添加共通xml
            if(addStaticXml)
            {
                xmlDoc.Seek(0, SeekOrigin.Begin);
                XmlDocManage xmlDocMang = new XmlDocManage(xmlDoc);
                //添加共通xml
                AppendStaticXml(registId, language, xmlDocMang, xmlList, dFileName,false);
                xmlDoc = xmlDocMang.GetXmlDocStream();
                xmlDoc.Seek(0, SeekOrigin.Begin);
                //生成的文件名称
                dFileName = dFileName + "." + opExt;
            }
            List<Stream> xmlDocCont = new List<Stream>();
            xmlDocCont.Add(xmlDoc);
            SetSendData(registId, language, m_baseFunction.m_common_type, registId, xmlDocCont,
               sXsl, opType, opExt, xslName, new String[] { dFileName }, 
               "", null, null);
        }

        //发送设置数据到服务(生成数据流)
        private void SetSendData(String registId, String language, List<Stream> xmlDocCont, String sXsl, String opType, 
            String opExt, String xslName, String[] dFileName, String groupCategory, String[] pervPage, String[] nextPage)
        {
            SetSendData(registId, language, m_baseFunction.m_common_type, registId, xmlDocCont, 
                sXsl, opType, opExt, xslName, dFileName, 
                groupCategory, pervPage, nextPage);
        }

        /// <summary>
        /// 发送设置数据到服务
        /// </summary>
        /// <param name="commonType">操作文件主目录</param>
        /// <param name="subCommonType">操作文件子目录</param>
        /// <param name="language">语言</param>
        /// <param name="sFileName">处理文件名</param>
        /// <param name="sXsl">生成样式的xsl</param>
        /// <param name="opType">生成文件类型(html,pdf,css,js)</param>
        /// <param name="opType">生成文件后缀名(html,pdf,css,js)</param>
        /// <param name="xslName">生成文件的样式(xslname)</param>
        /// <param name="dFileName">生成的html</param>
        /// <param name="groupCatagory">生成html类别</param>
        /// <param name="pervPag">前页链接</param>
        /// <param name="nextPage">后页链接</param>
        private void SetSendData(String registId, String language, String commonType, String subCommonType, List<Stream> xmlDocCont,
            String sXsl, String opType, String opExt,String xslName,String[] dFileName, 
            String groupCategory, String[] pervPage, String[] nextPage)
        {
            try
            {
                //当前用户
                for (int i = 0; i < xmlDocCont.Count; i++)
                {
                    String styleId = xslName;
                    String xslPath = sXsl + ".xsl";
                    String targetFilename = dFileName[i];
                    String prevPageUrl = (pervPage != null) ? pervPage[i] : "";
                    String nextPageUrl = (nextPage != null) ? nextPage[i] : "";
                    String category = groupCategory;

                    Stream sm = xmlDocCont[i];
                    if (m_cs != null)
                    {
                        m_cs.Transform_fs(sm,
                            registId,
                            language,
                            (FILE_TYPE)int.Parse(opType),
                            opExt,
                            styleId,
                            xslPath,
                            targetFilename,
                            prevPageUrl, nextPageUrl,
                            category);
                        //WriteLog.WriteDebug("userid=" + registId + " ");
                        //WriteLog.WriteDebug("language=" + language + " ");
                        //WriteLog.WriteDebug("styleId=" + styleId + " ");
                        //WriteLog.WriteDebug("xslPath=" + xslPath + " ");
                        //WriteLog.WriteDebug("targetFilename=" + targetFilename + " ");
                        //WriteLog.WriteDebug("-------------------------------------------------------------------");
                    }
                    //if (targetFilename.ToLower() == "index.php")
                    //{
                    //    //生成测试用临时文件
                    //    FileStream fs = new FileStream(Server.MapPath("~/")
                    //        + String.Format(@"\index.php", registId, targetFilename)
                    //        , FileMode.Create);
                    //    MemoryStream msm = (MemoryStream)sm;
                    //    msm.WriteTo(fs);
                    //    fs.Close();
                    //}
                    
                }
            }
            catch (SysException es)
            {
                WriteLog.WriteDebug(es.ToString());
                //throw new SysException("", es);
            }
        }
        #endregion
    }
}
