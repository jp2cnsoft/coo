using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh;

namespace Seika.Net
{
    public class SSH_Client
    {
        public static string Call(string hostname, string username, string passwd, string cmd)
        {
            SshExec exec = null;
            try
            {
                SshConnectionInfo input = new SshConnectionInfo();
                input.Host = hostname;
                input.User = username;
                input.Pass = passwd;

                exec = new SshExec(input.Host, input.User, input.Pass);

                exec.Connect();

                string rtn = exec.RunCommand(cmd);

                int idx = rtn.LastIndexOf('\n');

                if (idx >= 0)
                {
                    if (rtn.LastIndexOf('\n', idx - 1) == -1)
                    {
                        rtn = rtn.Replace("\n", "");
                    }
                }
                return rtn;
            }
            finally
            {
                if (exec != null) exec.Close();
            }
        }

        public struct SshConnectionInfo
        {
            public string Host;
            public string User;
            public string Pass;
            public string IdentityFile;
        }
    }
}
