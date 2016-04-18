namespace SabstrN
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_cut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.input_textBox = new System.Windows.Forms.TextBox();
            this.output_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_cut
            // 
            this.btn_cut.Location = new System.Drawing.Point(13, 58);
            this.btn_cut.Name = "btn_cut";
            this.btn_cut.Size = new System.Drawing.Size(58, 23);
            this.btn_cut.TabIndex = 2;
            this.btn_cut.Text = "Cut";
            this.btn_cut.UseVisualStyleBackColor = true;
            this.btn_cut.Click += new System.EventHandler(this.btn_cut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Length";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(179, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 4;
            // 
            // input_textBox
            // 
            this.input_textBox.Location = new System.Drawing.Point(13, 23);
            this.input_textBox.Name = "input_textBox";
            this.input_textBox.Size = new System.Drawing.Size(230, 20);
            this.input_textBox.TabIndex = 5;
            // 
            // output_textBox
            // 
            this.output_textBox.Location = new System.Drawing.Point(13, 102);
            this.output_textBox.Name = "output_textBox";
            this.output_textBox.Size = new System.Drawing.Size(230, 20);
            this.output_textBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 152);
            this.Controls.Add(this.output_textBox);
            this.Controls.Add(this.input_textBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cut);
            this.Name = "MainForm";
            this.Text = "WordCut";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox input_textBox;
        private System.Windows.Forms.TextBox output_textBox;
    }
}

