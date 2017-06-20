using DressApp.Model.ClothingItems;
using DressApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearSetButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de instancia
        public ClearSetButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Metodos

        //Borra el conjunto elegido
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            ClearMenu();
        }
        #endregion
    }
}
