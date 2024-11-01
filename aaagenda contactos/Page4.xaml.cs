﻿using System;
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

namespace aaagenda_contactos
{
    /// <summary>
    /// Lógica de interacción para Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new MiDbContext())
            {
                try
                {
                   
                    var nuevared_social = new red_social
                    {
                        Nombre_de_usuario = ""
                    };

               
                    dbContext.RedesSociales.Add(nuevared_social);
 
                    dbContext.SaveChanges();

                    
                    MessageBox.Show("Red social guardada exitosamente.");
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"Error al guardar red social: {ex.Message}");
                }
            }

            
            

        }
            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var ventana = Window.GetWindow(this) as MainWindow; // Asegúrate de que sea MainWindow
            if (ventana != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = ventana.FindName("Frame4") as Frame;
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
