using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeTypeButtonViewModel : TopMenuButtonViewModel
    {
        #region Contructor
        //inicializacion de instancia
        public ChangeTypeButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion

        #region Methodos
        
        //cambio el tipo de ropas mostradas
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.ChosenType = ClothingManager.Instance.ChosenType == ClothingItemBase.MaleFemaleType.Female
                ? ClothingItemBase.MaleFemaleType.Male : ClothingItemBase.MaleFemaleType.Female;

            ClothingManager.Instance.UpdateActualCategories();

            if (ClothingManager.Instance.Clothing != null)
            {
                ClothingManager.Instance.Clothing.Clear();
                foreach (var cloth in ClothingManager.Instance.LastChosenCategory.Clothes.Where(
                    cloth => cloth.Type == ClothingManager.Instance.ChosenType || cloth.Type == ClothingItemBase.MaleFemaleType.Both))
                    ClothingManager.Instance.Clothing.Add(cloth);
            }

            ClearMenu();
        }
        #endregion
    }
}
