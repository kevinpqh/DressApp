using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    public class YesCloseButtonViewModel : TopMenuButtonViewModel
    {
        #region Contructor
        
        //inicializacion de instancia
        public YesCloseButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion

        #region Metodos
        //cerrar la aplicacion
        public override void ClickExecuted()
        {
            PlaySound();
            Application.Current.MainWindow.Close();
        }
        #endregion
    }
}
