using System.Windows;

namespace DressApp.View.Buttons.Events
{

    //manejo de Cursores de Manos

    public class HandCursorEventArgs : RoutedEventArgs
    {
        #region prpiedades publicas
        
        // X 
        
        public double X { get; set; }
        
        // Y 
        
        public double Y { get; set; }
        #endregion

        #region Contructor
        
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
