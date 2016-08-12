namespace ClientStellaDesktopManager
{
	partial class FConfiguringPorts
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelPorts = new System.Windows.Forms.Label();
			this.labelBaudRate = new System.Windows.Forms.Label();
			this.comboBoxPortName = new System.Windows.Forms.ComboBox();
			this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
			this.buttonSaveSettings = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelPorts
			// 
			this.labelPorts.AutoSize = true;
			this.labelPorts.Location = new System.Drawing.Point(12, 21);
			this.labelPorts.Name = "labelPorts";
			this.labelPorts.Size = new System.Drawing.Size(40, 13);
			this.labelPorts.TabIndex = 0;
			this.labelPorts.Text = "Порты";
			// 
			// labelBaudRate
			// 
			this.labelBaudRate.AutoSize = true;
			this.labelBaudRate.Location = new System.Drawing.Point(12, 49);
			this.labelBaudRate.Name = "labelBaudRate";
			this.labelBaudRate.Size = new System.Drawing.Size(55, 13);
			this.labelBaudRate.TabIndex = 0;
			this.labelBaudRate.Text = "Скорость";
			// 
			// comboBoxPortName
			// 
			this.comboBoxPortName.FormattingEnabled = true;
			this.comboBoxPortName.Location = new System.Drawing.Point(87, 18);
			this.comboBoxPortName.Name = "comboBoxPortName";
			this.comboBoxPortName.Size = new System.Drawing.Size(121, 21);
			this.comboBoxPortName.TabIndex = 1;
			this.comboBoxPortName.Text = "Выбери порт";
			this.comboBoxPortName.SelectedValueChanged += new System.EventHandler(this.comboBoxPortName_SelectedValueChanged);
			// 
			// comboBoxBaudRate
			// 
			this.comboBoxBaudRate.FormattingEnabled = true;
			this.comboBoxBaudRate.Location = new System.Drawing.Point(87, 45);
			this.comboBoxBaudRate.Name = "comboBoxBaudRate";
			this.comboBoxBaudRate.Size = new System.Drawing.Size(121, 21);
			this.comboBoxBaudRate.TabIndex = 1;
			this.comboBoxBaudRate.SelectedValueChanged += new System.EventHandler(this.comboBoxBaudRate_SelectedValueChanged);
			// 
			// buttonSaveSettings
			// 
			this.buttonSaveSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSaveSettings.Location = new System.Drawing.Point(12, 81);
			this.buttonSaveSettings.Name = "buttonSaveSettings";
			this.buttonSaveSettings.Size = new System.Drawing.Size(198, 23);
			this.buttonSaveSettings.TabIndex = 2;
			this.buttonSaveSettings.Text = "Сохранить";
			this.buttonSaveSettings.UseVisualStyleBackColor = true;
			// 
			// FConfiguringPorts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonSaveSettings;
			this.ClientSize = new System.Drawing.Size(222, 116);
			this.Controls.Add(this.buttonSaveSettings);
			this.Controls.Add(this.comboBoxBaudRate);
			this.Controls.Add(this.comboBoxPortName);
			this.Controls.Add(this.labelBaudRate);
			this.Controls.Add(this.labelPorts);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FConfiguringPorts";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Настройка порта";
			this.Shown += new System.EventHandler(this.FConfiguringPorts_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelPorts;
		private System.Windows.Forms.Label labelBaudRate;
		private System.Windows.Forms.ComboBox comboBoxPortName;
		private System.Windows.Forms.ComboBox comboBoxBaudRate;
		private System.Windows.Forms.Button buttonSaveSettings;
	}
}