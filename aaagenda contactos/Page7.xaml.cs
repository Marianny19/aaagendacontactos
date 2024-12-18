﻿using System;
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

namespace aaagenda_contactos
{
    /// <summary>
    /// Lógica de interacción para Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public Page7()
        {
            InitializeComponent();
        }

        private void Registrar_contacto_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            var ventana = Window.GetWindow(this) as MainWindow;
            if (ventana != null)
            {

                var frame = ventana.FindName("Frame6") as Frame;
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
