using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS_report
{
	interface IReader : IEnumerator<AZSjournalRecords>
	{
		IList<AZSjournalRecords> ReadAll();
	}
}
