using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    public class MenuButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de una nueva instancia
        public MenuButtonViewModel(Bitmap image) : base(image)
        {
        }
        #endregion

        #region Metodos

        //Muestra o esconde los botones del menu top
        public override void ClickExecuted()
        {
            PlaySound();
            if (TopMenuManager.Instance.ActualTopMenuButtons == TopMenuManager.Instance.AllButtons)
                ClearMenu();
            else
            {
                TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.AllButtons;
                TopMenuManager.Instance.CameraButtonVisibility = Visibility.Visible;
                TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
