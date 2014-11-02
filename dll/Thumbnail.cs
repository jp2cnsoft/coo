using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace Seika.Util.Drawing
{
    /// <summary>
    /// 缩略图生成类
    /// </summary>
    public class Thumbnail
    {
        const int max_width = 150;

        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="inpath">源图片文件路径</param>
        /// <param name="outpath">文件输出路径</param>
        public void CreateThumbNail(string inpath, string outpath)
        {
            FileStream in_fs = new FileStream(inpath, FileMode.Open, FileAccess.Read, FileShare.Read);
            this.CreateThumbNail(in_fs, outpath);
        }

        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="inpath">源图片文件路径</param>
        /// <param name="outpath">文件输出路径</param>
        public void CreateThumbNailNoShare(string inpath, string outpath)
        {
            FileStream in_fs = new FileStream(inpath, FileMode.Open, FileAccess.Read, FileShare.Read);
            this.CreateThumbNailNoShare(in_fs, outpath);
        }


        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="sm">图片流</param>
        /// <param name="outpath">文件输出路径</param>
        public void CreateThumbNail(System.IO.Stream sm, string outpath)
        {
            MemoryStream ms = this.GetThumbNailJPGStream(sm, max_width);

            FileStream fs = new FileStream(outpath, FileMode.Create, FileAccess.Read, FileShare.Read);
            ms.WriteTo(fs);

            fs.Close();
            ms.Close();
        }

        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="sm">图片流</param>
        /// <param name="outpath">文件输出路径</param>
        public void CreateThumbNailNoShare(System.IO.Stream sm, string outpath)
        {
            MemoryStream ms = this.GetThumbNailJPGStream(sm, max_width);

            FileStream fs = new FileStream(outpath, FileMode.Create);
            ms.WriteTo(fs);

            fs.Close();
            ms.Close();
        }

        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="inpath">源图片路径</param>
        /// <returns>缩略图流</returns>
        public MemoryStream GetThumbNailJPGStream(System.IO.Stream ImgStream, int width_height)
        {
            Bitmap bmp = new Bitmap(ImgStream);
            System.Drawing.Image img = bmp;

            float wh = ((float)img.Size.Width / (float)img.Size.Height);
            int w = width_height, h = width_height;

            if (wh > 1)
            {
                h = (int)((float)h / wh);
            }
            else
            {
                w = (int)((float)w * wh);
            }
            
            img = img.GetThumbnailImage(w, h, null, IntPtr.Zero);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.WriteTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
