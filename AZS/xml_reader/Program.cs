using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AZS_report
{
    static class Program
    {
        static void Main()
        {
			new XMLReportBuilderLinq("xmlJournal.xml", "Test1.xml").CreateReport();
			new XMLReportBuilder("xmlJournal.xml", "Test1.xml").CreateReport();
		}
    }
}
