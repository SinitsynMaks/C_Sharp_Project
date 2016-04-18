using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SabstrN
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MessageBox.Show(truncateStr("adf asdf  dsf", 5));
        }

        private void btn_cut_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (input_textBox.Text == "")
            {
                n = 0;
            }
            else
            {
                n = Convert.ToInt32(textBox1.Text);
            }

            CutStrBeforN(input_textBox.Text, n);
        }

        public void CutStrBeforN(string inputStr, int maxChars)
        {
            var truncatedStr = string.Empty;

            if (maxChars <= 0)
            {
                output_textBox.Text = "";
                return;
            }
	        
	        if (inputStr == null || inputStr.Length < maxChars)
            {
                output_textBox.Text = inputStr;
                return;
            }

            int substrLength = 0;
	        int lastSpaceIndex = inputStr.LastIndexOf(" ", maxChars);

            if (lastSpaceIndex < 0)
            {
                truncatedStr = "";
            }
            else
            {
                substrLength = lastSpaceIndex;
                truncatedStr = inputStr.Substring(0, substrLength).Trim();
            }	        

            output_textBox.Text = truncatedStr;
        }

        public string truncateStr(string input, int maxLength)
        {
            /*string output = input;
            
            if (input.Length > maxLength)
            {
                int index;
                bool separator = false;
                for (index = maxLength; index >= 0; index--)
                    if (Char.IsSeparator(input[index]))
                        separator = true;
                    else if (separator)
                        break;
                int newLength = (index != 0) ? (index + 1) : 0;
                output = input.Substring(0, newLength);
            }
             */
            string output = input;
            if (input.Length > maxLength)
                output = input.Substring(0, input.LastIndexOf(' ', maxLength - 1) + 1).TrimEnd();
            return output;
        }

    }
}
