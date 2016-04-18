using System;
using System.IO;
using System.Collections.Generic;

namespace OutputPosition
{
	/// <summary>
	/// Базовый класс консольного приложения
	/// </summary>
	class Program
	{
		/// <summary>
		/// Главный метод приложения, анализирующий входные параметры.
		/// </summary>
		/// <param name="args">Аргументы консольного приложения</param>
		const char Ctrl_Z = (char)26;

		static void Main(string[] args)
		{
			List<string> dataList = new List<string>();
			if ((args.Length == 0) || (args[0] == "<") || (args[0] == ">"))
			{
				dataList = ReadData();
				OutPutData(dataList);
			}
			else
			{
				FileInfo fileInfo = null;
				for (int i=0; i < args.Length; i++)
				{
					string fileName = args[i];
					fileInfo = new FileInfo(fileName);
					if (fileInfo.Exists)
					{
						using (StreamReader reader = fileInfo.OpenText())
						{
							Console.SetIn(reader);
							dataList = ReadData();
							OutPutData(dataList);
						}
					}
					else
					{
						Console.WriteLine(String.Format("Ошибка чтения из файла (аргумент {0})", i));
					}
				}
			}
		}

		static List<string> ReadData()
		{
			List<string> data = new List<string>();
			string line;
			while ((line = Console.ReadLine()) != null)
			{
				int i = line.IndexOf(Ctrl_Z);
				if (i == -1)
					data.Add(line);
				else
				{
					data.Add(line.Substring(0, i));
					break;
				}
			}
			return data;
		}

		static void OutPutData(List<string> dataList)
		{
			dataList.ForEach(TransformAndOutPutString);
		}

		static void TransformAndOutPutString(string s)
		{
			string[] line = s.Split(',');
			string x = line[0].Replace('.', ',');
			string y = line[1].Replace('.',',');
			Console.WriteLine("X: {0}	Y: {1}", x, y);
		}
	}
}
