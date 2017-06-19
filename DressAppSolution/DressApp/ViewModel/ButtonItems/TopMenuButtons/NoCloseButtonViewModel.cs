using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    public class NoCloseButtonViewModel : TopMenuButtonViewModel
    {
        #region Contructor
        
        //inicializacion de una instancia
        public NoCloseButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Metodos
        
        //Cancelar la acción de cerrar la aplicación
        public override void ClickExecuted()
        {
            PlaySound();
            TopMenuManager.Instance.CloseAppGridVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
