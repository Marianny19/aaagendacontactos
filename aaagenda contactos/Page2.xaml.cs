using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using static MiDbContext;


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

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {
            var Nombre_agenda = txtnombre.Text;
            var ID_contacto = (int?)cmbcontactoaagendar.SelectedValue;
            var Descripcion_agenda = txtdescripcion.Text;
            var Fecha_agendada = txtfecha.SelectedDate;
            var Hora_agendada = DateTime.Parse(txthoraagenda.Text).ToUniversalTime();
            if (string.IsNullOrWhiteSpace(Nombre_agenda)||
            string.IsNullOrWhiteSpace(txthoraagenda.Text)||
            string.IsNullOrWhiteSpace(Descripcion_agenda)||
            Fecha_agendada == null ||
            ID_contacto == null)
            {
                MessageBox.Show("Ninguno de los campos puede estar vacío. Por favor, complete todos los campos.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nuevaAgenda = new agenda
            {
                Nombre_agenda = Nombre_agenda,
                ID_contacto = ID_contacto.Value,
                Descripcion_agenda = Descripcion_agenda,
                Fecha_agendada = Fecha_agendada.Value, 
                Hora_agendada = Hora_agendada
            };

            using (var dbContext = new MiDbContext())
            {
                try
                {
                    dbContext.agendas.Add(nuevaAgenda);
                    dbContext.SaveChanges();
                    txtnombre.Clear();
                    txthoraagenda.Clear();
                    txtfecha.SelectedDate = null; 
                    txtdescripcion.Clear();
                    cmbcontactoaagendar.SelectedItem = null;

                    MessageBox.Show("Agenda guardada exitosamente.");
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

        private void cmbcontactoaagendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
  }