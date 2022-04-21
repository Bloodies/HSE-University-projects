using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finn
{
    class MsgBody
    {
        public string Body { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public bool Finn { get; set; }
        public bool Decided { get; set; }
        public HashSet<int> Inc { get; set; }
        public HashSet<int> Ninc { get; set; }
    }
}
