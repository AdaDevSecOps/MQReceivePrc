﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Webservice.Response.CustomerAddress
{
    public class cmlResListOfcmlResInfoCstAddrLng
    {
        public List<cmlResInfoCstAddrLng> raItems { get; set; }
        public string rtCode { get; set; }
        public string rtDesc { get; set; }
    }
}
