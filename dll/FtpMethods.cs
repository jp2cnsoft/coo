using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using Seika.Util;

namespace Seika.Common.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class FtpState
    {
        private AutoResetEvent wait;

        public FtpState()
        {
            wait = new AutoResetEvent(false);
        }

        public AutoResetEvent OperationComplete
        {
            get { return wait; }
        }

        public FtpWebRequest Request { get; set;}

        public string FileName { get; set;}

        public System.Exception OperationException { get; set;}

        public string StatusDescription { get; set;}

        public Stream UpDownStream { get; set; }

        public String BannerMessage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FtpMethods
    {
        private static AutoResetEvent go = new AutoResetEvent(true);
        private static int TEST_COUNT = 5;
        private static int ERR_WAIT_TIME = 300;
        

        private AutoResetEvent waitObject;

        public String Username { get; set; }
        public String Password { get; set; }
        public String Port { get; set; }
        public String Host { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="Port"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        public FtpMethods(String Host, String Port, String Username, String Password)
        {
            this.Host = Host;
            this.Port = Port;
            this.Username = Username;
            this.Password = Password;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private FtpState GetFtpState(String path, String method)
        {
            String target = String.Format("ftp://{0}:{1}{2}", Host, Port, path);
            FtpState state = new FtpState();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(target);
            request.Method = method;
            request.UseBinary = true;
            request.KeepAlive = false;

            request.Credentials = new NetworkCredential(Username, Password);

            state.Request = request;
            state.FileName = target;

            return state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private FtpState GetFtpResponse(String path, String method)
        {
            return this.GetFtpResponse(path, null, method);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private static void EndGetDownloadStreamCallback(IAsyncResult ar)
        {
            //Log.WriteDebug("EndGetDownloadStreamCallback");

            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                state.UpDownStream = new MemoryStream();

                while((readBytes = requestStream.Read(buffer, 0, bufferLength)) > 0)
                {
                    state.UpDownStream.Write(buffer, 0, readBytes);
                    count += readBytes;
                }

                //Log.WriteDebug(String.Format("Writing {0} bytes to the stream.", count));

                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );
            }
            // Return exceptions to the main application thread.
            catch (System.Exception e)
            {
                Log.WriteError("Exception:" + state.FileName + "," + e.Message);

                // IMPORTANT: Close the request stream before sending the request.
                if (requestStream != null) requestStream.Close();
                state.OperationException = e;
                state.OperationComplete.Set();
                return;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private static void EndGetUploadStreamCallback(IAsyncResult ar)
        {
            //Log.WriteDebug("EndGetUploadStreamCallback");


            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;

                while ((readBytes = state.UpDownStream.Read(buffer, 0, bufferLength)) > 0)
                {
                    requestStream.Write(buffer, 0, readBytes);
                    count += readBytes;
                }

                //Log.WriteDebug(String.Format("Writing {0} bytes to the stream.", count));

                // IMPORTANT: Close the request stream before sending the request.
                requestStream.Close();
                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );
            }
            // Return exceptions to the main application thread.
            catch (System.Exception e)
            {
                Log.WriteError("Exception:" + state.FileName + "," + e.Message);

                state.OperationException = e;
                state.OperationComplete.Set();
                return;
            }

        }

        // The EndGetResponseCallback method  
        // completes a call to BeginGetResponse.
        private static void EndGetResponseCallback(IAsyncResult ar)
        {
            //Log.WriteDebug("EndGetResponseCallback");

            byte[] buff = new byte[2048];

            FtpState state = (FtpState)ar.AsyncState;
            FtpWebResponse response = null;
            Stream sm = null;
            try
            {
                response = (FtpWebResponse)state.Request.EndGetResponse(ar);
                sm = response.GetResponseStream();

                if (sm != null && sm.CanRead)
                {
                    state.UpDownStream = new MemoryStream();
                    int length = 0;

                    while ((length = sm.Read(buff, 0, 2048)) > 0)
                    {
                        state.UpDownStream.Write(buff, 0, length);
                    }
                    state.UpDownStream.Seek(0, SeekOrigin.Begin);
                }
                state.StatusDescription = response.StatusDescription;
                state.BannerMessage = response.BannerMessage;
            }
            // Return exceptions to the main application thread.
            catch (System.Exception e)
            {
                state.OperationException = e;
            }
            finally
            {
                try
                {
                    if (sm != null) sm.Close();
                    if (response != null) response.Close();
                }
                catch (Exception) { }
                state.OperationComplete.Set();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private static void EndGetStreamCallback(IAsyncResult ar)
        {
            //Log.WriteDebug("EndGetStreamCallback");


            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);

                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                state.UpDownStream = new MemoryStream();
                do
                {
                    readBytes = requestStream.Read(buffer, 0, bufferLength);
                    state.UpDownStream.Write(buffer, 0, readBytes);
                    count += readBytes;
                }
                while (readBytes != 0);

                requestStream.Close();

                state.UpDownStream.Seek(0, SeekOrigin.Begin);

                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );

            }
            catch (System.Exception e)
            {
                Log.WriteError("Exception:" + state.FileName + "," + e.Message);

                state.OperationException = e;
                state.OperationComplete.Set();
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private FtpState GetFtpResponse(String path, String targer, String method)
        {

                try
                {
                    go.WaitOne();

                    FtpState state = GetFtpState(path, method);

                    if (!String.IsNullOrEmpty(targer))
                    {
                        state.Request.RenameTo = targer;
                    }

                    // Get the event to wait on.
                    waitObject = state.OperationComplete;

                    // Asynchronously get the stream for the file contents.
                    state.Request.BeginGetResponse(
                        new AsyncCallback(EndGetResponseCallback),
                        state
                    );

                    // Block the current thread until all operations are complete.
                    waitObject.WaitOne();

                    state.Request.Abort();

                    // The operations either completed or threw an exception.
                    if (state.OperationException != null)
                    {
                        throw state.OperationException;
                    }
                    return state;
                }
                catch (Exception e)
                {
                    Log.WriteError("Exception(GetFtpResponse):" + path + "," + e.Message);
                    throw e;
                }   
                finally
                {
                    go.Set();
                }

        }

        /// <summary>
        /// 取得下转文件流
        /// </summary>
        /// <param name="ftpPath"></param>
        /// <returns></returns>
        public Stream GetDownloadStream(String path)
        {
            FtpState state = null;
                try 
                {
                    go.WaitOne();

                    Log.WriteDebug("GetDownloadStream:" + path);

                    state = GetFtpState(path, WebRequestMethods.Ftp.DownloadFile);

                    // Get the event to wait on.
                    waitObject = state.OperationComplete;

                    // Asynchronously get the stream for the file contents.
                    state.Request.BeginGetResponse(
                        new AsyncCallback(EndGetDownloadStreamCallback),
                        state
                    );

                    // Block the current thread until all operations are complete.
                    waitObject.WaitOne();

                    state.Request.Abort();

                    return state.UpDownStream;
                }
                catch (Exception e)
                {
                    Log.WriteError("Exception(GetDownloadStream):" + state.FileName + "," + e.Message);
                    throw e;
                }   
                finally
                {
                    go.Set();
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Exist(String path)
        {
            try
            {
                go.WaitOne();

                Log.WriteDebug("Exist:" + path);

                try
                {

                    FtpState state = GetFtpState(path + "*", WebRequestMethods.Ftp.ListDirectoryDetails);

                    // Get the event to wait on.
                    waitObject = state.OperationComplete;

                    // Asynchronously get the stream for the file contents.
                    state.Request.BeginGetResponse(
                        new AsyncCallback(EndGetResponseCallback),
                        state
                    );

                    // Block the current thread until all operations are complete.
                    waitObject.WaitOne();

                    System.IO.StreamReader sr = new System.IO.StreamReader(state.UpDownStream);
                    bool isIIS = state.BannerMessage.IndexOf("Microsoft") > 0;
                    string res = sr.ReadLine();
                    sr.Close();
                    state.UpDownStream.Close();

                    state.Request.Abort();

                    //IIS对应
                    if (isIIS)
                    {
                        return (res != null && res.Length > 0);
                    }
                    else
                    {
                        return (res != null);
                    }
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
            finally
            {
                go.Set();
            }
        }

        /// <summary>
        /// 建设目录
        /// </summary>
        /// <param name="ftpPath"></param>
        public void MakeDirectory(String path)
        {
            Log.WriteDebug("MakeDirectory:" + path);

            FtpState state = null;
            try
            {
                state = this.GetFtpResponse(path, WebRequestMethods.Ftp.MakeDirectory);
            }
            catch (Exception e)
            {
                if (!Exist(path)) throw e;
            }
        }

        /// <summary>
        /// FTP文件上传
        /// </summary>
        /// <param name="sm">上传文件的文件流</param>
        /// <param name="ftpPath">上传路径</param>
        public void PutFile(Stream sm, String ftpPath)
        {
            FtpState state = null;
            try 
            {
                go.WaitOne();

                Log.WriteDebug("PutFile:" + ftpPath);

                state = this.GetFtpState(ftpPath, WebRequestMethods.Ftp.UploadFile);
                state.UpDownStream = sm;

                // Get the event to wait on.
                waitObject = state.OperationComplete;

                // Asynchronously get the stream for the file contents.
                state.Request.BeginGetRequestStream(
                    new AsyncCallback(EndGetUploadStreamCallback),
                    state
                );

                // Block the current thread until all operations are complete.
                waitObject.WaitOne();

                state.Request.Abort();

                // The operations either completed or threw an exception.
                if (state.OperationException != null)
                {
                    throw state.OperationException;
                }
                else
                {
                    //Log.WriteDebug(String.Format("The operation completed - {0}", state.StatusDescription));
                }
                return;
            }
            catch (Exception e)
            {
                Log.WriteError("Exception(PutFile):" + state.FileName + "," + e.Message);

                throw e;
            }
            finally
            {
                go.Set();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="ftpPath"></param>
        public void PutFile(String localPath, String ftpPath)
        {
            Stream sm = null;
            try
            {
                sm = new FileStream(localPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                PutFile(sm, ftpPath);
            }
            finally
            {
                if (sm != null) sm.Close();
            }
        }

        public void RemoveFile(String path)
        {
            Log.WriteDebug("RemoveFile\t:" + path);

            GetFtpResponse(path, WebRequestMethods.Ftp.DeleteFile);
        }


        public void RemoveDirectoryTree(String path)
        {
            Log.WriteDebug("RemoveDirectoryTree:" + path);

            FtpFileInfo[] dl = GetDirDetailsList(path);

            for (int i = 0; i < dl.Length; i++)
            {
                if (dl[i].IsDir)
                {
                    RemoveDirectoryTree(path + "/" + dl[i].Filename);
                }
                else
                {
                    RemoveFile(path + "/" + dl[i].Filename);
                }
            }

            RemoveDirectory(path);
        }

        public void RemoveDirectoryTree(String path,String[] undeleteSubDir)
        {
            Log.WriteDebug("RemoveDirectoryTree:" + path);

            FtpFileInfo[] dl = GetDirDetailsList(path);

            for (int i = 0; i < dl.Length; i++)
            {
                if (dl[i].IsDir)
                {
                    if(Array.IndexOf(undeleteSubDir,dl[i].Filename) == -1)
                        RemoveDirectoryTree(path + "/" + dl[i].Filename);
                }
                else
                {
                    RemoveFile(path + "/" + dl[i].Filename);
                }
            }
            if(Exist(path))
                RemoveDirectory(path);
        }

        /// <summary>
        /// 删除FTP目录
        /// </summary>
        /// <param name="ftpPath"></param>
        public void RemoveDirectory(String path)
        {
            Log.WriteDebug("RemoveDirectory\t:" + path);

            try
            {
                this.GetFtpResponse(path, WebRequestMethods.Ftp.RemoveDirectory);
            }
            catch (System.Net.WebException e)
            {
                if (Exist(path))
                {
                    Log.WriteError("Exception(RemoveDirectory):" + path + "," + e.Message);
                   throw e;
                }
            }
        }

        public void MoveDirectory(String sDir, String tDir)
        {
            Log.WriteDebug("MoveDir:" + sDir + "," + tDir);

            this.GetFtpResponse(sDir, tDir, WebRequestMethods.Ftp.Rename);
        }

        public FtpFileInfo[] GetDirDetailsList(String path)
        {
            Log.WriteDebug("GetDirDetailsList\t;" + path);
                try 
                {
                    go.WaitOne();


                    FtpState state = GetFtpState(path, WebRequestMethods.Ftp.ListDirectoryDetails);

                    // Get the event to wait on.
                    waitObject = state.OperationComplete;

                    // Asynchronously get the stream for the file contents.
                    state.Request.BeginGetResponse(
                        new AsyncCallback(EndGetResponseCallback),
                        state
                    );

                    // Block the current thread until all operations are complete.
                    waitObject.WaitOne();

                    System.IO.StreamReader sr = new System.IO.StreamReader(state.UpDownStream);

                    string res = sr.ReadToEnd();
                    sr.Close();

                    String[] lines = res.Replace("\r\n", "\n").Split('\n');

                    ArrayList ds = new ArrayList();
                    Regex reg = null;

                    if (state.BannerMessage.IndexOf("Microsoft") > 0)
                    {
                        reg = new Regex(@"(?<mm>\d{2}?)-(?<dd>\d{2}?)-(?<yy>\d{2}?)\s+(?<hh>\d{2}?):(?<mi>\d{2}?)(?<ampm>.{2}?)\s+(?<dir>.*?)\s+(?<length>[\d|\s]+?)\s+(?<fn>.+?)$");
                    }
                    else
                    {
                        //drwxr-xr-x    5 500      50           4096 Aug 05 11:33 IMG 
                        //-rw-r--r--    1 500      50           8216 Sep 18 07:32 P3010P0310.html 
                        //"vsFTPd"
                        reg = new Regex(@"^(?<dir>[\w-]?)[\w-]+[ ]+\d[ ]+[\w\d]+[ ]+[\w\d]+[ ]+(?<length>[\d]+?) .+:\d{2} (?<fn>.+?)[ ]*$");
                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (reg.IsMatch(lines[i]))
                        {
                            Match match = reg.Match(lines[i]);

                            string length = match.Groups["length"].ToString();
                            string dir = match.Groups["dir"].ToString();
                            string fn = match.Groups["fn"].ToString();

                            if (String.IsNullOrEmpty(length.Trim())) length = "0";

                            FtpFileInfo fi = new FtpFileInfo();
                            fi.Filename = fn;
                            fi.IsDir = (dir == "<DIR>" || dir == "d" || dir == "D");
                            fi.FileSize = int.Parse(length);
                            //fi.CreateDate = new DateTime(
                            //    2000 + int.Parse(yy), int.Parse(mm), int.Parse(dd),
                            //        (ampm == "PM" ? 12 : 0) + int.Parse(hh), int.Parse(mi), 00);
                            if (fi.Filename == "." || fi.Filename == "..")
                                continue;
                            ds.Add(fi);
                        }
                    }

                    FtpFileInfo[] fis = new FtpFileInfo[ds.Count];
                    for (int i = 0; i < ds.Count; i++)
                    {
                        fis[i] = (FtpFileInfo)ds[i];
                    }

                    state.UpDownStream.Close();
                    return fis;
                }
                catch (Exception e)
                {
                    Log.WriteError("Exception(GetDirDetailsList):" + path + "," + e.Message);
                    throw e;
                }
                finally
                {
                    go.Set();
                }
        }
    }

    public enum FILE_DIR 
    {
        FILE, DIR
    }

    public class FtpFileInfo
    {
        public FtpFileInfo()
        {
            IsDir = false;
        }

        public String Filename { get; set;}
        //public String Extension { get; set; }
        public long FileSize { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDir { get; set; }
    }

}
