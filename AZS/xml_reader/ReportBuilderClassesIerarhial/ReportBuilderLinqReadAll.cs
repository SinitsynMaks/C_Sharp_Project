using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class ReportBuilderLinqReadAll : ReportBuilderAdapter
	{
		public ReportBuilderLinqReadAll(IReader reader, IWriter writer) : base(reader, writer)
		{
		}

		public override void CreateReport()
		{
			IList<AZSreportGroup> reportList = Converter.LinqConvert(Reader.ReadAll());
			Writer.Write(reportList);
		}
	}
}
