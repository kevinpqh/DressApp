using KinectFittingRoom.View.Buttons.Events;
using System;
using System.Globalization;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{

    /// Screenshot 

    class KinectScreenshotButton : KinectButton
    {
        #region Constants

        /// Timespan(Tiempo muerto)

        private const int Timespan = 3;
        #endregion Constants
        #region Private Fields

        /// screenshot contador

        private readonly DispatcherTimer _screenshotTimer;

        /// Cantidad segundos _screenshotTimer 

        private int _ticks;

        /// Audio de camara.

        private SoundPlayer _cameraPlayer;
        #endregion Private Fields
        #region .ctor

        /// constructor de KinectScreenshotButton

        public KinectScreenshotButton()
            : base()
        {
            _cameraPlayer = new SoundPlayer(Properties.Resources.CameraClick);
            _screenshotTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
            _screenshotTimer.Tick += ScreenshotTimer_Tick;
        }
        #endregion .ctor
        #region Methods

        /// Manipulador de timer _screenshotTimer

        private void ScreenshotTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if (_ticks < Timespan)
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = (Timespan - _ticks) + "...";
            else
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = "Whisky :)";

            if (_ticks > Timespan)
            {
                _screenshotTimer.Stop();
                _ticks = 0;
                (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Collapsed;
                MakeScreenshot();
            }
        }
        
        /// Imita el evento clikc para KinectScreenshotButtun
    
        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);

            (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = "3...";
            _screenshotTimer.Start();

            AfterClickTimer.Start();
        }

        /// hacer screenshot.

        private void MakeScreenshot()
        {
            int actualWidth = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualWidth;
            int actualHeight = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualHeight;
            int emptySpace = (int)(0.5 * (SystemParameters.PrimaryScreenWidth - actualWidth));

            string fileName = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss", CultureInfo.InvariantCulture) + ".png";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "DressApp");
            Directory.CreateDirectory(directoryPath);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth + emptySpace, actualHeight, 96, 96,
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ImageArea);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ClothesArea);
            renderTargetBitmap.Render(CreateWatermarkLayer(actualWidth + emptySpace, actualHeight));
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(new CroppedBitmap(renderTargetBitmap, new Int32Rect(emptySpace, 0, actualWidth, actualHeight))));
            using (Stream fileStream = File.Create(directoryPath + "\\" + fileName))
            {
                pngImage.Save(fileStream);
            }

            if(AreSoundsOn)
                _cameraPlayer.Play();
        }
        ///  watermark en screenshot (Epis mark)

        private Visual CreateWatermarkLayer(int width, int height)
        {
            int margin = 10;
            DrawingVisual visualWatermark = new DrawingVisual();
            BitmapImage image = new BitmapImage(new Uri(@"pack://application:,,,/Resources/watermark.png"));
            Point imageLocation = new Point(width - (image.Width + margin), height - (image.Height + margin));

            using (var drawingContext = visualWatermark.RenderOpen())
            {
                drawingContext.DrawRectangle(null, null, new Rect(0, 0, width, height));
                drawingContext.DrawImage(image, new Rect(imageLocation.X, imageLocation.Y, image.Width, image.Height));
            }
            visualWatermark.Opacity = 0.4;
            return visualWatermark;
        }
        #endregion Methods
    }
}
