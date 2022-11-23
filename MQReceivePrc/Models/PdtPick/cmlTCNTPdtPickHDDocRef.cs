using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNTPdtPickHDDocRef
    {
        /// <summary>
        ///
        /// </summary>
        public string FTAgnCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTBchCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthDocNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthRefDocNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthRefType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthRefKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDXthRefDocDate { get; set; }
    }
}
