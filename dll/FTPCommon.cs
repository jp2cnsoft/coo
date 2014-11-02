using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;

namespace Seika.Common.Net
{
    /// <summary>
    /// FTP操作共通类
    /// 完成文件上传，下载，创建目录，文件删除等功能。
    /// </summary>
    public class FTPCommon
    {
        public FTPCommon() { }

        //FTP操作类变量
        private FtpWebRequest m_reqFTP = null;

        public ServerConnectInfo ConnectInfo { get; set;}

        /// <summary>
        /// FTP共通类初期化
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passwd">密码</param>
        public FTPCommon(String userName, String passwd)
        {
            ConnectInfo = new ServerConnectInfo();
            this.ConnectInfo.UserName = userName;
            this.ConnectInfo.Password = passwd;
        }

        /// <summary>
        /// FTP共通类初期化
        /// </summary>
        /// <param name="connInfo">服务器信息</param>
        public FTPCommon(ServerConnectInfo connInfo)
        {
            this.ConnectInfo = connInfo;
        }

        /// <summary>
        /// FTP服务器登录函数
        /// </summary>
        /// <param name="ftpPath"></param>
        private  void Login(String ftpPath)
        {
            m_reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpPath));
            m_reqFTP.Credentials = new NetworkCredential(ConnectInfo.UserName, ConnectInfo.Password);
        }

        /// <summary>
        /// FTP服务器中创建目录用函数
        /// </summary>
        /// <param name="ftpDir">目录名</param>
        public void CreateDir(String ftpDir)
        {
            Login(ftpDir);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            ftpResponse.Close();     
        }

        /// <summary>
        /// 向FTP服务器的指定目录下，上传文件的功能函数
        /// </summary>
        /// <param name="ftpFilePath">上传路径</param>
        /// <param name="fStream">输入用数据流</param>
        /// <param name="Size">流空间长度</param>
        public void PutFileStream(String ftpFilePath, Stream fStream, long Size)
        {
            Login(ftpFilePath);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            m_reqFTP.UseBinary = true;
            m_reqFTP.ContentLength = Size;
            Stream ftpStream = m_reqFTP.GetRequestStream();
            int bufSize = 2048;
            byte[] fileBuf = new byte[bufSize];
            int writeCount;
            while ((writeCount = fStream.Read(fileBuf, 0, bufSize)) != 0)
            {
                ftpStream.Write(fileBuf, 0, writeCount);
            }
            ftpStream.Close();
        }

        /// <summary>
        /// 完成从FTP服务器下载指定文件的功能
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>取得的文件流</returns>
        public Stream GetPutFileStream(String fileName)
        {
            Login(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            m_reqFTP.UseBinary = true;
            Stream ftpStream = m_reqFTP.GetRequestStream();
            return ftpStream;
        }

        /// <summary>
        /// 完成从FTP服务器下载指定文件的功能
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>取得的文件流</returns>
        public Stream GetGetFileStream(String fileName)
        {
            Login(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            m_reqFTP.UseBinary = true;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();
            ftpResponse.Close();
            return ftpStream;
        }

        /// <summary>
        /// 完成删除FTP服务器上文件的功能
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void Delete(String fileName)
        {
            Login(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            ftpResponse.Close();     
        }

        /// <summary>
        /// 取得FTP服务器上指定文件的长度。
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>长度</returns>
        public long GetFileSize(String fileName)
        {
            long fileSize = 0;
            Login(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            fileSize = ftpResponse.ContentLength;
            ftpResponse.Close();
            return fileSize;
        }

        /// <summary>
        /// 取得FTP指定目录下的文件列表
        /// </summary>
        /// <param name="fileName">路径</param>
        /// <returns>文件名数组</returns>
        public String[] GetFileNameList(String fileName)
        {
            StringBuilder sFiles = new StringBuilder();
            Login(fileName);
            m_reqFTP.KeepAlive = false;          
            m_reqFTP.UseBinary = true;
            m_reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            WebResponse ftpResponse = m_reqFTP.GetResponse();
            StreamReader sReader = new StreamReader(ftpResponse.GetResponseStream());
            String sLine = sReader.ReadLine();
            while (sLine != null)
            {
                sFiles.Append(sLine);
                sFiles.Append("\n");
                sLine = sReader.ReadLine();
            }
            // to remove the trailing '\n'        
            sFiles.Remove(sFiles.ToString().LastIndexOf('\n'), 1);
            sReader.Close();
            ftpResponse.Close();
            return sFiles.ToString().Split('\n');        
        }
    }
}
