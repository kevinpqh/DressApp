using DressApp.Model.ClothingItems;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    public class SkirtButtonViewModel : ClothingButtonViewModel
    {
        #region Propiedades publicas
        
        // Gets or sets La articulación inferior para seguir la escala.
        public JointType BottomJointToTrackScale = JointType.KneeRight;
        #endregion

        #region Contructor
        // inicializar una instancia
        public SkirtButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Female, pathToModel)
        {
            Ratio = 0.9;
            DeltaY = 1;
        }
        #endregion

        #region comandos
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.DressItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.DressItem);
            ClothingManager.Instance.AddClothingItem<SkirtItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
