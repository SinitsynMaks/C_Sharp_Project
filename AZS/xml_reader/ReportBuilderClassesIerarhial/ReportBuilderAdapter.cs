namespace AZS_report
{
	abstract class ReportBuilderAdapter : IReportBuilder
	{
		protected IReader Reader { get; set; }
		protected IWriter Writer { get; set; }

		public ReportBuilderAdapter(IReader reader, IWriter writer)
		{
			Reader = reader;
			Writer = writer;
		}

		public abstract void CreateReport();
	}
}
