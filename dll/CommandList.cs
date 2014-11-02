using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seika.Transform;

namespace Seika.Transform.Command
{
    /// <summary>
    /// 转换命令保存用LIST
    /// </summary>
    public class CommandList
    {
        public int CommandIndex {get; set;}
        List<CooCommand> list = new List<CooCommand>();

        /// <summary>
        /// 取得下一个转换参数
        /// </summary>
        /// <returns></returns>
        public CooCommand Next()
        {
            CooCommand cmd = null;
            if (list.Count > 0)
            {
                cmd = list[0];
                list.RemoveAt(0);
            }
            return cmd;
        }

        /// <summary>
        /// 追加一个转换参数
        /// </summary>
        /// <param name="args"></param>
        public void Add(CooCommand args)
        {
            list.Add(args);
        }

        /// <summary>
        /// 清除所有的转换参数
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
    }
}
