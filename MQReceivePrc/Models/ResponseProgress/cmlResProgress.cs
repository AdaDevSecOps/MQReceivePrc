using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.ResponseProgress
{
    public class cmlResProgress
    {
        public int rnProg { get; set; }
        public string rtDocNo { get; set; }
        public string rtStatus { get; set; }
        public string rtMsgError { get; set; }
    }
}
