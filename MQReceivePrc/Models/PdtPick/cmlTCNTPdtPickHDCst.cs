using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNTPdtPickHDCst
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
        public string FTXthCardID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthCstTel { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthCstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthCardNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<int> FNXthCrTerm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDXthDueDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDXthBillDue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthCtrName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<DateTime> FDXthTnfDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FTXthRefTnfID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<Int64> FNXthAddrShip { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<Int64> FNXthAddrTax { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXthCstPnt { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Nullable<decimal> FCXthCstPntPmt { get; set; }
    }
}
