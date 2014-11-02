using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Seika.Transform.Command.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Seika.CooException;
using Seika.Transform.Exception;
using System.Text;
using System.Threading;
using Seika.Transform.Command.Enum;
using Seika.Net;
using Seika.COO.Util;

namespace Seika.Transform.Command.Client
{
    /// <summary>
    /// Summary description for CommandSenderBase
    /// </summary>
    public abstract class CommandSenderBase
    {
        public CommandResult Result { get; set; }

        public String UserId { get; set; }


        public CommandSenderBase()
        {
        }

        public void Open(String userId, String passwd)
        {
            try
            {
                //Login(userId, passwd);
                this.UserId = userId;
            }
            catch (System.Net.Sockets.SocketException se)
            {
                throw new SysException("ED01004100", se);
            }
        }

        /// <summary>
        /// 登录
        /// 密码非用户密码，为共用的连接密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="passwd">用户密码</param>
        /// <returns></returns>
        public void Login(String userId, String passwd)
        {
            /*
            Command cmd = new Command(CMD_IDS.CMD_LOGIN);

            CommandParam args = new CommandParam();
            args[CommandParam.PRM_USERID] = userId;
            args[CommandParam.PRM_PASSWD] = passwd;
            SendCommand(cmd, args);
            */
        }

        /// <summary>
        /// 关闭与转换服务器的连接
        /// </summary>
        public void Close()
        {
            /*
            try
            {
                //SendCommand(new Command(CMD_IDS.CMD_LOGOUT));
            }
            catch (System.Net.Sockets.SocketException se)
            {
                Console.WriteLine(se);
            }
            finally
            {
                Thread.Sleep(100);
                //AsyncSocketClient.Close(state);
            }*/
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <returns>命令结果对象</returns>
        protected CommandResult SendCommand(CMD_IDS cmd)
        {
            return SendCommand(cmd, null, null, null);
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="args">参数对象</param>
        /// <returns>命令结果对象</returns>
        protected CommandResult SendCommand(CMD_IDS cmd, CommandParam args)
        {
            return SendCommand(cmd, args, null, null);
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="args">参数对象</param>
        /// <param name="s">数据流</param>
        /// <returns>命令结果对象</returns>
        protected CommandResult SendCommand(CMD_IDS cmd, CommandParam args, String filename, Stream s)
        {
            CommandData cmdat = new CommandData();

            WriteLog.WriteDebug("<" + cmd.ToString() + ">Params=" + args.Export());

            cmdat.AddDiv(CMD_DIV.START);
            cmdat.AddByte((byte)cmd);
            cmdat.AddDiv(CMD_DIV.COMMAND_ID);
            cmdat.AddString(args.Export());
            cmdat.AddDiv(CMD_DIV.PARAM);
            if (!String.IsNullOrEmpty(filename))
            {
                cmdat.AddFile(filename, s);
            }

            cmdat.AddDiv(CMD_DIV.END);

            cmdat.MoveTop();

            CommandData recvData = new CommandData();

            AsyncSocketClient client = new AsyncSocketClient();
            client.SendData(cmdat, ref recvData);

            this.Result = MakeResult(recvData);
            return this.Result;
        }

        private CommandResult MakeResult(CommandData recvData)
        {
            CommandResult result = new CommandResult();

            int idata = 0;
            byte bdata = 0;
            bool isDiv = false;
            CMD_DIV theDiv;
            MemoryStream buff = new MemoryStream();
            byte[] divbuff;
            String filename = "";

            while ((idata = recvData.GetByte()) >= 0)
            {
                bdata = (byte)idata;
                theDiv = (CMD_DIV)bdata;

                if (isDiv)
                {
                    switch (theDiv)
                    {
                        case CMD_DIV.START:

                            break;
                        case CMD_DIV.COMMAND_ID:
                            result.CommandId = (CMD_IDS)buff.ToArray()[0];
                            break;
                        case CMD_DIV.RESULT:
                            result.RecordCd = (CMD_RESULT)buff.ToArray()[0];
                            break;
                        case CMD_DIV.ERROR_CD:
                            divbuff = buff.ToArray();
                            result.ErrorCd = Encoding.UTF8.GetString(divbuff, 0, divbuff.Length);
                            break;
                        case CMD_DIV.ERROR_MESSAGE:
                            divbuff = buff.ToArray();
                            result.ErrorMessage = Encoding.UTF8.GetString(divbuff, 0, divbuff.Length);
                            break;
                        case CMD_DIV.DATA:
                            result.Data = buff.ToArray();
                            break;
                        case CMD_DIV.FILE_NAME:
                            divbuff = buff.ToArray();
                            filename = Encoding.UTF8.GetString(divbuff, 0, divbuff.Length);
                            break;
                        case CMD_DIV.FILE_STREAM:
                            buff.Seek(0, SeekOrigin.Begin);
                            result.AddResultFileStream(filename, buff);
                            buff = new MemoryStream();
                            break;
                        case CMD_DIV.END:
                            break;
                        default:
                            buff.WriteByte(bdata);
                            isDiv = false;
                            continue;
                    }

                    isDiv = false;

                    buff.SetLength(0);

                    if (theDiv == CMD_DIV.END) break;
                }
                else
                {
                    isDiv = (theDiv == CMD_DIV.DIV);
                    if (!isDiv)
                    {
                        buff.WriteByte(bdata);
                    }
                }
            }

            return result;
        }
    }
}