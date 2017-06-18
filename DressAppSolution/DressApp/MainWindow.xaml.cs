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

namespace DressApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {



        #region Public Properties


        public Point LeftPosition
        {
            get { return (Point)GetValue(LeftPositionProperty); }
            set { SetValue(LeftPositionProperty, value); }
        }

        public Point RightPosition
        {
            get { return (Point)GetValue(RightPositionProperty); }
            set { SetValue(RightPositionProperty, value); }
        }
        #endregion Public Properties

        #region Dependency Properties

        public static readonly DependencyProperty RightPositionProperty =
            DependencyProperty.Register("RightPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));

        public static readonly DependencyProperty LeftPositionProperty =
            DependencyProperty.RegisterAttached("LeftPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));
        #endregion Dependency Properties

        #region .ctor


        public MainWindow()
        {
            InitializeComponent();
            Loaded += ((sender, e) => ClothesArea.SetTransformMatrix());
        }
        #endregion .ctor

        #region Private Methods

        private static void Hand_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow window = d as MainWindow;
            if (window != null) { }
            //window.HandleHandMoved(window.LeftPosition, window.RightPosition);
        }

        /*private void HandleHandMoved(Point leftHand, Point rightHand)
        {
            HandCursor.Visibility = Visibility.Collapsed;

            var element = (CloseAppGrid.Visibility == Visibility.Visible) ? CloseAppGrid.InputHitTest(leftHand) : ButtonPanelsCanvas.InputHitTest(leftHand);
            var hand = leftHand;

            if (!(element is UIElement))
            {
                element = (CloseAppGrid.Visibility == Visibility.Visible) ? CloseAppGrid.InputHitTest(rightHand) : ButtonPanelsCanvas.InputHitTest(rightHand);
                hand = rightHand;
                if (!(element is UIElement))
                {
                    ButtonsManager.Instance.RaiseCursorLeaveEvent(leftHand);
                    return;
                }
            }

            HandCursor.Visibility = Visibility.Visible;
            Canvas.SetLeft(HandCursor, hand.X - HandCursor.ActualWidth / 2.0);
            Canvas.SetTop(HandCursor, hand.Y - HandCursor.ActualHeight / 2.0);
            ButtonsManager.Instance.RaiseCursorEvents(element, hand);
        }*/
        #endregion Private Methods


    }
}
