using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LogDataTerminal
{
	/// <summary>
	/// Сводное описание для WebServiceLogRep
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
	[System.Web.Script.Services.ScriptService]
	public class WebServiceLogRep : System.Web.Services.WebService
	{
	}
}
