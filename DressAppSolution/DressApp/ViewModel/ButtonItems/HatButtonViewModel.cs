using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    public class HatButtonViewModel : ClothingButtonViewModel
    {
        #region Contructor
        // inicializar una instancia
        public HatButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Male, pathToModel)
        {
            Ratio = 0.8;
            DeltaY = 1.2;
        }
        #endregion

        #region comando
  
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<HatItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
