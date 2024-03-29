using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Seika.Common.Web
{
    public class DownloadWebFile
    {
        private const int downloadBlockSize = 1024;

        /// <summary>
        /// 文件下载静态函数
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="localpath"></param>
        /// <returns></returns>
        public static long Download(string uri, string localpath)
        {
            DownloadWebFile df = new DownloadWebFile();
            df.Url = uri;
            df.FileLocalPath = localpath;
            df.Connect();
            if (!df.IsFileExist)
            {
                throw new System.IO.FileNotFoundException();
            }
            df.DownloadFile();

            df.Close();
            return df.fileSize;
        }

        public void DownloadFile()
        {
            if (this.fileLocalPath == null)
                return;
            try
            {
                // create the download buffer
                byte[] buffer = new byte[downloadBlockSize];
                int readCount;
                // read a block of bytes and get the number of bytes read
                while ((int)(readCount = DownloadStream.Read(buffer, 0, downloadBlockSize)) > 0)
                {
                    // save block to end of file
                    SaveToFile(buffer, readCount, this.fileLocalPath);
                    // update total bytes read
                    totalDownloaded += readCount;
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
                this.isFileDownload = true;
            }
            //return true;
        }

        /// <summary>
        /// 将数据流保存到本地文件
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <param name="fileName"></param>
        private void SaveToFile(byte[] buffer, int count, string fileName)
        {
            FileStream f = null;

            try
            {
                f = File.Open(fileName, FileMode.Append, FileAccess.Write);
                f.Write(buffer, 0, count);
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
        }
        /// <summary>
        /// 连接到WEB服务器
        /// </summary>
        public void Connect()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                this.isFileExist = false;
                return;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                fileSize = response.ContentLength;
                fileLastModifyInInternet = response.LastModified;
                this.isFileExist = true;
            }
        }


        public Stream DownloadStream
        {
            get
            {
                if (this.start == this.fileSize)
                    return Stream.Null;
                if (this.stream == null)
                    this.stream = this.response.GetResponseStream();
                return this.stream;
            }
        }

        public long TotalDownloaded
        {
            get
            {
                return this.totalDownloaded;
            }
        }

        /// <summary>
        /// 本地文件路径
        /// </summary>
        public string FileLocalPath  
        {
            set
            {
                this.fileLocalPath = value;
            }
            get
            {
                return this.fileLocalPath;
            }
        }

        /// <summary>
        /// 下载文件的网络地址
        /// </summary>
        public string Url 
        {
            set
            {
                this.url = value;
            }
            get
            {
                return this.url;
            }
        }

        /// <summary>
        /// 互联网上文件是否存在？
        /// </summary>
        public bool IsFileExist   
        {
            get
            {
                return this.isFileExist;
            }
        }

        /// <summary>
        /// 文件是否已下载完成
        /// </summary>
        public bool IsFileDownload 
        {
            get
            {
                return this.isFileDownload;
            }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize  
        {
            get
            {
                return this.fileSize;
            }
        }

        /// <summary>
        /// 本地中文件最近下载的时间
        /// </summary>
        public DateTime FileLastModifyOnLocal  
        {
            set
            {
                this.fileLastModifyOnLocal = value;
            }
            get
            {
                return this.fileLastModifyOnLocal;
            }
        }

        /// <summary>
        /// 互联网中文件最近修改的时间
        /// </summary>
        public DateTime FileLastModifyInInternet  
        {
            get
            {
                return this.fileLastModifyInInternet;
            }
        }

        /// <summary>
        /// 关闭流
        /// </summary>
        public void Close()
        {
            this.response.Close();
        }

        //文件大小
        private long fileSize;
        //文件本地路径
        private string fileLocalPath;
        //本地最后修改时间
        private DateTime fileLastModifyOnLocal;
        //网上最后修改时间
        private DateTime fileLastModifyInInternet;  
        private HttpWebResponse response;
        //取得返回流
        private Stream stream;
        //文件中下载点
        private long start = 0;
        //网络中文件是否存在？
        private bool isFileExist;
        //文件是否下载完成?
        private bool isFileDownload;
        //文件的网络地址
        private string url;
        //已下载的数据量
        private long totalDownloaded; 
    }
}
