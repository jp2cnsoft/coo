using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Seika.Common.Net
{
    /// <summary>
    /// WEB文件下载工具类
    /// </summary>
    public class WebDownload
    {
        // 下载数据的缓存空间大小（单位：BYTE）
        private const int downloadBlockSize = 1024;

        /// <summary>
        /// 从WEB服务器下载指定URL的文件
        /// </summary>
        /// <param name="uri">下载文件URL</param>
        /// <param name="localpath">本地保存位置</param>
        /// <returns>取得文件长度</returns>
        public static long Get(string uri, string localpath)
        {
            // WEB下载类生成
            WebDownload df = new WebDownload();

            // 打定下载URL及本地保存路径
            df.Url = uri;
            df.FileLocalPath = localpath;

            // 连接服务器 
            df.Connect();

            // 文件不存在时
            if (!df.IsFileExist)
            {
                throw new System.IO.FileNotFoundException();
            }

            // 文件数据取得
            df.Get();

            // 关闭文件
            df.Close();

            // 退回文件长度
            return df.fileSize;
        }

        /// <summary>
        /// 从WEB服务器下载文件
        /// </summary>
        public void Get()
        {
            try {

                // 指定的本地文件存在时
                if (String.IsNullOrEmpty(this.fileLocalPath))
                {
                    // 删除本地文件。
                    File.Delete(this.fileLocalPath);
                }

                // 创建缓存
                byte[] buffer = new byte[downloadBlockSize];

                // 读取的字节数
                int readCount = 0;

                // 读取数据
                while ((int)(readCount = GetStream.Read(buffer, 0, downloadBlockSize)) > 0)
                {
                    // 保存数据
                    SaveFile(buffer, readCount, this.fileLocalPath);

                    // 统计下载总量
                    totalDownloaded += readCount;
                }
            }
            finally
            {
                // 关闭返信流
                if (response != null)
                {
                    response.Close();
                }

                // 下载同步FLAG更新
                this.isFileDownload = true;
            }
        }

        /// <summary>
        /// 将数据流保存到本地文件
        /// </summary>
        /// <param name="buffer">缓存</param>
        /// <param name="count">长度</param>
        /// <param name="fileName">文件名</param>
        private void SaveFile(byte[] buffer, int count, string fileName)
        {
            FileStream f = null;

            try
            {
                // 打开目标文件
                f = File.Open(fileName, FileMode.Append, FileAccess.Write);

                // 写入数据
                f.Write(buffer, 0, count);
            }
            finally
            {
                // 关闭文件
                if (f != null)
                {
                    f.Close();
                }
            }
        }
        /// <summary>
        /// 连接到WEB服务器
        /// </summary>
        public void Connect()
        {
            // 取得下载文件流
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);

            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                this.isFileExist = false;
                return;
            }

            // 下载成功时
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // 下载的容量
                fileSize = response.ContentLength;
                fileLastModifyInInternet = response.LastModified;
                this.isFileExist = true;
            }
        }

        /// <summary>
        /// 取得文件流
        /// </summary>
        public Stream GetStream
        {
            get
            {
                if (this.start == this.fileSize)
                {
                    return Stream.Null;
                }

                if (this.stream == null)
                {
                    this.stream = this.response.GetResponseStream();
                }
                return this.stream;
            }
        }

        /// <summary>
        /// 下载总量
        /// </summary>
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

        // WEB下载流对象
        private HttpWebResponse response;

        //取得返回流
        private Stream stream;

        //文件中下载点
        private long start;

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
