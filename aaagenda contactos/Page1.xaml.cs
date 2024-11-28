using aaagenda_contactos;
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
using System.Xml;
using static MiDbContext;

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public List<string> RedSocialItems { get; set; }

        private const int MaxPhoneNumbers = 14;
        public Page1()
        {
            InitializeComponent();
            RedSocialItems = new List<string> { "Facebook", "Twitter", "Instagram" };
            DataContext = this;
            CargarTiposContacto();
            CargarRedesSociales();
            CargarTipoTelefono();




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


        private void AddPhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si ya se alcanzó el límite de 14 TextBox
            if (PhoneNumbersCanvas.Children.Count >= MaxPhoneNumbers)
            {
                MessageBox.Show("Se ha alcanzado el límite de 14 números de teléfono.", "Límite alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mostrar el label "Otros números telefónicos" si es el primer número
            if (PhoneNumbersCanvas.Children.Count == 0)
            {
                AdditionalNumbersLabel.Visibility = Visibility.Visible;

                // Crear animación de opacidad para el label
                var labelFadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(300),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                };
                AdditionalNumbersLabel.BeginAnimation(UIElement.OpacityProperty, labelFadeInAnimation);
            }

            // Crear un StackPanel para contener el TextBox y el botón de eliminación
            StackPanel phoneNumberPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Opacity = 0 // Inicialmente invisible para la animación
            };

            // Crear el TextBox
            TextBox newTextBox = new TextBox
            {
                Width = 190,
                Height = 30,
                Margin = new Thickness(0, 0, 5, 0) // Separación con el botón de eliminación
            };

            // Crear el botón de eliminación
            Button removeButton = new Button
            {
                Content = "-",
                Width = 20,
                Height = 20

            };
            removeButton.Click += (s, args) => RemovePhoneNumber(phoneNumberPanel); // Asociar evento de clic

            // Agregar el TextBox y el botón al StackPanel
            phoneNumberPanel.Children.Add(newTextBox);
            phoneNumberPanel.Children.Add(removeButton);

            // Posicionar el StackPanel en el Canvas
            double topPosition = PhoneNumbersCanvas.Children.Count * 35 - 10;
            Canvas.SetLeft(phoneNumberPanel, 0);
            Canvas.SetTop(phoneNumberPanel, topPosition + 11);

            // Agregar el StackPanel al Canvas
            PhoneNumbersCanvas.Children.Add(phoneNumberPanel);

            // Animación de opacidad para mostrar el StackPanel gradualmente
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            phoneNumberPanel.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        private void RemovePhoneNumber(StackPanel phoneNumberPanel)
        {
            // Crear la animación de opacidad para desvanecer el StackPanel
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            fadeOutAnimation.Completed += (s, args) =>
            {
                // Eliminar el StackPanel del Canvas después de la animación
                PhoneNumbersCanvas.Children.Remove(phoneNumberPanel);

                // Reposicionar los elementos restantes
                for (int i = 0; i < PhoneNumbersCanvas.Children.Count; i++)
                {
                    var childPanel = PhoneNumbersCanvas.Children[i] as StackPanel;
                    if (childPanel != null)
                    {
                        Canvas.SetTop(childPanel, i * 35 - 10 + 11);
                    }
                }

                // Verificar si no quedan más números de teléfono
                if (PhoneNumbersCanvas.Children.Count == 0)
                {
                    // Crear la animación para desvanecer el TextBlock
                    var labelFadeOutAnimation = new DoubleAnimation
                    {
                        From = 1,
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(300),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    labelFadeOutAnimation.Completed += (s, labelArgs) =>
                    {
                        AdditionalNumbersLabel.Visibility = Visibility.Collapsed; // Ocultar el label después de la animación
                    };

                    // Iniciar la animación de desvanecimiento del TextBlock
                    AdditionalNumbersLabel.BeginAnimation(UIElement.OpacityProperty, labelFadeOutAnimation);
                }
            };

            // Iniciar la animación de desvanecimiento del StackPanel
            phoneNumberPanel.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
        private void CargarTiposContacto()
        {
            using (var context = new MiDbContext())
            {
                var tipo_contacto = context.tipos_contacto.ToList();

                cmbTipo_contacto.ItemsSource = tipo_contacto;
                cmbTipo_contacto.DisplayMemberPath = "Nombre_tipo_contacto";
                cmbTipo_contacto.SelectedValuePath = "ID_tipo_contacto";
            }
        }

        private void CargarRedesSociales()
        {
            using (var context = new MiDbContext())
            {
                var tipo_red_social = context.tipos_red_social.ToList();
                cmbTipo_red_social.ItemsSource = tipo_red_social;
                cmbTipo_red_social.DisplayMemberPath = "Nombre_red_social";
                cmbTipo_red_social.SelectedValuePath = "ID_tipo_red_social";
               
            }
        }
        private void CargarTipoTelefono()
        {
            using (var context = new MiDbContext())
            {
                var tipo_red_social = context.tipos_red_social.ToList();
                cmbTipo_telefono.Items.Add(new ComboBoxItem { Content = "Móvil" });
                cmbTipo_telefono.Items.Add(new ComboBoxItem { Content = "Casa" });
                cmbTipo_telefono.Items.Add(new ComboBoxItem { Content = "Trabajo" });

            }
        }
        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {
            var Nombre = txtnombre.Text;
            var Apellido = txtapellido.Text;
            var Email = txtemail.Text;
            var Tipo_Contacto = (int?)cmbTipo_contacto.SelectedValue;
            var numeroTelefono = numerotelefono.Text;
            var tipoTelefono = (cmbTipo_telefono.SelectedItem as ComboBoxItem)?.Content.ToString();
            var Tipo_red_social = (cmbTipo_red_social.SelectedItem as tipo_red_social)?.Id_tipo_red_social;
            var Nombre_de_usuario = txtnombreusuario.Text;
            if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Apellido) ||
            string.IsNullOrWhiteSpace(numeroTelefono) ||
            string.IsNullOrWhiteSpace(Nombre_de_usuario) ||
            string.IsNullOrWhiteSpace(tipoTelefono)||
            Tipo_Contacto == null ||
            Tipo_red_social == null)

            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nuevocontacto = new contacto
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                Tipo_Contacto = Tipo_Contacto ?? throw new InvalidOperationException("Tipo_Contacto no puede ser nulo"),
                Tipo_red_social = Tipo_red_social ?? throw new InvalidOperationException("Tipo_red_social no puede ser nulo")
            };

            var nuevotelefono = new teléfono
            {
                Número_de_teléfono = numeroTelefono,
                Tipo_teléfono = tipoTelefono,
                Id_contacto = nuevocontacto.ID_contacto,
            };
            var nuevaRedsocial = new red_social
            {
                Nombre_de_usuario = Nombre_de_usuario,
                ID_contacto = nuevocontacto.ID_contacto
            };
            
            using (var dbContext = new MiDbContext())
            {
                try
                {
                    dbContext.contactos.Add(nuevocontacto);
                    dbContext.telefono.Add(nuevotelefono);
                    dbContext.SaveChanges();
                    txtnombre.Clear();
                    txtapellido.Clear();
                    txtemail.Clear();
                    cmbTipo_contacto.SelectedItem = null;
                    numerotelefono = null;
                    cmbTipo_telefono.SelectedItem = null;
                    cmbTipo_red_social.SelectedItem = null;
                    txtnombreusuario.Clear();

                    MessageBox.Show("Contacto guardado exitosamente.");
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"Error al guardar : {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}");
                }
            }
        }
    }
}