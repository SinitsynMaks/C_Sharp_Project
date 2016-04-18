using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO; 
using System.Windows.Markup; 

namespace MyXamlPad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // При загрузке главного окна приложения поместить некоторый базовый текст XAML в текстовый блок. 
            if (File.Exists(System.Environment.CurrentDirectory + "\\YourXaml.xaml") ) 
              { 
                 txtXamlData.Text = File.ReadAllText("YourXaml.xaml"); 
              }

            else 
            { 
                txtXamlData.Text = "<Window xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n"
                                    +"xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" 
                                    +" Height =\"400\" Width =\"500\" WindowStartupLocation=\"CenterScreen\">\n" 
                                    +"<StackPanel>\n" 
                                    +"</StackPanel>\n" 
                                    + "</Window>"; 
            } 
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Записать данные из текстового блока в локальный файл *.xaml. 
            File.WriteAllText("YourXaml.xaml", txtXamlData.Text); 
        }

        private void btnViewXaml_Click(object sender, RoutedEventArgs e)
        {
            // Записать данные из текстового блока в локальный файл *.xaml. 
            File.WriteAllText("YourXaml.xaml", txtXamlData.Text);

            // Это окно, к которому будет динамически применяться XAML-раэметка. 
            Window myWindow = null;

            // Открыть локальный файл *.xaml. 
            try
            {
                using (Stream sr = File.Open("YourXaml.xaml", FileMode.Open))
                {
                    // Присоединить XAML-разметку к объекту Window. 
                    myWindow = (Window)XamlReader.Load(sr);

                    // Отобразить диалоговое окно и выполнить очистку. 
                    myWindow.ShowDialog();
                    myWindow.Close();
                    myWindow = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
