using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel
{
    public class KinectViewModel : ViewModelBase
    {
        #region Atributos Privados

        /// Gets the button player.

        //public static SoundPlayer ButtonPlayer { get; private set; }

        /// Gets or sets the clothing manager.

        /* ClothingManager ClothingManager
        {
            get { return _clothingManager; }
            set
            {
                if (_clothingManager == value)
                    return;
                _clothingManager = value;
                OnPropertyChanged("ClothingManager");
            }
        }*/

        /// The clothing manager
        //private ClothingManager _clothingManager;
        
        //servicio de kinect
        private readonly KinectService _kinectService;
        #endregion

        public KinectService KinectService
        {
            get { return _kinectService; }
        }

        public bool DebugModeOn
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }
        #region Constructor
        public KinectViewModel(KinectService kinectService)
        {
            //ButtonPlayer = new SoundPlayer(Properties.Resources.ButtonClick);
            //InitializeClothingCategories();
            _kinectService = kinectService;
            _kinectService.Initialize();
        }
        #endregion

    }
}
