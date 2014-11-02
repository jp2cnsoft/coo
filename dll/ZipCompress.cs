using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Seika.Common.Compress
{
    public class ZipCompress
    {
        /// <summary>
        /// ZIPファイルを指定パスに解凍
        /// </summary>
        /// <param name="path">回答先ディレクトリ</param>
        public void UnZip(Stream sm, String dir)
        {     
            ZipInputStream s = new ZipInputStream(sm);

            char c = dir[dir.Length - 1];
            if (c != '\\' && c != '/') dir += "/";

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(theEntry.Name);

                if (fileName == "")
                {
                    //生成解压目录
                    Directory.CreateDirectory(dir + theEntry.Name);
                }
                else
                {  
                    //解压文件到指定的目录
                    FileStream streamWriter = File.Create(dir + theEntry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else break;
                    }

                    streamWriter.Close();
                }
            }
            s.Close();
        }

        /// <summary>
        /// フォルダまたはファイルを圧縮
        /// </summary>
        /// <param name="path">圧縮元ディレクトリ</param>
        public void CreateZip(String dir)
        {
            
        }

        /// <summary>
        /// 创建压缩文件
        /// </summary>
        /// <param name="filename">压缩文件名</param>
        /// <param name="directory">数据源目录</param>
        public void Zip(string filename, string directory)
        {
            try
            {
                FastZip fastZip = new FastZip();
                fastZip.CreateEmptyDirectories = true;
                fastZip.CreateZip(filename, directory, true, "");
                fastZip = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 取得风格配置XML文件的文件流
        /// </summary>
        /// <param name="sm">输入源</param>
        /// <param name="rsm">取得的配置文件流</param>
        public void GetConfigXmlStream(Stream sm, Stream rsm)
        {
            ZipInputStream s = new ZipInputStream(sm);
            ZipEntry theEntry = null;

            while ((theEntry = s.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(theEntry.Name);

                if (fileName.ToLower() == "style.xml")
                {
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            rsm.Write(data, 0, size);
                        }
                        else
                        {
                            rsm.Seek(0, SeekOrigin.Begin);
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
