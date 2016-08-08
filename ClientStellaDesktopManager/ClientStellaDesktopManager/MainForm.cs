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
		public MainForm()
		{
			InitializeComponent();
		}

		public FConfiguringPorts PortConfig = null;

		private void MainForm_Shown(object sender, EventArgs e)
		{
			progressBarScanningDevice.Visible = false;

			// Создаем объект для работы с ком портом с нужным именем и скоростью
			Properties.Settings.Default.PortName = "COM1";
			ComPort.Open(Properties.Settings.Default.PortName);
		}

		private void PortSettings_Click(object sender, EventArgs e)
		{
			if (PortConfig == null)
			{
				PortConfig = new FConfiguringPorts();
				PortConfig.ShowDialog();
			}
			else
				PortConfig.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ComPort.Open(Properties.Settings.Default.PortName);
			ComPort.SendTestPaket();
			//ComPort.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ComPort.Open(Properties.Settings.Default.PortName);
		}
	}
}
