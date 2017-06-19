using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    public abstract class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region Constructor
        // inicializa una nueva instancia
        public TopMenuButtonViewModel(Bitmap image)
        {
            Image = image;
        }
        #endregion

        #region Metodos
        //Borra los botones adicionales en el menú superior
        public void ClearMenu()
        {
            TopMenuManager.Instance.ActualTopMenuButtons = null;
            TopMenuManager.Instance.CameraButtonVisibility = Visibility.Collapsed;
            TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
