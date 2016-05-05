namespace AZS_report
{
	class ReportBuilderReadConsequentally : ReportBuilderAdapter
	{
		protected Converter Converter { get; set; }

		public ReportBuilderReadConsequentally(IReader reader, IWriter writer) : base(reader, writer)
		{
			Converter = new Converter();
		}

		public override void CreateReport()
		{
			while (Reader.MoveNext())
				Converter.Add(Reader.Current);
			Writer.Write(Converter.ResultList);
		}
	}
}
