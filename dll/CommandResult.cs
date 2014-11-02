using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using Seika.Transform.Command.Data;
using Seika.Common.StringUtil;
using Seika.Transform.Command.Enum;
using System.Collections;

namespace Seika.Transform.Command.Data
{
    public class CommandResult
    {
        public CommandResult()
        {
            this.RecordCd = CMD_RESULT.SUCCESS;
            this.ErrorMessage = "";
            ResultFileTable = new Hashtable();
        }

        public CMD_RESULT RecordCd { get; set;}

        public CMD_IDS CommandId { get; set; }

        public String ErrorCd { get; set; }

        public String ErrorMessage { get; set; }

        public byte[] Data { get; set; }

        private Hashtable ResultFileTable { get; set; }

        public Stream GetResultFileStream(String filename)
        {
            return (Stream)this.ResultFileTable[filename];
        }

        public void AddResultFileStream(String filename, Stream sm)
        {
            sm.Seek(0, SeekOrigin.Begin);
            this.ResultFileTable.Add(filename, sm);
        }
    }
}
