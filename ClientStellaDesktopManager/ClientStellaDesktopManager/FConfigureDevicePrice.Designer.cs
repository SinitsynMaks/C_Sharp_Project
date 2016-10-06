namespace ClientStellaDesktopManager
{
	partial class FConfigureDevicePrice
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSaveSettings = new System.Windows.Forms.Button();
			this.AdressBox = new System.Windows.Forms.TextBox();
			this.PriceBox = new System.Windows.Forms.TextBox();
			this.SpeedBox = new System.Windows.Forms.TextBox();
			this.PasswordDUBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(22, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Адрес";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(22, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Цена";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(22, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Скорость";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(22, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 34);
			this.label4.TabIndex = 3;
			this.label4.Text = "Пароль\r\nпульта ДУ";
			// 
			// buttonSaveSettings
			// 
			this.buttonSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonSaveSettings.Location = new System.Drawing.Point(25, 180);
			this.buttonSaveSettings.Name = "buttonSaveSettings";
			this.buttonSaveSettings.Size = new System.Drawing.Size(185, 26);
			this.buttonSaveSettings.TabIndex = 4;
			this.buttonSaveSettings.Text = "Сохранить изменения";
			this.buttonSaveSettings.UseVisualStyleBackColor = true;
			// 
			// AdressBox
			// 
			this.AdressBox.Location = new System.Drawing.Point(110, 19);
			this.AdressBox.Name = "AdressBox";
			this.AdressBox.Size = new System.Drawing.Size(100, 20);
			this.AdressBox.TabIndex = 5;
			// 
			// PriceBox
			// 
			this.PriceBox.Location = new System.Drawing.Point(110, 54);
			this.PriceBox.Name = "PriceBox";
			this.PriceBox.Size = new System.Drawing.Size(100, 20);
			this.PriceBox.TabIndex = 6;
			// 
			// SpeedBox
			// 
			this.SpeedBox.Location = new System.Drawing.Point(110, 90);
			this.SpeedBox.Name = "SpeedBox";
			this.SpeedBox.Size = new System.Drawing.Size(100, 20);
			this.SpeedBox.TabIndex = 7;
			// 
			// PasswordDUBox
			// 
			this.PasswordDUBox.Location = new System.Drawing.Point(110, 132);
			this.PasswordDUBox.Name = "PasswordDUBox";
			this.PasswordDUBox.Size = new System.Drawing.Size(100, 20);
			this.PasswordDUBox.TabIndex = 8;
			// 
			// FConfigureDevicePrice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(239, 217);
			this.Controls.Add(this.PasswordDUBox);
			this.Controls.Add(this.SpeedBox);
			this.Controls.Add(this.PriceBox);
			this.Controls.Add(this.AdressBox);
			this.Controls.Add(this.buttonSaveSettings);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FConfigureDevicePrice";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Настройка устройства с ценой";
			this.Shown += new System.EventHandler(this.FConfigureDevicePrice_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSaveSettings;
		private System.Windows.Forms.TextBox AdressBox;
		private System.Windows.Forms.TextBox PriceBox;
		private System.Windows.Forms.TextBox SpeedBox;
		private System.Windows.Forms.TextBox PasswordDUBox;
	}
}