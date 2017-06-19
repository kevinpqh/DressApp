using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangePositionButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de instancia
        public ChangePositionButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methodos
        //muestra el boton
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
            TopMenuManager.Instance.ChangeSizePositionButtons = TopMenuManager.Instance.ChangePositionButtons;
            TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Visible;
        }
        #endregion
    }
}
