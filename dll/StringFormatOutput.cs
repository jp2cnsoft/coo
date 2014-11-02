using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Common.StringUtil
{
    public class StringFormatOutput
    {
        public static String RightSpace(string val, int size)
        {
            String result = val;
            int startIdx = result.Length;
            for (int i = startIdx; i < size; i++)
                result += " ";
            return result;
        }
    }
}
