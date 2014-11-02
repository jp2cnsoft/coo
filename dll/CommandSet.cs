using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Seika.Transform.Command
{
    public class CommandSet
    {
        private bool IsRead { get; set; }

        List<CommandList> list = new List<CommandList>();

        public CommandSet()
        {
            Cancellation();
        }

        public CommandList Next()
        {
            Lock();

            CommandList cmdlst = null;

            if (list.Count > 0) 
            {
                cmdlst = list[0];
                list.RemoveAt(0);
            }

            Cancellation();
            return cmdlst;
        }

        public void Add(CommandList cmdlist)
        {
            Lock();

            list.Add(cmdlist);

            Cancellation();
        }

        private void Lock()
        {
            while (!IsRead)
            {
                Thread.Sleep(10);
            }
            IsRead = false;
        }

        private void Cancellation()
        {
            IsRead = false;
        }
    }
}
