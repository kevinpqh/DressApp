using System.Windows;

namespace DressApp.View.Buttons.Events
{

    //Maneja el cursor de la mano

    public class ButtonsManager
    {
        #region Variables
    
        //Instancia HandCursorManager
    
        private static ButtonsManager _instance;
    
        //singleton
    
        private static bool _isInitialized;
    
        //ultimo elemento click por cursor
    
        private IInputElement _lastElement;
        #endregion Variables
        #region Properties
    
        //manager
    
        public static ButtonsManager Instance
        {
            get
            {
                if (!_isInitialized)
                    _instance = Initialize();
                return _instance;
            }
        }
        #endregion Properties
        #region .ctor
    
        //costructor de ButtonsManager
    
        private static ButtonsManager Initialize()
        {
            _isInitialized = true;
            return new ButtonsManager();
        }
        #endregion .ctor
        #region Methods

        //Levanta los eventos del cursor.

        public void RaiseCursorEvents(IInputElement element, Point cursorPosition)
        {
            element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorMoveEvent, cursorPosition));
            if (element != _lastElement)
            {
                if (_lastElement != null)
                    _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
                element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorEnterEvent, cursorPosition));
            }
            _lastElement = element;
        }

        //deja evento de salida del cursor

        public void RaiseCursorLeaveEvent(Point cursorPosition)
        {
            if (_lastElement == null) return;
            _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
            _lastElement = null;
        }
        #endregion Methods
    }
}
