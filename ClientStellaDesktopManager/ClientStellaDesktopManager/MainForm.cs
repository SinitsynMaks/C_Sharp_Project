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

		public string NameOfCurrentComPort; 
		public FConfiguringPorts PortConfigForm; //Поле главной формы - окно настроек порта
		public ComPort CurrentComPortObject; //Поле главной формы - объект_оболочка над компортом

		public MainForm()
		{
			InitializeComponent();
			PortConfigForm = null;
			CurrentComPortObject = null;
			NameOfCurrentComPort = Properties.Settings.Default.PortName; ; //По умолчанию в настройках будем подключаться к этому порту;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			CurrentComPortObject = new ComPort(); //В конструкторе уже известно об имеющихся портах в системе
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			progressBarScanningDevice.Visible = false;

			// Создаем объект для работы с ком портом с нужным именем и скоростью
			
			CurrentComPortObject.Open(NameOfCurrentComPort);// Открываем порт-объект
		}

		private void PortSettings_Click(object sender, EventArgs e)
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
				NameOfCurrentComPort = PortConfigForm.portName;
				Properties.Settings.Default.PortName = PortConfigForm.portName;
				Properties.Settings.Default.BaudRates = PortConfigForm.baudRates;
				string baudRate = PortConfigForm.baudRates;
				CurrentComPortObject.Open(NameOfCurrentComPort);
				MessageBox.Show("Текущий порт " + NameOfCurrentComPort + "\n" + "Текущая скорость " + baudRate, "Изменение настроек порта");
			}
			else
			{
				//MessageBox.Show("Ты просто закрыл окно");
				CurrentComPortObject.Open(NameOfCurrentComPort);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CurrentComPortObject.SendTestPaket();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			CurrentComPortObject.Open(Properties.Settings.Default.PortName);
		}
	}
}