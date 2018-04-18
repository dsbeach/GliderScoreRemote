using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GliderScoreRemote
{
    class DeferredData
    {
        public int udpPort { get; set; }
        public string comPort { get; set; }
        public DateTime sendTime { get; set; }
        public String message { get; set; }
    }
}
