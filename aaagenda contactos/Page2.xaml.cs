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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            cargarcontacto();
        }

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = ventana.FindName("Frame3") as Frame;
                if (frame != null)
                {
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

        private void cargarcontacto()
        {

            using (var context = new MiDbContext())
            {
                var contacto = context.contactos.ToList();

                cmbcontactoaagendar.ItemsSource = contacto;
                cmbcontactoaagendar.DisplayMemberPath = "Nombre"; 
                cmbcontactoaagendar.SelectedValuePath = "ID_contacto"; 
            }
        }
     }
  }