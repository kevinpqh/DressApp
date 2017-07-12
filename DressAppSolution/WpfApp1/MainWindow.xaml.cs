using HelixToolkit.Wpf;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        // private const string MODEL_PATH = @"C:\Users\kevin\Documents\DressApp\DressApp\DressAppSolution\DressApp\Resources\Models\Dresses\dress_beige.obj";
        private const string MODEL_PATH = @"C:\Users\kevin\Documents\DressApp\DressApp\DressAppSolution\DressApp\Resources\Models\Tops\big_top_dark.obj";

        Model3D device;
        public Window1()
        {
            InitializeComponent();
            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(MODEL_PATH);
            // Add to view port

            //viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort3d.Children.Add(device3D);
        }
        private Model3D Display3d(string model)
        {
            device = null;
            try
            {
                //Adding a gesture here
                //viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);

                var axis = new Vector3D(0, 0, 1);
                var angle = 173;

                var matrix = device.Transform.Value;
                matrix.Rotate(new Quaternion(axis, angle));

                device.Transform = new MatrixTransform3D(matrix);

                var axis1 = new Vector3D(1, 0, 0);
                var angle1 = -25;

                var matrix1 = device.Transform.Value;
                matrix1.Rotate(new Quaternion(axis1, angle1));

                device.Transform = new MatrixTransform3D(matrix1);


            }
            catch (Exception e)
            {
                // Handle exception in case can not find the 3D model file
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
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
