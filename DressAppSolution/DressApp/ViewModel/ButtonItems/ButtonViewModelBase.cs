using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace DressApp.ViewModel.ButtonItems
{
    //clase base del viewModel del Button
    public abstract class ButtonViewModelBase : ViewModelBase
    {
        #region Atributos Privados
        //Imagen del Button
        private Bitmap _image;
        #endregion

        #region Propiedades Publicas

        //Gets o sets de la imagen del button
        public Bitmap Image
        {
            get { return _image; }
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        #endregion

        #region Comandos
        // comando para ejecutar despues de pulsar el button
        private ICommand _clickCommand;
        //Get del comando
        public ICommand ClickCommand
        {
            get { return _clickCommand ?? (_clickCommand = new DelegateCommand(ClickExecuted)); }
        }
        //Se ejecuta cuando se pulsa el botón
        public abstract void ClickExecuted();
        #endregion

        #region Metodos Publicos
        // play al sonido asociado al button
        public void PlaySound()
        {
            if (TopMenuButtons.TopMenuManager.Instance.SoundsOn)
                KinectViewModel.ButtonPlayer.Play();
        }
        #endregion
    }
}
