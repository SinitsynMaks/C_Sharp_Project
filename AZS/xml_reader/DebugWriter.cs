using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class DebugWriter : IWriter
	{
		public void Write(IList<AZSreportGroup> reportList)
		{
			foreach (AZSreportGroup rGroup in reportList)
			{
				Console.WriteLine("--------------------------");
				Console.WriteLine("fuelcode={0}",rGroup.FuelCode);
				Console.WriteLine("currencyCode={0}", rGroup.CurrencyCode);
				foreach (AZSjournalRecords record in rGroup.OperList)
				{
					Console.WriteLine("	operNum={0}", record.operNum);
					Console.WriteLine("	amount={0}", record.amount);
				}
				Console.WriteLine("totalAmount={0}", rGroup.TotalAmount);
				Console.WriteLine("totalVolume={0}", rGroup.TotalVolume);
				Console.WriteLine("totalDiscount={0}", rGroup.TotalDiscount);
			}
		}
	}
}
