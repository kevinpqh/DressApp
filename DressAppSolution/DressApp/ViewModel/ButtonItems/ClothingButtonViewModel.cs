using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    
    // view model para los botones de ropa     
    public abstract class ClothingButtonViewModel : ButtonViewModelBase
    {
        #region Atributos Privados
         
        // categoria del item         
        private ClothingItemBase.ClothingType _category;
         
        // tipo de ropa         
        private ClothingItemBase.MaleFemaleType _type;
        #endregion

        #region propiedades publicas
         
        // Gets categoria del item
        public ClothingItemBase.ClothingType Category
        {
            get { return _category; }
        }
         
        // Gets tipo de item
        public ClothingItemBase.MaleFemaleType Type
        {
            get { return _type; }
        }
         
        // Gets o sets path del modelo         
        public string ModelPath { get; set; }

        // Ancho de caderas con márgenes         
        public double Ratio { get; set; }

        // El factor para mover el modelo en coordenadas Y        
        public double DeltaY { get; set; }

        #endregion
        
        #region Constructor
        //inicializacion de instancia
        protected ClothingButtonViewModel(ClothingItemBase.ClothingType category, ClothingItemBase.MaleFemaleType type, string pathToModel)
        {
            _category = category;
            _type = type;
            ModelPath = pathToModel;
        }
        #endregion
    }
}
