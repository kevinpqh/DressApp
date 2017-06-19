using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearItemsButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de instancia
        public ClearItemsButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion

        #region Methodos
        // muestra el boton de limpiar
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
            TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.ClearButtons;
        }
        #endregion
    }
}
