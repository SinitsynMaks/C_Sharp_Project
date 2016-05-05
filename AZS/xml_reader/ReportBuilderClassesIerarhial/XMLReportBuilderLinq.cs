using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class XMLReportBuilderLinq : ReportBuilderLinqReadAll
	{
		public XMLReportBuilderLinq(string inFileName, string outFileName) :
			base(new XMLReader(inFileName), new XmlWriter(outFileName))
		{

		}
	}
}
