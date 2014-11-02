using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Seika.Transform.Command.Data
{
    public class CmdDataRow : CmdSendData
    {
        public CmdDataRow(CmdColumns columns)
        {
            this.Columns = columns;
            cells = new CmdDataCell[this.Columns.Count];
            for (int i = 0; i < this.Columns.Count; i++)
            {
                cells[i] = new CmdDataCell();
            }
        }

        CmdDataCell[] cells = null;

        public CmdDataCell this[String columnName]
        {
            get { return this[Columns.GetColumndIndex(columnName)]; }
            set { this[Columns.GetColumndIndex(columnName)] = value; }
        }

        public CmdDataCell this[int columnIndex]
        {
            set { cells[columnIndex] = value; }
            get { return (CmdDataCell)cells[columnIndex]; }
        }

        public CmdColumns Columns { get; set; }

        public override String Export()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (i > 0) sb.Append(",");
                CmdDataCell dat = cells[i];
                if (dat == null)
                {
                    sb.Append("\"\"");
                }
                else
                {
                    sb.Append(dat.Export());
                }
            }

            return sb.ToString();
        }

        public override void Import(String data)
        {
            String rowdat = data + ",", columnVal = "";
            bool dset = true;
            int colNum = 0;

            for (int i = 0; i < rowdat.Length; i++)
            {
                if (rowdat[i] == ',' && dset)
                {
                    this.cells[colNum++].Import(columnVal);
                    if (colNum >= this.Columns.Count) break;
                    columnVal = "";
                    continue;
                } 
                else if (rowdat[i] == '\"')
                {
                    dset = !dset;
                }
                columnVal += rowdat[i];
            }
        }
    }
}
