using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf;
using Microsoft.Kinect;
using DressApp.ViewModel.ButtonItems;
using DressApp.ViewModel.Helpers;

namespace DressApp.ViewModel
{
    public sealed class ClothingManager : ViewModelBase
    {
        #region atributos privados
        private ObservableCollection<ClothingCategoryButtonViewModel> _actualClothingCategories;

        // una sola instancia de ClothingManager
        private static ClothingManager _instance;

        // La colección de prendas de vestir elegida
        private OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
        
        // Coleccion de prensdas de vestier
        private ObservableCollection<ClothingButtonViewModel> _clothing;
        
        // transformada de la vista
        private Matrix3D _viewportTransform;
        
        // transformada de la camara
        private Matrix3D _cameraTransform;

        // Gets or sets de model importer.
        private ModelImporter _importer;

        #endregion

        #region Propiedades publicas

         
        // Gets o sets de todas las categorias de ropas
        public ObservableCollection<ClothingCategoryButtonViewModel> ClothingCategories { get; set; }

        // Gets o Sets La colección de prendas de vestir elegida 
        public ObservableCollection<ClothingCategoryButtonViewModel> ActualClothingCategories
        {
            get { return _actualClothingCategories; }
            set
            {
                if (_actualClothingCategories == value)
                    return;
                _actualClothingCategories = value;
                OnPropertyChanged("ActualClothingCategories");
            }
        }

        /// Gets o sets El tipo de ropa elegido
        public ClothingItemBase.MaleFemaleType ChosenType { get; set; }
         
        // Gets o sets la ultima categoria elegida
        public ClothingCategoryButtonViewModel LastChosenCategory { get; set; }

        // Gets o sets La colección de losn modelos de ropa elegidos.
        public OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
        {
            get { return _chosenClothesModels; }
            set
            {
                if (_chosenClothesModels == value)
                    return;
                _chosenClothesModels = value;
                OnPropertyChanged("ChosenClothesModels");
            }
        }

        // Gets o sets La colección de ropa disponible.
        public ObservableCollection<ClothingButtonViewModel> Clothing
        {
            get { return _clothing; }
            set
            {
                if (_clothing == value)
                    return;
                _clothing = value;
                OnPropertyChanged("Clothing");
            }
        }
         
        // Methodo con acceso a una instancia de ClothingManager
        public static ClothingManager Instance
        {
            get { return _instance ?? (_instance = new ClothingManager()); }
        }
         
        // Gets or sets 
        public Matrix3D ViewportTransform
        {
            get { return _viewportTransform; }
            set
            {
                if (_viewportTransform == value)
                    return;
                _viewportTransform = value;
                OnPropertyChanged("ViewportTransform");
            }
        }
         
        /// Gets or sets de camara
        public Matrix3D CameraTransform
        {
            get { return _cameraTransform; }
            set
            {
                if (_cameraTransform == value)
                    return;
                _cameraTransform = value;
                OnPropertyChanged("CameraTransform");
            }
        }
        #endregion

        #region constructor
        // constructor de ClothingManager. 
        private ClothingManager()
        {
            ChosenType = ClothingItemBase.MaleFemaleType.Female;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            _importer = new ModelImporter();
        }
        #endregion

        #region metodos protegidos

        // escala del alto de la ropa -- ratio de escala
        public void ScaleImageHeight(double ratio)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.HeightScale += ratio;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }

        // escala del ancho de la ropa -- ratio de escala
        public void ScaleImageWidth(double ratio)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.WidthScale += ratio;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }

        // Cambio de poscion de la ropa -> Posicion delta
        public void ChangeImagePosition(double delta)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.DeltaPosition += delta;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        #endregion

        #region metodos publicos

        // actualiza la posicion del item
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            foreach (var model in ChosenClothesModels.Values)
                model.UpdateItemPosition(skeleton, sensor, width, height);
        }
 
        //actualizamos la categoria de ropa mostrada
        public void UpdateActualCategories()
        {
            if (ActualClothingCategories == null)
                ActualClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>();

            ActualClothingCategories.Clear();
            foreach (var category in ClothingCategories)
                if (category.Type == ClothingItemBase.MaleFemaleType.Both || category.Type == ChosenType)
                    ActualClothingCategories.Add(category);
        }

        // agregamos un item de ropa // categoria del item, path del modelo , articulacion inferior del tamaño del track, scala de ratio, posicion
        public void AddClothingItem<T>(ClothingItemBase.ClothingType category, string modelPath, JointType bottomJoint, double ratio, double deltaY)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ChosenClothesModels;
            tmpModels[category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), _importer.Load(modelPath), bottomJoint, ratio, deltaY);
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
        // agregamos un item de ropa // categoria del item, path del modelo , scala de ratio, posicion
        public void AddClothingItem<T>(ClothingItemBase.ClothingType category, string modelPath, double ratio, double deltaY)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ChosenClothesModels;
            tmpModels[category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), _importer.Load(modelPath), ratio, deltaY);
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }

        #endregion
    }
}
