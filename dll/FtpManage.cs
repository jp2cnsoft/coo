using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;

namespace Seika
{
    public class FtpManage
    {
        public  String m_uName = "";
        public  String m_uPsaaword = "";
        private  FtpWebRequest m_reqFTP = null;

        private  void LoginFtp(String ftpPath)
        {
            m_reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpPath));
            m_reqFTP.Credentials = new NetworkCredential(m_uName, m_uPsaaword);
        }

        public  void CreateDir(String ftpDir)
        {
            LoginFtp(ftpDir);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            ftpResponse.Close();     
        }

        public void UploadFile(String ftpFilePath, Stream fStream, long Size)
        {
            LoginFtp(ftpFilePath);
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
            fStream.Close();

        }

        public Stream DownloadFile(String fileName)
        {          
            LoginFtp(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            m_reqFTP.UseBinary = true;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();    
            ftpResponse.Close();
            return ftpStream;
        }

        public void DeleteFile(String fileName)
        {
            LoginFtp(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            ftpResponse.Close();     
        }

        public long GetFileSize(String fileName)
        {
            long fileSize = 0;
            LoginFtp(fileName);
            m_reqFTP.KeepAlive = false;
            m_reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse ftpResponse = (FtpWebResponse)m_reqFTP.GetResponse();
            fileSize = ftpResponse.ContentLength;
            ftpResponse.Close();
            return fileSize;
        }

        public String[] GetFileList(String fileName)
        {
            StringBuilder sFiles = new StringBuilder();
            LoginFtp(fileName);
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
