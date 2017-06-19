using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    public sealed class TopMenuManager : ViewModelBase
    {
        #region Atributos Privados
        // Instancia de TopMenuManager
        private static TopMenuManager _instance;
        
        /// Informacion del Sonido
        private bool _soundsOn;
         
        /// botones actuales del menu superior
        private ObservableCollection<TopMenuButtonViewModel> _actualTopMenuButtons;
         
        // Todo los botones del menu
        private ObservableCollection<TopMenuButtonViewModel> _allButtons;
         
        // Cambio del tamaño del boton
        private ObservableCollection<TopMenuButtonViewModel> _changeSizeButtons;

        // Cambia la posicion del boton
        private ObservableCollection<TopMenuButtonViewModel> _changePositionButtons;

        // Cambia la posicion del boton
        private ObservableCollection<TopMenuButtonViewModel> _changeSizePositionButtons;
         
        // Limpiar la vista de botones
        private ObservableCollection<TopMenuButtonViewModel> _clearButtons;
         
        // Muestra la buton de la camara
        private Visibility _cameraButtonVisibility;
         
        // Muestra el tramaño y la posion de los botones
        private Visibility _sizePositionButtonsVisibility;
         
        /// vista de CloseAppGrid
        private Visibility _closeAppGridVisibility;
        #endregion

        #region Propiedades Publicas

        //Metodo de acceso a una sola instancia
        public static TopMenuManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TopMenuManager();
                return _instance;
            }
        }

        //Get actual de los botones en el menu top
        public ObservableCollection<TopMenuButtonViewModel> ActualTopMenuButtons
        {
            get { return _actualTopMenuButtons; }
            set
            {
                if (_actualTopMenuButtons == value)
                    return;
                _actualTopMenuButtons = value;
                OnPropertyChanged("ActualTopMenuButtons");
            }
        }

        //Get todos los botones del menu top
        public ObservableCollection<TopMenuButtonViewModel> AllButtons
        {
            get { return _allButtons; }
        }

        //Gets o Sets del boton principal del menu
        public MenuButtonViewModel MenuButton
        {
            get;
            private set;
        }
        //Gets cambio de tamaño de boton
        public ObservableCollection<TopMenuButtonViewModel> ChangeSizeButtons
        {
            get { return _changeSizeButtons; }
        }

        // Get del cambio de posicion del boton
        public ObservableCollection<TopMenuButtonViewModel> ChangePositionButtons
        {
            get { return _changePositionButtons; }
        }

        //Get del cambio de posicion del boton
        public ObservableCollection<TopMenuButtonViewModel> ChangeSizePositionButtons
        {
            get { return _changeSizePositionButtons; }
            set
            {
                if (_changeSizePositionButtons == value)
                    return;
                _changeSizePositionButtons = value;
                OnPropertyChanged("ChangeSizePositionButtons");
            }
        }

        //Get limpiar botones
        public ObservableCollection<TopMenuButtonViewModel> ClearButtons
        {
            get { return _clearButtons; }
        }

        //Get o Set del boton de camara
        public ScreenshotButtonViewModel CameraButton
        {
            get;
            private set;
        }

        // Gets o sets de la visibility del button camara 
        public Visibility CameraButtonVisibility
        {
            get { return _cameraButtonVisibility; }
            set
            {
                if (_cameraButtonVisibility == value)
                    return;
                _cameraButtonVisibility = value;
                OnPropertyChanged("CameraButtonVisibility");
            }
        }
         
        // Gets o sets de la visibility del tamaño y la posicion del boton
        public Visibility SizePositionButtonsVisibility
        {
            get { return _sizePositionButtonsVisibility; }
            set
            {
                if (_sizePositionButtonsVisibility == value)
                    return;
                _sizePositionButtonsVisibility = value;
                OnPropertyChanged("SizePositionButtonsVisibility");
            }
        }
         
        // Gets o sets informacion del sonido
        public bool SoundsOn
        {
            get { return _soundsOn; }
            set
            {
                if (_soundsOn == value)
                    return;
                _soundsOn = value;
                OnPropertyChanged("SoundsOn");
            }
        }
         
        // Gets o sets boton cerrar
        public NoCloseButtonViewModel NoCloseButton
        {
            get;
            private set;
        }
         
        // Gets o sets boton de confirmacion sobre la cancelacion
        public YesCloseButtonViewModel YesCloseButton
        {
            get;
            private set;
        }
         
        // Gets o sets de visibility de CloseAppGrid
        public Visibility CloseAppGridVisibility
        {
            get { return _closeAppGridVisibility; }
            set
            {
                if (_closeAppGridVisibility == value)
                    return;
                _closeAppGridVisibility = value;
                OnPropertyChanged("CloseAppGridVisibility");
            }
        }

        #endregion

        #region Constructor
        private TopMenuManager()
        {
            InitializeTopMenuButtons();
            SoundsOn = true;
            CreateCloseButtons();
        }
        #endregion

        #region Metodos Privados
         
        // inicializa los botones del menu
        private void InitializeTopMenuButtons()
        {
            CreateAllTopMenuButtons();
            CreateChangePositionButtons();
            CreateChangeSizeButtons();
            CreateClearButtons();
            CameraButtonVisibility = Visibility.Collapsed;
            SizePositionButtonsVisibility = Visibility.Collapsed;
        }
        
        // Crea botones basicos de menu top
        private void CreateAllTopMenuButtons()
        {
            MenuButton = new MenuButtonViewModel(Properties.Resources.menu);
            CameraButton = new ScreenshotButtonViewModel(Properties.Resources.menu_camera);
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ChangeTypeButtonViewModel(Properties.Resources.menu_menWomen),
                new ChangeSizeButtonViewModel(Properties.Resources.menu_arrows),
                new ChangePositionButtonViewModel(Properties.Resources.arrows_up_down),
                new ClearItemsButtonViewModel(Properties.Resources.menu_clear),
                new SoundsButtonViewModel(Properties.Resources.menu_speaker),
                new ExitButtonViewModel(Properties.Resources.menu_doors)
            };
        }
        
        // Crea botones de posición de cambio
        private void CreateChangePositionButtons()
        {
            _changePositionButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MoveUpButtonViewModel(Properties.Resources.move_up),
                new MoveDownButtonViewModel(Properties.Resources.move_down)
            };
        }
        
        // Crea posiciones de tamaño de cambio
        private void CreateChangeSizeButtons()
        {
            _changeSizeButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MakeBiggerButtonViewModel(Properties.Resources.vertical_arrows),
                new MakeSmallerButtonViewModel(Properties.Resources.vertical_arrows_smaller),
                new MakeThinnerButtonViewModel(Properties.Resources.horizontal_arrows_thinner),
                new MakeWiderButtonViewModel(Properties.Resources.horizontal_arrows),
                new ScalePlusButtonViewModel(Properties.Resources.arrows_bigger),
                new ScaleMinusButtonViewModel(Properties.Resources.arrows_smaller)
            };
        }
        
        // Crea los botones limpiar
        private void CreateClearButtons()
        {
            _clearButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ClearLastItemButtonViewModel(Properties.Resources.menu_clearOne),
                new ClearSetButtonViewModel(Properties.Resources.menu_clearSet)
            };
        }
        /// <summary>
        /// Creates cancel and confirm closing buttons
        /// </summary>
        /// 
        // Crea botones de cancelación y confirmación
        private void CreateCloseButtons()
        {
            NoCloseButton = new NoCloseButtonViewModel(Properties.Resources.no);
            YesCloseButton = new YesCloseButtonViewModel(Properties.Resources.yes);
            CloseAppGridVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
