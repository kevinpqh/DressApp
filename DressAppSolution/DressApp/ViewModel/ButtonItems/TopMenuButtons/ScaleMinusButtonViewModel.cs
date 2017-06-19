using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ScaleMinusButtonViewModel : TopMenuButtonViewModel
    {
        #region Constantes
        private const double MinusFactor = -0.05;
        #endregion
        #region Constructor
        //inicializacion de instancia
        public ScaleMinusButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Metodos
        // hace mas pequeño
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
            {
                ClothingManager.Instance.ScaleImageHeight(MinusFactor);
                ClothingManager.Instance.ScaleImageWidth(MinusFactor);
            }

        }
        #endregion
    }
}
