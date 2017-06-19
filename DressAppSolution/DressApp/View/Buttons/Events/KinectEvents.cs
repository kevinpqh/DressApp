using System.Windows;

namespace KinectFittingRoom.View.Buttons.Events
{
    /// <summary>
    /// Handles the Kinect input
    /// </summary>
    public static class KinectEvents
    {
        #region  Events
        // Enter
        public static readonly RoutedEvent HandCursorEnterEvent
            = EventManager.RegisterRoutedEvent("HandCursorEnter", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
        
        // Mover

        public static readonly RoutedEvent HandCursorMoveEvent
            = EventManager.RegisterRoutedEvent("HandCursorMove", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));

        // Dejar evento

        public static readonly RoutedEvent HandCursorLeaveEvent
            = EventManager.RegisterRoutedEvent("HandCursorLeave", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));

        // click

        public static readonly RoutedEvent HandCursorClickEvent
            = EventManager.RegisterRoutedEvent("HandCursorClick", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));

        // Limpiar

        public static readonly RoutedEvent Clear3DItemsEvent
            = EventManager.RegisterRoutedEvent("Clear3DItems", RoutingStrategy.Direct
            , typeof(RoutedEventHandler), typeof(KinectEvents));
        #endregion Events
        #region Methods

        /// Agregar evento enter

        public static void AddHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorEnterEvent, handler);
        }

        /// Remover evento entert

        public static void RemoveHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorEnterEvent, handler);
        }

        /// Agregar evento moverse 

        public static void AddHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorMoveEvent, handler);
        }

        /// Remove evento moverse

        public static void RemoveHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorMoveEvent, handler);
        }

        /// Agregar evento dejar

        public static void AddHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorLeaveEvent, handler);
        }
        /// Remover evento dejar

        public static void RemoveHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorLeaveEvent, handler);
        }

        /// Agregar evento click

        public static void AddHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorClickEvent, handler);
        }

        /// Remover evento click

        public static void RemoveHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorClickEvent, handler);
        }

        /// Agregar evento clear3D 

        public static void AddClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(Clear3DItemsEvent, handler);
        }

        /// Remover evento clear3D 

        public static void RemoveClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(Clear3DItemsEvent, handler);
        }
        #endregion Methods
    }
}
