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
			openPriceDialog.ShowDialog();
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
				//AboutProgrammForm.Owner = this;
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
		}
	}
}