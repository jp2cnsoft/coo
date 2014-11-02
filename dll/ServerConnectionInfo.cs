using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Common.Net
{
    /// <summary>
    /// 连接服务用户信箱对象
    /// </summary>
    public class ServerConnectInfo
    {
        public String IP { get; set; }
        public String Name { get; set; }
        public String Port { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String AccessPath { get; set; }
    }
}
