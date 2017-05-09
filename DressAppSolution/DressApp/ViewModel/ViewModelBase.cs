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
        #region Protected Methods
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="property">The property.</param>
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion Protected Methods
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
