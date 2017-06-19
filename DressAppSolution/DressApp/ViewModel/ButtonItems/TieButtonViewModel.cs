using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    class TieButtonViewModel : ClothingButtonViewModel
    {
        #region Contructor
        // inicializar una instancia
        public TieButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Male, pathToModel)
        {
            Ratio = 1;
            DeltaY = 1.05;
        }
        #endregion

        #region comandos
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<TieItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
