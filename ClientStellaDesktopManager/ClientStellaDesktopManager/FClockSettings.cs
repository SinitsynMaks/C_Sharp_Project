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
			dataGridView1.Rows.Add(1, "NC", "Не используется", false, 0);
			dataGridView1.Rows.Add(2, "rAdi", "Радиация", false, 0);
			dataGridView1.Rows.Add(3, "Hu", "Влажность", false, 0);
			dataGridView1.Rows.Add(4, "PrES", "Давление", false, 0);
			dataGridView1.Rows.Add(5, "T2", "Температура (датчик2)", false, 0);
			dataGridView1.Rows.Add(6, "T1", "Температура (датчик1)", true, 1);
			dataGridView1.Rows.Add(7, "DATE", "Дата", true, 1);
			dataGridView1.Rows.Add(8, "AUTO", "1 - перебор режимов,0 - только время", true, 1);

			int totalHeight = 0; // высота всех столбцов в таблице
			for (int i = 0; i < 8; i++) // перебираем все строки и колонки 
			{
				totalHeight += dataGridView1.Rows[i].Height; // суммируем высоту каждой строки
			}
			dataGridView1.Height = totalHeight + dataGridView1.ColumnHeadersHeight; // меняем высоту dataGridView

			//OperatingModeView.CellValueChanged += OperatingModeView_CellValueChanged;
			//OperatingModeView.CellEnter += OperatingModeView_CellEnter;
			OperatingModeView.CellMouseClick += OperatingModeView_CellMouseClick;
		}

		private void OperatingModeView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			int neededRowindex = OperatingModeView.CurrentCell.RowIndex;
			int neededColumnindex = OperatingModeView.CurrentCell.ColumnIndex + 1;
			if (OperatingModeView.CurrentCell.Value.ToString() == "true")
			{
				OperatingModeView.CurrentCell = OperatingModeView[neededColumnindex, neededRowindex];
				//OperatingModeView[neededColumnindex, neededRowindex].Value = 1.ToString();
				OperatingModeView.CurrentCell.Value = 1.ToString();
			}
			else
			{
				OperatingModeView.CurrentCell = OperatingModeView[neededColumnindex, neededRowindex];
				//OperatingModeView[neededColumnindex, neededRowindex].Value = 1.ToString();
				//OperatingModeView[neededColumnindex, neededRowindex].Value = 0.ToString();
				OperatingModeView.CurrentCell.Value = 1.ToString();
			}
		}

		private void OperatingModeView_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			string msg = String.Format("Row: {0}, Column: {1}",
			dataGridView1.CurrentCell.RowIndex,
			dataGridView1.CurrentCell.ColumnIndex);
			MessageBox.Show(msg, "Current Cell");
		}

		private void OperatingModeView_CellValueChanged(object sender, EventArgs e)
		{
			int neededRowindex = OperatingModeView.CurrentCell.RowIndex;
			int neededColumnindex = OperatingModeView.CurrentCell.ColumnIndex + 1;
			if (OperatingModeView.CurrentCell.Value.ToString() == "true")
			{
				OperatingModeView[neededColumnindex, neededRowindex].Value = 1.ToString();
			}
			else
			{
				OperatingModeView[neededColumnindex, neededRowindex].Value = 0.ToString();
			}
		}
	}
}
