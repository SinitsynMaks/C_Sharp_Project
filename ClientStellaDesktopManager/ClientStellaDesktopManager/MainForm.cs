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
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			progressBarScanningDevice.Visible = false;
			CurrentComPortObject.Open(NameOfCurrentComPort, BaudRate);// Открываем порт с ранее сохраненными настройками

		}

		private void PortSettings_Click(object sender, EventArgs e)
		{
			CallSettingsForm();
		}

		public void CallSettingsForm()
		{
			if (PortConfigForm == null)
			{
				PortConfigForm = new FConfiguringPorts(this);
				CurrentComPortObject.Close(NameOfCurrentComPort);
				PortConfigForm.ShowDialog();
			}
			else
			{
				CurrentComPortObject.Close(NameOfCurrentComPort);
				PortConfigForm.ShowDialog();
			}

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
			labelPasswordDU.Text += CurrentComPortObject.GetPasswordPultDU();
		}

		private void SavePriceToFile_Click(object sender, EventArgs e)
		{

		}

		private void LoadPriceFromFile_Click(object sender, EventArgs e)
		{
			openPriceDialog.InitialDirectory = Application.StartupPath;
			if (openPriceDialog.ShowDialog() == DialogResult.OK)
			{
				string[] CurrentInfoDevice; //Массив по каждой строчке
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
					PriceOfFuel.KeyPress += new KeyPressEventHandler(EditPrice_KeyPress);
					PriceOfFuel.Enter += PriceOfFuel_Enter;
					//PriceOfFuel.KeyDown += PriceOfFuel_KeyDown;
					PriceOfFuel.TextChanged += new EventHandler(EditPrice_TextChanged);
					i++;
				}
			}
		}

		private void ChangePasswordPultDU_Click(object sender, EventArgs e)
		{
			if (ChangePasswordDUForm == null)
			{
				ChangePasswordDUForm = new FChangePasswordDU(this);
				ChangePasswordDUForm.ShowDialog();
			}
			else
			{
				ChangePasswordDUForm.ShowDialog();
			}
		}

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
					GlobalSettingForm = new FGlobalSetting(this);
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
		}

		private void EditPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(e.KeyChar == (char)Keys.Enter || e.KeyChar == ',' || e.KeyChar == '.' ||
				(e.KeyChar >= '0' && e.KeyChar <= '9') ||
				e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete))
			{
				e.KeyChar = (char)Keys.Clear;
			}
			if (((e.KeyChar == '.') || (e.KeyChar == ',')) && introducedPunctuation)//Если мы хотим ввести запятую или точку, но она уже введена
			{
				e.KeyChar = (char)Keys.Clear;// То мы не вводим ее второй раз
			}
			else if (((e.KeyChar == '.') || (e.KeyChar == ',')) && (sender as TextBox).Text.Length == 4)// Если же мы вводим запятую или точку и ее еще нет в тексте
			{
				(sender as TextBox).MaxLength = 5;// То можем вводить ее, расширяя текст до 5 символов
			}
			else
			{
				(sender as TextBox).MaxLength = 4;// Если же это не точка или запятая, 
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
			}
		}
	}
}