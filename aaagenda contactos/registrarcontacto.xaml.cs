using aaagenda_contactos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Page1(bool esModoEdicion)
        {
            InitializeComponent();


            if (esModoEdicion)
            {
                TituloTextBlock.Text = "Modificar Contacto";
                DescripcionTextBlock.Text = "Aquí puede modificar el contacto.";
                Registrar_contacto.Content = "Confirmar Cambios";

                

                DataContext = this;
                CargarTiposContacto();
                CargarRedesSociales();
                CargarTipoTelefono();
            }
        }

        public Page1() : this(false) { }

       

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

        private void ValidateForm()
        {
            bool isFormValid =
                !string.IsNullOrWhiteSpace(txtnombre.Text) &&
                !txtnombre.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)) &&
                !string.IsNullOrWhiteSpace(txtapellido.Text) &&
                !txtapellido.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)) &&
                !string.IsNullOrWhiteSpace(txtemail.Text) &&
                Regex.IsMatch(txtemail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                !string.IsNullOrWhiteSpace(numerotelefono.Text) &&
                !numerotelefono.Text.Any(c => !char.IsDigit(c));

            AddPhoneNumberButton.IsEnabled = isFormValid;
        }


        private void Txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Solo permite letras y espacios
            txtnombre.Text = new string(txtnombre.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            txtnombre.SelectionStart = txtnombre.Text.Length;
            ValidateForm();
        }

        private void Txtapellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Solo permite letras y espacios
            txtapellido.Text = new string(txtapellido.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            txtapellido.SelectionStart = txtapellido.Text.Length;
            ValidateForm();
        }

        private void Txtemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Verificar que tenga un formato de correo electrónico válido
            if (!Regex.IsMatch(txtemail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                txtemail.Background = Brushes.LightPink; // Indicar error
            }
            else
            {
                txtemail.Background = Brushes.White; // Sin error
            }
            ValidateForm();
        }


        private void CmbTipo_contacto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }

        private void Numerotelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto ingresado sin los guiones
            string rawText = new string(numerotelefono.Text.Where(char.IsDigit).ToArray());

            // Limitar el número de dígitos a 10 (máximo 10 caracteres)
            if (rawText.Length > 10)
            {
                rawText = rawText.Substring(0, 10);
            }

            // Formatear el número con guiones, asegurándonos de que no tratemos de acceder a índices fuera de rango
            string formattedText = "";

            if (rawText.Length > 0)
                formattedText += rawText.Substring(0, Math.Min(3, rawText.Length));
            if (rawText.Length > 3)
                formattedText += "-" + rawText.Substring(3, Math.Min(3, rawText.Length - 3));
            if (rawText.Length > 6)
                formattedText += "-" + rawText.Substring(6, Math.Min(4, rawText.Length - 6));

            // Asignar el texto formateado al TextBox, pero mantener el cursor en la posición correcta
            numerotelefono.Text = formattedText;

            // Mantener la posición del cursor al final del texto
            numerotelefono.SelectionStart = numerotelefono.Text.Length;

            // Validar el campo (solo números, con 10 dígitos)
            if (rawText.Length == 10)
            {
                numerotelefono.Background = Brushes.White; // Válido
            }
            else
            {
                numerotelefono.Background = Brushes.LightPink; // No válido
            }

            ValidateForm(); // Validar el formulario
        }

        private void Agregar_tipo_telefono(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {

                var mainFrame = mainWindow.MainFrame;
                var frame4 = mainWindow.Frame6;

                if (mainFrame != null && frame4 != null)
                {

                    mainWindow.ToggleMenu(sender, e);

                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(300),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {
                        mainFrame.Visibility = Visibility.Collapsed;

                        frame4.Navigate(new Page7());

                        frame4.Opacity = 0;
                        frame4.Visibility = Visibility.Visible;

                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1,
                            Duration = TimeSpan.FromMilliseconds(300),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                        };

                        fadeInAnimation.Completed += (s, args) =>
                        {
                            frame4.Opacity = 1;


                        };

                        frame4.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

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
                var tiposRedSocial = context.tipos_red_social.ToList();
                if (tiposRedSocial != null && tiposRedSocial.Any())
                {
                    cmbTipo_red_social.ItemsSource = tiposRedSocial;
                    cmbTipo_red_social.DisplayMemberPath = "Nombre_red_social"; // Asegúrate de que "Nombre" sea la propiedad que deseas mostrar
                    cmbTipo_red_social.SelectedValuePath = "Id_tipo_red_social"; // Asegúrate de que "ID" sea la propiedad que estás usando como valor interno
                }
                else
                {
                    cmbTipo_red_social.ItemsSource = null; // Esto asegurará que el ComboBox esté vacío si no hay datos
                }
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

            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MessageBox.Show("Por favor, complete el nombre.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (isEditMode)  // Verificamos si estamos en modo de edición
            {
                // Si estamos en modo edición, actualizamos el contacto y sus datos
                var contactoExistente = context.contactos
                    .FirstOrDefault(c => c.ID_contacto == contactoSeleccionado.ID_contacto);

                if (contactoExistente != null)
                {
                    // Actualizar los datos del contacto
                    contactoExistente.Nombre = Nombre;
                    contactoExistente.Apellido = Apellido;
                    contactoExistente.Email = Email;
                    contactoExistente.Tipo_Contacto = Tipo_Contacto ?? throw new InvalidOperationException("Tipo_Contacto no puede ser nulo");
                    contactoExistente.Tipo_red_social = Tipo_red_social ?? throw new InvalidOperationException("Tipo_red_social no puede ser nulo");

                    // Actualizar los datos del teléfono
                    var telefonoExistente = context.telefono
                        .FirstOrDefault(t => t.Id_contacto == contactoExistente.ID_contacto);
                    if (telefonoExistente != null)
                    {
                        telefonoExistente.Número_de_teléfono = numeroTelefono;
                        telefonoExistente.Tipo_teléfono = tipoTelefono;
                    }
                    else
                    {
                        // Si no existe teléfono, se crea uno nuevo
                        var nuevoTelefono = new teléfono
                        {
                            Número_de_teléfono = numeroTelefono,
                            Tipo_teléfono = tipoTelefono,
                            Id_contacto = contactoExistente.ID_contacto
                        };
                        context.telefono.Add(nuevoTelefono);
                    }

                    // Actualizar los datos de la red social
                    var redSocialExistente = context.red_sociall
                        .FirstOrDefault(rs => rs.ID_contacto == contactoExistente.ID_contacto);
                    if (redSocialExistente != null)
                    {
                        redSocialExistente.Nombre_de_usuario = Nombre_de_usuario;
                    }
                    else
                    {
                        // Si no existe red social, se crea una nueva
                        var nuevaRedSocial = new red_social
                        {
                            Nombre_de_usuario = Nombre_de_usuario,
                            ID_contacto = contactoExistente.ID_contacto
                        };
                        context.red_sociall.Add(nuevaRedSocial);
                    }

                    // Guardar los cambios
                    context.SaveChanges();
                    MessageBox.Show("Contacto modificado exitosamente.");
                }
                else
                {
                    MessageBox.Show("No se encontró el contacto para modificar.");
                }
            }
            else
            {
                // Si no estamos en modo de edición, se crea un nuevo contacto
                var nuevoContacto = new contacto
                {
                    Nombre = Nombre,
                    Apellido = Apellido,
                    Email = Email,
                    Tipo_Contacto = Tipo_Contacto ?? throw new InvalidOperationException("Tipo_Contacto no puede ser nulo"),
                    Tipo_red_social = Tipo_red_social ?? throw new InvalidOperationException("Tipo_red_social no puede ser nulo")
                };

                using (var dbContext = new MiDbContext())
                {
                    try
                    {
                        dbContext.contactos.Add(nuevoContacto);
                        dbContext.SaveChanges();

                        var nuevoTelefono = new teléfono
                        {
                            Número_de_teléfono = numeroTelefono,
                            Tipo_teléfono = tipoTelefono,
                            Id_contacto = nuevoContacto.ID_contacto
                        };

                        var nuevaRedSocial = new red_social
                        {
                            Nombre_de_usuario = Nombre_de_usuario,
                            ID_contacto = nuevoContacto.ID_contacto
                        };

                        dbContext.telefono.Add(nuevoTelefono);
                        dbContext.red_sociall.Add(nuevaRedSocial);
                        dbContext.SaveChanges();

                        txtnombre.Clear();
                        txtapellido.Clear();
                        txtemail.Clear();
                        cmbTipo_contacto.SelectedItem = null;
                        numerotelefono.Clear();
                        cmbTipo_telefono.SelectedItem = null;
                        cmbTipo_red_social.SelectedItem = null;
                        txtnombreusuario.Clear();
                        MessageBox.Show("Contacto guardado exitosamente.");
                    }
                    catch (DbUpdateException dbEx)
                    {
                        MessageBox.Show($"Error al guardar: {dbEx.InnerException?.Message ?? dbEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error inesperado: {ex.Message}");
                    }
                }
            }
        }

        private contacto contacto;
        // Variable para determinar si estamos en modo edición
        private bool isEditMode = false; // Variable para determinar si estamos en modo edición

        private readonly MiDbContext context;
        private contacto contactoSeleccionado;

        public Page1(contacto contactoSeleccionado, bool isEditMode)
        {
            InitializeComponent();

            context = new MiDbContext();
            this.contactoSeleccionado = contactoSeleccionado;
            this.isEditMode = isEditMode;  // Agrega la variable isEditMode

            // Cargar los datos y configuraciones necesarias
            CargarTiposContacto();
            CargarRedesSociales();
            CargarNombreUsuario();
            CargarTipoTelefono();

            if (isEditMode)
            {
                // Si estamos en modo de edición, cargamos los datos del contacto
                txtnombre.Text = contactoSeleccionado.Nombre;
                txtapellido.Text = contactoSeleccionado.Apellido;
                txtemail.Text = contactoSeleccionado.Email;
                cmbTipo_contacto.SelectedValue = contactoSeleccionado.Tipo_Contacto;
                cmbTipo_red_social.SelectedValue = contactoSeleccionado.Tipo_red_social;

                var telefono = context.telefono
                    .FirstOrDefault(t => t.Id_contacto == contactoSeleccionado.ID_contacto);

                if (telefono != null)
                {
                    numerotelefono.Text = telefono.Número_de_teléfono;
                    // Aquí se asigna el tipo de teléfono al ComboBox
                    foreach (var item in cmbTipo_telefono.Items)
                    {
                        if ((item as ComboBoxItem)?.Content.ToString() == telefono.Tipo_teléfono)
                        {
                            cmbTipo_telefono.SelectedItem = item;
                            break;
                        }
                    }
                }
                else
                {
                    numerotelefono.Text = "No definido";
                }

                // Cambiar el texto del botón a "Modificar contacto"
                Registrar_contacto.Content = "Modificar contacto";
            }
            else
            {
                // Si no estamos en modo de edición, la lógica es la de registrar un nuevo contacto
                Registrar_contacto.Content = "Registrar contacto";
            }
        }



        private void CargarNombreUsuario()
        {
            var redSocial = context.red_sociall
                .FirstOrDefault(rs => rs.ID_contacto == contactoSeleccionado.ID_contacto);

            if (redSocial != null)
            {
                txtnombreusuario.Text = redSocial.Nombre_de_usuario;
            }
            else
            {
                txtnombreusuario.Text = "No definido";
            }
        }


        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contactoEditado = button?.DataContext as contacto;

            if (contactoEditado != null)
            {
                var nuevoNombre = txtnombre.Text;
                var nuevoApellido = txtapellido.Text;
                var nuevoEmail = txtemail.Text;
                var nuevotipocontacto = (int)cmbTipo_contacto.SelectedValue;
                var nuevaredsocial = cmbTipo_red_social.SelectedValue;

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    MessageBox.Show("Por favor, complete el nombre", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var contactoExistente = context.contactos
                    .FirstOrDefault(c => c.ID_contacto == contactoEditado.ID_contacto);

                if (contactoExistente != null)
                {
                    contactoExistente.Nombre = nuevoNombre;
                    contactoExistente.Apellido = nuevoApellido;
                    contactoExistente.Email = nuevoEmail;
                    contactoExistente.Tipo_Contacto = nuevotipocontacto;
                    contactoExistente.Tipo_red_social = (int)nuevaredsocial;

                    context.SaveChanges();

                    // Navegar de vuelta a la página principal (con el DataGrid)
                    NavigationService.Navigate(new Page4());

                    MessageBox.Show("Contacto actualizado correctamente.");
                }
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
            {
            }
        }
    }
