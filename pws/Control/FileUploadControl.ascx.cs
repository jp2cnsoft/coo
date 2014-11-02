using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.COO.Web.PG;
using Seika.CooException;

public partial class Pages_FileUploadControl : ControlBase
{
    FileTools ft = new FileTools();
    /// <summary>
    /// 自定义控件名称
    /// </summary>
    public String ClientControlName
    {
        set{ ViewState["clientControlName"] = value; }
        get
        {
            if (ViewState["clientControlName"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["clientControlName"];
        }
    }
    /// <summary>
    /// 上传文件路径
    /// </summary>
    public String FileUpPath 
    {
        set { ViewState["fileUpPath"] = value; }
        get
        {
            if (ViewState["fileUpPath"] == null)
            {
                return @"/";
            }
            return ((String)ViewState["fileUpPath"]) == "" ? @"/" : ((String)ViewState["fileUpPath"]);
        }
    }
    /// <summary>
    /// 上传文件名
    /// </summary>
    public ArrayList FileName 
    {
        set
        {
            ViewState["fileName"] = value;
        }
        get
        {
            if (ViewState["fileName"] == null)
            {
                return new ArrayList();
            }
            return (ArrayList)ViewState["fileName"];
        }
    }
    /// <summary>
    /// 上传临时文件名
    /// </summary>
    public ArrayList FileNameTemp
    {
        set{ ViewState["fileNameTemp"] = value; }
        get
        {
            if (ViewState["fileNameTemp"] == null)
            {
                return new ArrayList();
            }
            return (ArrayList)ViewState["fileNameTemp"];
        }
    }

    /// <summary>
    /// 允许上传文件后缀名
    /// </summary>
    public String[] FileExt
    {
        set{ ViewState["fileExt"] = value; }
        get
        {
            if (ViewState["fileExt"] == null)
            {
                return new String[] { "GIF", "ICO", "BMP", "JPG", "JPEG", "PNG" };
            }
            return (String[])ViewState["fileExt"];
        }
    }

    /// <summary>
    /// 允许上传文件个数
    /// </summary>
    public int FileLen
    {
        set{ ViewState["fileLen"] = value; }
        get
        {
            if (ViewState["fileLen"] == null)
            {
                return 0;
            }
            return (int)ViewState["fileLen"];
        }
    }

    /// <summary>
    /// 允许上传文件大小
    /// </summary>
    public int FileSize
    {
        set{ ViewState["fileSize"] = value * 1024; }
        get
        {
            if (ViewState["fileSize"] == null)
            {
                return 0;
            }
            return (int)ViewState["fileSize"];
        }
    }

    /// <summary>
    /// 上传文件组件类型(0:单文件;1:多文件)
    /// </summary>
    public String FileType
    {
        set{ ViewState["fileType"] = value; }
        get
        {
            if (ViewState["fileType"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["fileType"];
        }
    }

    /// <summary>
    /// 说明
    /// </summary>
    public String FileRemark
    {
        set{ ViewState["fileRemark"] = value; }
        get
        {
            if (ViewState["fileRemark"] == null)
            {
                return String.Empty;
            }
            return (String)ViewState["fileRemark"];
        }
    }

    /// <summary>
    /// 上传文件目标类型(上传到前台的路径 mystyle or img)
    /// </summary>
    public String updateFileType 
    {
        set { ViewState["updateFileType"] = value; }
        get
        {
            if (ViewState["updateFileType"] == null)
            {
                return UpdateFileType.IMG;
            }
            return (String)ViewState["updateFileType"];
        }
    }

    public String imgdel;

    protected void Page_Load(object sender, EventArgs e)
    {
        PubInit();
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    //初始化全局变量
    private void PubInit() 
    {
        m_sessionManager = SessionManager.GetSessionManager(Session);
        imgdel = m_sessionManager.Page_UICultureID;
        //m_usrSiteMager = new UserSiteManager(GetLocalPath(), ServerUrlPath(), m_sessionManager.PageLogin_RegistId, m_sessionManager.PageLogin_HtmlStyle);
    }

    //初始化页面
    public void InitPage() 
    {
        PubInit();
        //初始化控件部分
        InitControl();
        //客户端页面初始化
        FileUploadShow();
        //初始化本地数据文件
        InitLDb();
    }

    //初始化控件
    public void InitControl() 
    {
        //设置系统默认支持的图片格式(外部传递值暂不起作用)
        //FileExt = new String[] { "GIF","ICO","BMP","JPG","JPEG","PNG"};
        //设置说明
        lblRemark.Text = FileRemark;
    }


    //客户端页面初始化
    private void FileUploadShow()
    {
        //单文件上传模式
        if (FileType == "0")
        {
            //制作单个上传客户端控件
            StringBuilder _cjs = new StringBuilder();
            m_sessionManager = new SessionManager(Session);
            //_cjs.AppendFormat("<a href=\"javascript:void(0);\" class=\"upfile\" style=\"background-image:url(../../Images/{0}_upfile.png);\">", m_sessionManager.Page_UICultureID);
            //_cjs.Append("<input type=\"file\" name=\"{0}\" id=\"{0}\"");
            //_cjs.Append("class=\"upfile\" style=\"width:200px;\"");
            //_cjs.Append("onChange=UpFileSingleChange('{0}','{1}') ");
            //_cjs.Append("onkeydown=\"event.returnValue=false;\" onpaste=\"return false;\"/>");
            //_cjs.Append("</a><span id=\"{1}\"></span>");
            _cjs.Append("<input type=\"file\" name=\"{0}\" id=\"{0}\"");
            _cjs.Append("class=\"upfile\"");
            _cjs.Append("onChange=UpFileSingleChange('{0}','{1}') ");
            _cjs.Append("/>");
            _cjs.Append("<span id=\"{1}\"></span>");
            //回写到客户端
            ltlAdd.Text = String.Format(_cjs.ToString(),
            ClientControlName, ClientControlName + "_prePic");
        }
        //多文件上传模式
        else if (FileType == "1")
        {
            //制作追加客户端控件
            StringBuilder _cjs = new StringBuilder();
            String fileTable = ClientControlName + "_FileTable";
            _cjs.Append("<input type=\"button\" onclick=\"AddFileTableRow('{0}','{1}','{2}','{3}')\" value=\"{4}\" />");
            //回写到客户端
            ltlAdd.Text = String.Format(_cjs.ToString(), fileTable,
                hidReShow.UniqueID.ToString(), ClientControlName,m_sessionManager.Page_UICultureID,
                GetResource("R00010.Text"));
            //客户端显示上传控件表格
            ltlTable.Text = String.Format("<table width=\"400px\" id=\"{0}\"></table>", fileTable);
        }
    }

    //初始化本地数据文件
    public void InitLDb() 
    {
        String fileUrlHead = ServerUrlPath();
        //建立操作表用于保存上传文件信息
        DataSetManage ds = new DataSetManage("file");
        ds.AddColumns("ID");
        ds.AddColumns("FILENAME");
        ds.AddColumns("FILEURL");
        ds.AddColumns("FILESTYPE");
        ds.AddColumns("FILESTATE");
        DataTable dt = ds.Get.Tables[0];
        //已经上传到正确目录的读取路径
        String cImg = "/img/";String cMystyle = "/mystyle/";
        String fileUrl = CodeSymbol.m_clientHost + "/";
        switch(updateFileType){
            case UpdateFileType.IMG:
                fileUrl = String.Format(fileUrl,m_sessionManager.PageLogin_RegistId)
                    + m_sessionManager.Page_UICultureID.ToLower() + cImg;
                break;
            case UpdateFileType.MYSTYLE:
                fileUrl = String.Format(fileUrl, m_sessionManager.PageLogin_RegistId)
                    + m_sessionManager.Page_UICultureID.ToLower() + cMystyle;
                break;
            case UpdateFileType.NONE:
                fileUrl = String.Format(fileUrl, "h")
                    + cImg;
                break;
            case UpdateFileType.CERT:
                fileUrl = String.Format(fileUrl, m_sessionManager.PageLogin_RegistId)
                    + "cert/";
                break;
        }
        

        //设置默认的显示文件
        if (FileName != null && FileName.Count > 0)
        {
            for (int i = 0; i < FileName.Count; i++)
            {
                DataRow r = dt.NewRow();
                r["ID"] = i.ToString();
                r["FILENAME"] = FileName[i].ToString();
                r["FILEURL"] = fileUrl + FileName[i].ToString();
                r["FILESTYPE"] = FileUploadState.init.ToString();
                r["FILESTATE"] = FileUploadState.init.ToString();
                dt.Rows.Add(r);
            }
        }
        //已经上传到临时目录的读取路径
        String fileUrlTemp = fileUrlHead + CodeSymbol.m_tempFileFolder;

        //取得当前表最大id
        int maxId = GetMaxId(dt);
        //设置临时上传默认的显示文件
        if (FileNameTemp != null && FileNameTemp.Count > 0)
        {
            for (int i = 0; i < FileNameTemp.Count; i++)
            {
                DataRow r = dt.NewRow();
                r["ID"] = Convert.ToString(maxId + i);
                r["FILENAME"] = FileNameTemp[i].ToString();
                r["FILEURL"] = fileUrlTemp + FileNameTemp[i].ToString();
                r["FILESTYPE"] = FileUploadState.initTemp.ToString();
                r["FILESTATE"] = FileUploadState.init.ToString();
                dt.Rows.Add(r);
            }
        }
        //保存初始化数据
        ViewState["file"] = dt;
        //设置显示的视图,显示已经上传到正确目录的文件及上传到临时目录的文件
        dataListFile.DataSource = SetDataView(dt, FileUploadState.init, FileUploadState.initTemp);
        dataListFile.DataBind();
    }

    protected void dataListFile_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (ViewState["file"] == null) return;

        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            HiddenField hidImgId = (HiddenField)e.Item.FindControl("hidImgId");
            int imgId = Convert.ToInt32(hidImgId.Value.ToString());
            //表行删除
            DataTable dt = (DataTable)ViewState["file"];
            //状态
            String fileState = String.Empty;
            foreach (DataRow r in dt.Rows)
            { 
                if(r["ID"].ToString() == imgId.ToString())
                {
                    fileState = r["FILESTYPE"].ToString();
                }
            }
            //文件状态
            FileUploadState fstate = (FileUploadState)Enum.Parse(typeof(FileUploadState), fileState);
            //如果该条记录为初期化时记录,则改变该条记录文件状态
            if (fstate == FileUploadState.init || fstate == FileUploadState.initTemp)
            {
                //设为删除状态
                dt.Rows[imgId]["FILESTATE"] = FileUploadState.del.ToString();
            }
            //更新文件集合
            FileName = DataTableField2ArrayList(SetDataViewDel(dt, FileUploadState.init, FileUploadState.init).ToTable(), "FILENAME");
            //更新临时文件集合
            FileNameTemp = DataTableField2ArrayList(SetDataViewDel(dt, FileUploadState.initTemp, FileUploadState.init).ToTable(), "FILENAME");
            ViewState["file"] = dt;
            //设置显示的视图,显示已经上传到正确目录的文件及上传到临时目录的文件
            dataListFile.DataSource = SetDataView(dt, FileUploadState.init, FileUploadState.initTemp);
            dataListFile.DataBind();
        }
    }

    /// <summary>
    /// 删除已上传文件
    /// </summary>
    public void FileClear() 
    {
        if (ViewState["file"] == null) return;

        for (int i = 0; i < dataListFile.Items.Count; i++)
        {
            HiddenField hidImgId = (HiddenField)dataListFile.Items[i].FindControl("hidImgId");
            int imgId = Convert.ToInt32(hidImgId.Value.ToString());
            //表行删除
            DataTable dt = (DataTable)ViewState["file"];
            //状态
            String fileState = String.Empty;
            foreach (DataRow r in dt.Rows)
            {
                if (r["ID"].ToString() == imgId.ToString())
                {
                    fileState = r["FILESTYPE"].ToString();
                }
            }
            //文件状态
            FileUploadState fstate = (FileUploadState)Enum.Parse(typeof(FileUploadState), fileState);
            //如果该条记录为初期化时记录,则改变该条记录文件状态
            if (fstate == FileUploadState.init)
            {
                //设为删除状态
                dt.Rows[imgId]["FILESTATE"] = FileUploadState.del.ToString();
            }
            //更新文件集合
            FileName = DataTableField2ArrayList(SetDataViewDel(dt, FileUploadState.init, FileUploadState.init).ToTable(), "FILENAME");
            //更新临时文件集合
            FileNameTemp = DataTableField2ArrayList(SetDataViewDel(dt, FileUploadState.initTemp, FileUploadState.init).ToTable(), "FILENAME");
            ViewState["file"] = dt;
            //设置显示的视图,显示已经上传到正确目录的文件及上传到临时目录的文件
            dataListFile.DataSource = SetDataView(dt, FileUploadState.init, FileUploadState.initTemp);
            dataListFile.DataBind();
        }
    }

    /// <summary>
    /// 文件上传到系统临时目录
    /// </summary>
    /// <returns></returns>
    public void FileUpload2TempPath() 
    {
        //临时目录路径 
        String tempPath = Request.PhysicalApplicationPath.ToString()
            + CodeSymbol.m_tempFileFolder;
        //上传到服务器
        Upload2Server(false, FileNameTemp, tempPath);
        FileDelTemp(tempPath);
    }

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <returns></returns>
    public void FileUpload() 
    {
        //上传路径 
        String truePath = Request.PhysicalApplicationPath.ToString()
            + CodeSymbol.m_tempFileFolder;
        //上传到服务器并生成到浏览服务器
        Upload2Server(true,FileName,truePath);
        //对用户在页面删除的文件进行删除处理
        FileDel(truePath);
    }

    /// <summary>
    /// 移动文件到真实上传目录
    /// </summary>
    public void FileUploadTempMove() 
    {
        FileTools fileTools = new FileTools();
        //文件目录头
        String filePathHead = Request.PhysicalApplicationPath.ToString() + CodeSymbol.m_tempFileFolder;
        //临时文件目录
        String sFilePath = filePathHead; 

        if (ViewState["file"] == null) return;

        DataTable dt = (DataTable)ViewState["file"];
        //读取上传目录集合中用户需要上传的文件
        dt = SetDataViewDel(dt, FileUploadState.initTemp, FileUploadState.init).ToTable();
        //m_UserSiteTrans.Open();
        foreach (DataRow row in dt.Rows)
        {
            String fileName = row["FILENAME"].ToString();

            //上传文件
            if (fileTools.CheckFile(sFilePath + fileName))
            {
                System.IO.FileStream file = new System.IO.FileStream(sFilePath + fileName, System.IO.FileMode.Open);
                file.Seek(0, System.IO.SeekOrigin.Begin);
                //填加已正确文件到FileName集合中
                FileName.Add(fileName);
                //取得文件后缀名
                //String ext = ft.GetFileExt(fileName);
                if (updateFileType == UpdateFileType.MYSTYLE)
                {   //保存自定义文件到自定义目录
                    UserSiteTransMyStyle(file, fileName);
                }
                else if (updateFileType == UpdateFileType.IMG)
                {   //保存用户上传文件到用户文件目录
                    UserSiteTransImg(file, fileName);
                }
                else if (updateFileType == UpdateFileType.NONE)
                {   //上传到固定的前台h目录
                    UserSiteTransNone(file, fileName);
                }
                else if (updateFileType == UpdateFileType.CERT)
                {   //上传到固定的前台cert目录
                    UserSiteTransCert(file);
                }
                file.Close();
                //删除临时文件
                fileTools.DelFile(sFilePath + fileName);
            }
            //清除临时文件集合
            FileNameTemp.Clear();
        }
        //m_UserSiteTrans.Close();
    }

    //上传文件到服务器
    private void Upload2Server(bool send,ArrayList fileNameArray,String truePath) 
    {
        FileTools fileTools = new FileTools();
        //遍历File表单元素
        HttpFileCollection files = HttpContext.Current.Request.Files;
        for (int i = 0; i < files.Count; i++)
        {
            //判断客户端控件名所属的控件
            if (CheckClientControl(files.AllKeys[i].ToString()))
            {
                //文件信息
                HttpPostedFile postedFile = files[i];

                String fileNameSour = System.IO.Path.GetFileName(postedFile.FileName);
                String fileName = String.Empty;
                if (!String.IsNullOrEmpty(fileNameSour))
                {
                    fileName = m_baseFunction.CreateRedonName() + i.ToString()
                        + m_objStrTool.GenerateRandCode(5) + "." + fileTools.GetFileExt(postedFile);
                    
                    //保存到HTML服务器
                    if (send)
                    {
                        if (updateFileType == UpdateFileType.MYSTYLE)
                        {
                            UserSiteTransMyStyle(postedFile.InputStream, fileName);
                        }
                        else if (updateFileType == UpdateFileType.IMG)
                        {
                            UserSiteTransImg(postedFile.InputStream, fileName);
                        }
                        else if (updateFileType == UpdateFileType.NONE)
                        {   //上传到固定的前台h目录
                            UserSiteTransNone(postedFile.InputStream, fileName);
                        }
                        else if (updateFileType == UpdateFileType.CERT)
                        {
                            fileName = fileNameSour;
                            //上传到固定的前台cert目录
                            UserSiteTransCert(postedFile.InputStream);
                        }
                    }
                    //保存到服务器
                    else
                    {
                        ShareSave(postedFile, truePath, fileName);
                    }
                    //记录上传文件
                    fileNameArray.Add(fileName);
                }
            }
        }
    }

    //保存
    private void ShareSave(HttpPostedFile postedFile, String upPath,String fileName) 
    {
        //判断文件夹是否存在
        if (!ft.CheckFolder(upPath))
        {
            //建立文件夹
            ft.CreateFolder(upPath);
        }
        String path = upPath + fileName;
        //建立文件
        postedFile.SaveAs(path);
    }

    //文件删除
    private void FileDel(String truePath) 
    {
        //初期化的记录的操作
        if (ViewState["file"] == null) return;

        DataTable dt = (DataTable)ViewState["file"];
        //读取文件列表集合中用户需要删除的文件
        DataTable ddt = SetDataViewDel(dt, FileUploadState.init, FileUploadState.del).ToTable();
        foreach (DataRow row in ddt.Rows)
        {
            if (updateFileType == UpdateFileType.IMG)
            {
                //删除文件
                ft.DelFile(truePath + "\\" + row["FILENAME"].ToString());
                //删除前台图片文件
                UserSiteTransImgDel(@"img/" + row["FILENAME"].ToString());
            }
        }
    }
    //临时文件删除
    private void FileDelTemp(String truePath)
    {
        //初期化的记录的操作
        if (ViewState["file"] == null) return;

        DataTable dt = (DataTable)ViewState["file"];
        //读取文件列表集合中用户需要删除的文件
        DataTable ddt = SetDataViewDel(dt, FileUploadState.initTemp, FileUploadState.del).ToTable();
        foreach (DataRow row in ddt.Rows)
        {
            //删除文件
            ft.DelFile(truePath + "\\" + row["FILENAME"].ToString());
        }
    }
    //图片前台调置
    private void UserSiteTransImg(System.IO.Stream stream, String dFileName) 
    {
        PageBase.m_usrSiteMager.UserLangUploadPicture(m_sessionManager.PageLogin_RegistId, m_sessionManager.Page_UICultureID, stream, dFileName);
    }
    //自定义样式前台调置
    private void UserSiteTransMyStyle(System.IO.Stream stream, String dFileName)
    {
        PageBase.m_usrSiteMager.UserLangUploadMyStyle(m_sessionManager.PageLogin_RegistId, m_sessionManager.Page_UICultureID, stream, dFileName);
    }
    //上传到前台固定h目录
    private void UserSiteTransNone(System.IO.Stream stream, String dFileName)
    {
        PageBase.m_usrSiteMager.UserLangUploadPicture("h", "", stream, dFileName);
    }
    //ICP认证固定目录
    private void UserSiteTransCert(System.IO.Stream stream)
    {
        PageBase.m_usrSiteMager.SetUserSiteCert(m_sessionManager.PageLogin_RegistId,stream);
    }
    //删除前台图片
    private void UserSiteTransImgDel(String dFilePath)
    {
        m_sessionManager = new SessionManager(Session);
        PageBase.m_usrSiteMager.UserLangDeletePicture(m_sessionManager.PageLogin_RegistId, m_sessionManager.Page_UICultureID,dFilePath);
    }

    //验证输入
    public bool CheckInput(ref ExceptionData epData) 
    {
        //出错数量
        int errNum = epData.Count;
        ArrayList fileList = new ArrayList();

        //遍历File表单元素
        HttpFileCollection files  = HttpContext.Current.Request.Files;
        for (int i = 0; i < files.Count; i++)
        {
            //判断客户端控件名所属的控件
            if (CheckClientControl(files.AllKeys[i].ToString())) 
            {
                //文件信息
                HttpPostedFile postedFile = files[i];

                String fileName = System.IO.Path.GetFileName(postedFile.FileName);

                if (!String.IsNullOrEmpty(fileName))
                {
                    //判断文件名是否合法
                    String[] fileNameHead = fileName.Split('.');
                    if (fileNameHead.Length < 2)
                    {
                        epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED01000240")));
                    }
                    //认证文件
                    if (updateFileType == UpdateFileType.CERT)
                    {
                        if (postedFile.FileName != "bazs.cert")
                                epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED01000430")));
                    }
                    //其它类型文件
                    else
                    {
                        //文件后缀验证
                        if (!CheckFileExt(ft.GetFileExt(postedFile)))
                            epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED01000190")));
                    }
                    //验证文件大小
                    if (FileSize < ft.FileSize(postedFile))
                    {
                        epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED00000170"), new String[] { fileName, (FileSize / 1024).ToString() }));
                    }
                    //删除文件到临时文件列表用于统计总上传条数
                    fileList.Add(fileName);
                }
            } 
        }
        //认证文件不允许为空
        if (updateFileType == UpdateFileType.CERT && fileList.Count == 0 && FileName.Count == 0)
            epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED01000100")));
        //上传的总文件条数
        int fileCount = 0;
        //判单个与多个上传状态
        if (FileType == "0")
        {
            //上传的总文件条数
            fileCount = FileNameTemp.Count + fileList.Count;
            if (fileCount > 1)
            {
                epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED00000180")));
            }
        }
        if (FileType == "1" )
        {
            //上传的总文件条数
            fileCount = FileName.Count + FileNameTemp.Count + fileList.Count;
            if (fileCount > FileLen)
            {
                epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED00000190"), new String[] { FileLen.ToString() }));
            }
            //复制已经存在的文件名到当前列表
            if (ViewState["file"] != null)
            {
                DataTable dt = SetDataViewDel((DataTable)ViewState["file"], FileUploadState.init, FileUploadState.initTemp, FileUploadState.del).ToTable();
                ArrayListCopy(fileList, DataTableField2ArrayList(dt, "FILENAME"));
            }
            //判断是否有同名文件
            if (CheckFileName(fileList))
            {
                epData.Add(new AppException(m_rsMansge.GetGlobalResMess("ED00000200")));
            }
        }
        
        if (epData.Count > errNum)
            return false;
        return true;
    }


    //arraylist考贝
    private void ArrayListCopy(ArrayList oary ,ArrayList cary) 
    {
        for (int i = 0; i < cary.Count; i++) 
        {
            oary.Add(cary[i].ToString());
        }
    }

    //数据表字段集转为arraylist
    private ArrayList DataTableField2ArrayList(DataTable dt,String field) 
    {
        ArrayList arr = new ArrayList();
        foreach (DataRow row in dt.Rows) 
        {
            arr.Add(row[field].ToString());
        }
        return arr;
    }

    //判断客户端控件名所属的控件
    private bool CheckClientControl(String controlName) 
    {
        if (String.IsNullOrEmpty(controlName)) 
        {
            return false;
        }
        String[] controls = controlName.Split('|');

        if (controlName == ClientControlName || controls[0] == ClientControlName)
        {
            return true;
        }
        return false;
    }

    //取得允许上传的字符串
    private String GetVilidationFileExt() 
    {
        StringBuilder _sb = new StringBuilder();
        for (int i = 0; i < FileExt.Length; i++)
        {
            _sb.Append(FileExt[i].ToString());
            _sb.Append(",");
        }
        _sb.Remove(_sb.Length - 1, 1);
        return _sb.ToString();
    }

    //验证文件后缀
    private bool CheckFileExt(String fileExt)
    {
        if (FileExt == null || fileExt == null) return false;
        for (int i = 0; i < FileExt.Length; i++)
        {
            if (fileExt.ToLower() == FileExt[i].ToString().ToLower()) 
            {
                return true;
            }
        }
        return false;
    }

    //验证文件名是否重复
    private bool CheckFileName(ArrayList fileList) 
    {
        for (int i = fileList.Count - 1; i >= 0; i--) 
        {
            for(int j = 0;j < fileList.Count - 1; j++)
            {
                if (fileList[i].ToString() == fileList[j].ToString()) 
                {
                    return true;
                }
            }
            fileList.RemoveAt(i);
        }
        return false;
    }

    // 设置视图状态
    private DataView SetDataViewDel(DataTable dt, FileUploadState type, FileUploadState state)
    {
        //设置显示的视图
        DataView dv = dt.DefaultView;
        dv.RowFilter = String.Format("FILESTYPE = '{0}' AND FILESTATE = '{1}'", type.ToString(),state.ToString());
        return dv;
    }

    // 设置视图状态
    private DataView SetDataViewDel(DataTable dt, FileUploadState type, FileUploadState type1,FileUploadState state)
    {
        //设置显示的视图
        DataView dv = dt.DefaultView;
        dv.RowFilter = String.Format("(FILESTYPE = '{0}' OR FILESTYPE = '{1}') AND FILESTATE <> '{2}'", type.ToString(), type1.ToString(),state.ToString());
        return dv;
    }


    //设置视图状态
    private DataView SetDataView(DataTable dt, FileUploadState state, FileUploadState state1)
    {
        //设置显示的视图
        DataView dv = dt.DefaultView;
        dv.RowFilter = String.Format("FILESTATE = '{0}' OR FILESTATE = '{1}'", state.ToString(), state1.ToString());
        return dv;
    }

    //取得最大id
    private int GetMaxId(DataTable dt)
    {
        int maxId = 0;
        if (dt == null) return maxId;
        //取得最大id
        if (dt.Rows.Count > 0)
        {
            maxId = Convert.ToInt32(dt.Compute("MAX(ID)", "")) + 1;
        }
        return maxId;
    }
}

public class UpdateFileType
{
    public const String NONE = "NONE";
    public const String MYSTYLE = "MYSTYLE";
    public const String IMG = "IMG";
    public const String CERT = "CERT";
}