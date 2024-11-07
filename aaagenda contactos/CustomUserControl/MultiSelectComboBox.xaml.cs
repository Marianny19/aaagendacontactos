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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aaagenda_contactos.CustomUserControl
{
    /// <summary>
    /// Lógica de interacción para MultiSelectComboBox.xaml
    /// </summary>
    public partial class MultiSelectComboBox : UserControl
    {
        public List<string> ItemsSource
        {
            get { return (List<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<string>), typeof(MultiSelectComboBox), new PropertyMetadata(new List<string>()));

        public string SelectedText
        {
            get { return (string)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        public static readonly DependencyProperty SelectedTextProperty =
            DependencyProperty.Register("SelectedText", typeof(string), typeof(MultiSelectComboBox), new PropertyMetadata(string.Empty));

        public MultiSelectComboBox()
        {
            InitializeComponent();
            ListBoxItems.ItemsSource = ItemsSource;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ListBoxItems.SelectedItems.Cast<string>().ToList();
            SelectedText = string.Join(", ", selectedItems);
        }
    }
}