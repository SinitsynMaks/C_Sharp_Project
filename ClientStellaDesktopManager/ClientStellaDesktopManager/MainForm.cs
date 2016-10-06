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
			CurrentComPortObject = new ComPort(); //В конструкторе класса ComPort уже известно об имеющихся портах в системе
			FormBorderStyle = FormBorderStyle.FixedSingle;
			CommonDataStore.digitCapacity = Properties.Settings.Default.DigitCapacity;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			progressBarScanningDevice.Visible = false;
			CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);// Открываем порт с ранее сохраненными настройками

		}

		private void PortSettings_Click(object sender, EventArgs e)// Обработка нажатия пункта меню "настройки порта"
		{
			CallSettingsForm();
		}

		public void CallSettingsForm()
		{
			if (PortConfigForm == null)
			{
				PortConfigForm = new FConfiguringPorts(CurrentComPortObject);
				CurrentComPortObject.Close(NameOfCurrentComPort);
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

		private void button1_Click(object sender, EventArgs e)
		{
			
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

			CurrentComPortObject.GetRealDeviceList(progressInfoMainForm);
			//Application.DoEvents();

			int i = 0;
			if (CurrentComPortObject.realDeviceList.Count > 0)
			{
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

			//System.Threading.Thread.Sleep(1000);
			labelInfoFromProcess.Visible = false;
			progressInfoMainForm.Visible = false;
		}

		private void SavePriceToFile_Click(object sender, EventArgs e)
		{

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

		private void AboutProgramm_Click(object sender, EventArgs e)//Обработчик нажатия пункта меню "О программе"
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
					GlobalSettingForm.ShowDialog();
				}
				else
				{
					GlobalSettingForm.ShowDialog();
				}
			}
			else
			{
				return;
			}

			if (GlobalSettingForm.DialogResult == DialogResult.OK)
			{
				Properties.Settings.Default.DigitCapacity = Convert.ToInt32(GlobalSettingForm.DigitCapacity);
				Properties.Settings.Default.Save();
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
	}
}