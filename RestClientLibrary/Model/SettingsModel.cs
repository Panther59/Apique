using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.Model
{
	public class SettingsModel
	{
		public bool? CertificateSupport { get; set; }
        public int? MaxHistoryDays { get; set; }
        public bool? SearchInHeaders { get; set; }
        public bool? SearchInRequest { get; set; }
        public bool? SearchInStatus { get; set; }
        public bool? ShowRequestContentInHistory { get; set; }
        public List<CertificateModel> Certificates { get; set; }
    }
}
