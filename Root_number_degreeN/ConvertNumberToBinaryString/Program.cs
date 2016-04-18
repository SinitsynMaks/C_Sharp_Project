using System;
using System.Text;

namespace ConvertNumberToBinaryString
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите неотрицательное целое число для преобразования");
			uint number;
			while (!(UInt32.TryParse(Console.ReadLine(), out number)))
				Console.WriteLine("Некорректно введено число для преобразования. Введите еще раз");
			Console.WriteLine("{0} - значение методом 'Convert.ToString'", Convert.ToString(number, 2));
			Console.WriteLine("{0} - значение нашим способом", ConvertNumberToBinaryString(number));
			Console.ReadLine();
		}

		public static string ConvertNumberToBinaryString(uint A)
		{
			if (A == 0) return "0";
			int bitLength = 1;
			uint B = A;
			while ((B >>= 1) != 0)
				bitLength++;
			char[] charArr = new char[bitLength];
			B = 1;
			for (int i = bitLength - 1; i >= 0; i--, B <<= 1)
				charArr[i] = ((A & B) == 0) ? '0' : '1';
			return new string(charArr);
		}
	}
}
