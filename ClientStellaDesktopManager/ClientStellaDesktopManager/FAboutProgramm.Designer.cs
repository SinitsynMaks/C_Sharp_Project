namespace ClientStellaDesktopManager
{
	partial class FAboutProgramm
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
			this.labelinfo = new System.Windows.Forms.Label();
			this.Secretlabel = new System.Windows.Forms.Label();
			this.AllrightsReservedelabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelinfo
			// 
			this.labelinfo.AutoSize = true;
			this.labelinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelinfo.Location = new System.Drawing.Point(56, 9);
			this.labelinfo.Name = "labelinfo";
			this.labelinfo.Size = new System.Drawing.Size(213, 80);
			this.labelinfo.TabIndex = 0;
			this.labelinfo.Text = "Программа\r\nдля отображения\r\nинформации о ценах\r\nна светодиодных табло";
			this.labelinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Secretlabel
			// 
			this.Secretlabel.AutoSize = true;
			this.Secretlabel.Location = new System.Drawing.Point(66, 132);
			this.Secretlabel.Name = "Secretlabel";
			this.Secretlabel.Size = new System.Drawing.Size(20, 13);
			this.Secretlabel.TabIndex = 1;
			this.Secretlabel.Text = "(C)";
			this.Secretlabel.DoubleClick += new System.EventHandler(this.Secretlabel_DoubleClick);
			// 
			// AllrightsReservedelabel
			// 
			this.AllrightsReservedelabel.AutoSize = true;
			this.AllrightsReservedelabel.Location = new System.Drawing.Point(81, 132);
			this.AllrightsReservedelabel.Name = "AllrightsReservedelabel";
			this.AllrightsReservedelabel.Size = new System.Drawing.Size(124, 13);
			this.AllrightsReservedelabel.TabIndex = 2;
			this.AllrightsReservedelabel.Text = "Все права защищены, ";
			// 
			// FAboutProgramm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 152);
			this.Controls.Add(this.AllrightsReservedelabel);
			this.Controls.Add(this.Secretlabel);
			this.Controls.Add(this.labelinfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FAboutProgramm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "О программе";
			this.Shown += new System.EventHandler(this.FAboutProgramm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelinfo;
		private System.Windows.Forms.Label Secretlabel;
		private System.Windows.Forms.Label AllrightsReservedelabel;
	}
}