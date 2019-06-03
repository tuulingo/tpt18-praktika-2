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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ussim2ng
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                double currentLeft = Canvas.GetLeft(rectangle1);
                double newleft = currentLeft + 20;
                Canvas.SetLeft(rectangle1, newleft);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(rectangle1, 20);
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                double currentLeft = Canvas.GetLeft(rectangle1);
                double newleft = currentLeft + 20;
                Canvas.SetLeft(rectangle1, newleft);
            }

            if (e.Key == Key.Down)
            {
                double currentDown = Canvas.GetTop(rectangle1);
                double newDown = currentDown + 20;
                Canvas.SetTop(rectangle1, newDown);
            }

            if (e.Key == Key.Left)
            {
                double currentLeft = Canvas.GetLeft(rectangle1);
                double newleft = currentLeft - 20;
                Canvas.SetLeft(rectangle1, newleft);
            }

            if (e.Key == Key.Up)
            {
                double currentLeft = Canvas.GetTop(rectangle1);
                double newleft = currentLeft - 20;
                Canvas.SetTop(rectangle1, newleft);
            }
        }
    }
}
