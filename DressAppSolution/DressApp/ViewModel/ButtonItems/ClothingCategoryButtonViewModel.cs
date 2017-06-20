using DressApp.Model.ClothingItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.ButtonItems
{
    public class ClothingCategoryButtonViewModel : ButtonViewModelBase
    {
        #region Atributos Privados
        
        //tipo
        private ClothingItemBase.MaleFemaleType _type;
      
        // Lista de ropas de la categoria seleccionada
        private List<ClothingButtonViewModel> _clothes;
        #endregion

        #region Propiedades Publicas
        // Get o Set de la lista de ropas
        public List<ClothingButtonViewModel> Clothes
        {
            get { return _clothes; }
            set
            {
                if (_clothes == value)
                    return;
                _clothes = value;
                OnPropertyChanged("Clothes");
            }
        }
     
        // Get de tipo de categoria
        public ClothingItemBase.MaleFemaleType Type
        {
            get { return _type; }
        }
        #endregion

        #region Comandos
        
        //se ejecuta cuando el boton de la categoria es pulsado
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.Clothing != null && ClothingManager.Instance.Clothing.Count != 0
                && ClothingManager.Instance.Clothing[0].Category == Clothes[0].Category)
                return;
            ClothingManager.Instance.LastChosenCategory = this;
            ClothingManager.Instance.Clothing = new ObservableCollection<ClothingButtonViewModel>();
            foreach (var cloth in Clothes)
                if (cloth.Type == ClothingManager.Instance.ChosenType || cloth.Type == ClothingItemBase.MaleFemaleType.Both)
                    ClothingManager.Instance.Clothing.Add(cloth);
        }
        #endregion

        #region Contructor
       
        // inicializar una instancia
        public ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType type)
        {
            _type = type;
        }
        #endregion
    }
}
