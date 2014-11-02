using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Command
{
    class CommandProcessBase
    {
    }

    public enum CommandType
    {
        FILE_COPY,
        FILE_DEL,
        FILE_RENAME,
        FILE_MOVE,
        DIR_DEL,
        DIR_RENAME,
        DIR_MAKE,
        XSL_TRANSFORM
    }
}
