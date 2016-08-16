using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ClientStellaDesktopManager
{
	public class ComPort
	{
		private SerialPort port = null; // Закрытое поле класса - объект компорта
		public Dictionary<string, SerialPort> MyComPortsDictionary = null; //Закрытое поле класса - словарь доступных портов
		private List<string> PortNamesList = null;  //Закрытое поле класса - массив всех доступных портов в системе
		public object[] baudRates = { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200, 128000, 256000 };

		public ComPort()
		{
			MyComPortsDictionary = new Dictionary<string, SerialPort>();
			PortNamesList = new List<string>();
			FillAvailablePortNamesList();
		}

		private void FillAvailablePortNamesList() // Закрытый метод класса, заполняющий список актуальных ком-портов в системе
		{
			string[] allComPortListOnThisComputer = SerialPort.GetPortNames(); // Get an array of com_ports on this comp
			PortNamesList.Clear();
			for (int i = 0; i < allComPortListOnThisComputer.Length; i++)
			{
				try
				{
					port = new SerialPort(allComPortListOnThisComputer[i], 9600); //Открываем порт с указанным именем и скоростью 9600 по умолчанию
					port.Open();
					port.Close();
					PortNamesList.Add(allComPortListOnThisComputer[i]); // Если порт доступен(не открыт никем), добавляем его в лист
					MyComPortsDictionary.Add(allComPortListOnThisComputer[i], port); // Here. Available port is stored in the dictionary
				}
				catch (Exception)
				{
					continue;
				}
			}
			port = null;
			PortNamesList.Sort();
		}

		public string[] AvailablePortNames
		{
			get
			{
				FillAvailablePortNamesList();
				return PortNamesList.ToArray();
			}
		}

		public bool IsPortOpened(string portName) // Метод, говорящий закрыт порт или открыт
		{
			if (MyComPortsDictionary.ContainsKey(portName)) //Если в словаре есть такой порт
				return MyComPortsDictionary[portName].IsOpen; // Проверяем его состояние на открытость
			else
				return false;
		}

		public void Open(string portName, int baudrate)
		{
			if (MyComPortsDictionary.ContainsKey(portName)) //Если в словаре есть такой порт
			{
				port = MyComPortsDictionary[portName];
				if (!(port.IsOpen))
				{
					port.Open();
					port.BaudRate = baudrate;
				}
			}
			else
			{
				MessageBox.Show("Программе не удалось подключиться к порту  " + portName + "\n" + "Возможно порт занят другим приложением или недоступен", "Ошибка открытия порта");
			}	
		}

		public void Close(string portName)
		{
			if (!(port == null)) //Если порт уже не закрыт.
			{
				MyComPortsDictionary[portName].Close(); //То закрываем его
				port = null; // Значение переменной null означает что порт закрыт.
			}
		}

		public void SendTestPaket()
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

		public string GetPasswordPultDU()
		{
			byte[] datain = new byte[15];
			System.Text.StringBuilder s =new System.Text.StringBuilder();
			try
			{
				byte[] data = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 16, 0xFF, 0x0D, 0x0A };
				port.Write(data, 0, data.Length);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
								"Возможно, порт закрыт или недоступен.", "Ошибка отправки пакета в порт");
			}

			port.Read(datain,0,15);

			for (int i = 0; i < 4; i++)
			{
				s.Append(Convert.ToString(datain[i+1]));
			}

			return s.ToString();
		}

		public bool SetPasswordPultDU(byte[] dataTochange)
		{

			return false;
		}
	}
}
