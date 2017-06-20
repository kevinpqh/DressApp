using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    class GlassesButtonViewModel : ClothingButtonViewModel
    {
        #region Contructor
        // inicializar una instancia
        public GlassesButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Both, pathToModel)
        {
            Ratio = 0.3;
            DeltaY = 1.2;
        }
        #endregion

        #region comandos
        // presionde boton
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<GlassesItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
