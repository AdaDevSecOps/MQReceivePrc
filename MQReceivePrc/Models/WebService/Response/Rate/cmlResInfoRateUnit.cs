﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Webservice.Response.Rate
{
    public class cmlResInfoRateUnit
    {
        public string rtRteCode { get; set; }
        public int rnRtuSeq { get; set; }
        public Nullable<double> rcRtuFac { get; set; }
    }
}
