﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.SaleRT.RentalPay
{
    class cmlTRTTPayRC
    {
        /// <summary>
        /// สาขาสร้าง
        /// </summary>
        public string FTBchCode { get; set; }

        /// <summary>
        /// เลขที่เอกสาร  Def : XYYPOS-1234567 Gen ตาม TCNTAuto
        /// </summary>
        public string FTXshDocNo { get; set; }

        /// <summary>
        /// ลำดับการชำระเงินต่อ 1 เลขที่เอกสาร
        /// </summary>
        public int FNXsrSeqNo { get; set; }

        /// <summary>
        /// รหัสการชำระ
        /// </summary>
        public string FTRcvCode { get; set; }

        /// <summary>
        /// ชื่อการรับชำระ
        /// </summary>
        public string FTRcvName { get; set; }

        /// <summary>
        /// เลขที่อ้างอิง1
        /// </summary>
        public string FTXrcRefNo1 { get; set; }

        /// <summary>
        /// เลขที่อ้างอิง2
        /// </summary>
        public string FTXrcRefNo2 { get; set; }

        /// <summary>
        /// วันที่อ้างอิง
        /// </summary>
        public DateTime? FDXrcRefDate { get; set; }

        /// <summary>
        /// สาขาธนาคาร
        /// </summary>
        public string FTXrcRefDesc { get; set; }

        /// <summary>
        /// รหัสธนาคาร
        /// </summary>
        public string FTBnkCode { get; set; }

        /// <summary>
        /// สกุลเงิน
        /// </summary>
        public string FTRteCode { get; set; }

        /// <summary>
        /// อัตราแลกเปลี่ยน
        /// </summary>
        public double FCXrcRteFac { get; set; }

        /// <summary>
        /// ยอดคงค้าง เช่น 480+100 (รวมยอดมัดจำ)
        /// </summary>
        public double FCXrcFrmLeftAmt { get; set; }

        /// <summary>
        /// ยอดแบงค์  เช่น 1000
        /// </summary>
        public double FCXrcUsrPayAmt { get; set; }

        /// <summary>
        /// หักยอดมัดจำสินค้า เช่น 100
        /// </summary>
        public double FCXrcDep { get; set; }

        /// <summary>
        /// ยอดชำระจริง  เช่น 480   (ไม่รวมยอดมัดจำ)
        /// </summary>
        public double FCXrcNet { get; set; }

        /// <summary>
        /// เงินทอน เช่น 420
        /// </summary>
        public double FCXrcChg { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string FTXrcRmk { get; set; }

        /// <summary>
        /// รหัสเครื่อง EDC
        /// </summary>
        public string FTPhwCode { get; set; }

        /// <summary>
        /// เลขที่เอกสารอ้างอิง
        /// </summary>
        public string FTXrcRetDocRef { get; set; }

        /// <summary>
        /// สถานะใช้งาน function รับชำระแบบ ว่าง/Null :Online ,1: Offline
        /// </summary>
        public string FTXrcStaPayOffline { get; set; }

        /// <summary>
        /// วันที่ปรับปรุงรายการล่าสุด
        /// </summary>
        public DateTime? FDLastUpdOn { get; set; }

        /// <summary>
        /// ผู้ปรับปรุงรายการล่าสุด
        /// </summary>
        public string FTLastUpdBy { get; set; }

        /// <summary>
        /// วันที่สร้างรายการ
        /// </summary>
        public DateTime? FDCreateOn { get; set; }

        /// <summary>
        /// ผู้สร้างรายการ
        /// </summary>
        public string FTCreateBy { get; set; }
    }
}
