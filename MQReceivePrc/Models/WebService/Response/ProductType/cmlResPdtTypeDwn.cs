﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Webservice.Response.ProductType
{
    public class cmlResPdtTypeDwn
    {
        public List<cmlResInfoPdtType> raPdtType { get; set; }
        public List<cmlResInfoPdtTypeLng> raPdtTypeLng { get; set; }
    }
}
