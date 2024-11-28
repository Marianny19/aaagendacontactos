using Contactos;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static aaagenda_contactos.MainWindow;
using static MiDbContext;

namespace aaagenda_contactos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ObservableCollection para almacenar los contactos
        public ObservableCollection<contacto> Contactos { get; set; } = new ObservableCollection<contacto>();
        private bool _isEditing = false; // Variable para saber si se está editando un contacto
        private contacto _currentEditingContact; // Variable para almacenar el contacto que se está editando

        public MainWindow()
        {
            InitializeComponent();
            cargarcontacto();
            Cargartipocontactos();


            // Inicializar la lista de contactos
        }
        private void cargarcontacto()
        {
            using (var context = new MiDbContext())
            {
                var contactos = context.contactos
                .Include(c => c.TipoContacto)
                .Include(c => c.TipoRedSocial)
                .ToList();

                ContactosDataGrid.ItemsSource = contactos;
            }
        }

        public bool isMenuOpen = false;

        public void ToggleMenu(object sender, RoutedEventArgs e)
        {
            // Cambia el estado del menú
            isMenuOpen = !isMenuOpen;

            // Asegúrate de que el SideMenu sea visible mientras está en animación
            SideMenu.Visibility = Visibility.Visible;

            // Define el objetivo de desplazamiento en el eje X
            double targetOffset = isMenuOpen ? 0 : -200;

            // Animación de desplazamiento para el SideMenu
            var slideAnimation = new DoubleAnimation
            {
                To = targetOffset,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Aplicar animación en el eje X del SideMenuTransform
            SideMenuTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);

            // Ocultar el SideMenu después de la animación si se está cerrando
            slideAnimation.Completed += (s, e) =>
            {
                if (!isMenuOpen)
                    SideMenu.Visibility = Visibility.Collapsed;
            };

            // Animación de opacidad para ContentBorder
            var contentOpacityAnimation = new DoubleAnimation
            {
                To = isMenuOpen ? 0 : 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Inicia la animación de opacidad para ContentBorder
            ContentBorder.BeginAnimation(OpacityProperty, contentOpacityAnimation);

            // Desactiva la interacción del usuario en ContentBorder mientras SideMenu está abierto
            ContentBorder.IsHitTestVisible = !isMenuOpen;
        }

        // Evento para el botón Modificar en la columna de acciones

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contactoEditado = button?.DataContext as contacto;

            if (contactoEditado != null)
            {
                using (var context = new MiDbContext())
                {
                    var contactoExistente = context.contactos
                        .FirstOrDefault(c => c.ID_contacto == contactoEditado.ID_contacto);

                    if (contactoExistente != null)
                    {
                        // Actualizar los datos en la base de datos
                        contactoExistente.Nombre = contactoEditado.Nombre;
                        contactoExistente.Apellido = contactoEditado.Apellido;
                       
                        contactoExistente.Email = contactoEditado.Email;
                        contactoExistente.Tipo_Contacto = contactoEditado.Tipo_Contacto;
                        contactoExistente.Tipo_red_social = contactoEditado.Tipo_red_social;

                        context.SaveChanges();
                        MessageBox.Show("Contacto actualizado correctamente.");
                    }
                }
            }
        }

        private void txtSearch2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto de búsqueda y convertirlo a minúsculas
            string searchTerm = txtSearch2.Text.ToLower().Trim();

            // Obtener la vista por defecto del DataGrid
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(ContactosDataGrid.ItemsSource);

            // Filtrar por nombre y apellido
            collectionView.Filter = item =>
            {
                // Verificar si el item es del tipo Contacto
                var contacto = item as contacto;
                if (contacto == null) return false;

                // Crear una cadena con el nombre completo (nombre + apellido)
                string fullName = (contacto.Nombre + " " + contacto.Apellido).ToLower();

                // Comprobar si el término de búsqueda está en el nombre completo (nombre + apellido)
                return fullName.Contains(searchTerm);
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la ventana
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
                var frame = MainFrame;
                if (frame != null)
                {
                    // Crear animación de desvanecimiento para ocultar el Frame
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(0),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Navegar a la nueva página al finalizar el desvanecimiento
                        frame.Navigate(new Page1());

                        // Hacer que el Frame sea visible para la animación de aparición
                        frame.Visibility = Visibility.Visible;

                        // Crear animación de aparición
                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1,
                            Duration = TimeSpan.FromMilliseconds(300),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                        };

                        // Iniciar la animación de aparición
                        frame.BeginAnimation(OpacityProperty, fadeInAnimation);
                    };

                    // Iniciar la animación de desvanecimiento
                    frame.BeginAnimation(OpacityProperty, fadeOutAnimation);
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

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
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
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                var frame = ventana.FindName("Frame2") as Frame;
                if (frame != null)
                {
                    frame.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void Frame5_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            {
                {
                    var button = sender as Button;
                    var ContactoSeleccionado = ContactosDataGrid.SelectedItem as contacto;

                    if (ContactoSeleccionado != null)
                    {
                        var result = MessageBox.Show(
                            $"¿Estás seguro de que deseas eliminar el contacto con ID {ContactoSeleccionado.ID_contacto}?",
                            "Confirmación de eliminación",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (var context = new MiDbContext())
                            {
                                var ContactoAEliminar = context.contactos
                                    .FirstOrDefault(c => c.ID_contacto == ContactoSeleccionado.ID_contacto);

                                if (ContactoAEliminar != null)
                                {
                                    context.contactos.Remove(ContactoAEliminar);
                                    context.SaveChanges();

                                    cargarcontacto();

                                    MessageBox.Show("Contacto eliminado correctamente.");
                                }
                                else
                                {
                                    MessageBox.Show("El contacto no se encontró en la base de datos.");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se seleccionó ningún contacto para eliminar.");
                    }
                }
            }
        }
        private void Tipodecontacto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tipodecontacto.SelectedItem is tipo_contacto tipoSeleccionado)
            {
                using (var context = new MiDbContext())
                {
                    var contactosFiltrados = context.contactos
                        .Where(c => c.Tipo_Contacto == tipoSeleccionado.ID_tipo_contacto)
                        .Include(c => c.TipoContacto)
                        .Include(c => c.TipoRedSocial)
                        .ToList();

                    ContactosDataGrid.ItemsSource = contactosFiltrados;
                }
            }
        }
        private void Cargartipocontactos()
        {
            using (var context = new MiDbContext())
            {
                var tipo_contacto = context.tipos_contacto.ToList();

                Tipodecontacto.ItemsSource = tipo_contacto;
                Tipodecontacto.DisplayMemberPath = "Nombre_tipo_contacto";
                Tipodecontacto.SelectedValuePath = "ID_tipo_contacto";
            }
        }
    }
}