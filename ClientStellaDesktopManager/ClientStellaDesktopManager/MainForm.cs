using System;
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
			if (!System.IO.File.Exists("settings.xml"))
			{
				CreateDefaultXmlsettingsFile();
			}

			//В конструкторе класса ComPort стало известно об имеющихся портах в системе
			CurrentComPortObject = new ComPort(); 
			FormBorderStyle = FormBorderStyle.FixedSingle;
			int digitCapacity = Properties.Settings.Default.DigitCapacity;
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

		private void button1_Click(object sender, EventArgs e)
		{
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

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("settings.xml");
			XmlNode node = xmlDoc.SelectSingleNode("allsettings/FromRealdeviceList");

			if ((node != null) && (node.HasChildNodes))
			{
				int i = 0;
				foreach (XmlNode device in node.ChildNodes)
				{
					MessageBox.Show(device.Name);
					if (device.Name != "clockdevice ")
					{
						Label NameOfFuel = new Label();
						TextBox PriceOfFuel = new TextBox();
						panelForPriceDisplay.Controls.Add(NameOfFuel);
						panelForPriceDisplay.Controls.Add(PriceOfFuel);
						NameOfFuel.Text = device.Name;
						NameOfFuel.Enabled = true;
						NameOfFuel.Width = 30;
						NameOfFuel.Font = new Font(new FontFamily("Arial"), 5);
						PriceOfFuel.Text = CurrentComPortObject.GetPriceFromCurrentDevice(Convert.ToByte(device.InnerText));
						PriceOfFuel.Enabled = true;
						PriceOfFuel.Width = 100;
						PriceOfFuel.MaxLength = 5;
						PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
						NameOfFuel.Location = new Point(20, i * PriceOfFuel.Height + 40);
						PriceOfFuel.Location = new Point(70, i * PriceOfFuel.Height + 40);
						i++;
					}
					else
					{
						labelPasswordDU.Text = "Текущий пароль пульта ДУ: " + CurrentComPortObject.GetPasswordPultDU();
					}
				}
			}
			else
			{
				// Получаем realDeviceList (SortedList)
				CurrentComPortObject.GetRealDeviceList(progressInfoMainForm);
				if (CurrentComPortObject.realDeviceList.Count > 0)
				{
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
							NameOfFuel.Width = 30;
							NameOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							PriceOfFuel.Text = CurrentComPortObject.GetPriceFromCurrentDevice((byte)infoFromOneDevice.Key);
							PriceOfFuel.Enabled = true;
							PriceOfFuel.Width = 100;
							PriceOfFuel.MaxLength = 5;
							PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							NameOfFuel.Location = new Point(20, i * PriceOfFuel.Height + 40);
							PriceOfFuel.Location = new Point(70, i * PriceOfFuel.Height + 40);
							i++;
						}
						else
						{
							labelPasswordDU.Text = "Текущий пароль пульта ДУ: " + CurrentComPortObject.GetPasswordPultDU();
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

			System.Threading.Thread.Sleep(100);
			Application.DoEvents();
			System.Threading.Thread.Sleep(500);
			labelInfoFromProcess.Visible = false;
			progressInfoMainForm.Visible = false;
		}

		private void SavePriceToFile_Click(object sender, EventArgs e)
		{
			if (CurrentComPortObject.realDeviceList.Count > 0)
			{
				foreach (KeyValuePair<int, string> infoFromOneDevice in CurrentComPortObject.realDeviceList)
				{
					string str = "";
					str = infoFromOneDevice.ToString();
					MessageBox.Show(str);
				}
			}
		}

		private void LoadPriceFromFile_Click(object sender, EventArgs e)
		{
			panelForPriceDisplay.Controls.Clear();
			openPriceDialog.InitialDirectory = Application.StartupPath;
			if (openPriceDialog.ShowDialog() == DialogResult.OK)
			{
				string[] CurrentInfoDevice; //Массив для каждой разбитой строки

				string[] lines = System.IO.File.ReadAllLines(openPriceDialog.FileName);
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
					NameOfFuel.Width = 30;
					NameOfFuel.Font = new Font(new FontFamily("Arial"),14);
					PriceOfFuel.Text = CurrentInfoDevice[1];
					PriceOfFuel.Enabled = true;
					PriceOfFuel.Width = 70;
					PriceOfFuel.MaxLength = 5;
					PriceOfFuel.Font = new Font(new FontFamily("Arial"), 14);
					NameOfFuel.Location = new Point(20, i*PriceOfFuel.Height + 40);
					PriceOfFuel.Location = new Point(70, i*PriceOfFuel.Height + 40);
					PriceOfFuel.Enter += PriceOfFuel_Enter;
					PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
					PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
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

			//Если в окне настроек мы нажали клавишу "Сохранить изменения"
			if (GlobalSettingForm.DialogResult == DialogResult.OK)
			{
				panelForPriceDisplay.Controls.Clear();

				if (CurrentComPortObject.realDeviceList.Count > 0)
				{
					int rowcount = GlobalSettingForm.CustomVisibleInfoTable.Rows.Count;
					for (int i = 0; i < rowcount; i++)
					{
						if ((bool)GlobalSettingForm.CustomVisibleInfoTable[1, i].Value)
						{
							string labeltext = GlobalSettingForm.CustomVisibleInfoTable[2, i].Value.ToString();
							string fueltext = GlobalSettingForm.CustomVisibleInfoTable[4, i].Value.ToString();
							Label NameOfFuel = new Label();
							TextBox PriceOfFuel = new TextBox();
							panelForPriceDisplay.Controls.Add(NameOfFuel);
							panelForPriceDisplay.Controls.Add(PriceOfFuel);
							NameOfFuel.Text = labeltext;
							NameOfFuel.Enabled = true;
							NameOfFuel.Width = 40;
							NameOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							PriceOfFuel.Text = fueltext;
							PriceOfFuel.Enabled = true;
							PriceOfFuel.Width = 100;
							PriceOfFuel.MaxLength = 5;
							PriceOfFuel.Font = new Font(new FontFamily("Arial"), 10);
							NameOfFuel.Location = new Point(20, i * PriceOfFuel.Height + 40);
							PriceOfFuel.Location = new Point(70, i * PriceOfFuel.Height + 40);
						}
					}
				}

				int count = GlobalSettingForm.CustomVisibleInfoTable.RowCount;
				if (count > 0)
				{
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


					XmlElement UserTableViewSection = xmlDoc.CreateElement("UserTableViewSection");
					xmlDoc.DocumentElement.AppendChild(UserTableViewSection);

					for (int i = 0; i < count; i++)
					{
						XmlElement CurrentUserTableViewRow = xmlDoc.CreateElement("Row");
						CurrentUserTableViewRow.SetAttribute("number", (i+1).ToString()); // Порядковый номер строки
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
			else
			{
					//Если ты просто закрыл окно настроек;
					CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);
			}
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
			if ((sender as TextBox).Text.Contains('.') || (sender as TextBox).Text.Contains(','))
			{
				(sender as TextBox).MaxLength = 5;
				introducedPunctuation = true;
			}
			else
			{
				(sender as TextBox).MaxLength = 4;
				introducedPunctuation = false;
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

		}
	}
}