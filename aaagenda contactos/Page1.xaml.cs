using aaagenda_contactos;
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

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                // Encontrar el Frame en la ventana principal
                var frame = ventana.FindName("MainFrame") as Frame;
                if (frame != null)
                {
                    // Crear animación de desvanecimiento
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, // Cambiar a opacidad 0
                        Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de easing
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Cambiar la visibilidad del Frame a Collapsed después de que la animación se complete
                        frame.Visibility = Visibility.Collapsed;
                    };

                    // Iniciar la animación de desvanecimiento
                    frame.BeginAnimation(OpacityProperty, fadeOutAnimation);
                }
            }
        }

        private void minimizar(object sender, RoutedEventArgs e)
        {

            Window ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                ventana.WindowState = WindowState.Minimized;
            }
        }
        /* private void Agregar_tipo_contacto(object sender, RoutedEventArgs e)
         {
             Tipo_contacto.Items.Add(txttipocontacto.Text);
         }
         private void Agregar_tipo_red_social(object sender, RoutedEventArgs e)
         {
             Tipo_red_social.Items.Add(txttiporedsocial.Text);
         }

         */

        //Click="Agregar_tipo_contacto"
        //Click="Agregar_tipo_red_social"

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Agregar_tipo_contacto(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                // Encontrar el Frame en la ventana principal
                var mainFrame = mainWindow.MainFrame; // Este es el Frame que contiene Page1
                var frame4 = mainWindow.Frame5; // Este es el Frame que contiene Page4

                if (mainFrame != null && frame4 != null)
                {
                    // Desplegar el menú antes de ocultar el MainFrame
                    mainWindow.ToggleMenu(sender, e); // Llamada al método ToggleMenu en MainWindow

                    // Crear animación de desvanecimiento para ocultar el MainFrame
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, // Reducir la opacidad a 0 (invisible)
                        Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Colapsar el MainFrame al finalizar el desvanecimiento
                        mainFrame.Visibility = Visibility.Collapsed;

                        // Navegar a Page4 en Frame4 antes de ajustar su opacidad y visibilidad
                        frame4.Navigate(new Page5());

                        // Ajustar la opacidad de Frame4 a 0 antes de hacerlo visible
                        frame4.Opacity = 0;
                        frame4.Visibility = Visibility.Visible;

                        // Crear animación de aparición para Frame4
                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1, // Aumentar la opacidad a 1 (completamente visible)
                            Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                        };

                        // Iniciar la animación de aparición
                        fadeInAnimation.Completed += (s, args) =>
                        {
                            // Asegurarse de que la opacidad sea 1 después de la animación
                            frame4.Opacity = 1;

                            // Mostrar overlay para bloquear la interacción

                        };

                        frame4.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

                    // Iniciar la animación de desvanecimiento
                    mainFrame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                }

            }
        }

        private void Agregar_tipo_red_social(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                // Encontrar el Frame en la ventana principal
                var mainFrame = mainWindow.MainFrame; // Este es el Frame que contiene Page1
                var frame4 = mainWindow.Frame4; // Este es el Frame que contiene Page4

                if (mainFrame != null && frame4 != null)
                {
                    // Desplegar el menú antes de ocultar el MainFrame
                    mainWindow.ToggleMenu(sender, e); // Llamada al método ToggleMenu en MainWindow

                    // Crear animación de desvanecimiento para ocultar el MainFrame
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0, // Reducir la opacidad a 0 (invisible)
                        Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        // Colapsar el MainFrame al finalizar el desvanecimiento
                        mainFrame.Visibility = Visibility.Collapsed;

                        // Navegar a Page4 en Frame4 antes de ajustar su opacidad y visibilidad
                        frame4.Navigate(new Page4());

                        // Ajustar la opacidad de Frame4 a 0 antes de hacerlo visible
                        frame4.Opacity = 0;
                        frame4.Visibility = Visibility.Visible;

                        // Crear animación de aparición para Frame4
                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1, // Aumentar la opacidad a 1 (completamente visible)
                            Duration = TimeSpan.FromMilliseconds(300), // Duración de la animación
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // Función de suavizado
                        };

                        // Iniciar la animación de aparición
                        fadeInAnimation.Completed += (s, args) =>
                        {
                            // Asegurarse de que la opacidad sea 1 después de la animación
                            frame4.Opacity = 1;

                            // Mostrar overlay para bloquear la interacción
                           
                        };

                        frame4.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

                    // Iniciar la animación de desvanecimiento
                    mainFrame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                }
            }
        }




    }
}