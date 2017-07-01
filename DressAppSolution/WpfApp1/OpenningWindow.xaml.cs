using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

   

        private void Dress_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("C:\\Program Files (x86)\\Notepad++\\notepad++.exe");
            this.Close();
        }

        private void Management_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Program Files (x86)\\Internet Explorer\\iexplore.exe");
            this.Close();

        }
    }
}
