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
using System.Windows.Threading;

namespace Ussim2ng
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double CellSize = 30;
        const int Cellcount = 16;
        DispatcherTimer timer;

        Direction snakeDirection;

        public MainWindow()
        {

            InitializeComponent();
            DrawBackground();
            initsnake();
            food();
            Canvas.SetTop(snake, 0);
            Canvas.SetLeft(snake, 0);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_tick;
            timer.Start();
        }

        private void DirectSnake(Direction direction)
        {
            snakeDirection = direction;
            lblSnakeDirection.Content = $"Direction: {direction}";

        }

        private void Timer_tick(object sender, EventArgs e)
        {
            MoveSnake(snakeDirection);
        }

        public void MoveSnake(Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Down)
            {
                    double CurrentTop = Canvas.GetTop(snake);
                    double Newtop = direction == Direction.Up
                        ? CurrentTop - CellSize
                        : CurrentTop + CellSize;
                    Canvas.SetTop(snake, Newtop);
            }

            if (direction == Direction.Left || direction == Direction.Right)
            {
                double Currentleft = Canvas.GetLeft(snake);
                double Newleft = direction == Direction.Left
                    ? Currentleft + CellSize
                    : Currentleft - CellSize;
                Canvas.SetLeft(snake, Newleft);
            }
        }

        public void food()
        {
            Random rnd = new Random();
            int row = rnd.Next(Cellcount);
            int column = rnd.Next(Cellcount);
            int left = rnd.Next(Cellcount);
            int top = rnd.Next(Cellcount);
            for (column= 0; column < Cellcount; column++)
            {
                for (row = 0; row < Cellcount; row++)
                {
                    Ellipse food = new Ellipse();
                    food.Width = CellSize;
                    food.Height = CellSize;
                    food.Fill = Brushes.Red;
                    Canvas.SetLeft(food, left * CellSize);
                    Canvas.SetTop(food, top * CellSize);
                    board.Children.Add(food);


                }
            }
        }

        private void initsnake()
        {
            snake.Width = CellSize;
            snake.Height = CellSize;
            double coord = Cellcount * CellSize / 2;
            Canvas.SetTop(snake, coord);
            Canvas.SetLeft(snake,coord);

            DirectSnake(Direction.Down);
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
            Direction direction;
            switch (e.Key)
            {
                case Key.Up:
                    direction = Direction.Up;
                    break;
                case Key.Down:
                    direction = Direction.Down;
                    break;
                case Key.Left:
                    direction = Direction.Right;
                    break;
                case Key.Right:
                    direction = Direction.Left;
                    break;
                default:
                    return;
            }

            snakeDirection = direction;

        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

    }
}
