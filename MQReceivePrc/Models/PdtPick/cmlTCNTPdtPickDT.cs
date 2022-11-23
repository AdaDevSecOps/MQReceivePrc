using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNTPdtPickDT
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
        public Nullable<int> FNXtdSeqNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTPdtCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdPdtName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTPunCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTPunName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdBarCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdVatType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTVatCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdVatRate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdSaleType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdSalePrice { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdQty { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdQtyAll { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdSetPrice { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdQtyOrd { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdStaPrcStk { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdStaAlwDis { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTPdtStaSet { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXtdRmk { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDLastUpdOn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTLastUpdBy { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDCreateOn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTCreateBy { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTPplCode { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdAmtB4DisChg { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public string FTXtdDisChgTxt { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdDis { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdChg { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdNet { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdCostIn { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdCostEx { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public string FTXtdStaPdt { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdQtyLef { get; set; } //*Arm 65-10-10

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXtdQtyRfn { get; set; } //*Arm 65-10-10
    }
}
