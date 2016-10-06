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
	public partial class FConfigureDevicePrice : Form
	{
		public Label PasswordDULabel
		{
			get
			{
				return label4;
			}
			set
			{
				label4 = value;
			}
		}

		public TextBox passwordDUtextBox
		{
			get
			{
				return PasswordDUBox;
			}
			set
			{
				PasswordDUBox = value;
			}
		}

		public Button ButtonSaveSettings
		{
			get
			{
				return buttonSaveSettings;
			}
			set
			{
				buttonSaveSettings = value;
			}
		}

		public FConfigureDevicePrice()
		{
			InitializeComponent();
		}

		private void FConfigureDevicePrice_Shown(object sender, EventArgs e)
		{

		}
	}
}
