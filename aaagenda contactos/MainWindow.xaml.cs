using Contactos;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aaagenda_contactos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ObservableCollection para almacenar los contactos
        public ObservableCollection<Contacto> Contactos { get; set; }
        private bool _isEditing = false; // Variable para saber si se está editando un contacto
        private Contacto _currentEditingContact; // Variable para almacenar el contacto que se está editando

        public MainWindow()
        {
            InitializeComponent();

            // Inicializar la lista de contactos
            Contactos = new ObservableCollection<Contacto>
            {
                new Contacto { Nombre = "Juan", Telefono = "123456789" },
                new Contacto { Nombre = "María", Telefono = "987654321" }
            };

            // Asignar el ItemsSource del DataGrid a la lista de contactos
            ContactosDataGrid.ItemsSource = Contactos;
        }

        // Evento para el botón Modificar en la columna de acciones
        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isEditing)
            {
                MessageBox.Show("Por favor, guarda los cambios antes de modificar otro contacto.");
                return; // Salir si ya se está editando otro contacto
            }

            // Obtener el contacto del parámetro del comando
            var button = sender as Button;
            var contacto = button.CommandParameter as Contacto;

            if (contacto != null)
            {
                // Hacer los campos del contacto editables
                DataGridRow row = (DataGridRow)ContactosDataGrid.ItemContainerGenerator.ContainerFromItem(contacto);
                if (row != null)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    if (presenter != null)
                    {
                        for (int i = 0; i < ContactosDataGrid.Columns.Count; i++)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            if (cell != null)
                            {
                                TextBox textBox = FindVisualChild<TextBox>(cell);
                                if (textBox != null)
                                {
                                    textBox.IsEnabled = true; // Hacer el TextBox editable
                                    textBox.Focus(); // Opcional: dar foco al TextBox
                                }
                            }
                        }
                    }
                }

                // Mostrar el botón Guardar
                guardar.Visibility = Visibility.Visible;
                _isEditing = true; // Marcar que se está editando
                _currentEditingContact = contacto; // Guardar el contacto que se está editando
            }
        }

        // Evento para el botón Guardar
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentEditingContact != null)
            {
                // Aquí puedes agregar la lógica para guardar los cambios del contacto
                MessageBox.Show($"Guardado: {_currentEditingContact.Nombre}, {_currentEditingContact.Telefono}");

                // Ocultar el botón Guardar después de guardar
                guardar.Visibility = Visibility.Collapsed;
                _isEditing = false; // Marcar que ya no se está editando
                _currentEditingContact = null; // Limpiar la referencia del contacto

                // Deshabilitar los TextBox después de guardar
                DataGridRow row = (DataGridRow)ContactosDataGrid.ItemContainerGenerator.ContainerFromItem(_currentEditingContact);
                if (row != null)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    if (presenter != null)
                    {
                        for (int i = 0; i < ContactosDataGrid.Columns.Count; i++)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            if (cell != null)
                            {
                                TextBox textBox = FindVisualChild<TextBox>(cell);
                                if (textBox != null)
                                {
                                    textBox.IsEnabled = false; // Deshabilitar el TextBox después de guardar
                                }
                            }
                        }
                    }
                }
            }
        }

        // Función para obtener el hijo visual de un tipo específico
        private T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                {
                    return tChild;
                }
                T childOfChild = GetVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        // Función para encontrar un TextBox en una celda
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                {
                    return tChild;
                }

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        // Evento para navegar a Page1
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = ventana.FindName("MainFrame") as Frame;
                if (frame != null)
                {
                    frame.Visibility = Visibility.Visible; // Asegurarse de que el Frame sea visible
                    frame.Navigate(new Page1()); // Navegar a Page1
                }
            }
        }

        

        // Arrastrar la ventana
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;

        // Maximizar y restaurar ventana
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (!IsMaximized)
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
            }
        }

        // Evento del DataGrid
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lógica para manejar el cambio de selección
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {

                var frame = ventana.FindName("Frame2") as Frame;
                if (frame != null)
                {
                    frame.Visibility = Visibility.Visible;
                    frame.Navigate(new Page3());

                }
            }
        }
        // Clase Contacto
        public class Contacto
        {
            public string Nombre { get; set; }
            public string Telefono { get; set; }
        }
    }
}