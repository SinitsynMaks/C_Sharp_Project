using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClientStellaDesktopManager
{
	public class ComPort
	{
		// Закрытое поле класса - сам .NET объект для работы с компортом
		private SerialPort port = null;

		//Словарь доступных портов типа "COM1=port1"
		public Dictionary<string, SerialPort> MyComPortsDictionary = null;

		//Поле - список имен всех портов в системе. 
		//Заполняется методом FillAvailablePortNamesList
		private List<string> AvailablePortNamesList = null;

		// Список пар вида "<адрес>-<тип устройства>" сортированных по адресу.
		//Заполняется методом GetRealDeviceList.
		public SortedList<int, string> realDeviceList;

		public object[] baudRates = { 110, 300, 600, 1200, 2400, 4800, 9600, 14400,
									19200, 38400, 56000, 57600, 115200, 128000, 256000 };

		//Объект, отображающий ход выполнения задачи
		private ProgressBar progressInfo;

		public ComPort()
		{
			MyComPortsDictionary = new Dictionary<string, SerialPort>();
			AvailablePortNamesList = new List<string>();

			// Данный метод заполнил AvailablePortNamesList (List<string>)
			FillAvailablePortNamesList();

			realDeviceList = new SortedList<int, string>();
		}

		public int GetReadTimeOut(string portname)
		{
			return MyComPortsDictionary[portname].ReadTimeout;
		}

		// Метод, заполняющий AvailablePortNamesList и Dictionary<string, SerialPort>
		// актуальными ком-портами в системе
		private void FillAvailablePortNamesList()
		{
			Close(Properties.Settings.Default.PortName);

			// Заполняем массив именами всех COM-портов на машине
			string[] PortNamesArray = SerialPort.GetPortNames();

			AvailablePortNamesList.Clear();
			for (int i = 0; i < PortNamesArray.Length; i++)
			{
				try
				{
					//Создается NET-овский объект SerialPort для управление портом
					//с указанным именем и скоростью 9600, передаваемыми в конструкторе
					port = new SerialPort(PortNamesArray[i], 9600); 

					//Проверяем го на доступность "железным" путем "открытия-закрытия"
					port.Open();
					port.Close();

					// Если порт доступен(не занят никем), добавляем его в List<string>
					AvailablePortNamesList.Add(PortNamesArray[i]);

					// Также сохраняем доступный порт в словаре, 
					// ассоциируя с именем порта NET объект порта
					MyComPortsDictionary.Add(PortNamesArray[i], port); 
				}
				catch (Exception)
				{
					continue;
				}
			}
			port = null;
			AvailablePortNamesList.Sort();
			//Open(Properties.Settings.Default.PortName, Properties.Settings.Default.BaudRates);
		}

		/* Метод всего лишь преобразует полученный
		   AvailablePortNamesList (List<string>) в массив */
		public string[] GetAvailablePortNamesList
		{
			get
			{
				FillAvailablePortNamesList();
				return AvailablePortNamesList.ToArray();
			}
		}

		// Метод, говорящий закрыт порт или открыт
		public bool IsPortOpened(string portName) 
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

					//Операция чтения ждет 100 мс, после чего выдается исключение
					port.ReadTimeout = 100; 
				}
			}
			else
			{
				MessageBox.Show("Программе не удалось подключиться к порту  " + portName + "\n" +
								"Возможно порт занят другим приложением или недоступен",
								"Ошибка открытия порта");
			}	
		}

		public void Close(string portName)
		{
			if (!(port == null)) //Если порт еще не закрыт.
			{
				MyComPortsDictionary[portName].Close(); //То закрываем его
				port = null; // Значение переменной null означает что порт закрыт.
			}
		}

		//Запрос пароля у управляющего устройства
		public string GetPasswordPultDU()
		{
			// Пока считаем что пароль пульта хранится в управляющем устройстве с адресом 1
			byte[] data = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 16, 0xFF, 0x0D, 0x0A };
			byte[] datain = new byte[15];
			System.Text.StringBuilder s =new System.Text.StringBuilder();
			try
			{
				port.Write(data, 0, data.Length);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
								"Возможно, порт закрыт или недоступен.", "Ошибка опроса устройства");
			}

			System.Threading.Thread.Sleep(50);

			try
			{
				port.Read(datain, 0, 15);
			}
			catch (TimeoutException)
			{
				MessageBox.Show("Определить пароль не удалось.\nПричина: время ожидания ответа от устройства превысило максимальное значение",
								"Ошибка изменения пароля");
				return "- - - -";
			}

			if ((datain[0]==0) & (datain[10]==data[11]) & (datain[11]==255))
			{
				for (int i = 0; i < 4; i++)
				{
					s.Append(Convert.ToString(datain[i + 1]));
				}
				return s.ToString();
			}
			else
			{
				return "- - - -";
			}
		}

		//Установка нового пароля пульта ДУ на управляющем устройстве
		public bool SetPasswordPultDU(byte[] dataTochange)
		{
			try
			{
				port.Write(dataTochange, 0, dataTochange.Length);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
								"Возможно, порт закрыт или недоступен.", "Ошибка опроса устройства");
			}

			System.Threading.Thread.Sleep(50);

			byte[] datain = new byte[15];
			try
			{
				port.Read(datain, 0, 15);
			}
			catch (TimeoutException)
			{
				MessageBox.Show("Пароль изменить не удалось.\nПричина: время ожидания ответа от устройства превысило максимальное значение",
								"Ошибка изменения пароля");
				return false;
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Пароль изменить не удалось.\nПричина:порт, которому предназначались данные, закрыт", "Ошибка доступа к порту");
				return false;
			}
			if ((datain[0]==0) & (datain[10]==6) & (datain[11]==255))
			{
				MessageBox.Show("Пароль успешно изменен");
				return true;
			}
			else
			{
				MessageBox.Show("Пароль изменить не удалось.\nПричина: поступили неверные данные от устройства", "Ошибка изменения пароля");
				return false;
			}
		}

		// Опрос всех имеющихся в сети устройств и запись их 
		// в сортированный список realDeviceList (SortedList)
		public void GetRealDeviceList(ProgressBar bar)
		{
			realDeviceList.Clear();
			progressInfo = bar;

			byte[] datain = new byte[15];
			byte[] pollingDevicesPackage = new byte[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 15, 0xFF, 0x0D, 0x0A };

			for (byte i = 0; i <= 19; i++)
			{
				progressInfo.Value = i+1;

				pollingDevicesPackage[0] = (byte)(i+1);

				try
				{
					port.Write(pollingDevicesPackage, 0, 15);
				}
				catch (InvalidOperationException)
				{
					MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
								"Возможно, порт закрыт или недоступен.", "Ошибка опроса устройства");
					return;
				}

				System.Threading.Thread.Sleep(50);

				try
				{
					port.Read(datain, 0, 15);
				}
				catch (Exception)
				{
					continue;
				}

				if ((datain[8] == i+1) & (datain[9] == 2) & (datain[10] == 15) & (datain[11] == 255))
				{
					realDeviceList.Add(i+1, "панель с ценой");
				}
				if ((datain[8] == i+1) & (datain[9] == 3) & (datain[10] == 15) & (datain[11] == 255))
				{
					realDeviceList.Add(i+1, "часы");
				}
				Array.Clear(datain, 0, 15);
			}
		}

		//Запрос цены у конеретного устройства по его адресу
		public string GetPriceFromCurrentDevice(byte adress)
		{
			byte[] datain = new byte[15];

			//Пакет запроса цены
			byte[] callingPricesValuePackage = new byte[15] { adress, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0x0B, 0xFF, 0x0D, 0x0A };

			try
			{
				port.Write(callingPricesValuePackage, 0, 15);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет в порт " + port.PortName + "\n" +
							"Возможно, порт закрыт или недоступен.", "Ошибка опроса устройства");
				return "-";
			}

			System.Threading.Thread.Sleep(50);

			try
			{
				port.Read(datain, 0, 15);
			}
			catch (Exception)
			{
				return "-";
			}

			if ((datain[8] == adress) & (datain[10] == 11) & (datain[11] == 255))
			{
				string price = "";
				for (byte i = 1; i < 5; i++)
				{
					if (datain[i] == 11)
					{
						continue;
					}
					else
					{
						price += datain[i].ToString();
					}
				}
				if (datain[7] > 0)
				{
					price = price.Insert(price.Length - datain[7], ",");
				}
				return price;
			}
			else
			{
				return "-";
			}
		}

		// Установка цены на конкретном устройстве
		public void SetPriceOnCurrentDevice(byte adress, string price)
		{
			// Вспомогательная переменная для строки без пунктуации
			string s1 = "";

			// Массив для принимаемых данных
			byte[] datain = new byte[15];

			//Пока пакет установки цены проинициализировали так:
			byte[] SetPricePackage = new byte[15] { adress, 0, 0, 0, 0, 255, 255, 0, 0, 1, 0, 1, 0xFF, 0x0D, 0x0A };

			// Теперь корректируем данные
			if (price.Contains(".") || price.Contains(","))
			{
				for (int i = 0; i < price.Length; i++)
				{
					if (price[i] == '.' || price[i] == ',')
					{
						SetPricePackage[7] = (byte)(price.Length - i - 1);
						continue;
					}
					else
					{
						s1 += price[i];
					}
					//MessageBox.Show(price[i].ToString());
				}
			}
			else
			{
				s1 = price;
			}

			int x = 0;
			if (s1.Length < 4)
			{
				// Заполняем старшие байты (слева)
				for (int i = 0; i < (4 - s1.Length); i++)
				{
					SetPricePackage[i + 1] = 11;
				}

				// Заполняем младшие байты (справа)
				for (int i = 4 - s1.Length; i < 4; i++)
				{
					SetPricePackage[i + 1] = Convert.ToByte(s1[x].ToString());
					x = x + 1;
				}
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					SetPricePackage[i + 1] = Convert.ToByte(s1[i].ToString());
				}
			}

			try
			{
				port.Write(SetPricePackage, 0, 15);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не удалось отправить пакет на запись в порт " + port.PortName + "\n" +
							"Возможно, порт закрыт или недоступен.", "Ошибка записи цены");
				return;
			}

			System.Threading.Thread.Sleep(50);

			try
			{
				port.Read(datain, 0, 15);
			}
			catch (Exception)
			{
				MessageBox.Show("Устройство c адресом " + adress.ToString() + " не ответило на запись цены");
				return;
			}

			if ((datain[8] == adress) & (datain[10] == 1) & (datain[11] == 255))
			{
				return;
			}
			else
			{
				MessageBox.Show("Поступили неверные данные от устройства с ценой");
			}
		}

		// Запрос времени у часов
		public void GetTime(byte adress)
		{

		}
	}
}
