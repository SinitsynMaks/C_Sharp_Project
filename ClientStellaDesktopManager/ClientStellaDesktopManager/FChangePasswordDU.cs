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
	public partial class FChangePasswordDU : Form
	{
		private MainForm linkToMainForm;
		private List<int> ValidKey;

		public FChangePasswordDU(MainForm link)
		{
			InitializeComponent();
			linkToMainForm = link;
			int[] keypressed = { 8, 13, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 127 };
			ValidKey = new List<int>(keypressed);
		}

		private void FChangePasswordDU_Shown(object sender, EventArgs e)
		{
			EditPasswordBox.Clear();
			EditPasswordBox.Focus();
		}

		private void buttonSavePassword_Click(object sender, EventArgs e)
		{
			if (EditPasswordBox.Text.Length != 4)
			{
				MessageBox.Show("Введи 4 цифры для пароля","Error");
				EditPasswordBox.Focus();
				DialogResult = DialogResult.None;
				return;
			}

			byte digit1 = Convert.ToByte(EditPasswordBox.Text[0].ToString());
			byte digit2 = Convert.ToByte(EditPasswordBox.Text[1].ToString());
			byte digit3 = Convert.ToByte(EditPasswordBox.Text[2].ToString());
			byte digit4 = Convert.ToByte(EditPasswordBox.Text[3].ToString());

			byte[] PasswordPultDUPaket = { 1, digit1, digit2, digit3, digit4, 11, 11, 0, 0, 1, 0, 6, 255, 0x0D, 0x0A};

			if (linkToMainForm.CurrentComPortObject.SetPasswordPultDU(PasswordPultDUPaket))
			{
				Close();
			}
			else
			{
				return;
			}
		}

		private void EditPasswordBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				buttonSavePassword_Click(this, e);
			}
			int simbol = Convert.ToInt32(e.KeyChar);
			if (!(ValidKey.Contains(simbol)))
			{
				e.KeyChar = (char)Keys.Clear;
			}
			//MessageBox.Show("ASCII код нажатой клавиши: "+ Convert.ToInt32(e.KeyChar).ToString());
		}
	}
}
