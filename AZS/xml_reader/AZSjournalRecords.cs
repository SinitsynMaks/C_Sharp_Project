using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
    class AZSjournalRecords
    {
        public string operNum { get; }
        public string currencyCode { get; }
        public decimal amount { get; }
		public decimal discount { get; }
		public string fuelCode { get; }
		public double fuelVolume { get; }

        public AZSjournalRecords(string operNum, string currencyCode, decimal amount,
			decimal discount, string fuelCode, double fuelVolume)
        {
            this.operNum = operNum;
			this.currencyCode = currencyCode;
			this.amount = amount;
			this.discount = discount;
			this.fuelCode = fuelCode;
			this.fuelVolume = fuelVolume;
        }

        public override string ToString()
        {
			string infoFuel = string.Format("Код топлива: {0}, код валюты транзакции: {1}", fuelCode, currencyCode);
            return infoFuel;
        }
    }
}
