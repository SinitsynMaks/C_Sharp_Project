namespace ClientStellaDesktopManager
{
	partial class FGlobalSetting
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
			this.MonitoringDevicesGroup = new System.Windows.Forms.GroupBox();
			this.RealDeviceTable = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.comboBoxDigitValue = new System.Windows.Forms.ComboBox();
			this.labelDigitCapacity = new System.Windows.Forms.Label();
			this.buttonScanning = new System.Windows.Forms.Button();
			this.SettingsDisplayDevicesGroup = new System.Windows.Forms.GroupBox();
			this.UserTableView = new System.Windows.Forms.DataGridView();
			this.ColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnActivated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColumnLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnAdress = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.buttonApplyChanges = new System.Windows.Forms.Button();
			this.GlobalServisesFormProgress = new System.Windows.Forms.ProgressBar();
			this.MonitoringDevicesGroup.SuspendLayout();
			this.SettingsDisplayDevicesGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UserTableView)).BeginInit();
			this.SuspendLayout();
			// 
			// MonitoringDevicesGroup
			// 
			this.MonitoringDevicesGroup.Controls.Add(this.RealDeviceTable);
			this.MonitoringDevicesGroup.Controls.Add(this.comboBoxDigitValue);
			this.MonitoringDevicesGroup.Controls.Add(this.labelDigitCapacity);
			this.MonitoringDevicesGroup.Controls.Add(this.buttonScanning);
			this.MonitoringDevicesGroup.Location = new System.Drawing.Point(12, 12);
			this.MonitoringDevicesGroup.Name = "MonitoringDevicesGroup";
			this.MonitoringDevicesGroup.Size = new System.Drawing.Size(439, 249);
			this.MonitoringDevicesGroup.TabIndex = 0;
			this.MonitoringDevicesGroup.TabStop = false;
			this.MonitoringDevicesGroup.Text = "Мониторинг устройств";
			// 
			// RealDeviceTable
			// 
			this.RealDeviceTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.RealDeviceTable.FullRowSelect = true;
			this.RealDeviceTable.GridLines = true;
			this.RealDeviceTable.Location = new System.Drawing.Point(235, 8);
			this.RealDeviceTable.MultiSelect = false;
			this.RealDeviceTable.Name = "RealDeviceTable";
			this.RealDeviceTable.Size = new System.Drawing.Size(202, 240);
			this.RealDeviceTable.TabIndex = 3;
			this.RealDeviceTable.UseCompatibleStateImageBehavior = false;
			this.RealDeviceTable.View = System.Windows.Forms.View.Details;
			this.RealDeviceTable.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RealDeviceTable_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Тип";
			this.columnHeader2.Width = 105;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Адрес";
			// 
			// comboBoxDigitValue
			// 
			this.comboBoxDigitValue.FormattingEnabled = true;
			this.comboBoxDigitValue.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.comboBoxDigitValue.Location = new System.Drawing.Point(91, 187);
			this.comboBoxDigitValue.Name = "comboBoxDigitValue";
			this.comboBoxDigitValue.Size = new System.Drawing.Size(35, 21);
			this.comboBoxDigitValue.TabIndex = 2;
			this.comboBoxDigitValue.SelectedValueChanged += new System.EventHandler(this.comboBoxDigitValue_SelectedValueChanged);
			// 
			// labelDigitCapacity
			// 
			this.labelDigitCapacity.AutoSize = true;
			this.labelDigitCapacity.Location = new System.Drawing.Point(12, 190);
			this.labelDigitCapacity.Name = "labelDigitCapacity";
			this.labelDigitCapacity.Size = new System.Drawing.Size(73, 13);
			this.labelDigitCapacity.TabIndex = 1;
			this.labelDigitCapacity.Text = "Разрядность";
			// 
			// buttonScanning
			// 
			this.buttonScanning.Location = new System.Drawing.Point(15, 144);
			this.buttonScanning.Name = "buttonScanning";
			this.buttonScanning.Size = new System.Drawing.Size(127, 23);
			this.buttonScanning.TabIndex = 0;
			this.buttonScanning.Text = "Найти устройства";
			this.buttonScanning.UseVisualStyleBackColor = true;
			this.buttonScanning.Click += new System.EventHandler(this.buttonScanning_Click);
			// 
			// SettingsDisplayDevicesGroup
			// 
			this.SettingsDisplayDevicesGroup.Controls.Add(this.UserTableView);
			this.SettingsDisplayDevicesGroup.Location = new System.Drawing.Point(12, 277);
			this.SettingsDisplayDevicesGroup.Name = "SettingsDisplayDevicesGroup";
			this.SettingsDisplayDevicesGroup.Size = new System.Drawing.Size(439, 257);
			this.SettingsDisplayDevicesGroup.TabIndex = 1;
			this.SettingsDisplayDevicesGroup.TabStop = false;
			this.SettingsDisplayDevicesGroup.Text = "Настройки отображения устройств";
			// 
			// UserTableView
			// 
			this.UserTableView.AllowUserToAddRows = false;
			this.UserTableView.AllowUserToDeleteRows = false;
			this.UserTableView.AllowUserToResizeColumns = false;
			this.UserTableView.AllowUserToResizeRows = false;
			this.UserTableView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.UserTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.UserTableView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumber,
            this.ColumnActivated,
            this.ColumnLabel,
            this.ColumnType,
            this.ColumnValue,
            this.ColumnAdress});
			this.UserTableView.Location = new System.Drawing.Point(0, 19);
			this.UserTableView.MultiSelect = false;
			this.UserTableView.Name = "UserTableView";
			this.UserTableView.RowHeadersVisible = false;
			this.UserTableView.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.UserTableView.Size = new System.Drawing.Size(436, 232);
			this.UserTableView.TabIndex = 0;
			this.UserTableView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserTableView_CellContentClick);
			// 
			// ColumnNumber
			// 
			this.ColumnNumber.HeaderText = "№";
			this.ColumnNumber.Name = "ColumnNumber";
			this.ColumnNumber.ReadOnly = true;
			this.ColumnNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnNumber.Width = 27;
			// 
			// ColumnActivated
			// 
			this.ColumnActivated.HeaderText = "Активность";
			this.ColumnActivated.Name = "ColumnActivated";
			this.ColumnActivated.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnActivated.Width = 73;
			// 
			// ColumnLabel
			// 
			this.ColumnLabel.HeaderText = "Метка";
			this.ColumnLabel.Name = "ColumnLabel";
			this.ColumnLabel.Width = 70;
			// 
			// ColumnType
			// 
			this.ColumnType.HeaderText = "Тип";
			this.ColumnType.Name = "ColumnType";
			this.ColumnType.ReadOnly = true;
			this.ColumnType.Width = 110;
			// 
			// ColumnValue
			// 
			this.ColumnValue.HeaderText = "Значение";
			this.ColumnValue.Name = "ColumnValue";
			this.ColumnValue.Width = 90;
			// 
			// ColumnAdress
			// 
			this.ColumnAdress.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.ColumnAdress.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ColumnAdress.HeaderText = "Адрес";
			this.ColumnAdress.Name = "ColumnAdress";
			this.ColumnAdress.Sorted = true;
			this.ColumnAdress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ColumnAdress.Width = 62;
			// 
			// buttonApplyChanges
			// 
			this.buttonApplyChanges.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonApplyChanges.Location = new System.Drawing.Point(120, 548);
			this.buttonApplyChanges.Name = "buttonApplyChanges";
			this.buttonApplyChanges.Size = new System.Drawing.Size(221, 39);
			this.buttonApplyChanges.TabIndex = 2;
			this.buttonApplyChanges.Text = "Применить изменения";
			this.buttonApplyChanges.UseVisualStyleBackColor = true;
			this.buttonApplyChanges.Click += new System.EventHandler(this.buttonApplyChanges_Click);
			// 
			// GlobalServisesFormProgress
			// 
			this.GlobalServisesFormProgress.Location = new System.Drawing.Point(12, 262);
			this.GlobalServisesFormProgress.Maximum = 20;
			this.GlobalServisesFormProgress.Name = "GlobalServisesFormProgress";
			this.GlobalServisesFormProgress.Size = new System.Drawing.Size(439, 13);
			this.GlobalServisesFormProgress.TabIndex = 3;
			this.GlobalServisesFormProgress.Value = 20;
			this.GlobalServisesFormProgress.Visible = false;
			// 
			// FGlobalSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 599);
			this.Controls.Add(this.GlobalServisesFormProgress);
			this.Controls.Add(this.buttonApplyChanges);
			this.Controls.Add(this.SettingsDisplayDevicesGroup);
			this.Controls.Add(this.MonitoringDevicesGroup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FGlobalSetting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Сервисные настройки";
			this.Shown += new System.EventHandler(this.FGlobalSetting_Shown);
			this.MonitoringDevicesGroup.ResumeLayout(false);
			this.MonitoringDevicesGroup.PerformLayout();
			this.SettingsDisplayDevicesGroup.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.UserTableView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox MonitoringDevicesGroup;
		private System.Windows.Forms.GroupBox SettingsDisplayDevicesGroup;
		private System.Windows.Forms.DataGridView UserTableView;
		private System.Windows.Forms.Button buttonApplyChanges;
		private System.Windows.Forms.ComboBox comboBoxDigitValue;
		private System.Windows.Forms.Label labelDigitCapacity;
		private System.Windows.Forms.Button buttonScanning;
		private System.Windows.Forms.ListView RealDeviceTable;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ProgressBar GlobalServisesFormProgress;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumber;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnActivated;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColumnAdress;
	}
}