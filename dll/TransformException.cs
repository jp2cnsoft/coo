using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Exception
{
    public class TransformException : System.Exception
    {
        public String MessageId { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        public TransformException(String messageId)
        {
            MessageId = messageId;
        }
        public TransformException(String messageId, String message) : base (message)
        {
            MessageId = messageId;
        }

    }
}
