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
	public partial class FPassToGlobalSettings : Form
	{
		private string password = "1234";
		private MainForm linkToMainForm;

		public FPassToGlobalSettings(MainForm link)
		{
			InitializeComponent();
			linkToMainForm = link;
		}

		private void FPassToGlobalSettings_Shown(object sender, EventArgs e)
		{
			textBoxEnterPAssword.Focus();
			textBoxEnterPAssword.Clear();
			DialogResult = DialogResult.None;
		}

		private void buttonEnterPassword_Click(object sender, EventArgs e)
		{
			if (textBoxEnterPAssword.Text == password)
			{
				DialogResult = DialogResult.OK;
			}
			else
			{
				MessageBox.Show("Вы ввели неверный пароль");
				textBoxEnterPAssword.Focus();
			}
		}

		private void textBoxEnterPAssword_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				DialogResult = DialogResult.OK;
			}
		}
	}
}
