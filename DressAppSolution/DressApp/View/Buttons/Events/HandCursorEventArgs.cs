using System.Windows;

namespace KinectFittingRoom.View.Buttons.Events
{

    //manejo de Cursores de Manos

    public class HandCursorEventArgs : RoutedEventArgs
    {
        #region Public Properties
        
        // X 
        
        public double X { get; set; }
        
        // Y 
        
        public double Y { get; set; }
        #endregion Public Properties
        #region .ctor
        
        //constructor HandCursorEventArgs
        
        public HandCursorEventArgs(RoutedEvent routedEvent, double x, double y)
            : base(routedEvent)
        {
            X = x;
            Y = y;
        }

        //constructor  sobrecarga HandCursorEventArgs

        public HandCursorEventArgs(RoutedEvent routedEvent, Point point)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
        }
        #endregion .ctor
    }
}
