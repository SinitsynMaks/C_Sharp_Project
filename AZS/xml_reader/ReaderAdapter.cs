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
		protected IEnumerator Enumerator { get; set; } // Свойство, доступное только потомкам этого класса

		public abstract AZSjournalRecords GetCurrent();

		public AZSjournalRecords Current // Реализация метода интерфейса IEnumerator
		{
			get
			{
				return GetCurrent(); // Реализован в потомке XMLReader
			}
		}

		object IEnumerator.Current // Не понял что это за свойство
		{
			get
			{
				return GetCurrent();
			}
		}

		public void Dispose() // Реализация метода интерфейса IDosposable
		{
			throw new NotImplementedException();
		}

		public virtual bool MoveNext() // Реализация метода интерфейса IEnumerator
		{
			return Enumerator.MoveNext();
		}

		public virtual void Reset() //Реализация метода интерфейса IEnumerator
		{
			Enumerator.Reset();
		}

		public IList<AZSjournalRecords> ReadAll() //Реализация метода интерфейса IReader
		{
			List<AZSjournalRecords> list = new List<AZSjournalRecords>();
			Reset();
			while (MoveNext())
				list.Add(Current);
			return list;
		}
	}
}
