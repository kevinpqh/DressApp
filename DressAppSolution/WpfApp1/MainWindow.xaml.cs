﻿using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        // private const string MODEL_PATH = @"C:\Users\kevin\Documents\DressApp\DressApp\DressAppSolution\DressApp\Resources\Models\Dresses\dress_beige.obj";
        private string currentModel = "";
        private int indexCategory = 0;
        private int indexModel= -1;
        private string newTexture = "";
        private string[] mapModel = { "Tops", "Dresses" , "Skirts" };
        private string[] mapModelTops = { "big_top_colourful",
            "big_top_dark",
            "big_top_green",
            "long_top_blue",
            "long_top_pink",
            "top_blue",
            "tshirt_coral_blue",
            "tshirt_green_blue",
            "tshirt_navy",
            "tshirt_olive"};

        private string[] mapModelSkirt = {"big_skirt_green",
        "big_skirt_waves",
        "long_skirt_bikes",
        "long_skirt_indian",
        "medium_skirt_checked",
        "medium_skirt_denim",
        "medium_skirt_green_stripes",
        "medium_skirt_navy",
        "medium_skirt_red",
        "medium_skirt_violet"};

        private string[] mapModelDresses = {"dress_beige",
        "dress_blue",
        "dress_dark",
        "dress_darkgreen",
        "dress_egipt",
        "dress_green",
        "dress_kitchen",
        "dress_navy",
        "dress_orange",
        "dress_pink"};

        private ImageBrush[] brush = new ImageBrush[10];
        private BitmapImage[] bitImage = new BitmapImage[10];
        private Button[] buttomAdmin = new Button[10];

        Model3D device;

        public WebBrowser WebBrowserbrowser { get; private set; }

        public Window1()
        {
            // Inicializar
            InitializeComponent();

            //Background
            ImageBrush background_style = new ImageBrush();
            background_style.ImageSource = LoadImage(@"Resources\dressapp.jpg");
            background_main.Background = background_style;
            background_main.Background.Opacity = 0.45;

            //Logotipos
            ImageBrush logo1 = new ImageBrush();
           logo1.ImageSource = LoadImage(@"Resources\logo.jpg");
            Logo_1.Background = logo1;


            ImageBrush logo2 = new ImageBrush();
            logo2.ImageSource = LoadImage(@"Resources\logo2.png");
            Logo_2.Background = logo2;
            
            //Categorias de prendas
            ImageBrush hat = new ImageBrush();
            hat.ImageSource = LoadImage(@"Resources\Icons\Categories\hat_2.jpg");
            Hat.Background = hat;

            ImageBrush dress = new ImageBrush();
            dress.ImageSource = LoadImage(@"Resources\Icons\Categories\dress_2.jpg");
            Dresses.Background = dress;

            ImageBrush glass = new ImageBrush();
            glass.ImageSource = LoadImage(@"Resources\Icons\Categories\glasses_2.jpg");
            Glasses.Background = glass;

            ImageBrush t_shirt = new ImageBrush();
            t_shirt.ImageSource = LoadImage(@"Resources\Icons\Categories\polo_2.jpg");
            T_shirt.Background = t_shirt;

            ImageBrush skirt = new ImageBrush();
            skirt.ImageSource = LoadImage(@"Resources\Icons\Categories\skirt_2.jpg");
            Skirt.Background = skirt;

            ImageBrush bags = new ImageBrush();
            bags.ImageSource = LoadImage(@"Resources\Icons\Categories\bag_2.jpg");
            Bags.Background = bags; 
            // Add to button
            buttomAdmin[0] = Button1;
            buttomAdmin[1] = Button2;
            buttomAdmin[2] = Button3;
            buttomAdmin[3] = Button4;
            buttomAdmin[4] = Button5;
            buttomAdmin[5] = Button6;
            buttomAdmin[6] = Button7;
            buttomAdmin[7] = Button8;
            buttomAdmin[8] = Button9;
            buttomAdmin[9] = Button10;


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

        private void EditTexture_Click(object sender, RoutedEventArgs e)
        {
            if (indexModel == -1) {
                MessageBox.Show("Escoger modelo", "Error");
                return;
            }
            TemporalExchange("");
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
                        // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                MessageBox.Show(filename, "Información");
                
                ExchangeTexture(filename);
              
            }

            switch (indexCategory)
            {
                case 0:
                    T_shirt_Click(null, null);
                    break;
                case 1:
                    Dresses_Click(null, null);
                    break;
                case 2:
                    Skirt_Click(null, null);
                    break;
            }
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

        private void ExchangeTexture(String NewTexture)
        {
            //TODO: cambiar txtura ficheros

            GC.Collect();
            GC.WaitForPendingFinalizers();
            System.IO.File.Copy(NewTexture, @"Resources\Materials\" + currentModel+".jpg", true);
        }

        private void TemporalExchange(String Texture)
        {
            //TODO: cambiar txtura ficheros
            brush[indexModel] = new ImageBrush();
            bitImage[indexModel] = LoadImage(@"Resources\watermark.png");
            brush[indexModel].ImageSource = bitImage[indexModel];
            buttomAdmin[indexModel].Background = brush[indexModel];
        }

        //El boton Hat_Click esta deshabilitado
        private void Hat_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void T_shirt_Click(object sender, RoutedEventArgs e)
        {
            indexCategory = 0;
            for (int i = 0; i < 10; i++)
            {
                brush[i] = new ImageBrush();
                bitImage[i] = LoadImage(@"Resources\Materials\" + mapModelTops[i] + ".jpg");
                brush[i].ImageSource = bitImage[i];
                buttomAdmin[i].Background = brush[i];
            }
           
        }

        private void Blouse_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Dresses_Click(object sender, RoutedEventArgs e)
        {
            indexCategory = 1;
            for (int i = 0; i < 10; i++)
            {
                brush[i] = new ImageBrush();
                bitImage[i] = LoadImage(@"Resources\Materials\" + mapModelDresses[i] + ".jpg");
                brush[i].ImageSource = bitImage[i];
                buttomAdmin[i].Background = brush[i];
            }
        }

        private void Skirt_Click(object sender, RoutedEventArgs e)
        {
            indexCategory = 2;
            for (int i = 0; i < 10; i++)
            {
                brush[i] = new ImageBrush();
                bitImage[i] = LoadImage(@"Resources\Materials\" + mapModelSkirt[i] + ".jpg");
                brush[i].ImageSource = bitImage[i];
                buttomAdmin[i].Background = brush[i];
            }

        }

        private void Bags_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Botones del lado derecho a manipular
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            change(0);
            indexModel = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            change(1);
            indexModel = 1;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            change(2);
            indexModel = 2;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            change(3);
            indexModel = 3;
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            change(4);
            indexModel = 4;
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            change(5);
            indexModel = 5;
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            change(6);
            indexModel = 6;
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            change(7);
            indexModel = 7;
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            change(8);
            indexModel = 8;
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            change(9);
            indexModel = 9;
        }

        private void change(int i) {
            if (indexCategory == 0) {
                currentModel = mapModelTops[i];
            }
            if (indexCategory == 1){
                currentModel = mapModelDresses[i];
            }
            if (indexCategory == 2){
                currentModel = mapModelSkirt[i];
            }

        }

        private void ShowModel_Click(object sender, RoutedEventArgs e)
        {
            ViewModel3D view = new ViewModel3D(@"Resources\Models\"+mapModel[indexCategory]+"\\"+currentModel+".obj");
            //view.DataContext = MODEL_PATH;
            view.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

            Process.Start("DressApp.exe");
        }

        private void PreguntaUsuario_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                string path = AppDomain.CurrentDomain.BaseDirectory + @"Resources/Manual de Usuario.pdf";
                Uri pdf = new Uri(path, UriKind.RelativeOrAbsolute);
                process.StartInfo.FileName = pdf.LocalPath;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception error)
            {
                MessageBox.Show("Could not open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
