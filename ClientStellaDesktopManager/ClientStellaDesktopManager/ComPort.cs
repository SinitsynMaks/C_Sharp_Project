using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ClientStellaDesktopManager
{
	public static class ComPort
	{
		private static SerialPort port; // Закрытое поле класса - ключевое поле-компорт

		private static string[] availablePortsList = null;
		public static string[] AvailablePortNames
		{
			get
			{
				availablePortsList = SerialPort.GetPortNames();
				List<string> PortNames = new List<string>();
				if (port != null)
					if (port.IsOpen)
						port.Close();
				for (int i = 0; i < availablePortsList.Length; i++)
				{
					try
					{
						port = new SerialPort(availablePortsList[i], 9600);
						port.Open();
						port.Close();
						PortNames.Add(availablePortsList[i]); // Если порт доступен, добавляем его в лист
					}
					catch (Exception)
					{
						continue;
					}
				}
				PortNames.Sort();
				return PortNames.ToArray();
			}
		}
		public static string[] baudRates = { "110", "300", "600", "1200", "2400", "4800", "9600", "14400",
											"19200", "38400", "56000", "57600", "115200", "128000", "256000" };

		private static string _portName; // Закрытое поле - имя порта
		public static string portName // Свойство для чтения закрытого поля _portName
		{
			get { return _portName; }
			set { _portName = value; }
		}

		private static int _baudRate; // Закрытое поле - скорость передачи данных

		public static bool IsPortOpened // Свойство, говорящее закрыт порт или открыт
		{
			get
			{
				if (port == null)
					return false;
				else
					return port.IsOpen;
			}
		}

		public static void Open(string portName)
		{
			try
			{
				port = new SerialPort(portName, 9600);
				port.Open();
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("Программе не удалось подключиться к порту: " + portName, "Ошибка подключения!");
				return;
			}
		}

		public static void Close()
		{
			try
			{
				port.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("Программе не удалось закрыть порт " + portName, "Ошибка закрытия порта");
			}
		}

		public static void SendTestPaket()
		{
			try
			{
				byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
				port.Write(data, 0, data.Length);
			}
			catch(InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
								"Возможно, порт закрыт или недоступен.", "Ошибка отправки пакета в порт");
			}

		}
	}
}
