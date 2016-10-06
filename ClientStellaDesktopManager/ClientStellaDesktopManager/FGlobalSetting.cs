using System;
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
	public partial class FGlobalSetting : Form
	{
		public ComPort comport;
		public string DigitCapacity;
		public FConfigureDevicePrice PriceDeviceForm;
		public FClockSettings ClockSettingsForm;

		public ProgressBar progressInfo
		{
			get { return GlobalServisesFormProgress; }
			set { GlobalServisesFormProgress = value; }
		}

		public FGlobalSetting(ComPort port)
		{
			InitializeComponent();
			comport = port;
		}

		private void FGlobalSetting_Shown(object sender, EventArgs e)
		{
			DigitCapacity = Properties.Settings.Default.DigitCapacity.ToString();
			comboBoxDigitValue.SelectedIndex = comboBoxDigitValue.Items.IndexOf(DigitCapacity);
		}

		private void buttonScanning_Click(object sender, EventArgs e)
		{
			progressInfo.Visible = true;
			comport.GetRealDeviceList(progressInfo);

			int index = 0;
			if (comport.realDeviceList.Count > 0)
			{
				foreach (KeyValuePair<int, string> infoForDevice in comport.realDeviceList)
				{
					index = comport.realDeviceList.IndexOfKey(infoForDevice.Key);
					ListViewItem item = new ListViewItem((index + 1).ToString());
					item.SubItems.Add(infoForDevice.Value);
					item.SubItems.Add(infoForDevice.Key.ToString()); //Добавление адреса из листа
					RealDeviceTable.Items.Add(item);

					//В комбобокс колонки адресов добавляется очередной адрес из списка
					ColumnAdress.Items.Add(item.SubItems[2].Text);

					//Собственно само заполнение UserTableView
					UserTableView.Rows.Add(item.SubItems[0].Text, true, "",
						item.SubItems[1].Text, "", ColumnAdress.Items[index].ToString());
				}
			}
			int totalHeight = 0; // высота всех столбцов в таблице
			for (int i = 0; i < UserTableView.Rows.Count; i++) // перебираем все строки и колонки 
			{
				totalHeight += UserTableView.Rows[i].Height; // суммируем высоту каждой строки
			}
			UserTableView.Height = totalHeight + UserTableView.ColumnHeadersHeight; // меняем высоту dataGridView
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
						ClockSettingsForm = new FClockSettings();
						ClockSettingsForm.Show();
					}
					//MessageBox.Show(item.SubItems[1].Text);
					return;
				}
			}
		}

		private void buttonApplyChanges_Click(object sender, EventArgs e)
		{
			
		}

		private void comboBoxDigitValue_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!(DigitCapacity == comboBoxDigitValue.SelectedItem.ToString()))
			{
				DigitCapacity = comboBoxDigitValue.SelectedItem.ToString();
				//buttonSaveSettings.Enabled = true;
			}
		}

		private void UserTableView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
