using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientStellaDesktopManager
{
	public partial class FAboutProgramm : Form
	{
		private MainForm linkToMainForm;


		public FAboutProgramm(MainForm link)
		{
			InitializeComponent();
			linkToMainForm = link;
		}

		private void FAboutProgramm_Shown(object sender, EventArgs e)
		{
			DateTime date = DateTime.Now;
			AllrightsReservedelabel.Text = "";
			AllrightsReservedelabel.Text = "Все права защищены, " + date.Year.ToString() + " год.";
		}

		private void Secretlabel_DoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		//private void FAboutProgramm_FormClosing(object sender, FormClosingEventArgs e)
		//{
		//	if (e.CloseReason == CloseReason.UserClosing)
		//	{
		//		e.Cancel = true;
		//		Hide();
		//	}
		//}
	}
}
