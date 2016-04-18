using System;

namespace Root_number_degreeN
{
	class Program
	{
		const double epsilon = 0.00001;

		static void Main(string[] args)
		{
			Console.WriteLine("Введите значение числа А");
			double A;
			while (!(double.TryParse(Console.ReadLine(), out A)))
				Console.WriteLine("Некорректно введено число А. Введите еще раз");

			Console.WriteLine("Введите значение извлекаемого корня N");
			double N;
			while (!(double.TryParse(Console.ReadLine(), out N)))
				Console.WriteLine("Некорректно введено число N. Введите еще раз");

			PrintResult(MyMath.Root(N, A, epsilon), Math.Pow(A, (1 / N)));

			Console.ReadKey();
		}

		public static void PrintResult(double myResult, double netResult)
		{
			Console.WriteLine("Моим методом: {0}. Методом Math.Pow: {1}", myResult, netResult);
			Console.WriteLine("Погрешность результата между машинным и ручным методом: {0}", Math.Abs(myResult - netResult));
		}
		
	}

	public class MyMath
	{
		public static double Root(double N, double A, double eps)
		{
			var x0 = 1.0; //Начальное предположение, примем равным числу 1
			var x1 = (1 / N) * ((N - 1) * x0 + A / Math.Pow(x0, N - 1));

			while (Math.Abs(x1 - x0) > eps)
			{
				x0 = x1;
				x1 = (1 / N) * ((N - 1) * x0 + A / Math.Pow(x0, N - 1));
			}
			return x1;
		}
	}
}
