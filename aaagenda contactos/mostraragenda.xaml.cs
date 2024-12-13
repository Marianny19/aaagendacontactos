using aaagenda_contactos;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media;
using static MiDbContext;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public ObservableCollection<agenda> Agendas { get; set; }
        private bool _isEditing = false; 
        private agenda _currentEditingAgenda; 
        private agenda agendaSeleccionada;
        public Page3()
        {
            InitializeComponent();
            Cargarcontactosagendado();
            CargarContactos();

            this.Resources.Add("BoolToVis", new BoolToVisConverter());
            
        }
        private void Cargarcontactosagendado()
        {
            using (var context = new MiDbContext())
            {
                var Agendas = context.agendas
             .Include(c => c.IDContacto)
                    .ToList();

                CDataGrid.ItemsSource = Agendas;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower().Trim();

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(CDataGrid.ItemsSource);

            collectionView.Filter = item =>
            {
                var agenda = item as agenda;  
                if (agenda == null) return false;

                string agendaName = agenda.Nombre_agenda.ToLower();
                string agendaDescription = agenda.Descripcion_agenda.ToLower();

                return agendaName.Contains(searchTerm) || agendaDescription.Contains(searchTerm);
            };
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            agendaSeleccionada = (agenda)CDataGrid.SelectedItem;

        }



        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que fue presionado
            var button = sender as Button;

            // Obtener el registro seleccionado del DataGrid
            var agendaSeleccionada = button?.DataContext as agenda; // Suponiendo que el DataContext es del tipo 'Agenda'

            if (agendaSeleccionada != null)
            {
                // Obtener la ventana principal
                var mainWindow = Application.Current.MainWindow as MainWindow;

                if (mainWindow != null)
                {
                    // Ocultar Page3 (suponiendo que tienes un Frame para Page3)
                    mainWindow.Frame2.Visibility = Visibility.Collapsed;

                    // Navegar a Page2 y pasar los datos del registro seleccionado
                    mainWindow.MainFrame.Navigate(new Page2(agendaSeleccionada));
                }
            }
        }


        private void CerrarPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el Frame principal y ocultar el Frame de Page2
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Frame2.Visibility = Visibility.Collapsed;  // Ocultar Page2
                mainWindow.Frame3.Visibility = Visibility.Visible;     // Mostrar Page3
            }
        }





        private void AbrirPantallaRegistrarAgendas(agenda agendaSeleccionada)
        {
            // Crear y mostrar la ventana o página de registro
            var registrarAgendasPage = new Page2(agendaSeleccionada); // Cambia Page2 si usas otro nombre
            NavigationService.Navigate(registrarAgendasPage); // Si estás usando navegación de páginas
        }





        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var frame = mainWindow.Frame3;
                if (frame != null)
                {
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, 
                        Duration = TimeSpan.FromMilliseconds(0), 
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } 
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        frame.Navigate(new Page2());

                        frame.Visibility = Visibility.Visible;

                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1,
                            Duration = TimeSpan.FromMilliseconds(300), 
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } 
                        };

                        frame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

                    frame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                    {

                    }

                }
            }
        }
        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
       

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
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var AgendaEditada = button?.DataContext as agenda;

            if (AgendaEditada != null)
            {
                using (var context = new MiDbContext())
                {
                    var AgendaExistente = context.agendas
                        .FirstOrDefault(c => c.ID_agenda == AgendaEditada.ID_agenda);

                    if (AgendaExistente != null)
                    {
                        AgendaExistente.Nombre_agenda = AgendaEditada.Nombre_agenda;
                        AgendaExistente.Descripcion_agenda = AgendaEditada.Descripcion_agenda;
                        AgendaExistente.Fecha_agendada = AgendaEditada.Fecha_agendada;
                        AgendaExistente.ID_contacto = AgendaEditada.ID_contacto;

                        context.SaveChanges();
                        MessageBox.Show("Agenda actualizada correctamente.");
                    }
                }
            }
        }

       


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            {
                var button = sender as Button;
                var AgendaSeleccionada = CDataGrid.SelectedItem as agenda;

                if (AgendaSeleccionada != null)
                {
                    var result = MessageBox.Show(
                        $"¿Estás seguro de que deseas eliminar la agenda con ID {AgendaSeleccionada.ID_agenda}?",
                        "Confirmación de eliminación",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (var context = new MiDbContext())
                        {
                            var AgendaAEliminar = context.agendas
                                .FirstOrDefault(c => c.ID_agenda == AgendaSeleccionada.ID_agenda);

                            if (AgendaAEliminar != null)
                            {
                                context.agendas.Remove(AgendaAEliminar);
                                context.SaveChanges();

                                Cargarcontactosagendado();

                                MessageBox.Show("Agenda eliminada correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("La agenda no se encontró en la base de datos.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se seleccionó ningúna agenda para eliminar.");
                }
            }
        }
        private void CargarContactos()
        {
            using (var context = new MiDbContext())
            {
                var contactos = context.contactos.ToList();

                Contactosagendados.ItemsSource = contactos;
                Contactosagendados.DisplayMemberPath = "Nombre";
                Contactosagendados.SelectedValuePath = "ID_contacto"; 
            }
        }

        private void Contactosagendados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Contactosagendados.SelectedItem is contacto contactoSeleccionado)
            {
                using (var context = new MiDbContext())
                {
                    var agendasFiltradas = context.agendas
                        .Where(a => a.ID_contacto == contactoSeleccionado.ID_contacto)
                        .Include(a => a.IDContacto)
                        .ToList();
                    CDataGrid.ItemsSource = agendasFiltradas;
                }
            }

        }
    }
}