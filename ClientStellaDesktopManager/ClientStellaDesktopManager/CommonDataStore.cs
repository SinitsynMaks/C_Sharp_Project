using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ClientStellaDesktopManager
{
	public sealed class MySettings : ConfigurationSection
	{
		#region Default
		private static MySettings _default = null;

		private static bool InitDefault()
		{
			if (_default == null)
			{
				MySettings sets = null;
				try
				{
					Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
					if (config.Sections["MySettings"] == null)
					{
						sets = new MySettings();
						config.Sections.Add("MySettings", sets);
						config.Save(ConfigurationSaveMode.Minimal, true);
					}
					else sets = (MySettings)config.Sections["MySettings"];
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Ошибка получения настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				_default = sets;
			}
			return true;
		}

		public static MySettings CreateMySettingsSection()
		{
			if (_default == null)
				InitDefault();
			return _default;
		}
		#endregion

		public MySettings()
		{
			base.Properties.Add(new ConfigurationProperty("db_server", typeof(string), "localhost"));
			base.Properties.Add(new ConfigurationProperty("db_user", typeof(string), "root"));
			base.Properties.Add(new ConfigurationProperty("db_pass", typeof(string), "pass"));
			base.Properties.Add(new ConfigurationProperty("db_name", typeof(string), "test2"));
		}

		public void Save()
		{
			this.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
		}

		public string DBServer
		{
			get
			{
				return (string)this["db_server"];
			}
			set
			{
				this["db_server"] = value;
			}
		}

		public string DBUser
		{
			get
			{
				return (string)this["db_user"];
			}
			set
			{
				this["db_user"] = value;
			}
		}

		public string DBPassword
		{
			get
			{
				return (string)this["db_pass"];
			}
			set
			{
				this["db_pass"] = value;
			}
		}

		public string DBName
		{
			get
			{
				return (string)this["db_name"];
			}
			set
			{
				this["db_name"] = value;
			}
		}
	}
}
