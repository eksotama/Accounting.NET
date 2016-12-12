using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public enum MessageType
    {
        Ok,
        Error
    }

    public class Message
    {
        public MessageType Type { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
