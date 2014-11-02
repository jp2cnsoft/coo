using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seika.Common.Net;
using System.Collections;
using System.IO;
using Seika.Common.StringUtil;
using Seika.Transform.Command.Data;
using Seika.Transform.Command.Enum;

namespace Seika.Transform.Command
{
    public class CooCommand
    {
        /// <summary>
        /// 命令ID
        /// </summary>
        public CMD_IDS CommandId { get; set; }

        public CooCommand()
        {
        }

        public CooCommand(CMD_IDS cmd)
        {
            this.CommandId = cmd;
        }

        public String LogMessage()
        {
            //            return System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:dd")
            //                + " COMMANDID = " + this.CommandId.ToString();
            return "COMMANDID = " + this.CommandId.ToString();
        }
    }
}
