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
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Resources;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        //El boton Hat_Click esta deshabilitado
        private void Hat_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void T_shirt_Click(object sender, RoutedEventArgs e)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Luma\DressApp\DressAppSolution\DressApp\Resources\Materials\short_skirt_checked.jpg"));
            Button1.Background = brush;

            List<string> prendas = new List<string>();
        }

        private void Blouse_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Dresses_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Skirt_Click(object sender, RoutedEventArgs e)
        {
           


        }

        private void Bags_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Botones del lado derecho a manipular
       

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
