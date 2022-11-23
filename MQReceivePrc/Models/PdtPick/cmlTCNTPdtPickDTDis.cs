using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.PdtPick
{
    public class cmlTCNTPdtPickDTDis
    {
        /// <summary>
        ///ตัวแทนขาย
        /// </summary>
        public string FTAgnCode { get; set; }

        /// <summary>
        ///สาขาสร้าง
        /// </summary>
        public string FTBchCode { get; set; }

        /// <summary>
        ///เลขที่เอกสาร
        /// </summary>
        public string FTXthDocNo { get; set; }

        /// <summary>
        ///ลำดับ
        /// </summary>
        public Nullable<int> FNXtdSeqNo { get; set; }

        /// <summary>
        ///วัน/เวลาทำรายการ [dd/mm/yyyy H:mm:ss]
        /// </summary>
        public Nullable<DateTime> FDXtdDateIns { get; set; }

        /// <summary>
        ///เลขที่อ้างอิง
        /// </summary>
        public string FTXtdRefCode { get; set; }

        /// <summary>
        ///สถานะส่วนลด 1: ลดรายการ ,2: ลดท้ายบิล ,3: ยอดใช้ค่าบริการ
        /// </summary>
        public Nullable<int> FNXtdStaDis { get; set; }

        /// <summary>
        ///ข้อความมูลค่าลดชาร์จ เช่น 5 หรือ 5%
        /// </summary>
        public string FTXtdDisChgTxt { get; set; }

        /// <summary>
        ///ประเภทลดชาร์จ 1:ลดบาท 2: ลด % 3: ชาร์จบาท 4: ชาร์จ %
        /// </summary>
        public string FTXtdDisChgType { get; set; }

        /// <summary>
        ///มูลค่าสุทธิก่อนลดชาร์จ
        /// </summary>
        public Nullable<decimal> FCXtdNet { get; set; }

        /// <summary>
        ///ยอดลด/ชาร์จ
        /// </summary>
        public Nullable<decimal> FCXtdValue { get; set; }

        /// <summary>
        ///รหัส Discount policy
        /// </summary>
        public string FTDisCode { get; set; }

        /// <summary>
        ///รหัสเหตุผล
        /// </summary>
        public string FTRsnCode { get; set; }
    }
}
