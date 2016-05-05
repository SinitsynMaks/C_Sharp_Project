namespace AZS_report
{
	class XMLReportBuilder : ReportBuilderReadConsequentally
	{
		public XMLReportBuilder(string inFileName, string outFileName) :
			base(new XMLReader(inFileName), new XmlWriter(outFileName))
		{

		}
	}
}
