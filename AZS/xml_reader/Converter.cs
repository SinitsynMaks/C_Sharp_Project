using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	class Converter
	{
		struct GroupKey
		{
			public string FuelCode {get; set;}
			public string CurrencyCode {get; set;}
			public GroupKey(string fuelCode, string currencyCode)
			{
				FuelCode = fuelCode;
				CurrencyCode = currencyCode;
			}
		}

		class GroupComparer : IComparer<GroupKey>
		{
			public int Compare(GroupKey x, GroupKey y)
			{
				int result;
				if ((result = x.FuelCode.CompareTo(y.FuelCode)) == 0)
					return x.CurrencyCode.CompareTo(y.CurrencyCode);
				return result;
			}
		}

		private SortedList<GroupKey, AZSreportGroup> SortedList { get; set; }
		public IList<AZSreportGroup> ResultList {
			get
			{
				return SortedList.Values;
			}
		}

		public Converter()
		{
			SortedList = new SortedList<GroupKey, AZSreportGroup>(new GroupComparer());
		}

		public void Add(AZSjournalRecords record)
		{
			AZSreportGroup reportGroup;
			if (!SortedList.TryGetValue(new GroupKey(record.fuelCode, record.currencyCode), out reportGroup))
			{
				reportGroup = new AZSreportGroup(record.fuelCode, record.currencyCode);
				SortedList.Add(new GroupKey(record.fuelCode, record.currencyCode), reportGroup);				
			}
			reportGroup.OperList.Add(record);
			reportGroup.TotalAmount += record.amount;
			reportGroup.TotalDiscount += record.discount;
			reportGroup.TotalVolume += record.fuelVolume;
		}

		public void AddCollection(IEnumerable<AZSjournalRecords> records)
		{
			foreach (AZSjournalRecords record in records)
				Add(record);
		}

		public static IList<AZSreportGroup> LinqConvert(IList<AZSjournalRecords> journal)
		{
			var query =
				from record in journal
				group record by new { fuelCode = record.fuelCode, currencyCode = record.currencyCode } into g
				select new AZSreportGroup
				{
					FuelCode = g.Key.fuelCode,
					CurrencyCode = g.Key.currencyCode,
					OperList = g.ToList(),
					TotalAmount = g.Sum(record => record.amount),
					TotalVolume = g.Sum(record => record.fuelVolume),
					TotalDiscount = g.Sum(record => record.discount)
				};
			return query.ToList();
		}
	}
}
