using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Sale
{
    public class cmlTPSTSalHDDocRef
    {
        /// <summary>
        ///
        /// </summary>
        public string FTBchCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXshDocNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXshRefDocNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXshRefType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXshRefKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDXshRefDocDate { get; set; }
    }
}
