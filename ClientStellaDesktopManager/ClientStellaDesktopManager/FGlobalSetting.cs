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
		private MainForm linkToMainForm;

		public FGlobalSetting(MainForm link)
		{
			InitializeComponent();
			linkToMainForm = link;
		}
	}
}
