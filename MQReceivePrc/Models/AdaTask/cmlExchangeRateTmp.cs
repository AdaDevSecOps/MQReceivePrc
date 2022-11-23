using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Models.AdaTask
{
	public class cmlExchangeRateTmp
	{
		public string FTFmtCode { get; set; }
		public string FTRteIsoCode { get; set; }
		public Nullable<decimal> FCRteRate { get; set; }
		public Nullable<DateTime> FDLastUpdOn { get; set; }
		public string FTLastUpdBy { get; set; }
		public Nullable<DateTime> FDCreateOn { get; set; }
		public string FTCreateBy { get; set; }
	}
}
