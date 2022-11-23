using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNMPdtLocPickGrp_L
    {
        /// <summary>
        ///รหัสกลุ่มผู้จัดสินค้า
        /// </summary>
        public string FTPigCode { get; set; }

        /// <summary>
        ///รหัสภาษา
        /// </summary>
        public Nullable<Int64> FNLngID { get; set; }

        /// <summary>
        ///ชื่อกลุ่มผู้จัดสินค้า
        /// </summary>
        public string FTPigName { get; set; }
    }
}
