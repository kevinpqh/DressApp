using DressApp.View.Buttons.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DressApp.View.Buttons
{
 
    public class KinectButton : Button
    {
        #region Constants
        // segundos de evneto Click 

        private const int ClickTimeout = 40;

        //segundos despues de Click 

        private const int AfterClickTimeout = 10;
        #endregion Constants
        #region Private Fields

        // Determina si la mano esta sobre boton

        private bool _handIsOverButton;

        // ultima posicion de mano

        private Point _lastHandPosition;
        #endregion Private Fields
        #region Events

        // evento entrer

        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(KinectButton));

        // evento moverse

        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(KinectButton));

        // evento moverse

        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectEvents.HandCursorLeaveEvent.AddOwner(typeof(KinectButton));

        // evento click 

        public static readonly RoutedEvent HandCursorClickEvent
            = KinectEvents.HandCursorClickEvent.AddOwner(typeof(KinectButton));
        #endregion Events
        #region Event handlers

        // enter evento manipular

        public event HandCursorEventHandler HandCursorEnter
        {
            add { AddHandler(HandCursorEnterEvent, value); }
            remove { RemoveHandler(HandCursorEnterEvent, value); }
        }

        // moverse evento manipular

        public event HandCursorEventHandler HandCursorMove
        {
            add { AddHandler(HandCursorMoveEvent, value); }
            remove { RemoveHandler(HandCursorMoveEvent, value); }
        }

        // Hmanipular evento dejar

        public event HandCursorEventHandler HandCursorLeave
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }

        // manipular eventeo click

        public event HandCursorEventHandler HandCursorClick
        {
            add { AddHandler(HandCursorClickEvent, value); }
            remove { RemoveHandler(HandCursorClickEvent, value); }
        }
        #endregion Event handlers
        #region Properties

        // Get si cursos esta sobre boton

        public bool HandIsOverButton
        {
            get { return _handIsOverButton; }
        }

        // get si ocurrio click

        public bool IsClicked
        {
            get { return (bool)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }

        // informacion de Sonido.

        public bool AreSoundsOn
        {
            get { return (bool)GetValue(AreSoundsOnProperty); }
            set { SetValue(AreSoundsOnProperty, value); }
        }

        // Gets y sets comando para invocar cuando se presiona este botón
        public new ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // tiempo trancurrido _clickTimer

        protected int ClickTicks { get; set; }

        // tiempo trancurrido _afterClickTimer

        protected int AfterClickTicks { get; set; }

        // Determina cuanto tiempo desde que ocurrio el evento HandCursorEnterEvent 

        protected DispatcherTimer ClickTimer { get; private set; }

        // Determina cuanto tiempo desde que ocurrio el evento HandCursorClickEvent

        protected DispatcherTimer AfterClickTimer { get; private set; }
        #endregion Properties
        #region Dependency Properties

        // propiedades de IsClicked 

        public static readonly DependencyProperty IsClickedProperty = DependencyProperty.Register(
            "IsClicked", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));

        // propiedades de  AreSoundsOn

        public static readonly DependencyProperty AreSoundsOnProperty = DependencyProperty.Register(
            "AreSoundsOn", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));
        #endregion Dependency Properties
        #region .ctor

        // cosntructor de KinectButton

        public KinectButton()
        {
            SetValue(IsClickedProperty, false);
            _handIsOverButton = false;
            ClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            ClickTicks = 0;
            ClickTimer.Tick += ClickTimer_Tick;
            AfterClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            AfterClickTicks = 0;
            AfterClickTimer.Tick += AfterClickTimer_Tick;

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
            HandCursorClick += KinectButton_HandCursorClick;
        }
        #endregion .ctor
        #region Methods

        // Manipular evento de  HandCursorEnter 

        protected void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = true;
            ClickTimer.Start();
        }

        // Manipualr evneto de HandCursorMove 

        protected void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            _lastHandPosition = new Point(args.X, args.Y);
        }

        // Manipular evento de HandCursorLeave 

        protected void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = false;
            if (IsClicked)
                SetValue(IsClickedProperty, false);
            ResetTimer(ClickTimer);
        }

        // contador de of_clickTimer

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            ClickTicks++;

            if (ClickTicks <= ClickTimeout)
                return;

            ResetTimer(ClickTimer);
            RaiseEvent(new HandCursorEventArgs(HandCursorClickEvent, _lastHandPosition));
        }

        // contador de _afterClickTimer

        private void AfterClickTimer_Tick(object sender, EventArgs e)
        {
            AfterClickTicks++;

            if (AfterClickTicks <= AfterClickTimeout)
                return;

            ResetTimer(AfterClickTimer);
            SetValue(IsClickedProperty, false);
        }

        // Imitar click 

        protected virtual void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);
            AfterClickTimer.Start();
        }

        // Resets contadores 

        protected virtual void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
                AfterClickTicks = 0;
        }
        #endregion Methods
    }
}
