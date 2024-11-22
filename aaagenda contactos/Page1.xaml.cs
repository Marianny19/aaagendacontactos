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
        public List<string> RedSocialItems { get; set; }

        private const int MaxPhoneNumbers = 14;
        public Page1()
        {
            InitializeComponent();

            RedSocialItems = new List<string> { "Facebook", "Twitter", "Instagram" };
            DataContext = this;
            //CargarTiposContacto();
            //CargarRedesSociales();

        }
        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {

            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {

                var frame = ventana.FindName("MainFrame") as Frame;
                if (frame != null)
                {

                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(300),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    fadeOutAnimation.Completed += (s, args) =>
                    {

                        frame.Visibility = Visibility.Collapsed;
                    };


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

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Agregar_tipo_contacto(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {

                var mainFrame = mainWindow.MainFrame;
                var frame4 = mainWindow.Frame5;

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

                        frame4.Navigate(new Page5());

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


        private void Agregar_tipo_red_social(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var mainFrame = mainWindow.MainFrame;
                var frame4 = mainWindow.Frame4;

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

                        frame4.Navigate(new Page4());

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
            if (PhoneNumbersCanvas.Children.Count >= MaxPhoneNumbers)
            {
                MessageBox.Show("Se ha alcanzado el límite de 14 números de teléfono.", "Límite alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (PhoneNumbersCanvas.Children.Count == 0)
            {
                AdditionalNumbersLabel.Visibility = Visibility.Visible;

                var labelFadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(300),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                };
                AdditionalNumbersLabel.BeginAnimation(UIElement.OpacityProperty, labelFadeInAnimation);
            }

            StackPanel phoneNumberPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Opacity = 0
            };

            TextBox newTextBox = new TextBox
            {
                Width = 190,
                Height = 30,
                Margin = new Thickness(0, 0, 5, 0)
            };

            Button removeButton = new Button
            {
                Content = "-",
                Width = 20,
                Height = 20

            };
            removeButton.Click += (s, args) => RemovePhoneNumber(phoneNumberPanel);

            phoneNumberPanel.Children.Add(newTextBox);
            phoneNumberPanel.Children.Add(removeButton);

            double topPosition = PhoneNumbersCanvas.Children.Count * 35 - 10;
            Canvas.SetLeft(phoneNumberPanel, 0);
            Canvas.SetTop(phoneNumberPanel, topPosition + 11);

            PhoneNumbersCanvas.Children.Add(phoneNumberPanel);

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
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            fadeOutAnimation.Completed += (s, args) =>
            {
                PhoneNumbersCanvas.Children.Remove(phoneNumberPanel);

                for (int i = 0; i < PhoneNumbersCanvas.Children.Count; i++)
                {
                    var childPanel = PhoneNumbersCanvas.Children[i] as StackPanel;
                    if (childPanel != null)
                    {
                        double targetTop = i * 35 - 10 + 11;

                        var moveUpAnimation = new DoubleAnimation
                        {
                            To = targetTop,
                            Duration = TimeSpan.FromMilliseconds(150),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                        };

                        childPanel.BeginAnimation(Canvas.TopProperty, moveUpAnimation);
                    }
                }

                if (PhoneNumbersCanvas.Children.Count == 0)
                {
                    var labelFadeOutAnimation = new DoubleAnimation
                    {
                        From = 1,
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(300),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    labelFadeOutAnimation.Completed += (s, labelArgs) =>
                    {
                        AdditionalNumbersLabel.Visibility = Visibility.Collapsed;
                    };

                    AdditionalNumbersLabel.BeginAnimation(UIElement.OpacityProperty, labelFadeOutAnimation);
                }
            };

            phoneNumberPanel.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
        /*private void CargarTiposContacto()
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
        }*/

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {
            var Nombre = txtnombre.Text;
            var Apellido = txtapellido.Text;
            var Email = txtemail.Text;
            var Tipo_Contacto = (int?)cmbTipo_contacto.SelectedValue;
            var numeroTelefono = numerotelefono.ToString();
            var Tipo_telefono = (cmbTipo_telefono.SelectedItem as teléfono)?.Tipo_teléfono;
            var Tipo_red_social = (cmbTipo_red_social.SelectedItem as tipo_red_social)?.Id_tipo_red_social;
            var Nombre_de_usuario = txtnombreusuario.Text;
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Apellido) ||
                string.IsNullOrWhiteSpace(numeroTelefono) ||
                string.IsNullOrWhiteSpace(Nombre_de_usuario)||
                (string.IsNullOrWhiteSpace(Tipo_telefono))
                )
            {
                MessageBox.Show("Ninguno de los campos puede estar vacío. Por favor, complete todos los campos.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

//        private void CargarTiposContacto()
//        {
//            using (var context = new MiDbContext())
//            {
//                var tipo_contacto = context.tipos_contacto.ToList();

//                cmbTipo_contacto.ItemsSource = tipo_contacto;
//                cmbTipo_contacto.DisplayMemberPath = "Nombre_tipo_contacto";
//                cmbTipo_contacto.SelectedValuePath = "ID_tipo_contacto";
//            }
//        }
//    }
//}