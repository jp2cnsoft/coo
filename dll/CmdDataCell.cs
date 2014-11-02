using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Command.Data
{
    public class CmdDataCell : CmdSendData
    {
        public String data = "";

        public String StringValue
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public bool BoolValue
        {
            get { return (this.data == "1"); }
            set { this.data = (value ? "1" : "0"); }
        }

        public int IntValue
        {
            get { return int.Parse(this.data); }
            set { this.data = value.ToString(); }
        }

        public long LongValue
        {
            get { return long.Parse(this.data); }
            set { this.data = value.ToString(); }
        }

        public double DoubleValue
        {
            get { return double.Parse(this.data); }
            set { this.data = value.ToString(); }
        }

        public DateTime DateTimeValue
        {
            get { return DateTime.Parse(this.data); }
            set { this.data = value.ToString(); }
        }

        public override String Export()
        {
            return "\"" + this.data.Replace("\"", "\"\"") + "\"";
        }

        public override void Import(String data)
        {
            this.data = data.Substring(1, data.Length - 2).Replace("\"\"", "\"");
        }
    }
}
