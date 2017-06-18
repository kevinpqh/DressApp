using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf;
using Microsoft.Kinect;

namespace DressApp.ViewModel
{
    public sealed class ClothingManager : ViewModelBase
    {
        #region atributos privados
        //private ObservableCollection<ClothingCategoryButtonViewModel> _actualClothingCategories;

        // Only instance of ClothingManager
        private static ClothingManager _instance;
        
        // The chosen clothing models collection
        //private OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
        
        // The clothing collection
        //private ObservableCollection<ClothingButtonViewModel> _clothing;
        
        // The viewport transform
        private Matrix3D _viewportTransform;
        
        // The camera transform
        private Matrix3D _cameraTransform;

        // Gets or sets the model importer.
        // The model importer.
        private ModelImporter _importer;

        #endregion

        #region metodos publicos

        /// Gets or sets the clothing categories collection.
        /// The clothing categories collection.
        //public ObservableCollection<ClothingCategoryButtonViewModel> ClothingCategories { get; set; }
        
        // Gets or sets the actually displayed collection of categories
        /*public ObservableCollection<ClothingCategoryButtonViewModel> ActualClothingCategories
        {
            get { return _actualClothingCategories; }
            set
            {
                if (_actualClothingCategories == value)
                    return;
                _actualClothingCategories = value;
                OnPropertyChanged("ActualClothingCategories");
            }
        }*/
        // Gets or sets the chosen type of clothes
        //public ClothingItemBase.MaleFemaleType ChosenType { get; set; }
        // Gets or sets last chosen category
        //public ClothingCategoryButtonViewModel LastChosenCategory { get; set; }
        
        // Gets or sets the chosen clothing models collection.
        // The chosen clothing models collection.
        /*public OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
        {
            get { return _chosenClothesModels; }
            set
            {
                if (_chosenClothesModels == value)
                    return;
                _chosenClothesModels = value;
                OnPropertyChanged("ChosenClothesModels");
            }
        }*/
        // Gets or sets the available clothing collection.
        // The available clothing collection.
        /*public ObservableCollection<ClothingButtonViewModel> Clothing
        {
            get { return _clothing; }
            set
            {
                if (_clothing == value)
                    return;
                _clothing = value;
                OnPropertyChanged("Clothing");
            }
        }*/
        // Method with access to only instance of ClothingManager
        public static ClothingManager Instance
        {
            get { return _instance ?? (_instance = new ClothingManager()); }
        }
        // Gets or sets the viewport transform.
        // The viewport transform.
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
        // Gets or sets the camera transform.
        // The camera transform.
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
        // Private constructor of ClothingManager. 
        
        private ClothingManager()
        {
            //ChosenType = ClothingItemBase.MaleFemaleType.Female;
            //ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            _importer = new ModelImporter();
        }
        #endregion

        #region metodos protegidos

        #endregion

        #region metodos publicos


        #endregion
    }
}
