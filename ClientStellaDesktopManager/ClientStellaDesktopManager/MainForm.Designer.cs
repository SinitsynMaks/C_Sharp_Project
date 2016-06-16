namespace ClientStellaDesktopManager
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelPasswordDU = new System.Windows.Forms.Label();
			this.panelForDateTime = new System.Windows.Forms.Panel();
			this.labelDateTimeInfo = new System.Windows.Forms.Label();
			this.buttonSetTimeFromPK = new System.Windows.Forms.Button();
			this.checkBoxAutoSinhrTime = new System.Windows.Forms.CheckBox();
			this.progressBarScanningDevice = new System.Windows.Forms.ProgressBar();
			this.panelForPriceDisplay = new System.Windows.Forms.Panel();
			this.buttonRead = new System.Windows.Forms.Button();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.Mainmenu = new System.Windows.Forms.MenuStrip();
			this.Menufile = new System.Windows.Forms.ToolStripMenuItem();
			this.PortSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panelForDateTime.SuspendLayout();
			this.Mainmenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelPasswordDU
			// 
			this.labelPasswordDU.AutoSize = true;
			this.labelPasswordDU.Location = new System.Drawing.Point(12, 36);
			this.labelPasswordDU.Name = "labelPasswordDU";
			this.labelPasswordDU.Size = new System.Drawing.Size(154, 13);
			this.labelPasswordDU.TabIndex = 0;
			this.labelPasswordDU.Text = "Текущий пароль пульта ДУ: ";
			// 
			// panelForDateTime
			// 
			this.panelForDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelForDateTime.Controls.Add(this.labelDateTimeInfo);
			this.panelForDateTime.Controls.Add(this.buttonSetTimeFromPK);
			this.panelForDateTime.Controls.Add(this.checkBoxAutoSinhrTime);
			this.panelForDateTime.Location = new System.Drawing.Point(12, 61);
			this.panelForDateTime.Name = "panelForDateTime";
			this.panelForDateTime.Size = new System.Drawing.Size(310, 136);
			this.panelForDateTime.TabIndex = 1;
			// 
			// labelDateTimeInfo
			// 
			this.labelDateTimeInfo.AutoSize = true;
			this.labelDateTimeInfo.Location = new System.Drawing.Point(118, 48);
			this.labelDateTimeInfo.Name = "labelDateTimeInfo";
			this.labelDateTimeInfo.Size = new System.Drawing.Size(43, 26);
			this.labelDateTimeInfo.TabIndex = 2;
			this.labelDateTimeInfo.Text = "Время:\r\nДата:";
			// 
			// buttonSetTimeFromPK
			// 
			this.buttonSetTimeFromPK.Location = new System.Drawing.Point(13, 48);
			this.buttonSetTimeFromPK.Name = "buttonSetTimeFromPK";
			this.buttonSetTimeFromPK.Size = new System.Drawing.Size(90, 66);
			this.buttonSetTimeFromPK.TabIndex = 1;
			this.buttonSetTimeFromPK.Text = "Установить время с ПК";
			this.buttonSetTimeFromPK.UseVisualStyleBackColor = true;
			// 
			// checkBoxAutoSinhrTime
			// 
			this.checkBoxAutoSinhrTime.AutoSize = true;
			this.checkBoxAutoSinhrTime.Location = new System.Drawing.Point(13, 14);
			this.checkBoxAutoSinhrTime.Name = "checkBoxAutoSinhrTime";
			this.checkBoxAutoSinhrTime.Size = new System.Drawing.Size(168, 17);
			this.checkBoxAutoSinhrTime.TabIndex = 0;
			this.checkBoxAutoSinhrTime.Text = "Атосинхронизация времени";
			this.checkBoxAutoSinhrTime.UseVisualStyleBackColor = true;
			// 
			// progressBarScanningDevice
			// 
			this.progressBarScanningDevice.Location = new System.Drawing.Point(12, 197);
			this.progressBarScanningDevice.Name = "progressBarScanningDevice";
			this.progressBarScanningDevice.Size = new System.Drawing.Size(310, 13);
			this.progressBarScanningDevice.Step = 1;
			this.progressBarScanningDevice.TabIndex = 2;
			this.progressBarScanningDevice.Tag = "";
			this.progressBarScanningDevice.Value = 100;
			this.progressBarScanningDevice.Visible = false;
			// 
			// panelForPriceDisplay
			// 
			this.panelForPriceDisplay.AutoScroll = true;
			this.panelForPriceDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelForPriceDisplay.Enabled = false;
			this.panelForPriceDisplay.Location = new System.Drawing.Point(12, 210);
			this.panelForPriceDisplay.Name = "panelForPriceDisplay";
			this.panelForPriceDisplay.Size = new System.Drawing.Size(310, 360);
			this.panelForPriceDisplay.TabIndex = 3;
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(12, 587);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(138, 33);
			this.buttonRead.TabIndex = 4;
			this.buttonRead.Text = "Считать";
			this.buttonRead.UseVisualStyleBackColor = true;
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(184, 587);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(138, 33);
			this.buttonWrite.TabIndex = 4;
			this.buttonWrite.Text = "Записать";
			this.buttonWrite.UseVisualStyleBackColor = true;
			// 
			// Mainmenu
			// 
			this.Mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menufile});
			this.Mainmenu.Location = new System.Drawing.Point(0, 0);
			this.Mainmenu.Name = "Mainmenu";
			this.Mainmenu.Size = new System.Drawing.Size(334, 24);
			this.Mainmenu.TabIndex = 5;
			this.Mainmenu.Text = "menuStrip1";
			// 
			// Menufile
			// 
			this.Menufile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PortSettings});
			this.Menufile.Name = "Menufile";
			this.Menufile.Size = new System.Drawing.Size(48, 20);
			this.Menufile.Text = "Файл";
			// 
			// PortSettings
			// 
			this.PortSettings.Name = "PortSettings";
			this.PortSettings.Size = new System.Drawing.Size(169, 22);
			this.PortSettings.Text = "Настройки порта";
			this.PortSettings.Click += new System.EventHandler(this.PortSettings_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(149, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Дзынь";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(247, 32);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Connect";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 632);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonWrite);
			this.Controls.Add(this.buttonRead);
			this.Controls.Add(this.panelForPriceDisplay);
			this.Controls.Add(this.progressBarScanningDevice);
			this.Controls.Add(this.panelForDateTime);
			this.Controls.Add(this.labelPasswordDU);
			this.Controls.Add(this.Mainmenu);
			this.MainMenuStrip = this.Mainmenu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Управление стеллой";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.panelForDateTime.ResumeLayout(false);
			this.panelForDateTime.PerformLayout();
			this.Mainmenu.ResumeLayout(false);
			this.Mainmenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelPasswordDU;
		private System.Windows.Forms.Panel panelForDateTime;
		private System.Windows.Forms.Button buttonSetTimeFromPK;
		private System.Windows.Forms.CheckBox checkBoxAutoSinhrTime;
		private System.Windows.Forms.Label labelDateTimeInfo;
		private System.Windows.Forms.ProgressBar progressBarScanningDevice;
		private System.Windows.Forms.Panel panelForPriceDisplay;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.MenuStrip Mainmenu;
		private System.Windows.Forms.ToolStripMenuItem Menufile;
		private System.Windows.Forms.ToolStripMenuItem PortSettings;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}

