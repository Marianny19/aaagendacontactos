using Contactos;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<contacto> Contactos { get; set; }
        private bool _isEditing = false;
        private contacto _currentEditingContact;

        public MainWindow()
        {
            InitializeComponent();

            Contactos = new ObservableCollection<contacto>
             {
                 new contacto { Nombre = "Juan", Apellido = "123456789" },
                 new contacto { Nombre = "María", Apellido = "987654321" }
             };

            ContactosDataGrid.ItemsSource = Contactos;
        }

        public bool isMenuOpen = false;

        public void ToggleMenu(object sender, RoutedEventArgs e)
        {
            isMenuOpen = !isMenuOpen;

            SideMenu.Visibility = Visibility.Visible;

            double targetOffset = isMenuOpen ? 0 : -200;

            var slideAnimation = new DoubleAnimation
            {
                To = targetOffset,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            SideMenuTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);

            slideAnimation.Completed += (s, e) =>
            {
                if (!isMenuOpen)
                    SideMenu.Visibility = Visibility.Collapsed;
            };

            var contentOpacityAnimation = new DoubleAnimation
            {
                To = isMenuOpen ? 0 : 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            ContentBorder.BeginAnimation(OpacityProperty, contentOpacityAnimation);

            ContentBorder.IsHitTestVisible = !isMenuOpen;
        }

        

     


        private void ModificarButton_Click(object sender, RoutedEventArgs e)
         {
             if (_isEditing)
             {
                 MessageBox.Show("Por favor, guarda los cambios antes de modificar otro contacto.");
                 return; 
             }

             var button = sender as Button;
             var contacto = button.CommandParameter as contacto;

             if (contacto != null)
             {
                 DataGridRow row = (DataGridRow)ContactosDataGrid.ItemContainerGenerator.ContainerFromItem(contacto);
                 if (row != null)
                 {
                     DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                     if (presenter != null)
                     {
                         for (int i = 0; i < ContactosDataGrid.Columns.Count; i++)
                         {
                             DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                             if (cell != null)
                             {
                                 TextBox textBox = FindVisualChild<TextBox>(cell);
                                 if (textBox != null)
                                 {
                                     textBox.IsEnabled = true; 
                                     textBox.Focus(); 
                                 }
                             }
                         }
                     }
                 }

                 guardar.Visibility = Visibility.Visible;
                 _isEditing = true;
                 _currentEditingContact = contacto; 
             }
         }

         private void GuardarButton_Click(object sender, RoutedEventArgs e)
         {
             if (_currentEditingContact != null)
             {
                 MessageBox.Show($"Guardado: {_currentEditingContact.Nombre}, {_currentEditingContact.Apellido}");

                 guardar.Visibility = Visibility.Collapsed;
                 _isEditing = false; 
                 _currentEditingContact = null; 

                 DataGridRow row = (DataGridRow)ContactosDataGrid.ItemContainerGenerator.ContainerFromItem(_currentEditingContact);
                 if (row != null)
                 {
                     DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                     if (presenter != null)
                     {
                         for (int i = 0; i < ContactosDataGrid.Columns.Count; i++)
                         {
                             DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                             if (cell != null)
                             {
                                 TextBox textBox = FindVisualChild<TextBox>(cell);
                                 if (textBox != null)
                                 {
                                     textBox.IsEnabled = false;
                                 }
                             }
                         }
                     }
                 }
             }
         }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

             for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
             {
                 var child = VisualTreeHelper.GetChild(parent, i);
                 if (child is T tChild)
                 {
                     return tChild;
                 }
                 T childOfChild = GetVisualChild<T>(child);
                 if (childOfChild != null)
                 {
                     return childOfChild;
                 }
             }
             return null;
         }

         private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
         {
             if (parent == null) return null;

             for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
             {
                 var child = VisualTreeHelper.GetChild(parent, i);
                 if (child is T tChild)
                 {
                     return tChild;
                 }

                 T childOfChild = FindVisualChild<T>(child);
                 if (childOfChild != null)
                 {
                     return childOfChild;
                 }
             }
             return null;
         }



   
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ventana = Window.GetWindow(this);
            if (ventana != null)
            {
                var frame = MainFrame;
                if (frame != null)
                {
                    var fadeOutAnimation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(0),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                     fadeOutAnimation.Completed += (s, args) =>
                     {
                         frame.Navigate(new Page1());

                         frame.Visibility = Visibility.Visible;

                         var fadeInAnimation = new DoubleAnimation
                         {
                             To = 1,
                             Duration = TimeSpan.FromMilliseconds(300),
                             EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                         };

                         frame.BeginAnimation(OpacityProperty, fadeInAnimation);
                     };

                     frame.BeginAnimation(OpacityProperty, fadeOutAnimation);
                 }
             }
         }




         private void Border_MouseDown(object sender, MouseButtonEventArgs e)
         {
             if (e.ChangedButton == MouseButton.Left)
             {
                 this.DragMove();
             }
         }

         private bool IsMaximized = false;

         private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
         {
             if (e.ClickCount == 2)
             {
                 if (!IsMaximized)
                 {
                     this.WindowState = WindowState.Maximized;
                     IsMaximized = true;
                 }
                 else
                 {
                     this.WindowState = WindowState.Normal;
                     this.Width = 1080;
                     this.Height = 720;
                     IsMaximized = false;
                 }
             }
         }

         private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
         }

         private void MainFrame_Navigated(object sender, NavigationEventArgs e)
         {

         }
         private void Button_Click_1(object sender, RoutedEventArgs e)
         {
             var ventana = Window.GetWindow(this);
             if (ventana != null)
             {

                 var frame = ventana.FindName("Frame2") as Frame;
                 if (frame != null)
                 {
                     frame.Visibility = Visibility.Visible;
                     frame.Navigate(new Page3());

                 }
             }
         }
         private void Button_Click_2(object sender, RoutedEventArgs e)
         {
             var ventana = Window.GetWindow(this);
             if (ventana != null)
             {
                 var frame = ventana.FindName("Frame2") as Frame;
                 if (frame != null)
                 {
                     frame.Visibility = Visibility.Collapsed;
                 }
             }
         }




        public class contacto
        {
            public string Nombre { get; set; }

            public string Apellido { get; set; }
        }


    }
}