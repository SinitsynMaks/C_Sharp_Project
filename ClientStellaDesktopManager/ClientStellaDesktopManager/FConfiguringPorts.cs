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
		public FConfiguringPorts()
		{
			InitializeComponent();
		}

		private void FConfiguringPorts_Shown(object sender, EventArgs e)
		{
			comboBoxPortName.Items.Clear(); // Очистка содержимого бокса для нового заполненияё
			comboBoxPortName.Items.AddRange(ComPort.AvailablePortNames); // Загружаем список доступных портов в бокс через свойство класса ComPort
			comboBoxPortName.SelectedIndex = comboBoxPortName.Items.IndexOf(Properties.Settings.Default.PortName);// Отображаем порт из настроект
			comboBoxBaudRate.Items.AddRange(ComPort.baudRates);
			comboBoxBaudRate.SelectedIndex = comboBoxBaudRate.Items.IndexOf("9600");
		}

		private void comboBoxPortName_SelectedValueChanged(object sender, EventArgs e)
		{
			Portselection();
		}

		private void Portselection()
		{
			if (Properties.Settings.Default.PortName != comboBoxPortName.SelectedItem.ToString())
			{
				Properties.Settings.Default.PortName = comboBoxPortName.SelectedItem.ToString();
				Properties.Settings.Default.Save();
			}
		}
	}
}
