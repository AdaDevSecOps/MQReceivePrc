using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.WebService.Request.ExchangeRate
{
    public class cmlReqExchangeRate
    {
        public List<cmlReqInfoExchangeRate> paoRate { get; set; }
    }
}
