using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Seika.Transform.Command.Enum;

namespace Seika.Transform.Command.Data
{
    public class CommandData
    {
        private MemoryStream ms = new MemoryStream();

        public void AddDiv(CMD_DIV div)
        {
            AddByte((byte)CMD_DIV.DIV);
            AddByte((byte)div);
        }

        public void AddByte(byte data)
        {
            ms.WriteByte(data);
        }
        public void AddBytes(byte[] data)
        {
            AddBytes(data, 0, data.Length);
        }
        public void AddBytes(byte[] data, int startIdx, int length)
        {
            ms.Write(data, startIdx, length);
        }
        public void AddString(String data)
        {
            AddBytes(Encoding.ASCII.GetBytes(data));
        }

        public void MoveTop()
        {
            ms.Seek(0, SeekOrigin.Begin);
        }

        public int GetByte()
        {
            return ms.ReadByte();
        }

        public int GetBytes(byte[] buff, int startIdx, int length)
        {
            return ms.Read(buff, startIdx, length);
        }
        public int GetBytes(byte[] buff)
        {
            return GetBytes(buff, 0, buff.Length);
        }

        public long GetLength()
        {
            return this.ms.Length;
        }

        public void AddData(byte[] data)
        {
            this.AddBytes(data);
            this.AddDiv(CMD_DIV.DATA);
        }

        public void AddFile(String filename, Stream sm)
        {
            this.AddString(filename);
            this.AddDiv(CMD_DIV.FILE_NAME);
            int idata = 0;
            byte bdata = 0;
            while ((idata = sm.ReadByte()) >= 0)
            {
                bdata = (byte)idata;
                if (CMD_DIV.DIV == (CMD_DIV) bdata)
                {
                    this.AddByte(bdata);
                }
                this.AddByte(bdata);
            }
            this.AddDiv(CMD_DIV.FILE_STREAM);
        }

        public void AddCommandData(CommandData data)
        {
            int length = 0;
            byte[] buff = new byte[1024];
            data.MoveTop();
            while((length = data.GetBytes(buff, 0, buff.Length)) > 0)
            {
                this.AddBytes(buff, 0, length);
            }
        }

        public void Close()
        {
            this.ms.Close();
        }
    }
}
