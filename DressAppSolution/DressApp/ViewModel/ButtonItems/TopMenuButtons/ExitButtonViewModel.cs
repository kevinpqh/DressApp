using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ExitButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de instancia
        public ExitButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methodos
 
        // cierra el botones mostrados
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
            TopMenuManager.Instance.CloseAppGridVisibility = Visibility.Visible;
        }
        #endregion
    }
}
