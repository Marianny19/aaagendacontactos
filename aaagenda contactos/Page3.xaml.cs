using aaagenda_contactos;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media;

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
            Cargarcontactosagendado();

            // Registrar el convertidor como recurso de la página
            this.Resources.Add("BoolToVis", new BoolToVisConverter());
            
        }
        private void Cargarcontactosagendado()
        {
            using (var context = new MiDbContext())
            {
                var agenda = context.agendas
                    .Select(c => new
                    
                    {
                        c.ID_agenda,
                        c.Nombre_agenda,
                        c.Descripcion_agenda,
                        c.Fecha_agendada, 
                        
                        c.ID_contacto
                    })
                    .ToList();

                CDataGrid.ItemsSource = agenda;
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
            using (var context = new MiDbContext())
            {
                var agenda = context.agendas
                    .Select(c => new

                    {
                        c.ID_agenda,
                        c.Nombre_agenda,
                        c.Descripcion_agenda,
                        c.Fecha_agendada,

                        c.ID_contacto
                    })
                    .ToList();

                CDataGrid.ItemsSource = agenda;
            }

        var dataGrid = sender as DataGrid;
            var selectedItem = dataGrid.SelectedItem;

            {
                Window.GetWindow(this)?.Close();
            }
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
    }
}
