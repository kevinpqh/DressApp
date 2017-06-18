using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace DressApp.View.Helpers
{
    public class ItemsVisual3D : ModelVisual3D
    {
        #region Dependency Properties

        // Propiedades de ItemTemplate 
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate3D), typeof(ItemsVisual3D), new PropertyMetadata(null));

        // Propiedades de items source 

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(ItemsVisual3D)
            , new PropertyMetadata(null, (s, e) => ((ItemsVisual3D)s).ItemsSourceChanged(e)));
        #endregion Dependency Properties
        #region Public Properties

        // Gets y sets de la clase DataTemplate3D usado para mostrar cada item.

        public DataTemplate3D ItemTemplate
        {
            get { return (DataTemplate3D)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Gets y sets de la colleccion usada para generar el contenido de la clase ItemsVisual3D

        public ICollection ItemsSource
        {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion Public Properties
        #region Private Methods

        // Mano cambia propiedades de ItemsSource .

        private void ItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Children.Clear();

            foreach (var model in (from object item in ItemsSource
                                   select ItemTemplate.CreateItem(item)).Where(model => model != null))
                Children.Add(model);
        }
        #endregion Private Methods
    }
}
