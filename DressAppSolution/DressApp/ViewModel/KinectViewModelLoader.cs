using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel
{
    public class KinectViewModelLoader
    {
        #region Datos Privados

        /// Kinect view model
        static KinectViewModel _kinectViewModel;
        /// Kinect service
        static KinectService _kinectService;

        #endregion

       
        /// Gets view model.
        public KinectViewModel KinectViewModel
        {
            get { return _kinectViewModel ?? (_kinectViewModel = new KinectViewModel(_kinectService)); }
        }

        #region Contructor
        /// Initializes a new instance of the <see cref="KinectViewModelLoader"/> class.
        public KinectViewModelLoader()
        {
            _kinectService = new KinectService();
            _kinectService.Initialize();
            
        }
        #endregion



        #region Metodos Publicos        
        public static void Cleanup()
        {
            _kinectService.Cleanup();
        }
        #endregion


    }
}
