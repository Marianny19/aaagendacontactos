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

            // Registrar el convertidor como recurso de la página
            this.Resources.Add("BoolToVis", new BoolToVisConverter());
        }

        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void MinimizarVentana_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
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
}
