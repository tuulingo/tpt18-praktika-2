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

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double CellSize = 30;
        const int Cellcount = 16;
        DispatcherTimer timer;
        Snake snake;

        //Direction snakeDirection;

        public MainWindow()
        {

            InitializeComponent();
            DrawBackground();

            snake = new Snake(snakeShape, CellSize, Cellcount);
            snake.Init();
            food();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_tick;
            timer.Start();
        }

        /* private void DirectSnake(Direction direction)
         {
             snakeDirection = direction;
             lblSnakeDirection.Content = $"Direction: {direction}";

         }*/

        private void Timer_tick(object sender, EventArgs e)
        {
            snake.Move();
        }

        public void food()
        {
            Random rnd = new Random();
            int row = rnd.Next(Cellcount);
            int column = rnd.Next(Cellcount);
            int left = rnd.Next(Cellcount);
            int top = rnd.Next(Cellcount);
            for (column = 0; column < Cellcount; column++)
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
                    direction = Direction.Left;
                    break;
                case Key.Right:
                    direction = Direction.Right;
                    break;
                default:
                    return;
            }

            snake.ChangeDirection(direction);

        }
    }
}
