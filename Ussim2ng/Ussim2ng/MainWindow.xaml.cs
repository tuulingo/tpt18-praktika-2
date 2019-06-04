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

namespace Ussim2ng
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double CellSize = 30;
        const int Cellcount = 16;

        public MainWindow()
        {
            InitializeComponent();

            test();
        }

        public void test()
        {

            for (int y = 0; y < Cellcount; y++)
            {
                for (int x = 0; x < Cellcount; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0 || x % 2 == 1 && y % 2 == 1)
                    { 
                        Rectangle r1 = new Rectangle();
                        r1.Height = CellSize;
                        r1.Width = CellSize;
                        r1.Fill = Brushes.LimeGreen;
                        Canvas.SetTop(r1, x * CellSize);
                        Canvas.SetLeft(r1, y * CellSize);
                        board.Children.Add(r1);
                    }

                }

            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                double currentLeft = Canvas.GetLeft(rectangle1);
                double newleft = currentLeft + 30;
                Canvas.SetLeft(rectangle1, newleft);
            }

            if (e.Key == Key.Down)
            {
                double currentDown = Canvas.GetTop(rectangle1);
                double newDown = currentDown + 30;
                Canvas.SetTop(rectangle1, newDown);
            }

            if (e.Key == Key.Left)
            {
                double currentLeft = Canvas.GetLeft(rectangle1);
                double newleft = currentLeft - 30;
                Canvas.SetLeft(rectangle1, newleft);
            }

            if (e.Key == Key.Up)
            {
                double currentLeft = Canvas.GetTop(rectangle1);
                double newleft = currentLeft - 30;
                Canvas.SetTop(rectangle1, newleft);
            }
        }
    }
}
