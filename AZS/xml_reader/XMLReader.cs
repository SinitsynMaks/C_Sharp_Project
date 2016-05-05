using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AZS_report
{
	class XMLReader : ReaderAdapter
	{
		public XMLReader(string fileName)
		{
			XmlDocument Doc = new XmlDocument();
			Doc.Load(fileName);
			XmlNodeList RecordList = Doc.GetElementsByTagName("record");
			Enumerator = RecordList.GetEnumerator();
		}

		public override AZSjournalRecords GetCurrent()
		{
			XmlNode node = (XmlNode)Enumerator.Current;
			string operNumber = node["operNum"].InnerText;
			string valutaCode = node["currencyCode"].InnerText;
			decimal amountSumm = decimal.Parse(node["amount"].InnerText);
			decimal discountSumm = decimal.Parse(node["discount"].InnerText);
			string Code = node["fuelCode"].InnerText;
			double Volume = double.Parse(node["fuelVolume"].InnerText);
			return new AZSjournalRecords(operNumber, valutaCode, amountSumm, discountSumm, Code, Volume);
		}
	}
}
