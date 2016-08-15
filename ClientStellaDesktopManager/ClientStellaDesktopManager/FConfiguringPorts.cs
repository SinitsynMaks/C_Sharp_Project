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
		private MainForm LinkToMainForm = null;
		public string portName;
		public int baudRate;

		public FConfiguringPorts(MainForm Link)
		{
			InitializeComponent();
			LinkToMainForm = Link;
			portName = Properties.Settings.Default.PortName;
			baudRate = Properties.Settings.Default.BaudRates;
		}

		private void FConfiguringPorts_Shown(object sender, EventArgs e)
		{
			comboBoxPortName.Items.Clear(); // Очистка содержимого бокса для нового заполненияё
			comboBoxPortName.Items.AddRange(LinkToMainForm.CurrentComPortObject.AvailablePortNames); // Загружаем список доступных портов в бокс через свойство класса ComPort
			comboBoxPortName.SelectedIndex = comboBoxPortName.Items.IndexOf(portName); // Отображаем порт из настроект
			comboBoxBaudRate.Items.Clear();
			comboBoxBaudRate.Items.AddRange(LinkToMainForm.CurrentComPortObject.baudRates);
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
