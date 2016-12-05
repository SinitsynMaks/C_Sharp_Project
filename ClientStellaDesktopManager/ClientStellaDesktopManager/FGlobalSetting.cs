using System;
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
using System.IO;

namespace ClientStellaDesktopManager
{
	public partial class FGlobalSetting : Form
	{
		public ComPort comport;
		public string DigitCapacity;

		public string portName;
		public int baudRate;

		public FConfigureDevicePrice PriceDeviceForm;
		public FClockSettings ClockSettingsForm;

		public FGlobalSetting(ComPort port)
		{
			InitializeComponent();
			comport = port;
		}

		public ProgressBar progressInfo
		{
			get { return GlobalServisesFormProgress; }
			set { GlobalServisesFormProgress = value; }
		}

		public DataGridView CustomVisibleInfoTable
		{
			get { return UserTableView; }
			set { UserTableView = value; }
		}

		private void FGlobalSetting_Shown(object sender, EventArgs e)
		{
			portName = Properties.Settings.Default.PortName;
			baudRate = Properties.Settings.Default.BaudRates;

			comboBoxPortName.Items.Clear();

			// Загружаем список доступных портов в бокс через свойство класса ComPort
			comboBoxPortName.Items.AddRange(comport.GetAvailablePortNamesList);

			// Отображаем порт из настроект
			comboBoxPortName.SelectedIndex = comboBoxPortName.Items.IndexOf(portName);

			comboBoxBaudrate.Items.Clear();
			comboBoxBaudrate.Items.AddRange(comport.baudRates);
			comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf(baudRate);

			DigitCapacity = Properties.Settings.Default.DigitCapacity.ToString();
			comboBoxDigitValue.SelectedIndex = comboBoxDigitValue.Items.IndexOf(DigitCapacity);

			buttonApplyChanges.Enabled = true;
			comport.Open(Properties.Settings.Default.PortName, Properties.Settings.Default.BaudRates);
		}

		private void buttonScanning_Click(object sender, EventArgs e)
		{
			progressInfo.Visible = true;
			buttonApplyChanges.Enabled = true;
			comport.GetRealDeviceList(progressInfo); //Образовался comport.realDeviceList (SortedList)
			RealDeviceTable.Items.Clear();//Очистили первую таблицу
			UserTableView.Rows.Clear();//Очистили вторую таблицу


			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("settings.xml");

			//Удаляем секцию/секции в конфиг файле, если она/они там есть
			XmlNodeList nodelist = xmlDoc.SelectNodes("allsettings/FromRealdeviceList");
			if (nodelist.Count > 0)
			{
				foreach (XmlNode item in nodelist)
				{
					xmlDoc.DocumentElement.RemoveChild(item);
				}
				xmlDoc.Save("settings.xml");
			}

			int index = 0;
			if (comport.realDeviceList.Count > 0)
			{
				XmlElement realDeviceListSection = xmlDoc.CreateElement("FromRealdeviceList");
				xmlDoc.DocumentElement.AppendChild(realDeviceListSection);

				foreach (KeyValuePair<int, string> infoForDevice in comport.realDeviceList)
				{
					index = comport.realDeviceList.IndexOfKey(infoForDevice.Key);

					//Первая строка сразу создается со значением первого столбца - порядоквым индексом 
					ListViewItem item = new ListViewItem((index + 1).ToString());

					//Значение второго столбца - имя реального устройства в сети
					item.SubItems.Add(infoForDevice.Value);

					//Значение 3-го столбца - адрес реального устройства в сети
					item.SubItems.Add(infoForDevice.Key.ToString());
					RealDeviceTable.Items.Add(item);

					//В список комбобокса колонки адресов добавляется очередной адрес из списка
					ColumnAdress.Items.Add(item.SubItems[2].Text);
					if (infoForDevice.Value != "часы")
					{
						//Собственно само заполнение UserTableView
						UserTableView.Rows.Add(
							(index + 1).ToString(),					// 1 столбец - порядковый номер строки
							true,									// 2 столбец - видимость в главном окне
							"",										// 3 столбец - метка/псевдоним адреса/имя топлива
							item.SubItems[1].Text,					// 4 столбец - тип устройства
							comport.GetPriceFromCurrentDevice((byte)infoForDevice.Key),		// 5 столбец - значение цены
							ColumnAdress.Items[index].ToString());	// 6 столбец - значение адреса
					}

					XmlElement CurrentDeviceElement = null;
					if (infoForDevice.Value == "панель с ценой")
					{
						CurrentDeviceElement = xmlDoc.CreateElement("pricedevice");
					}
					else
					{
						CurrentDeviceElement = xmlDoc.CreateElement("clockdevice");
					}
					CurrentDeviceElement.InnerText = infoForDevice.Key.ToString();
					realDeviceListSection.AppendChild(CurrentDeviceElement);
					//realDeviceListSection.SetAttribute("Адрес" + infoForDevice.Key.ToString(), infoForDevice.Value);
				}

				XmlTextWriter tr = new XmlTextWriter("settings.xml", null);
				tr.Formatting = Formatting.Indented;
				xmlDoc.WriteContentTo(tr);
				tr.Close();
			}

			/*
			int totalHeight = 0; // высота всех столбцов в таблице
			for (int i = 0; i < UserTableView.Rows.Count; i++) // перебираем все строки и колонки 
			{
				totalHeight += UserTableView.Rows[i].Height; // суммируем высоту каждой строки
			}
			UserTableView.Height = totalHeight + UserTableView.ColumnHeadersHeight; // меняем высоту dataGridView
			*/

			System.Threading.Thread.Sleep(300);
			Application.DoEvents();
			System.Threading.Thread.Sleep(600);
			progressInfo.Visible = false;
		}

		private void RealDeviceTable_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			foreach (ListViewItem item in RealDeviceTable.Items)
			{
				if (item.Selected)
				{
					if (item.SubItems[1].Text == "панель с ценой")
					{
						PriceDeviceForm = new FConfigureDevicePrice();
						if (item.SubItems[2].Text != "1")
						{
							PriceDeviceForm.Height = 190;
							PriceDeviceForm.PasswordDULabel.Visible = false;
							PriceDeviceForm.passwordDUtextBox.Visible = false;
							PriceDeviceForm.ButtonSaveSettings.Location = new Point(25, 120);
						}
						PriceDeviceForm.ButtonSaveSettings.Enabled = false;
						PriceDeviceForm.Show();
					}
					else
					{
						ClockSettingsForm = FClockSettings.CreateFclocksettingsWindow();
						ClockSettingsForm.Show();
					}
					//MessageBox.Show(item.SubItems[1].Text);
					return;
				}
			}
		}

		private void comboBoxDigitValue_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(DigitCapacity == comboBoxDigitValue.SelectedItem.ToString()))
			{
				DigitCapacity = comboBoxDigitValue.SelectedItem.ToString();
				buttonApplyChanges.Enabled = true;
			}
		}

		private void comboBoxPortName_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(portName == comboBoxPortName.SelectedItem.ToString()))
			{
				comport.Close(portName);
				portName = comboBoxPortName.SelectedItem.ToString();
				buttonApplyChanges.Enabled = true;
				comport.Open(portName, Properties.Settings.Default.BaudRates);
			}
		}

		private void comboBoxBaudrate_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(baudRate == (int)comboBoxBaudrate.SelectedItem))
			{
				baudRate = (int)comboBoxBaudrate.SelectedItem;
				buttonApplyChanges.Enabled = true;
			}
		}
	}
}
