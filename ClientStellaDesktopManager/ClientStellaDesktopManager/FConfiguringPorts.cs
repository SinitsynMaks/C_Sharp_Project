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
	public partial class FConfiguringPorts : Form
	{
		private ComPort LinkToComPort = null;

		public string portName;
		public int baudRate;

		public FConfiguringPorts(ComPort Link)
		{
			InitializeComponent();
			LinkToComPort = Link;
		}

		private void FConfiguringPorts_Shown(object sender, EventArgs e)
		{
			portName = Properties.Settings.Default.PortName;
			baudRate = Properties.Settings.Default.BaudRates;

			comboBoxPortName.Items.Clear();

			// Загружаем список доступных портов в бокс через свойство класса ComPort
			comboBoxPortName.Items.AddRange(LinkToComPort.GetAvailablePortNamesList);

			// Отображаем порт из настроект
			comboBoxPortName.SelectedIndex = comboBoxPortName.Items.IndexOf(portName); 
			comboBoxBaudRate.Items.Clear();
			comboBoxBaudRate.Items.AddRange(LinkToComPort.baudRates);
			comboBoxBaudRate.SelectedIndex = comboBoxBaudRate.Items.IndexOf(baudRate);
			buttonSaveSettings.Enabled = false;
		}

		private void comboBoxPortName_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(portName == comboBoxPortName.SelectedItem.ToString()))
			{
				portName = comboBoxPortName.SelectedItem.ToString();
				buttonSaveSettings.Enabled = true;
			}
		}

		private void comboBoxBaudRate_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(baudRate == (int)comboBoxBaudRate.SelectedItem))
			{
				baudRate = (int)comboBoxBaudRate.SelectedItem;
				buttonSaveSettings.Enabled = true;
			}
		}
	}
}
