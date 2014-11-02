using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Seika.Transform.Command.Enum;

namespace Seika.Transform.Command
{
    public class CmdSendBuffer
    {
        public CMD_IDS cmdId { get; set;}

        List<byte[]> argsList = new List<byte[]>();
        List<byte[]> dataList = new List<byte[]>();
        public Stream ArgsStream { get; set;}

        private int[] pos = {0,0,0};
        private List<byte[]> sendList = new List<byte[]>();
    
        public CmdSendBuffer()
        {
            this.ArgsStream = null;
        }

        public void AddParam(byte[] data)
        {
            argsList.Add(data);
        }

        public void AddParam(String data)
        {
            argsList.Add(Encoding.UTF8.GetBytes(data));
        }

        public void AddData(byte[] data)
        {
            dataList.Add(data);
        }

        public void AddData(String data)
        {
            dataList.Add(Encoding.UTF8.GetBytes(data));
        }

        public void BeginGetData()
        {
            pos = new int[] { 0, 0, 0 };
            sendList.Add(new byte[] { (byte)CMD_SD_DIV.RESET, (byte)CMD_SD_DIV.START });
            sendList.Add(new byte[] { (byte) this.cmdId});
            sendList.Add(new byte[] { (byte)CMD_SD_DIV.RESET, (byte)CMD_SD_DIV.CMD });
            sendList.Add(new byte[] { (byte)CMD_SD_DIV.RESET, (byte)CMD_SD_DIV.CMD });
        }


        public int GetNextData(byte[] data, int length)
        {
            
            int cnt = 0;
            int ext = 0;
             /*
            while (cnt < length)
            {
                if (ext == 0)
                {
                    switch (type)
                    {
                        case CMD_SENDDATA_FLAG.START:   // 开始

                            break;
                        case CMD_SENDDATA_FLAG.PARAM:   // 命令
                            break;

                        case CMD_SENDDATA_FLAG.DATA:    // 命令验证码

                            break;
                        case CMD_SENDDATA_FLAG.END:     // 送信结束

                            break;
                    }
                }
                else
                {
                    switch (ext)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                }
            }

            switch (type)
            {
                case 0:
                    data[cnt++] = argsList[lstIdx][bytIdx];

                    for (lstIdx; lstIdx < argsList.Count; lstIdx++)
                    {
                        for (btyIdx; btyIdx < argsList[lstIdx].Length; btyIdx++)
                        {
                            data[cnt++] = argsList[lstIdx][bytIdx];
                            if (cnt == length) return cnt;
                        }
                    }



                    break;
                case 1:

                    break;
                case 2:

                    break;
            }*/
            return cnt;    
        }

    }
}
