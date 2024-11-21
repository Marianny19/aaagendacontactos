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
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                var frame = ventana.FindName("Frame3") as Frame;
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

            // Validar selección de hora y minutos
            var selectedHour = hourComboBox.SelectedItem as ComboBoxItem;
            var selectedMinute = minuteComboBox.SelectedItem as ComboBoxItem;

            if (string.IsNullOrWhiteSpace(Nombre_agenda) ||
                string.IsNullOrWhiteSpace(Descripcion_agenda) ||
                Fecha_agendada == null ||
                ID_contacto == null ||
                selectedHour == null ||
                selectedMinute == null)
            {
                MessageBox.Show("Todos los campos deben estar completos, incluyendo la hora y los minutos.",
                                "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Combinar fecha, hora y minutos en un solo DateTime
            var hora = int.Parse((string)selectedHour.Content);
            var minutos = int.Parse((string)selectedMinute.Content);
            var fechaConHora = Fecha_agendada.Value.AddHours(hora).AddMinutes(minutos);
            var fechaConHoraUtc = DateTime.SpecifyKind(fechaConHora, DateTimeKind.Local).ToUniversalTime();

            var nuevaAgenda = new agenda
            {
                Nombre_agenda = Nombre_agenda,
                ID_contacto = ID_contacto.Value,
                Descripcion_agenda = Descripcion_agenda,
                Fecha_agendada = fechaConHoraUtc

            };

            using (var dbContext = new MiDbContext())
            {
                try
                {
                    dbContext.agendas.Add(nuevaAgenda);
                    dbContext.SaveChanges();

                    // Limpiar campos
                    txtnombre.Clear();
                    txtfecha.SelectedDate = null;
                    txtdescripcion.Clear();
                    cmbcontactoaagendar.SelectedItem = null;
                    hourComboBox.SelectedItem = null;
                    minuteComboBox.SelectedItem = null;

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