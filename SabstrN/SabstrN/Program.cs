using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace SabstrN
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new MainForm());
           List<String> list = new List<string>(8) { "", "", "", ""};
           int i = 0;
           foreach (String s in list)
           {
               i++;
               if ((i % 2) == 0)
                   list.Add("");
           }
           MessageBox.Show(i.ToString());
        }
    }
}
