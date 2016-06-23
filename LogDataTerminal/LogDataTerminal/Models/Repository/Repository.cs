using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataTerminal.Models.Repository
{
	public class Repository
	{
		private static Dictionary<int, log> data = new Dictionary<int, log>();
		public IEnumerable<log> logs
		{
			get
			{
				return data.Values;
				//return null; имитация сообщения об ошибке
			}
		}

		static Repository()
		{
			log[] dataLog = new log[] {
				new log { Name = "Terminal_1", Date = "19.06.2016", Message = "Ready"},
				new log { Name = "Terminal_2", Date = "20.06.2016", Message = "Error"},
				new log { Name = "Terminal_3", Date = "21.06.2016", Message = "Stopped"},
				new log { Name = "Terminal_1", Date = "22.06.2016", Message = "Non"},
				new log { Name = "Terminal_4", Date = "23.06.2016", Message = "Open"},
				new log { Name = "Terminal_5", Date = "23.06.2016", Message = "Start"},
				new log { Name = "Terminal_4", Date = "23.06.2016", Message = "Closed"}
			};

			for (int i = 0; i < dataLog.Length; i++)
			{
				dataLog[i].logID = i;
				data[i] = dataLog[i];
			}
		}
	}
}