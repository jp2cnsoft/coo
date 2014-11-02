using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Command.Data
{
    public abstract class CmdSendData
    {
        public abstract String Export();
        public abstract void Import(String dat);
    }
}
