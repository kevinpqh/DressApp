using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DressApp.View.Buttons.Events;

namespace DressApp.View.Helpers
{
    // Clase ItemsControl que responde a los eventos de Kinect

    public class ScrollableCanvas : ItemsControl
    {
        #region Constants

        // Número de segundos para verificar la posición de la mano

        private const int EnterTimeout = 4;

        // Movimiento De controles en paneles

        private const int Distance = 10;

        // Tiempo de animacion milisegundos

        private const int TimeOfAnimation = 10;

        // El factor de altura mínimo

        private const double MinHeightFactor = 0.2;

        // El factor de altura máxima

        private const double MaxHeightFactor = 0.4;
        #endregion
        #region Fields

        // Posición de LeftPanel(Panel izquierdo)

        private Point _leftPanelPosition;

        // Determina la cantidad de tiempo transcurrido desde la posición de la mano sobre el canvas.

        private readonly DispatcherTimer _enterTimer;

        // Número de señales que han transcurrido para _enterTimer

        private int _enterTimerTicks;

        // Determina si la mano está sobre el canvas

        private bool _isHandOverCanvas;

        // Posición real de la mano

        private Point _handPosition;

        // Posición del último botón en el panel

        double _lastButtonPositionY;

        // Posición del primer botón en el panel

        double _firstButtonPositionY;

        // Punto de inicio de la animación

        double _startAnimationPoint;

        // Define si los botones se mueven

        private bool _isMoved;

        // Límite superior para iniciar el desplazamiento hacia arriba

        private double _canvasMinHeight;

        // Límite inferior para empezar a desplazarse hacia abajo

        private double _canvasMaxHeight;
        #endregion
        #region Events

        // cursor de la mano introduce el evento

        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(ScrollableCanvas));

        // cursor de la mano deja el evento

        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectEvents.HandCursorLeaveEvent.AddOwner(typeof(ScrollableCanvas));

        // Movimiento de cursor de mano

        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(ScrollableCanvas));
        #endregion
        #region Event handlers

        // El cursor de mano introduce el controlador de eventos

        public event HandCursorEventHandler HandCursorEnter
        {
            add { AddHandler(HandCursorEnterEvent, value); }
            remove { RemoveHandler(HandCursorEnterEvent, value); }
        }

        //  cursor de mano deja de controlador de eventos

        public event HandCursorEventHandler HandCursorLeave
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }

        // cursor de mano mueve el controlador de eventos

        public event HandCursorEventHandler HandCursorMove
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }
        #endregion Event handlers
        #region .ctor

        // Inicializa una nueva instancia de la clase ScrollableCanvas

        public ScrollableCanvas()
        {
            HandCursorEnter += ScrollableCanvas_HandCursorEnter;
            HandCursorLeave += ScrollableCanvas_HandCursorLeave;
            HandCursorMove += ScrollableCanvas_HandCursorMove;

            _enterTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            _enterTimerTicks = 0;
            _enterTimer.Tick += EnterTimer_Tick;

            Items.CurrentChanged += (sender, args) => { _startAnimationPoint = 0; };
        }
        #endregion
        #region Methods

        // Cuenta el número de segundos del temporizador de_enterTimer

        private void EnterTimer_Tick(object sender, EventArgs e)
        {
            _enterTimerTicks++;

            if (_enterTimerTicks < EnterTimeout)
                return;

            _enterTimer.Stop();
            _enterTimerTicks = 0;
            if (_isHandOverCanvas)
                RaiseEvent(new HandCursorEventArgs(HandCursorEnterEvent, _handPosition));
        }

        // Maneja el evento HandCursorMove

        private void ScrollableCanvas_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (_isHandOverCanvas)
                _handPosition = new Point(args.X, args.Y);
        }

        // Maneja el evento HandCursorLeave

        private void ScrollableCanvas_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _isHandOverCanvas = false;
        }

        // Maneja el evento HandCursorEnter

        private void ScrollableCanvas_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            if (!_isHandOverCanvas)
                _handPosition = new Point(args.X, args.Y);
            _isHandOverCanvas = true;
            _isMoved = true;

            StackPanel stackPanel = (Name == "LeftScrollableCanvas") ? FindChild<StackPanel>(Application.Current.MainWindow, "LeftStackPanel") : FindChild<StackPanel>(Application.Current.MainWindow, "RightStackPanel");
            if (stackPanel.Children.Count == 0)
                return;

            SetPositions(stackPanel);
            if (!CheckHandPosition(stackPanel))
                return;

            if (_isHandOverCanvas)
                _enterTimer.Start();
        }

        // Comprueba la posición de la mano y ejecuta el método MoveButtons

        private bool CheckHandPosition(StackPanel stackPanel)
        {
            if (_handPosition.Y > _canvasMinHeight && _handPosition.Y < _canvasMaxHeight)
                return false;
            if (_handPosition.Y > _canvasMaxHeight)
                while (_isMoved && _lastButtonPositionY + _startAnimationPoint > _canvasMaxHeight)
                    MoveButtons(stackPanel, true);
            else if (_handPosition.Y < _canvasMinHeight)
                while (_isMoved && _firstButtonPositionY + _startAnimationPoint < _firstButtonPositionY)
                    MoveButtons(stackPanel, false);
            return true;
        }

        // Establece las posiciones de los botones primero y último en el panel

        private void SetPositions(StackPanel stackPanel)
        {
            if (_firstButtonPositionY == 0)
                _firstButtonPositionY = stackPanel.Children[0].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;
            _lastButtonPositionY = stackPanel.Children[stackPanel.Children.Count - 1].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;

            if (_leftPanelPosition.X == 0 && _leftPanelPosition.Y == 0)
            {
                _leftPanelPosition = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                _canvasMinHeight = ActualHeight * MinHeightFactor + _leftPanelPosition.Y;
                _canvasMaxHeight = ActualHeight * MaxHeightFactor + _leftPanelPosition.Y;
            }
        }

        // Mueve los botones de los paneles

        private void MoveButtons(StackPanel stackpanel, bool moveUp)
        {
            _startAnimationPoint = moveUp ? _startAnimationPoint - Distance : _startAnimationPoint + Distance;

            Button button;
            TranslateTransform translation = new TranslateTransform();
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(TimeOfAnimation),
                From = moveUp ? _startAnimationPoint + Distance : _startAnimationPoint,
                To = moveUp ? _startAnimationPoint : _startAnimationPoint + Distance
            };

            foreach (var control in stackpanel.Children)
            {
                button = FindChild<Button>(control as ContentPresenter, "");
                if (button != null)
                    button.RenderTransform = translation;
            }

            translation.BeginAnimation(TranslateTransform.YProperty, animation);
            _isMoved = !_isMoved;
        }

        // Buscar control de hijos en VisualTreeHelper en ParentControl

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    T foundChild = FindChild<T>(child, childName);
                    if (foundChild != null)
                        return foundChild;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                        return (T)child;
                }
                else
                    return (T)child;
            }
            return null;
        }
        #endregion
    }
}
