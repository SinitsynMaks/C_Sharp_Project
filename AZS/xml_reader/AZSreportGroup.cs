using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class AZSreportGroup
	{
		public string FuelCode { get; set; }
		public string CurrencyCode { get; set; }
		public List<AZSjournalRecords> OperList { get; set; }
		public decimal TotalAmount { get; set; }
		public double TotalVolume { get; set; }
		public decimal TotalDiscount { get; set; }

		public AZSreportGroup()
		{
			OperList = new List<AZSjournalRecords>();
		}

		public AZSreportGroup(string fuelCode, string currencyCode) : this()
		{			
			FuelCode = fuelCode;
			CurrencyCode = currencyCode;
		}
	}
}
