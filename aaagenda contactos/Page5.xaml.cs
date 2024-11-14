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
            var ventana = Window.GetWindow(this) as MainWindow; 
            if (ventana != null)
            {
                
                var frame = ventana.FindName("Frame5") as Frame;
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
