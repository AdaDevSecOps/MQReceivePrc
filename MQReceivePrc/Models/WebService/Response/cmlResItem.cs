﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.WebService.Response
{
    public class cmlResItem<T>
    {
        public T roItem { get; set; }
        public string rtCode { get; set; }
        public string rtDesc { get; set; }
    }
}
