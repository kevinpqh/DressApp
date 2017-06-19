using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeSizeButtonViewModel : TopMenuButtonViewModel
    {
        #region Contructor
        //Inicializamos una istancia
        public ChangeSizeButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion

        #region Methodos
        
        //muestra el boton de cambio de tamaño del item
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
            TopMenuManager.Instance.ChangeSizePositionButtons = TopMenuManager.Instance.ChangeSizeButtons;
            TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Visible;
        }
        #endregion
    }
}
