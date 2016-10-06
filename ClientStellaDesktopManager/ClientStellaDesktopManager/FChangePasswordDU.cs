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
		private List<int> ValidKey;
		private char[] keypressed = { (char)Keys.Enter, (char)Keys.Back, (char)Keys.Delete,
									  '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
		public byte[] PasswordPultDUPaket;

		public string PasswordString
		{
			get { return EditPasswordBox.Text;}
		}

		public FChangePasswordDU()
		{
			InitializeComponent();
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
			else
			{
				byte digit1 = Convert.ToByte(EditPasswordBox.Text[0].ToString());
				byte digit2 = Convert.ToByte(EditPasswordBox.Text[1].ToString());
				byte digit3 = Convert.ToByte(EditPasswordBox.Text[2].ToString());
				byte digit4 = Convert.ToByte(EditPasswordBox.Text[3].ToString());
				PasswordPultDUPaket = new byte[] { 1, digit1, digit2, digit3, digit4, 11, 11, 0, 0, 1, 0, 6, 255, 0x0D, 0x0A };
			}
		}

		private void EditPasswordBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				buttonSavePassword_Click(this, e);
				DialogResult = DialogResult.OK;
			}

			if (Array.IndexOf(keypressed, e.KeyChar) == -1)
			{
				e.KeyChar = (char)Keys.Clear;
			}
			//MessageBox.Show("ASCII код нажатой клавиши: "+ Convert.ToInt32(e.KeyChar).ToString());
		}
	}
}
