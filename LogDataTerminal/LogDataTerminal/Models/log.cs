using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataTerminal.Models
{
	[Serializable]
	public class log
	{
		public int logID { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
		public string Message { get; set; }
	}
}