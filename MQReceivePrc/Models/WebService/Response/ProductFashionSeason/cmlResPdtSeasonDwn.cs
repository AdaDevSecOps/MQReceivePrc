﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.Webservice.Response.ProductFashionSeason
{
    public class cmlResPdtSeasonDwn
    {
        public List<cmlResInfoPdtSeason> raPdtSeason { get; set; }
        public List<cmlResInfoPdtSeasonLng> raPdtSeasonLng { get; set; }
    }
}
