using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MiDbContext;

namespace aaagenda_contactos
{
    /// <summary>
    /// Lógica de interacción para Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {
            var tipocontacto = txttipocontacto.Text;

            if (string.IsNullOrWhiteSpace(tipocontacto))
            {
                MessageBox.Show("El tipo de contacto no puede estar vacío.");
                return;
            }

            var nuevotipo_contacto = new tipo_contacto
            {
                Nombre_tipo_contacto = tipocontacto
            };

            using (var dbContext = new MiDbContext())
            {
                try
                {
                    dbContext.tipos_contacto.Add(nuevotipo_contacto);
                    dbContext.SaveChanges();
                    txttipocontacto.Clear();
                    

                    MessageBox.Show("Tipo de contacto guardado exitosamente.");
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"Error al guardar tipo contacto: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}");
                }
            }
        }

        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var ventana = Window.GetWindow(this) as MainWindow; // Asegúrate de que sea MainWindow
            if (ventana != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = ventana.FindName("Frame5") as Frame;
                if (frame != null)
                {
                    // Llamar al método ToggleMenu para cerrar el menú
                    ventana.ToggleMenu(sender, e); // Cerrar el menú

                    // Crear animación de desvanecimiento para ocultar el Frame
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, // Reducir la opacidad a 0 (invisible)
                        Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Cambiar la visibilidad del Frame a Collapsed al finalizar el desvanecimiento
                        frame.Visibility = Visibility.Collapsed;
                    };

                    // Iniciar la animación de desvanecimiento
                    frame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                }
            }
        }
    }
}
