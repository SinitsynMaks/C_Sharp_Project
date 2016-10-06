namespace ClientStellaDesktopManager
{
	partial class FChangePasswordDU
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
			this.EditPasswordBox = new System.Windows.Forms.TextBox();
			this.buttonSavePassword = new System.Windows.Forms.Button();
			this.labelInform = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// EditPasswordBox
			// 
			this.EditPasswordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.EditPasswordBox.Location = new System.Drawing.Point(12, 25);
			this.EditPasswordBox.MaxLength = 4;
			this.EditPasswordBox.Name = "EditPasswordBox";
			this.EditPasswordBox.Size = new System.Drawing.Size(215, 29);
			this.EditPasswordBox.TabIndex = 0;
			this.EditPasswordBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditPasswordBox_KeyPress);
			// 
			// buttonSavePassword
			// 
			this.buttonSavePassword.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSavePassword.Location = new System.Drawing.Point(12, 69);
			this.buttonSavePassword.Name = "buttonSavePassword";
			this.buttonSavePassword.Size = new System.Drawing.Size(215, 35);
			this.buttonSavePassword.TabIndex = 1;
			this.buttonSavePassword.Text = "Сохранить";
			this.buttonSavePassword.UseVisualStyleBackColor = true;
			this.buttonSavePassword.Click += new System.EventHandler(this.buttonSavePassword_Click);
			// 
			// labelInform
			// 
			this.labelInform.AutoSize = true;
			this.labelInform.Location = new System.Drawing.Point(43, 9);
			this.labelInform.Name = "labelInform";
			this.labelInform.Size = new System.Drawing.Size(159, 13);
			this.labelInform.TabIndex = 2;
			this.labelInform.Text = "Введите новый пароль в поле";
			// 
			// FChangePasswordDU
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(239, 122);
			this.Controls.Add(this.labelInform);
			this.Controls.Add(this.buttonSavePassword);
			this.Controls.Add(this.EditPasswordBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FChangePasswordDU";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Изменение пароля пульта ДУ";
			this.Shown += new System.EventHandler(this.FChangePasswordDU_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox EditPasswordBox;
		private System.Windows.Forms.Button buttonSavePassword;
		private System.Windows.Forms.Label labelInform;
	}
}