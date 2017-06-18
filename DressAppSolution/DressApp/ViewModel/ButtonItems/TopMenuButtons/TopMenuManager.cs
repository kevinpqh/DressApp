using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
    }
}
