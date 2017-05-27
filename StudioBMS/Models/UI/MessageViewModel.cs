using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioBMS.Models.UI
{
    public class MessageViewModel
    {
        private readonly string[] _classes = { "alert-danger", "alert-warning", "alert-info", "alert-success" };
        public string Message { get; set; }
        public MessageType Type { get; set; }

        public string GetClass()
        {
            return _classes[(byte) Type];
        }
    }

    public enum MessageType
    {
        Danger = 0,
        Warning,
        Info,
        Success
    }
}
