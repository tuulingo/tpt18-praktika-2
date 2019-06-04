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
            DrawBackground();
            initsnake();
            Canvas.SetTop(snake, 0);
            Canvas.SetLeft(snake, 0);
        }

        public void MoveSnake(bool up, bool down, bool right, bool left)
        {
            if (up || down)
            {
                    double CurrentTop = Canvas.GetTop(snake);
                    double Newtop = up
                        ? CurrentTop - CellSize
                        : CurrentTop + CellSize;
                    Canvas.SetTop(snake, Newtop);
            }

            if (left || right)
            {
                double Currentleft = Canvas.GetLeft(snake);
                double Newleft = left
                    ? Currentleft + CellSize
                    : Currentleft - CellSize;
                Canvas.SetLeft(snake, Newleft);
            }
        }

        public void food()
        {
            Random rnd = new Random();

            for (int y = 0; y < 1; y++)
            {
                for (int x = 0; x < 1; x++)
                {
                    Ellipse food = new Ellipse();
                    food.Width = CellSize;
                    food.Height = CellSize;

                }
            }
        }

        private void initsnake()
        {
            snake.Width = CellSize;
            snake.Height = CellSize;
            Canvas.SetTop(snake, Cellcount / 2);
            Canvas.SetLeft(snake, Cellcount / 2);
        }


        public void DrawBackground()
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
            bool up = e.Key == Key.Up;
            bool down = e.Key == Key.Down;
            bool left = e.Key == Key.Left;
            bool right = e.Key == Key.Right;

            MoveSnake(up, down, left, right);

        }
    }
}
