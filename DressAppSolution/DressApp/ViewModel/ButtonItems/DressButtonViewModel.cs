using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    public class DressButtonViewModel : ClothingButtonViewModel
    {
        #region Prpiedades Publicas
        //Gets or sets articulación inferior para seguir la escala.
        public JointType BottomJointToTrackScale = JointType.HipCenter;
        #endregion

        #region Contructor
        // inicializar una instancia
        public DressButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Female, pathToModel)
        {
            Ratio = 1.7;
            DeltaY = 1.05;
        }
        #endregion

        #region comando
       
        // cuando se presiona el boton
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.SkirtItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.SkirtItem);
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.TopItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.TopItem);
            ClothingManager.Instance.AddClothingItem<DressItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
