﻿using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace ClientStellaDesktopManager
{
	public partial class MainForm : Form
	{

		public string NameOfCurrentComPort; //Для класса главной формы имя текущего компорта - основополагающее свойство. 
		public int BaudRate; //Второе основополагающее свойство главной формы - скорость подключения к компорту.

		public FAboutProgramm AboutProgrammForm; //Ссылка на окно "О программе"
		public FConfiguringPorts PortConfigForm; //Поле главной формы - ссылка на окно настроек порта.
		public FChangePasswordDU ChangePasswordDUForm; // Ссылка на окно изменения пароля пульта ДУ
		public FPassToGlobalSettings PassToGlobalSettingsForm;
		public FGlobalSetting GlobalSettingForm;
		public ComPort CurrentComPortObject; //Поле главной формы - объект_оболочка над компортом
		public SortedList<int, string> ChangedTextBoxList = null; // Список измененных эдитов

		public ProgressBar progressInfoMainForm
		{
			get
			{
				return progressBarScanningDevice;
			}
			set
			{
				progressBarScanningDevice = value;
			}
		}

		private bool introducedPunctuation; //Флажок показывающий - введена ли запятая или точка в поле с ценой

		public MainForm()
		{
			InitializeComponent();
			PortConfigForm = null;
			ChangePasswordDUForm = null;
			CurrentComPortObject = null;
			AboutProgrammForm = null;
			PassToGlobalSettingsForm = null;
			GlobalSettingForm = null;
			NameOfCurrentComPort = Properties.Settings.Default.PortName; //По умолчанию при запуске будем подключаться к этому порту;
			BaudRate = Properties.Settings.Default.BaudRates;
			introducedPunctuation = false;
			//Keys[] keypressed = { 8, 13, 44, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 127 };
			//ValidKeyMainForm = new List<Keys>(keypressed);
			
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			string filepath = Application.StartupPath + @"\settings.xml";
			if (!System.IO.File.Exists(filepath))
			{
				CreateDefaultXmlsettingsFile();
			}

			//В конструкторе класса ComPort стало известно об имеющихся портах в системе
			CurrentComPortObject = new ComPort(); 
			FormBorderStyle = FormBorderStyle.FixedSingle;
			int digitCapacity = Properties.Settings.Default.DigitCapacity;
		}

		private void CreateDefaultXmlsettingsFile()
		{
			XmlTextWriter textWritter = new XmlTextWriter("settings.xml", null);
			textWritter.WriteStartDocument();
			textWritter.Formatting = Formatting.Indented;
			textWritter.WriteStartElement("allsettings");
			textWritter.WriteEndElement();
			textWritter.Close();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("settings.xml");

			XmlElement ComPortsettingsSection = xmlDoc.CreateElement("ComPortsettings");
			xmlDoc.DocumentElement.AppendChild(ComPortsettingsSection);

			XmlElement Port = xmlDoc.CreateElement("Port");
			Port.InnerText = "COM1";
			ComPortsettingsSection.AppendChild(Port);

			XmlElement PortIndex = xmlDoc.CreateElement("PortIndex");
			PortIndex.InnerText = "0";
			ComPortsettingsSection.AppendChild(PortIndex);

			xmlDoc.Save("settings.xml");
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			progressBarScanningDevice.Visible = false;

			// Открываем порт с ранее сохраненными настройками
			CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
		}

		// Обработка нажатия пункта меню "настройки порта"
		private void PortSettings_Click(object sender, EventArgs e)
		{
			CallSettingsForm();
		}
		public void CallSettingsForm()
		{
			if (PortConfigForm == null)
			{
				PortConfigForm = new FConfiguringPorts(CurrentComPortObject);
				//CurrentComPortObject.Close(NameOfCurrentComPort);
				PortConfigForm.ShowDialog();
			}
			else
			{
				CurrentComPortObject.Close(NameOfCurrentComPort);
				PortConfigForm.ShowDialog();
			}

			//Если пользователь нажал ОК на форме "Настройки порта"
			if (PortConfigForm.DialogResult == DialogResult.OK)
			{
				Properties.Settings.Default.PortName = PortConfigForm.portName;
				Properties.Settings.Default.BaudRates = PortConfigForm.baudRate;
				Properties.Settings.Default.Save();
				NameOfCurrentComPort = PortConfigForm.portName;
				BaudRate = PortConfigForm.baudRate;
				CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
				MessageBox.Show("Текущий порт " + NameOfCurrentComPort + "\n" + "Текущая скорость " + BaudRate, "Изменение настроек порта");
			}
			else
			{
				//MessageBox.Show("Ты просто закрыл окно");
				CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
			}
		}

		// Тестовая кнопка
		private void button1_Click(object sender, EventArgs e)
		{
			/*
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("settings.xml");
			XmlNodeList nodelist = xmlDoc.SelectNodes("allsettings/UserTableViewSection");
			if (nodelist.Count > 0)
			{
				foreach (XmlNode item in nodelist)
				{
					xmlDoc.DocumentElement.RemoveChild(item);
				}
			}
			xmlDoc.Save("settings.xml");
			*/

			/*
			MySettings MySection = MySettings.CreateMySettingsSection();
			MySection.DBName = "SWH";
			MySection.DBPassword = "zx";
			MySection.DBServer = "172";
			MySection.DBUser = "sini";
			foreach (var item in MySection.CurrentConfiguration.Sections)
			{
				MessageBox.Show(item.ToString());
			}
			*/

			//CreateDefaultXmlsettingsFile();
			//XmlDocument doc = new XmlDocument();
			//doc.Load("settings.xml");
			//MessageBox.Show(CurrentComPortObject.GetReadTimeOut("COM1").ToString());
		}

		//Обработчик нажатия кнопки "СЧИТАТЬ"
		private void buttonRead_Click(object sender, EventArgs e)
		{
			panelForPriceDisplay.Controls.Clear();

			progressInfoMainForm.Visible = true;
			progressInfoMainForm.Maximum = 20;
			progressInfoMainForm.Value = 0;

			Label labelInfoFromProcess = new Label();
			labelInfoFromProcess.Visible = true;
			labelInfoFromProcess.Text = "Выполняется опрос устройств...";
			labelInfoFromProcess.AutoSize = true;
			panelForPriceDisplay.Controls.Add(labelInfoFromProcess);
			Application.DoEvents();

			string filepath = Application.StartupPath + @"\settings.xml";
			if (System.IO.File.Exists(filepath))
			{
				ReadPriceFromXMLFile(filepath);
			}
			else
			{
				ReadPriceFromRealDeviceList();
			}
			

			System.Threading.Thread.Sleep(100);
			Application.DoEvents();
			System.Threading.Thread.Sleep(500);
			labelInfoFromProcess.Visible = false;
			progressInfoMainForm.Visible = false;
		}
		private void ReadPriceFromXMLFile(string filepath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("settings.xml");

			// Ищем устройство часы и спрашиваем время
			XmlNode clockNode = xmlDoc.SelectSingleNode("allsettings/FromRealdeviceList/clockdevice");
			if (clockNode != null)
			{
				//Пакет на запрос времени у устройства с часами
			}

			#region Второй способ получения названия топлива и значения цены
			/*
			XmlNode tablePriceNode = xmlDoc.SelectSingleNode("allsettings/UserTableViewSection");
			if ((tablePriceNode != null) && (tablePriceNode.HasChildNodes))
			{
				int i = 0;
				// Обход по каждой строке таблицы устройств с ценой
				foreach (XmlNode device in tablePriceNode.ChildNodes)
				{
					Label NameOfFuel = new Label();
					TextBox PriceOfFuel = new TextBox();
					panelForPriceDisplay.Controls.Add(NameOfFuel);
					panelForPriceDisplay.Controls.Add(PriceOfFuel);

					// Получаем список атрибутов у элемента
					XmlAttributeCollection attr = device.Attributes;

					// Если устройство с первым адресом, спросим у него пароль пульта ДУ
					if (attr.Item(5).Value == "1")
					{
						labelPasswordDU.Text = "Текущий пароль пульта ДУ: " + CurrentComPortObject.GetPasswordPultDU();
					}

					// Проверяем, есть ли название у топлива
					if (attr.Item(2).Value == "")
					{
						NameOfFuel.Text = attr.Item(5).Value;
					}
					else
					{
						NameOfFuel.Text = attr.Item(2).Value;
					}
					*/
			#endregion

			// Опрашиваем сохраненные устройства из секции Aliases, если они есть у нас
			XmlNode aliasNode = xmlDoc.SelectSingleNode("allsettings/Aliases");
			if ((aliasNode != null) && (aliasNode.HasChildNodes))
			{
				int i = 0;
				// Обход по каждой строке секции Alias
				foreach (XmlNode device in aliasNode.ChildNodes)
				{
					Label NameOfFuel = new Label();
					TextBox PriceOfFuel = new TextBox();
					panelForPriceDisplay.Controls.Add(NameOfFuel);
					panelForPriceDisplay.Controls.Add(PriceOfFuel);

					NameOfFuel.Text = device.Name;
					NameOfFuel.Enabled = true;
					NameOfFuel.Width = 130;
					NameOfFuel.Font = new Font(new FontFamily("Arial"), 8);

					PriceOfFuel.Text = CurrentComPortObject.GetPriceFromCurrentDevice(Convert.ToByte(device.InnerText));
					PriceOfFuel.Enabled = true;
					PriceOfFuel.Width = 100;
					PriceOfFuel.MaxLength = 5;
					PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
					PriceOfFuel.Tag = device.InnerText;
					NameOfFuel.Location = new Point(20, i * PriceOfFuel.Height + 40);
					PriceOfFuel.Location = new Point(150, i * PriceOfFuel.Height + 40);
					PriceOfFuel.Enter += PriceOfFuel_Enter;
					PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
					PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
					PriceOfFuel.MouseEnter += PriceOfFuel_MouseEnter;
					i++;
				}
			}
			// Если устройств в таблице не оказалось (нет сохраненных данных об устройствах)
			else
			{
				ReadPriceFromRealDeviceList();
			}
		}
		private void ReadPriceFromRealDeviceList()
		{
			// Получаем realDeviceList (SortedList)
			CurrentComPortObject.GetRealDeviceList(progressInfoMainForm);
			if (CurrentComPortObject.realDeviceList.Count > 0)
			{
				labelPasswordDU.Text = "Текущий пароль пульта ДУ: " + CurrentComPortObject.GetPasswordPultDU();
				int i = 0;
				foreach (KeyValuePair<int, string> infoFromOneDevice in CurrentComPortObject.realDeviceList)
				{
					if (infoFromOneDevice.Value != "часы")
					{
						Label NameOfFuel = new Label();
						TextBox PriceOfFuel = new TextBox();
						panelForPriceDisplay.Controls.Add(NameOfFuel);
						panelForPriceDisplay.Controls.Add(PriceOfFuel);
						NameOfFuel.Text = infoFromOneDevice.Key.ToString();
						NameOfFuel.Enabled = true;
						NameOfFuel.Width = 100;
						NameOfFuel.Font = new Font(new FontFamily("Arial"), 10);
						PriceOfFuel.Text = CurrentComPortObject.GetPriceFromCurrentDevice((byte)infoFromOneDevice.Key);
						PriceOfFuel.Enabled = true;
						PriceOfFuel.Width = 100;
						PriceOfFuel.MaxLength = 5;
						PriceOfFuel.Tag = infoFromOneDevice.Key;
						PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
						NameOfFuel.Location = new Point(20, i * PriceOfFuel.Height + 40);
						PriceOfFuel.Location = new Point(150, i * PriceOfFuel.Height + 40);
						PriceOfFuel.Enter += PriceOfFuel_Enter;
						PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
						PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
						PriceOfFuel.MouseEnter += PriceOfFuel_MouseEnter;
						i++;
					}
				}
			}
			else
			{
				Label NonIformation = new Label();
				panelForPriceDisplay.Controls.Add(NonIformation);
				NonIformation.AutoSize = true;
				NonIformation.Text = "Устройств в сети не обнаружено";
				NonIformation.Location = new Point(40, 40);
			}
		}

		private void SavePriceToFile_Click(object sender, EventArgs e)
		{
			string filepath = Application.StartupPath + @"\settings.xml";
			if (System.IO.File.Exists(filepath))
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(filepath);

				// Ищем секцию с табличными данными и берем оттуда необходимое
				XmlNode tableNode = xmlDoc.SelectSingleNode("allsettings/UserTableViewSection");

				// Если такая секция есть и у секции есть строчки
				if ((tableNode != null) && (tableNode.HasChildNodes))
				{
					System.IO.File.Delete(Application.StartupPath + @"\Ceny.cen");

					// Обход по каждой строке таблицы устройств с ценой
					foreach (XmlNode device in tableNode.ChildNodes)
					{
						// Получаем список атрибутов у строки
						XmlAttributeCollection attr = device.Attributes;
						string str = null;
						if (attr.Item(2).Value == "")
						{
							str = attr.Item(5).Value + "=" + attr.Item(4).Value + Environment.NewLine;
						}
						else
						{
							str = attr.Item(2).Value + "=" + attr.Item(4).Value + Environment.NewLine;
						}
						System.IO.File. AppendAllText(Application.StartupPath + @"\Ceny.cen", str);
					}
				}
			}
		}

		private void LoadPriceFromFile_Click(object sender, EventArgs e)
		{
			panelForPriceDisplay.Controls.Clear();
			openPriceDialog.InitialDirectory = Application.StartupPath;
			if (openPriceDialog.ShowDialog() == DialogResult.OK)
			{
				// Массив всех считанных строк из файла цен
				string[] lines = System.IO.File.ReadAllLines(openPriceDialog.FileName);

				//Массив для каждой разбитой строки
				string[] CurrentInfoDevice;

				int i = 0;
				foreach (string line in lines)
				{
					CurrentInfoDevice = line.Split('=');
					Label NameOfFuel = new Label();
					TextBox PriceOfFuel = new TextBox();
					panelForPriceDisplay.Controls.Add(NameOfFuel);
					panelForPriceDisplay.Controls.Add(PriceOfFuel);
					NameOfFuel.Text = CurrentInfoDevice[0];
					NameOfFuel.Enabled = true;
					NameOfFuel.Width = 100;
					NameOfFuel.Font = new Font(new FontFamily("Arial"),14);
					PriceOfFuel.Text = CurrentInfoDevice[1];
					PriceOfFuel.Enabled = true;
					PriceOfFuel.Width = 70;
					PriceOfFuel.MaxLength = 5;
					PriceOfFuel.Tag = CurrentInfoDevice[0];
					PriceOfFuel.Font = new Font(new FontFamily("Arial"), 14);
					NameOfFuel.Location = new Point(20, i*PriceOfFuel.Height + 40);
					PriceOfFuel.Location = new Point(150, i*PriceOfFuel.Height + 40);
					PriceOfFuel.Enter += PriceOfFuel_Enter;
					PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
					PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
					PriceOfFuel.MouseEnter += PriceOfFuel_MouseEnter;
					i++;
				}
			}
		}

		//Обработка нажатия пункта меню "Изменить пароль пульта ДУ"
		private void ChangePasswordPultDU_Click(object sender, EventArgs e)
		{
			if (ChangePasswordDUForm == null)
			{
				ChangePasswordDUForm = new FChangePasswordDU();
				ChangePasswordDUForm.ShowDialog();
			}
			else
			{
				ChangePasswordDUForm.ShowDialog();
			}

			if (ChangePasswordDUForm.DialogResult == DialogResult.OK)
			{
				if (CurrentComPortObject.SetPasswordPultDU(ChangePasswordDUForm.PasswordPultDUPaket))
				{
					labelPasswordDU.Text = "";
					labelPasswordDU.Text = "Текущий пароль пульта ДУ: " + ChangePasswordDUForm.PasswordString;
				}
			}
		}

		//Обработчик нажатия пункта меню "О программе"
		private void AboutProgramm_Click(object sender, EventArgs e)
		{
			#region Открытие вспомогательных окон до начала настройки
			if (AboutProgrammForm == null)
			{
				AboutProgrammForm = new FAboutProgramm(this);
				AboutProgrammForm.ShowDialog();
			}
			else
			{
				AboutProgrammForm.ShowDialog();
			}

			if (AboutProgrammForm.DialogResult == DialogResult.OK)
			{
				if (PassToGlobalSettingsForm == null)
				{
					PassToGlobalSettingsForm = new FPassToGlobalSettings(this);
					PassToGlobalSettingsForm.ShowDialog();
				}
				else
				{
					PassToGlobalSettingsForm.ShowDialog();
				}
			}
			else
			{
				return;
			}

			if (PassToGlobalSettingsForm.DialogResult == DialogResult.OK)
			{
				if (GlobalSettingForm == null)
				{
					GlobalSettingForm = new FGlobalSetting(CurrentComPortObject);
					//CurrentComPortObject.Close(NameOfCurrentComPort);
					GlobalSettingForm.ShowDialog();
				}
				else
				{
					CurrentComPortObject.Close(NameOfCurrentComPort);
					GlobalSettingForm.ShowDialog();
				}
			}
			else
			{
				return;
			}
			#endregion

			if (GlobalSettingForm.DialogResult == DialogResult.OK)
			{
				panelForPriceDisplay.Controls.Clear();

				int rowCount = GlobalSettingForm.CustomVisibleInfoTable.Rows.Count;
				if (rowCount > 0)
				{
					#region Подготовка XML документа и очистка секций UserTableViewSection, Aliases
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load("settings.xml");

					XmlNodeList nodelist = xmlDoc.SelectNodes("allsettings/UserTableViewSection");
					if (nodelist.Count > 0)
					{
						foreach (XmlNode item in nodelist)
						{
							xmlDoc.DocumentElement.RemoveChild(item);
						}
						xmlDoc.Save("settings.xml");
					}

					nodelist = xmlDoc.SelectNodes("allsettings/Aliases");
					if (nodelist.Count > 0)
					{
						foreach (XmlNode item in nodelist)
						{
							xmlDoc.DocumentElement.RemoveChild(item);
						}
						xmlDoc.Save("settings.xml");
					}
					XmlElement UserTableViewSection = xmlDoc.CreateElement("UserTableViewSection");
					XmlElement AliasSection = xmlDoc.CreateElement("Aliases");
					xmlDoc.DocumentElement.AppendChild(UserTableViewSection);
					xmlDoc.DocumentElement.AppendChild(AliasSection);
					xmlDoc.Save("settings.xml");
					#endregion


					int j = 0;
					for (int i = 0; i < rowCount; i++)
					{
						#region Формирование лейблов и эдитов по данным таблицы
						if ((bool)GlobalSettingForm.CustomVisibleInfoTable[1, i].Value)
						{
							string labeltext = null;
							if (GlobalSettingForm.CustomVisibleInfoTable[2, i].Value.ToString() == "")
							{
								labeltext = GlobalSettingForm.CustomVisibleInfoTable[5, i].Value.ToString();
							}
							else
							{
								labeltext = GlobalSettingForm.CustomVisibleInfoTable[2, i].Value.ToString();
							}
							string fueltext = GlobalSettingForm.CustomVisibleInfoTable[4, i].Value.ToString();
							Label NameOfFuel = new Label();
							TextBox PriceOfFuel = new TextBox();
							panelForPriceDisplay.Controls.Add(NameOfFuel);
							panelForPriceDisplay.Controls.Add(PriceOfFuel);
							NameOfFuel.Text = labeltext;
							NameOfFuel.Enabled = true;
							NameOfFuel.Width = 130;
							NameOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							PriceOfFuel.Text = fueltext;
							PriceOfFuel.Enabled = true;
							PriceOfFuel.Width = 100;
							PriceOfFuel.MaxLength = 5;
							PriceOfFuel.Tag = GlobalSettingForm.CustomVisibleInfoTable[5, i].Value.ToString();
							PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							NameOfFuel.Location = new Point(20, j * PriceOfFuel.Height + 40);
							PriceOfFuel.Location = new Point(170, j * PriceOfFuel.Height + 40);
							PriceOfFuel.Enter += PriceOfFuel_Enter;
							PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
							PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
							PriceOfFuel.MouseEnter += PriceOfFuel_MouseEnter;
							j = j + 1;
						}
						else
						{
							continue;
						}
						#endregion

						#region Занесение данных из таблицы CustomVisibleInfoTable в XML файл
						XmlElement CurrentUserTableViewRow = xmlDoc.CreateElement("Row");
						CurrentUserTableViewRow.SetAttribute("number", (i + 1).ToString()); // Порядковый номер строки
						CurrentUserTableViewRow.SetAttribute("visible",
							GlobalSettingForm.CustomVisibleInfoTable[1, i].Value.ToString()); // Значение из 2 ячейки строки
						CurrentUserTableViewRow.SetAttribute("label",
							GlobalSettingForm.CustomVisibleInfoTable[2, i].Value.ToString()); // Значение из 3 ячейки строки
						CurrentUserTableViewRow.SetAttribute("device",
							GlobalSettingForm.CustomVisibleInfoTable[3, i].Value.ToString()); // Значение из 4 ячейки строки
						CurrentUserTableViewRow.SetAttribute("value",
							GlobalSettingForm.CustomVisibleInfoTable[4, i].Value.ToString()); // Значение из 5 ячейки строки
						CurrentUserTableViewRow.SetAttribute("adress",
							GlobalSettingForm.CustomVisibleInfoTable[5, i].Value.ToString()); // Значение из 6 ячейки строки
						UserTableViewSection.AppendChild(CurrentUserTableViewRow);
						#endregion

						#region Формирование дополнительной секции Aliases
						string userNameElement = GlobalSettingForm.CustomVisibleInfoTable[2, i].Value.ToString();
						string adressNameElement = GlobalSettingForm.CustomVisibleInfoTable[5, i].Value.ToString();
						XmlElement AliasElement = null;
						if (userNameElement == "")
						{
							AliasElement = xmlDoc.CreateElement(adressNameElement);
						}
						else
						{
							AliasElement = xmlDoc.CreateElement(userNameElement);
						}
						AliasElement.InnerText = adressNameElement;
						AliasSection.AppendChild(AliasElement);
						#endregion
					}
					xmlDoc.Save("settings.xml");
				}

				Properties.Settings.Default.DigitCapacity = Convert.ToInt32(GlobalSettingForm.DigitCapacity);
				Properties.Settings.Default.PortName = GlobalSettingForm.portName;
				Properties.Settings.Default.BaudRates = GlobalSettingForm.baudRate;
				Properties.Settings.Default.Save();
				NameOfCurrentComPort = GlobalSettingForm.portName;
				BaudRate = GlobalSettingForm.baudRate;
				CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
				MessageBox.Show("Текущий порт " + NameOfCurrentComPort + "\n"
					+ "Текущая скорость " + BaudRate, "Изменение настроек порта");
			}
			else // if (GlobalSettingForm.DialogResult == DialogResult.OK)
			{
					//Если ты просто закрыл окно настроек;
					CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
			}
		}

		private void PriceOfFuel_MouseEnter(object sender, EventArgs e)
		{
			TextBox currTextBox = sender as TextBox;
			//ToolTip.Show("Привет - это твоя первая подсказка", currTextBox);
			ToolTip t = new ToolTip();
			t.SetToolTip(currTextBox, "Значение цены для устройства с адресом " + currTextBox.Tag.ToString());
		}

		private void EditPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(e.KeyChar == (char)Keys.Enter || e.KeyChar == ',' || e.KeyChar == '.' ||
				(e.KeyChar >= '0' && e.KeyChar <= '9') ||
				e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete))
			{
				e.KeyChar = (char)Keys.Clear;
			}
			//Если мы хотим ввести запятую или точку, но она уже введена...
			if (((e.KeyChar == '.') || (e.KeyChar == ',')) && introducedPunctuation)
			{
				e.KeyChar = (char)Keys.Clear;// То мы не можем ввести ее второй раз
			}
			//Если же мы вводим запятую или точку и ее еще нет в текстовом поле
			else if (((e.KeyChar == '.') || (e.KeyChar == ','))) //&& ((sender as TextBox).Text.Length == 4))
			{
				(sender as TextBox).MaxLength = 5;// То можно ввести точку или запятую, расширяя текст до 5 символов
			}
		}

		private void EditPrice_TextChanged(object sender, EventArgs e)
		{
			TextBox currTextBox = sender as TextBox;
			if (currTextBox.Text.Contains('.') || currTextBox.Text.Contains(','))
			{
				currTextBox.MaxLength = 5;
				introducedPunctuation = true;
			}
			else
			{
				currTextBox.MaxLength = 4;
				introducedPunctuation = false;
			}

			if (ChangedTextBoxList == null)
			{
				ChangedTextBoxList = new SortedList<int, string>();
			}

			//MessageBox.Show((ChangedTextBoxList.IndexOfKey(Convert.ToInt32(currTextBox.Tag)).ToString()));
			if (ChangedTextBoxList.IndexOfKey(Convert.ToInt32(currTextBox.Tag)) != -1)
			{
				ChangedTextBoxList.Remove(Convert.ToInt32(currTextBox.Tag));
				ChangedTextBoxList.Add(Convert.ToInt32(currTextBox.Tag), currTextBox.Text);
			}
			else
			{
				ChangedTextBoxList.Add(Convert.ToInt32(currTextBox.Tag), currTextBox.Text);
			}
		}

		private void PriceOfFuel_Enter(object sender, EventArgs e)
		{
			if ((sender as TextBox).Text.Contains('.') || (sender as TextBox).Text.Contains(','))
			{
				introducedPunctuation = true;
			}
			else
			{
				introducedPunctuation = false;
				(sender as TextBox).MaxLength = 4;
			}
		}

		private void buttonWrite_Click(object sender, EventArgs e)
		{
			if ((ChangedTextBoxList != null) && (ChangedTextBoxList.Count > 0))
			{
				//CurrentComPortObject.SetPriceOnCurrentDevice(1, "1234");
				foreach (KeyValuePair<int, string> items in ChangedTextBoxList)
				{
					CurrentComPortObject.SetPriceOnCurrentDevice((byte)items.Key, items.Value);
				}
				ChangedTextBoxList.Clear();
			}
			else
			{
				MessageBox.Show("Вы не сделали изменений в ценах");
			}
		}
	}
}