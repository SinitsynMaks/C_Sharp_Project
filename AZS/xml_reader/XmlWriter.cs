using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class XmlWriter : IWriter
	{
		public string Filename { get; set; }

		public XmlWriter(string filename)
		{
			Filename = filename;
		}

		public void Write(IList<AZSreportGroup> reportList)
		{
			using (StreamWriter writer = new StreamWriter(Filename))
			{
				writer.WriteLine("<report>");
				foreach (AZSreportGroup rGroup in reportList)
				{
					writer.WriteLine("	<group>");
					writer.WriteLine("		<fuelcode> {0} </fuelcode>", rGroup.FuelCode);
					writer.WriteLine("		<currencyCode> {0} </currencyCode>", rGroup.CurrencyCode);
					writer.WriteLine("		<operList>");
					writer.WriteLine("			<oper>");
					foreach (AZSjournalRecords record in rGroup.OperList)
					{
						writer.WriteLine("				<operNum> {0} </operNum>", record.operNum);
						writer.WriteLine("				<amount> {0} </amount>", record.amount);
						writer.WriteLine("				<discount> {0} </discount>", record.discount);
						writer.WriteLine("				<currencyCode> {0} </currencyCode>", record.currencyCode);
						writer.WriteLine("				<fuelVolume> {0} </fuelVolume>", record.fuelVolume);
						writer.WriteLine("				<fuelCode> {0} </fuelCode>", record.fuelCode);
					}
					writer.WriteLine("			</oper>");
					writer.WriteLine("		</operList>");
					writer.WriteLine("		<totalAmount> {0} </totalAmount>", rGroup.TotalAmount);
					writer.WriteLine("		<totalVolume> {0} <totalVolume>", rGroup.TotalVolume);
					writer.WriteLine("		<totalDiscount> {0} <totalDiscount>", rGroup.TotalDiscount);
					writer.WriteLine("	</group>");
				}
				writer.WriteLine("</report>");
			}
		}
	}
}
