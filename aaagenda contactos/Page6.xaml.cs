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
    /// Lógica de interacción para Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public List<string> RedSocialItems { get; set; }
        public Page6(contacto contactoSeleccionado)
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

        private void CargarDatosContacto(contacto contacto)
        {
            // Cargar los datos del contacto en los controles de Page6
            txtnombre.Text = contacto.Nombre;
            txtapellido.Text = contacto.Apellido;
            txtemail.Text = contacto.Email;

            // Cargar tipos de contacto en el ComboBox
            using (var context = new MiDbContext())
            {
                var tiposContacto = context.tipos_contacto.ToList();
                cmbTipo_contacto.ItemsSource = tiposContacto;
                cmbTipo_contacto.DisplayMemberPath = "Nombre_tipo_contacto";
                cmbTipo_contacto.SelectedValuePath = "ID_tipo_contacto";
                cmbTipo_contacto.SelectedValue = contacto.Tipo_Contacto;

                var tiposRedSocial = context.tipos_red_social.ToList();
                cmbTipo_red_social.ItemsSource = tiposRedSocial;
                cmbTipo_red_social.DisplayMemberPath = "Nombre_red_social";
                cmbTipo_red_social.SelectedValuePath = "Id_tipo_red_social";
                cmbTipo_red_social.SelectedValue = contacto.Tipo_red_social;
            }

            // Cargar números de teléfono si es necesario
            // Aquí deberás implementar la lógica para cargar los teléfonos del contacto
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
            string.IsNullOrWhiteSpace(tipoTelefono) ||
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