using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    class BagButtonViewModel : ClothingButtonViewModel
    {
        #region Contructor
        // inicializar una instancia
        public BagButtonViewModel(ClothingItemBase.ClothingType type, ClothingItemBase.MaleFemaleType maleFemaleType, string pathToModel)
            : base(type, maleFemaleType, pathToModel)
        {
            Ratio = 1;
            DeltaY = 1.1;
        }
        #endregion

        #region comando
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<BagItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
