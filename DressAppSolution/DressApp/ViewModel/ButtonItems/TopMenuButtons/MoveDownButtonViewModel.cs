using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class MoveDownButtonViewModel : TopMenuButtonViewModel
    {
        #region constantes
        private const double MoveFactor = 0.05;
        #endregion
        #region Constructor
        //inicializacion de instancia
        public MoveDownButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion

        #region Metodos

        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ChangeImagePosition(MoveFactor);
        }
        #endregion
    }
}
