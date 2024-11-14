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
            var nombreRedSocial = txtnombre.Text;

            if (string.IsNullOrWhiteSpace(nombreRedSocial))
            {
                MessageBox.Show("El nombre de la red social no puede estar vacío.");
                return;
            }

            var nuevotipo_red_social = new tipo_red_social
            {
                Nombre_red_social = nombreRedSocial
            };

            using (var dbContext = new MiDbContext())
            {
                try
                {
                    dbContext.tipos_red_social.Add(nuevotipo_red_social);
                    dbContext.SaveChanges();
                    txtnombre.Clear();

                    MessageBox.Show("Red social guardada exitosamente.");
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
               
            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
                {

                }



                private void CerrarVentana_Click(object sender, RoutedEventArgs e)
                {
                    var ventana = Window.GetWindow(this) as MainWindow; 
                    if (ventana != null)
                    {
                        var frame = ventana.FindName("Frame4") as Frame;
                        if (frame != null)
                        {
                            ventana.ToggleMenu(sender, e); 

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

                   
                            frame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                        }
                    }
                }

            }
        }
 