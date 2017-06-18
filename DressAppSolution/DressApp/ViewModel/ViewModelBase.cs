using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DressApp.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Metodos Protegidos

        //cuando se llama a la propiedad de cambio  [property changed]
        //<param name="property">propiedad.</param>
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion Metodos Protegidos
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
