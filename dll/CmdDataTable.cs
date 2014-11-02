using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Seika.Transform.Command.Data
{
    public class CmdDataTable : CmdSendData
    {
        public CmdColumns Columns = null;
        public int Count { get { return rows.Count; } }

        public CmdDataTable(String[] columns)
        {
            this.Columns = new CmdColumns(columns);
        }

        public CmdDataRow this[int rowNum]
        {
            get { return (CmdDataRow)rows[rowNum]; }
            set { rows[rowNum] = value; }
        }

        public void AddRow(CmdDataRow row)
        {
            rows.Add(row);
        }

        public void RemoveRow(int rowNum)
        {
            rows.RemoveAt(rowNum);
        }

        public override void Import(String data)
        {
            StringBuilder sb = new StringBuilder();
            bool dset = true;
            int rowNum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '\n' && dset)
                {
                    if (rowNum == 0)
                    {
                        this.Columns = new CmdColumns(sb.ToString());
                        rowNum++;
                        sb = new StringBuilder();
                    }
                    else
                    {
                        CmdDataRow row = new CmdDataRow(this.Columns);
                        row.Import(sb.ToString());
                        this.AddRow(row);
                        sb = new StringBuilder();
                    }
                    continue;
                }
                else if (data[i] == '\n')
                {
                    dset = !dset;
                }
                sb.Append(data[i]);
            }
        }

        public override String Export()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Columns.Export()).Append("\n");
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].Export()).Append("\n");
            }
            return sb.ToString();
        }

        public CmdDataRow GetNewRow()
        {
            return new CmdDataRow(this.Columns);
        }

        private ArrayList rows = new ArrayList();
    }
}
