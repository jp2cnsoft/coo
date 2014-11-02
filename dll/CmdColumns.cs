using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Seika.Transform.Command.Data
{
    public class CmdColumns
    {
        String[] columns = null;

        public String this[int idx]
        {
            get { return columns[idx]; }
            set { columns[idx] = value; }
        }

        public int Count
        {
            get { return columns.Length; }
        }

        public CmdColumns(String csvdat)
        {
            Import(csvdat);
        }

        public CmdColumns(String[] columns)
        {
            this.columns = columns;
        }   

        public CmdColumns(ArrayList columns)
        {
            this.columns = new String[columns.Count];
            for (int i = 0; i < columns.Count; i++)
            {
                this.columns[i] = (String)columns[i];
            }
        }

        public int GetColumndIndex(String columnName)
        {
            for (int i = 0; i < this.columns.Length; i++)
            {
                if (columnName == this.columns[i]) return i;
            }
            throw new System.Exception("CmdColumns.GetColumndIndex(-001)");
        }

        public String Export()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("\"" + this[i] + "\"");
            }

            return sb.ToString();
        }

        public void Import(String data)
        {
            ArrayList cols = new ArrayList();
            String rowdat = data + ",", columnVal = "";
            bool dset = true;

            for (int i = 0; i < rowdat.Length; i++)
            {
                if (rowdat[i] == ',' && dset)
                {
                    cols.Add(columnVal.Replace("\"\"", "\""));
                    columnVal = "";
                    continue;
                }
                else if (rowdat[i] == '\"')
                {
                    dset = !dset;
                }
                columnVal += rowdat[i];
            }

            this.columns = new String[cols.Count];

            for (int i = 0; i < cols.Count; i++)
            {
                String item = (String)cols[i];
                item = item.Substring(1, item.Length - 2);
                this.columns[i] = item;
            }
        }
    }
}
