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
using System.Collections;

/// <summary>
/// Summary description for FileTools
/// </summary>
/// 
namespace Seika.COO.Web.PG
{
    public class FileTools
    {
        public FileTools(){}

        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <returns></returns>
        public bool CheckFolder(String path)
        {
            return Directory.Exists(path) ? true : false;
        }
         /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool CheckFile(String path)
        {
            return System.IO.File.Exists(path) ? true : false;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public bool CreateFolder(string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path);
            return (di.Exists) ? true : false;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public bool CreateFile(string path)
        {
            FileInfo fi = new FileInfo(path);
            return (fi.Exists) ? true : false;
        }

        /// <summary>
        /// 删除文件夹及该文件夹下的文件
        /// </summary>
        /// <returns></returns>
        public bool DelFolder(String folder)
        {
            if (!string.IsNullOrEmpty(folder))
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public bool DelFile(String file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Exists)
                {
                    fi.Delete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 移动单个文件
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="dFilePath"></param>
        /// <returns></returns>
        public bool MoveFile(String sFilePath,String dFilePath) 
        {
            FileInfo fi = new FileInfo(sFilePath);
            if (fi.Exists)
            {
                fi.MoveTo(dFilePath);
                return true;
            }
            return false;
        }

        //取得文件名
        public string GetFileName(HttpPostedFile file)
        {
            if (file.ContentLength != 0)
            {
                int i = file.FileName.LastIndexOf(".");
                return file.FileName.Substring(0, i);
            }
            return null;
        }

        //取得文件扩展名
        public string GetFileExt(HttpPostedFile file)
        {
            String fileType = file.ContentType.ToString();
            switch (fileType)
            {
                case "image/fax":
                      return "fax";
                case "image/gif":
                      return "gif";
                case "image/x-icon":
                      return "ico";
                case "image/bmp":
                      return "bmp";
                case "image/pjpeg":
                      return "jpg";
                case "image/jpeg":
                      return "jpg";
                case "image/pnetvue":
                      return "net";
                case "image/png":
                      return "png";
                case "image/x-png":
                      return "png";
                case "application/x-png":
                      return "png";
                case "image/vnd.rn-realpix":
                      return "rp";
                case "image/tif":
                      return "tif";
                case "image/vnd.wap.wbmp":
                      return "wbmp";
                case "application/x-shockwave-flash":
                      return "swf";
            }
            return null;
        }

        //取得文件扩展名
        public string GetFileExt(String fileName)
        {
            String ext = String.Empty;
            if (!String.IsNullOrEmpty(fileName)) 
            {
                String[] e = fileName.Split('.');
                if (e.Length > 1) 
                {
                    ext = e[e.Length - 1];
                }
            }
            return ext;
        }

        //取得文件大小
        public int FileSize(HttpPostedFile file)
        {
            if (file.ContentLength != 0)
            {
                return file.ContentLength;
            }
            return 0;
        }

        //取得指定分卷文件中的最新文件卷标序号
        public int GetNewFile(String path,String fileName) 
        {
            //临时变量
            int temp = -1;
            //取得文件具体名称
            ArrayList list = GetFileList(path, fileName);

            for (int i = 0; i < list.Count; i++) 
            {
                String gFileName = list[i].ToString();
                int iFileName = gFileName.IndexOf('_');
                if (iFileName != -1)
                {
                    String sFileName = gFileName.Substring(iFileName + 1, gFileName.Length - iFileName - 1);

                    if (!String.IsNullOrEmpty(sFileName))
                    {
                        int s = Convert.ToInt32(sFileName);
                        if (s > temp && s != 9999)
                        {
                            temp = s;
                        }
                    }
                }
            }
            return temp;
        }

        //取得文件夹下应用文件列表
        public ArrayList GetFileList(String path, String fileName) 
        {
            ArrayList list = new ArrayList();
            //取得文件具体名称
            String fileId = fileName;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.xml");
            for (int i = 0; i < fileInfo.Length; i++)
            {
                String gFileName = fileInfo[i].Name.ToString();
                String gFileId = gFileName.Substring(0, gFileName.IndexOf('.'));
                int iFileId = gFileId.IndexOf('_');
                if (iFileId == -1 && fileId == gFileId) 
                {
                    list.Add(gFileId);
                }
                if (iFileId != -1 && fileId == gFileId.Substring(0, iFileId))
                {
                    list.Add(gFileId);
                }
            }
            return list;
        }

        //取得文件夹下应用文件列表
        public ArrayList GetFileList(String path)
        {
            ArrayList list = new ArrayList();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.*");
            for (int i = 0; i < fileInfo.Length; i++)
            {
                list.Add(fileInfo[i].Name.ToString());
            }
            return list;
        }


        /// <summary>
        /// 目录Copy
        /// </summary>
        /// <param name="SourceDirectory">源目录</param>
        /// <param name="TargetDirectory">目标目录</param>
        public void CopyDirectory(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);
            //Determine whether the source directory exists.
            if (!source.Exists)
            {
                return;
            }
            if (!target.Exists)
            {
                target.Create();
            }
            //Copy files.
            FileInfo[] sourceFiles = source.GetFiles();
            for (int i = 0; i < sourceFiles.Length; ++i)
            {
                File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + sourceFiles[i].Name, true);
            }
        }

        /// <summary>
        /// 文件Copy
        /// </summary>
        /// <param name="SourceDirectory">源文件</param>
        /// <param name="TargetDirectory">目标文件</param>
        public void CopyFile(string SourceFile ,string TargetFile)
        {
            if (!CheckFile(SourceFile))
            {
                return;
            }
            //Copy files.
            File.Copy(SourceFile, TargetFile, true);
        }
    }

    /// <summary>
    /// 文件上传的状态
    /// </summary>
    public enum FileUploadState
    {
        //初期
        init = 0,
        //临时初期
        initTemp = 1,
        //添加
        add = 2,
        //修改
        mdi = 3,
        //删除
        del = 4
    }
}
