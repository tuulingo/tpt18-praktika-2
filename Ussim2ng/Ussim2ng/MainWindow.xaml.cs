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

        Direction snakeDirection;
        GameStatus gamestatus;
        int snakeRow;
        int snakeCol;


        public MainWindow()
        {

            InitializeComponent();
            DrawBackground();

            InitSnake();
            food();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_tick;
            timer.Start();

            ChangeGameStatus(GameStatus.Ongoing);
        }

        private void InitSnake()
        {
            snakeShape.Height = CellSize;
            snakeShape.Width = CellSize;
            SetShape(snakeShape, snakeRow, snakeCol - 4);
            int index = Cellcount / 2;
            snakeRow = index;
            snakeCol = index;

            ChangeSnakeDirection(Direction.Up);
        }

        private void ChangeGameStatus(GameStatus newGameStatus)
        {
            gamestatus = newGameStatus;
            lblGameStatus.Content =
                $"Status: {gamestatus}";
        }

        private void ChangeSnakeDirection(Direction direction)
        {
            snakeDirection = direction;
            lblSnakeDirection.Content =
                $"Direction: {direction}";
        }

        private void MoveSnake()
        {
            switch (snakeDirection)
            {
                case Direction.Up:
                    snakeRow--;
                    break;
                case Direction.Down:
                    snakeRow++;
                    break;
                case Direction.Left:
                    snakeCol--;
                    break;
                case Direction.Right:
                    snakeCol++;
                    break;
                default:
                    return;
            }

            

            if (snakeRow < 0 || snakeRow >= 16 || snakeCol < 0 || snakeCol >= 16)
            {

                ChangeGameStatus(GameStatus.GameOver);
                return;

            }
            SetShape(snakeShape, snakeRow, snakeCol);

        }

        private void SetShape(Shape shape, int row, int col)
        {
            double top = row * CellSize;
            double left = col * CellSize;

            Canvas.SetTop(shape, top);
            Canvas.SetLeft(shape, left);
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            if (gamestatus != GameStatus.Ongoing)
            {
                return;
            }

            MoveSnake();
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
            SolidColorBrush color1 = Brushes.LightGreen;
            SolidColorBrush color2 = Brushes.LimeGreen;

            for (int row = 0; row < Cellcount; row++)
            {
                SolidColorBrush color =
                    row % 2 == 0 ? color1 : color2;

                for (int col = 0; col < Cellcount; col++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = CellSize;
                    r.Height = CellSize;
                    r.Fill = color;
                    Canvas.SetTop(r, row * CellSize);
                    Canvas.SetLeft(r, col * CellSize);
                    board.Children.Add(r);

                    color = color == color1 ? color2 : color1;
                }
            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            {
                if (gamestatus != GameStatus.Ongoing)
                {
                    return;
                }

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
                ChangeSnakeDirection(direction);
            }

            

        }


    }
}
