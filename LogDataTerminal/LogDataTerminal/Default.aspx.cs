using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogDataTerminal.Models;
using LogDataTerminal.Models.Repository;

namespace LogDataTerminal
{
	public partial class Default : System.Web.UI.Page
	{
		[System.Web.Services.WebMethod()]
		[System.Web.Script.Services.ScriptMethod()]
		public IEnumerable<Models.log> GetLogData([Form] string filterSelect)
		{
			var logData = new Repository().logs;
			return (filterSelect ?? "All") == "All" ? logData : logData.Where(p => p.Date == filterSelect);
		}

		[System.Web.Services.WebMethod()]
		[System.Web.Script.Services.ScriptMethod()]
		public IEnumerable<string> GetNameLog()
		{
			try
			{
				return new string[] { "All" }.Concat(new Repository().logs.Select(p => p.Date).Distinct().OrderByDescending(c => c));
			}
			catch (Exception)
			{
				return new string[] { "Error! No records in base" };
				//Здесь надо отобразить сообщение об ошибке на странице как-то по другому
			}
		}
	}
}