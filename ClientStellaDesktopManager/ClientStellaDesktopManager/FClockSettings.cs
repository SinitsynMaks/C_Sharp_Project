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
	public partial class FClockSettings : Form
	{
		public Button ButtonSaveSettings
		{
			get
			{
				return buttonsave;
			}
			set
			{
				buttonsave = value;
			}
		}

		public DataGridView OperatingModeView
		{
			get
			{
				return dataGridView1;
			}
			set
			{
				dataGridView1 = value;
			}
		}

		public FClockSettings()
		{
			InitializeComponent();
		}

		private void FClockSettings_Shown(object sender, EventArgs e)
		{
			//Column4.Items.Add("Выкл");// Items[0]
			//Column4.Items.Add("Вкл");// Items[1]

			dataGridView1.Rows.Add(1, "NC", "Не используется", false, 0);
			dataGridView1.Rows.Add(2, "rAdi", "Радиация", false, 0);
			dataGridView1.Rows.Add(3, "Hu", "Влажность", false, 0);
			dataGridView1.Rows.Add(4, "PrES", "Давление", false, 0);
			dataGridView1.Rows.Add(5, "T2", "Температура (датчик2)", false, 0);
			dataGridView1.Rows.Add(6, "T1", "Температура (датчик1)", true, 1);
			dataGridView1.Rows.Add(7, "DATE", "Дата", true, 1);
			dataGridView1.Rows.Add(8, "AUTO", "Вкл - перебор режимов,\nВыкл - только время", true, 1);

			int totalHeight = 0; // высота всех столбцов в таблице
			for (int i = 0; i < 8; i++) // перебираем все строки и колонки 
			{
				totalHeight += dataGridView1.Rows[i].Height; // суммируем высоту каждой строки
			}
			dataGridView1.Height = totalHeight + dataGridView1.ColumnHeadersHeight; // меняем высоту dataGridView
			ButtonSaveSettings.Enabled = false;

			OperatingModeView.CellValueChanged += OperatingModeView_CellValueChanged;
			OperatingModeView.CurrentCellDirtyStateChanged += OperatingModeView_CurrentCellDirtyStateChanged;
		}

		private void OperatingModeView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (dataGridView1.IsCurrentCellDirty)
			{
				dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}

		private void OperatingModeView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			int neededRowindex = e.RowIndex;
			int neededColumnindex = e.ColumnIndex + 1;

			if (dataGridView1.Columns[e.ColumnIndex].Name == "Column4")
			{
				ButtonSaveSettings.Enabled = true;
				if ((bool)OperatingModeView[e.ColumnIndex, e.RowIndex].Value)
				{
					OperatingModeView[neededColumnindex, neededRowindex].Value = 1;
				}
				else
				{
					OperatingModeView[neededColumnindex, neededRowindex].Value = 0;
				}
			}
		}
	}
}
