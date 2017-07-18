using HelixToolkit.Wpf;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

using System.Windows.Media.Media3D;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ViewModel3D : Window
    {
        Model3D device;
        //private const string MODEL_PATH = @"C:\Users\kevin\Documents\DressApp\DressApp\DressAppSolution\DressApp\Resources\Models\Tops\big_top_dark.obj";
        ModelVisual3D device3D;
        public ViewModel3D()
        {
            InitializeComponent();


        }
        public ViewModel3D(string path)
        {
            InitializeComponent();

            device3D = new ModelVisual3D();
            device3D.Content = Display3d(path);
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

        bool isDataDirty = false;

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Closing called");

            viewPort3d.Children.Remove(device3D);


            // If data is dirty, notify user and ask for a response
            if (this.isDataDirty)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }
}
