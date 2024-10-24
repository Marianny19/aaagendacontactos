using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

        }
        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void MinimizarVentana_Click(object sender, RoutedEventArgs e)
        {
            Window ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                ventana.WindowState = WindowState.Minimized;
            }
        }
        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var dataGrid = sender as DataGrid;
            var selectedItem = dataGrid.SelectedItem;

            {
                Window.GetWindow(this)?.Close();
            }
        }
    }

    // Convertidor que convierte de bool a Visibility
    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}