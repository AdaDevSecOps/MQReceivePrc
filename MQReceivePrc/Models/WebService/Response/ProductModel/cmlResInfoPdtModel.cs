﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Webservice.Response.ProductModel
{
    public class cmlResInfoPdtModel
    {
        public string rtPmoCode { get; set; }
        public Nullable<DateTime> rdLastUpdOn { get; set; }
        public Nullable<DateTime> rdCreateOn { get; set; }
        public string rtLastUpdBy { get; set; }
        public string rtCreateBy { get; set; }
    }
}
