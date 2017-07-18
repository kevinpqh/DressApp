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
            ImageBrush back = new ImageBrush();
            back.ImageSource = LoadImage(@"Resources\background.jpg");
            gridBackground.Background = back;
        }
        private BitmapImage LoadImage(string myImageFile)
        {
            BitmapImage myRetVal = null;
            if (myImageFile != null)
            {
                BitmapImage image = new BitmapImage();
                using (System.IO.FileStream stream = System.IO.File.OpenRead(myImageFile))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                myRetVal = image;
            }
            return myRetVal;
        }


        private void Dress_Click(object sender, RoutedEventArgs e)
        {
            //DressApp.App vare = new DressApp.App();
            //vare.InitializeComponent();
            //DressApp.App vare = new DressApp.App();

            //DressApp.MainWindow DressWindow = new DressApp.MainWindow();
            ////DressWindow.SetResourceReference(new DressApp.ViewModel.KinectViewModelLoader());
            //DressWindow.DataContext = new DressApp.ViewModel.KinectViewModelLoader();
            //DressWindow.Show();
            //          vare.Run();
            Close();

            Process.Start("DressApp.exe");
            
        }

        
        private void Management_Click(object sender, RoutedEventArgs e)
        {
            Window1 managementWindow = new Window1();
            managementWindow.Show();
            Close();

        }
    }
}
