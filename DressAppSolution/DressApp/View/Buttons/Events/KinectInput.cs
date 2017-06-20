using System.Windows;

namespace DressApp.View.Buttons.Events
{

    // Declaracion de controlador de eventos para HandCursor 

    public delegate void HandCursorEventHandler(object sender, HandCursorEventArgs args);

    // Maneja la entrada de Kinect

    public static class KinectInput
    {
        #region  Events

        // cursor de mano introduce evento

        public static readonly RoutedEvent HandCursorEnterEvent
            = EventManager.RegisterRoutedEvent("HandCursorEnter", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectInput));

        // movimiento del cursor

        public static readonly RoutedEvent HandCursorMoveEvent
            = EventManager.RegisterRoutedEvent("HandCursorMove", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectInput));

        // cursor de mano deja el evento

        public static readonly RoutedEvent HandCursorLeaveEvent
            = EventManager.RegisterRoutedEvent("HandCursorLeave", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectInput));

        // Click del cursor 

        public static readonly RoutedEvent HandCursorClickEvent
            = EventManager.RegisterRoutedEvent("HandCursorClick", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectInput));
        #endregion Events
        #region Methods

        // Agrega a cursor evento de manipular

        public static void AddHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorEnterEvent, handler);
        }

        // Remover a cursor evento de manipula

        public static void RemoveHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorEnterEvent, handler);
        }

        // Añade evento mover

        public static void AddHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorMoveEvent, handler);
        }

        // Remover evento mover

        public static void RemoveHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorMoveEvent, handler);
        }

        // Agregar evento dejar

        public static void AddHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorLeaveEvent, handler);
        }
        
        // Removes evento dejar

        public static void RemoveHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorLeaveEvent, handler);
        }

        // Agregar evento click

        public static void AddHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorClickEvent, handler);
        }
        
        // Remove evento click

        public static void RemoveHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorClickEvent, handler);
        }
        #endregion Methods
    }
}
