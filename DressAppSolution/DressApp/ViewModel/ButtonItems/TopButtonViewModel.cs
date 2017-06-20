using DressApp.Model.ClothingItems;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    public class TopButtonViewModel : ClothingButtonViewModel
    {
        #region Propiedades publicas
        // Gets or sets La articulación inferior para seguir la escala.
        public JointType BottomJointToTrackScale = JointType.Spine;
        #endregion

        #region Contructor
        // inicializar una instancia
        public TopButtonViewModel(ClothingItemBase.ClothingType type, ClothingItemBase.MaleFemaleType maleFemaleType,
            string pathToModel)
            : base(type, maleFemaleType, pathToModel)
        {
            Ratio = 1.2;
            DeltaY = 0.95;
        }
        #endregion

        #region comandos
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.DressItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.DressItem);
            ClothingManager.Instance.AddClothingItem<TopItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
