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

		public FChangePasswordDU(MainForm link)
		{
			InitializeComponent();
			linkToMainForm = link;
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

			byte[] PasswordPultDUPaket = { 1, Convert.ToByte(EditPasswordBox.Text[1]), Convert.ToByte(EditPasswordBox.Text[2]),
											Convert.ToByte(EditPasswordBox.Text[3]), Convert.ToByte(EditPasswordBox.Text[4]),
											11, 11, 0, 0, 1, 0, 6, 255, 0x0D, 0x0A};

			if (linkToMainForm.CurrentComPortObject.SetPasswordPultDU(PasswordPultDUPaket))
			{
				MessageBox.Show("Пароль успешно изменен");
				Close();
			}
			else
			{
				MessageBox.Show("Пароль изменить не удалось");
				return;
			}

		}
	}
}
