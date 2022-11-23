using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.WebService.Response.ExchangeRate
{
    public class cmlResInfoExchangeRate
    {
        public string rtFmtCode { get; set; }
        public string rtRteIsoCode { get; set; }
        public Nullable<decimal> rcRteRate { get; set; }
        public Nullable<DateTime> rdLastUpdOn { get; set; }
        public string rtLastUpdBy { get; set; }
        public Nullable<DateTime> rdCreateOn { get; set; }
        public string rtCreateBy { get; set; }
    }
}
