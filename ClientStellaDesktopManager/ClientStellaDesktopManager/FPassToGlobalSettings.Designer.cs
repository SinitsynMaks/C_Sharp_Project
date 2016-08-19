namespace ClientStellaDesktopManager
{
	partial class FPassToGlobalSettings
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
			this.textBoxEnterPAssword = new System.Windows.Forms.TextBox();
			this.buttonEnterPassword = new System.Windows.Forms.Button();
			this.buttonChangePassword = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxEnterPAssword
			// 
			this.textBoxEnterPAssword.Location = new System.Drawing.Point(12, 12);
			this.textBoxEnterPAssword.Name = "textBoxEnterPAssword";
			this.textBoxEnterPAssword.Size = new System.Drawing.Size(181, 20);
			this.textBoxEnterPAssword.TabIndex = 0;
			// 
			// buttonEnterPassword
			// 
			this.buttonEnterPassword.Location = new System.Drawing.Point(12, 38);
			this.buttonEnterPassword.Name = "buttonEnterPassword";
			this.buttonEnterPassword.Size = new System.Drawing.Size(81, 39);
			this.buttonEnterPassword.TabIndex = 1;
			this.buttonEnterPassword.Text = "OK";
			this.buttonEnterPassword.UseVisualStyleBackColor = true;
			this.buttonEnterPassword.Click += new System.EventHandler(this.buttonEnterPassword_Click);
			// 
			// buttonChangePassword
			// 
			this.buttonChangePassword.Location = new System.Drawing.Point(99, 38);
			this.buttonChangePassword.Name = "buttonChangePassword";
			this.buttonChangePassword.Size = new System.Drawing.Size(94, 39);
			this.buttonChangePassword.TabIndex = 2;
			this.buttonChangePassword.Text = "Сменить\r\nпароль";
			this.buttonChangePassword.UseVisualStyleBackColor = true;
			// 
			// FPassToGlobalSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(213, 84);
			this.Controls.Add(this.buttonChangePassword);
			this.Controls.Add(this.buttonEnterPassword);
			this.Controls.Add(this.textBoxEnterPAssword);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FPassToGlobalSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Введи пароль";
			this.Shown += new System.EventHandler(this.FPassToGlobalSettings_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxEnterPAssword;
		private System.Windows.Forms.Button buttonEnterPassword;
		private System.Windows.Forms.Button buttonChangePassword;
	}
}