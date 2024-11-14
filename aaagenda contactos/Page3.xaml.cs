using aaagenda_contactos;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Contactos
{
    /// <summary>
    /// Lógica de interacción para Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            this.Resources.Add("BoolToVis", new BoolToVisConverter());
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var frame = mainWindow.Frame3;
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
                        frame.Navigate(new Page2());

                        frame.Visibility = Visibility.Visible;

                        var fadeInAnimation = new DoubleAnimation
                        {
                            To = 1, 
                            Duration = TimeSpan.FromMilliseconds(300), 
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } 
                        };

                        frame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                    };

                    frame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
                    {

                    }

                }
            }
        }




        
        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var dataGrid = sender as DataGrid;
            var selectedItem = dataGrid.SelectedItem;

            {
                Window.GetWindow(this)?.Close();
            }

        }
    }

    
    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
