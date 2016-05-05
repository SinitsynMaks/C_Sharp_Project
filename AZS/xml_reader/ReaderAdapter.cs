using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	abstract class ReaderAdapter : IReader
	{
		protected IEnumerator Enumerator { get; set; }

		public abstract AZSjournalRecords GetCurrent();

		public AZSjournalRecords Current
		{
			get
			{
				return GetCurrent();
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return GetCurrent();
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public virtual bool MoveNext()
		{
			return Enumerator.MoveNext();
		}

		public virtual void Reset()
		{
			Enumerator.Reset();
		}

		public IList<AZSjournalRecords> ReadAll()
		{
			List<AZSjournalRecords> list = new List<AZSjournalRecords>();
			Reset();
			while (MoveNext())
				list.Add(Current);
			return list;
		}
	}
}
