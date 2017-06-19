using DressApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearLastItemButtonViewModel : TopMenuButtonViewModel
    {
        #region Constructor
        //inicializacion de instancia
        public ClearLastItemButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Metodos
       
        //Borra el último elemento elegido
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count == 0)
                return;

            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothesModels;
            tmp.Remove(tmp.LastKey);
            ClothingManager.Instance.ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            ClearMenu();
        }
        #endregion
    }
}
