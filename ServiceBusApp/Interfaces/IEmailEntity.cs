using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.Interfaces
{
    public class IEmailEntity
    {
        public string title { get; set; }
        public string emailTo { get; set; }
        public string body { get; set; }
    }
}
