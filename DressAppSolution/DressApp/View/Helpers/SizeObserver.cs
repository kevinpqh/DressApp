using System.Windows;

namespace DressApp.View.Helpers
{

    // Permite el enlace a las propiedades de sólo lectura

    public static class SizeObserver
    {
        #region Dependency Properties
     
        // Prpiedades de observer 
     
        public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
            "Observe", typeof(bool), typeof(SizeObserver), new FrameworkPropertyMetadata(OnObserveChanged));

        // anchura observer

        public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
            "ObservedWidth", typeof(double), typeof(SizeObserver));

        // altura observada

        public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
            "ObservedHeight", typeof(double), typeof(SizeObserver));
        #endregion Dependency Properties
        #region Public Static Methods
     
        // Gets de la propiedades del observe
     
        public static bool GetObserve(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ObserveProperty);
        }

        // Sets de la propiedades del observer 

        public static void SetObserve(FrameworkElement frameworkElement, bool observe)
        {
            frameworkElement.SetValue(ObserveProperty, observe);
        }

        // Gets anchura del elemento de armazón observer

        public static double GetObservedWidth(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedWidthProperty);
        }

        // Sets anchura del elemento del marco observer

        public static void SetObservedWidth(FrameworkElement frameworkElement, double observedWidth)
        {
            frameworkElement.SetValue(ObservedWidthProperty, observedWidth);
        }

        // Gets altura del elemento del marco observer

        public static double GetObservedHeight(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedHeightProperty);
        }

        // Sets altura del elemento del marco observado

        public static void SetObservedHeight(FrameworkElement frameworkElement, double observedHeight)
        {
            frameworkElement.SetValue(ObservedHeightProperty, observedHeight);
        }
        #endregion Public Static Methods
        #region Private Static Methods
     
        // cuando observe cambia

        private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)dependencyObject;

            if ((bool)e.NewValue)
            {
                frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
                UpdateObservedSizesForFrameworkElement(frameworkElement);
            }
            else
                frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
        }

        // llama cuando el tamaño del elemento de estructura cambio

        private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
        }

        // Actualiza los tamaños observados para el elemento de estructura

        private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement)
        {
            frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
            frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);
        }
        #endregion Private Static Methods
    }
}
