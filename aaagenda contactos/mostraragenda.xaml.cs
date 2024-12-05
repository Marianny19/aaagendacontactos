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

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public ObservableCollection<agenda> Agendas { get; set; }
        private bool _isEditing = false; // Variable para saber si se está editando un contacto
        private agenda _currentEditingAgenda;
        public Page3()
        {
            InitializeComponent();
            Cargarcontactosagendado();
            CargarContactos();

            // Registrar el convertidor como recurso de la página
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



        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = mainWindow.Frame3;
                if (frame != null)
                {
                    // Crear animación de desvanecimiento para ocultar el Frame
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, // Reducir la opacidad a 0 (invisible)
                        Duration = TimeSpan.FromMilliseconds(0), // Duración de la animación
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Navegar a la nueva página al finalizar el desvanecimiento
                        frame.Navigate(new Page2());

                        // Hacer que el Frame sea visible para la animación de aparición
                        frame.Visibility = Visibility.Visible;

                        // Crear animación de aparición
                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1, // Aumentar la opacidad a 1 (completamente visible)
                            Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                        };

                        // Iniciar la animación de aparición
                        frame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

                    // Iniciar la animación de desvanecimiento
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
                        // Actualizar los datos en la base de datos
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
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