using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNTPdtPick
    {
        public List<cmlTCNTPdtPickHD> aoTCNTPdtPickHD { get; set; }
        public List<cmlTCNTPdtPickDT> aoTCNTPdtPickDT { get; set; }
        public List<cmlTCNTPdtPickDTSN> aoTCNTPdtPickDTSN { get; set; }
        //public List<cmlTCNMPdtLocPickGrp> aoTCNMPdtLocPickGrp { get; set; }
        //public List<cmlTCNMPdtLocPickGrp_L> aoTCNMPdtLocPickGrp_L { get; set; }
        public List<cmlTCNTPdtPickHDDocRef> aoTCNTPdtPickHDDocRef { get; set; }
        public List<cmlTCNTPdtPickHDCst> aoTCNTPdtPickHDCst { get; set; }
        public List<cmlTCNTPdtPickDTDis> aoTCNTPdtPickDTDis { get; set; }
    }
}
