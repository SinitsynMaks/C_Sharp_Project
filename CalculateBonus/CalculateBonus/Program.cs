using System;
using System.Text;

namespace CalculateBonus
{
	class Program
	{

		static int Salary;
		static byte percentOfSalary;
		static double CorrectFactor;
		static int TaxRate;

		const int defaultSalary = 500;
		const byte defaultpercentOfSalary = 10;
		const double defaultCorrectFactor = 0.96;

		static string surnameSalary;
		static string positionCode;
		static string depCode;
		static decimal bonus;
		static bool stavkaIncluded;


		static int Main(string[] args)
		{
			ReturnCodes returnCode = ParseInputString(args);
			if (returnCode == ReturnCodes.success)
			{
				CalculateSalaryBonus();
				GetResult();
			}

			return (int)returnCode;
		}

		/// <summary>
		/// Метод для проверки корректности входных данных
		/// </summary>
		/// <param name="argsCommandLine"></param>
		/// <returns> Значения из перечисления</returns>
		#region ParseInputString
		public static ReturnCodes ParseInputString(string[] argsCommandLine)
		{
			if (argsCommandLine.Length == 0)
			{
				Console.WriteLine("Не задан обязательный (нулевой) параметр для расчета премии: 'номер отдела, номер должности   фамилия'");
				Console.ReadLine();
				return ReturnCodes.NullCodeOrSurname;
			}
			for (int i = 1; i < (argsCommandLine.Length > 4 ? 4 : argsCommandLine.Length) + 1; i++)
			{
				switch (i)
				{
					case 1:
						string[] CodesAndSurname = argsCommandLine[0].Split(',');

						if ((CodesAndSurname.Length != 2) || (CodesAndSurname[0].Length != 2))
						{
							Console.WriteLine("Не корректно указан код отдела, код должности или имени сотрудника: {0}", argsCommandLine[0]);
							Console.ReadLine();
							return ReturnCodes.NullCodeOrSurname;
						}

						char depCodes = CodesAndSurname[0][0];
						if ((!Char.IsDigit(depCodes)) || (!Enum.IsDefined(typeof(Ranks), (int)Char.GetNumericValue(depCodes))))
						{
							Console.WriteLine("Отдела с указанным номером {0} не существует", depCodes);
							Console.ReadLine();
							return ReturnCodes.NullCodeOfDepartment;
						}

						char jobCodes = CodesAndSurname[0][1];
						if ((!Char.IsDigit(jobCodes)) || (!Enum.IsDefined(typeof(Departments), (int)Char.GetNumericValue(jobCodes))))
						{
							Console.WriteLine("Должности с указанным номером {0} не существует", jobCodes);
							Console.ReadLine();
							return ReturnCodes.NullCodeOfSalary;
						}
						surnameSalary = CodesAndSurname[1].ToUpper();
						depCode = new String(CodesAndSurname[0][0], 1);
						positionCode = new String(CodesAndSurname[0][1], 1);

						Salary = defaultSalary;
						percentOfSalary = defaultpercentOfSalary;
						CorrectFactor = defaultCorrectFactor;

						break;

					case 2:
						if (!(Int32.TryParse(argsCommandLine[1], out Salary)))
						{
							Salary = defaultSalary;
							Console.WriteLine("Значение зарплаты введено некорректно. Расчет премии будет произведен со значениями по умолчанию: {0}", defaultSalary);
						}
						break;

					case 3:
						if (!(Byte.TryParse(argsCommandLine[2], out percentOfSalary)))
						{
							percentOfSalary = defaultpercentOfSalary;
							Console.WriteLine("Значение процента введено некорректно. Расчет премии будет произведен со значением по умолчанию: {0}", defaultpercentOfSalary);
						}
						break;

					case 4:
						if (!(Double.TryParse(argsCommandLine[3], out CorrectFactor)))
						{
							CorrectFactor = defaultCorrectFactor;
							Console.WriteLine("Поправочный коэффициент введен некорректно. Расчет премии будет произведен со значением по умолчанию: {0}", defaultCorrectFactor);
						}
						break;
				}
			}
			return ReturnCodes.success;
		}
		#endregion

		#region CalculateSalaryBonus
		public static decimal CalculateSalaryBonus()
		{
			bonus = (Decimal)(Salary * (percentOfSalary / 100.0) * CorrectFactor);
			PayTax(ref bonus, out TaxRate);
			return bonus;
		}
		#endregion

		static bool PayTax(ref decimal bonus, out int TaxRate)
		{
			const int currentTaxRate = 13;
			TaxRate = currentTaxRate;
			if (percentOfSalary > 10)
			{
				bonus -= bonus * (TaxRate / (decimal)100);
				stavkaIncluded = true;
				return true;
			}
			return false;
		}

		public static void GetResult()
		{
			Console.WriteLine("Фамилия: {0}", surnameSalary);
			Console.WriteLine("Должность: {0}", Enum.Parse(typeof(Ranks), positionCode));
			Console.WriteLine("Отдел: {0}", Enum.Parse(typeof(Departments), depCode));
			Console.WriteLine("Премия: {0}", bonus);
			if (stavkaIncluded)
				Console.WriteLine("Включая налог {0}", TaxRate);
			else
				Console.WriteLine("Не включая налог {0}", TaxRate);
			Console.ReadLine();
		}
}
}