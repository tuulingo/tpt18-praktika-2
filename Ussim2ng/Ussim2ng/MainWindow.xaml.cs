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
        Direction direction;
        Random rnd = new Random();
        Direction snakeDirection;
        GameStatus gamestatus;

        int foodRow;
        int foodCol;

        int snakeheadRow;
        int snakeheadCol;

        int points = 0;
        LinkedList<Rectangle> snakeParts = new LinkedList<Rectangle>();

        public MainWindow()
        {

            InitializeComponent();
            DrawBackground();

            InitSnake();
            InitFood();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += Timer_tick;
            timer.Start();
            ChangePoints(0);

            rnd = new Random();

            ChangeGameStatus(GameStatus.Ongoing);
        }


        private void InitSnake()
        {

            int index = Cellcount / 2;

            for (int i = 0; i < 3; i++)
            {
                int row = index;
                int col = index + i;

                Rectangle r = new Rectangle();
                r.Height = CellSize;
                r.Width = CellSize;
                r.Fill = Brushes.MidnightBlue;
                Panel.SetZIndex(r, 10);
                SetShape(r, row, index + i);

                SetShape(r, row, col);
                board.Children.Add(r);
                snakeParts.AddLast(r);

                if (i == 0)
                {
                    snakeheadRow = row;
                    snakeheadCol = col;
                }

            }

            ChangeSnakeDirection(Direction.Up);
        }

        private void InitFood()
        {
            foodShape.Height = CellSize;
            foodShape.Width = CellSize;

            foodRow = rnd.Next(0, Cellcount);
            foodCol = rnd.Next(0, Cellcount);
            SetShape(foodShape, foodRow, foodCol);
        }

        private void Eat()
        {

            /*   LinkedList<int> b = new LinkedList<int>();
               b.AddLast(1);
               b.AddLast(2);
               int temp = b.Last();

               for (temp = 0; temp == points; temp++ )
               {
                   Ellipse saba = new Ellipse();
                   saba.Fill = Brushes.MidnightBlue;
                   if(snakePart == foodShape)
                   {
                       b.AddLast(saba);
                   }
               }

               temp = b.Last();*/

        }


        private void ChangePoints(int newPoints)
        {
            points = newPoints;
            lblPoints.Content =
                $"Points: {points}";
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
            Rectangle newHead = snakeParts.Last.Value;
            snakeParts.RemoveLast();

            SetShape(newHead, snakeheadRow, snakeheadCol);

            
            switch (snakeDirection)
            {
                case Direction.Up:
                    snakeheadRow--;
                    snakeParts.AddFirst(newHead);
                    break;
                case Direction.Down:
                    snakeheadRow++;
                    snakeParts.AddFirst(newHead);
                    break;
                case Direction.Left:
                    snakeheadCol--;
                    snakeParts.AddFirst(newHead);
                    break;
                case Direction.Right:
                    snakeheadCol++;
                    snakeParts.AddFirst(newHead);
                    break;
                default:
                    return;
            }
            /*
            

            if (snakePart.Row < 0 || snakePart.Row >= 16 || snakePart.Col < 0 || snakePart.Col >= 16)
            {

                ChangeGameStatus(GameStatus.GameOver);
                return;

            }
            SetShape(snakeShape, snakePart.Row, snakePart.Col);

            bool food = snakePart.Row == foodRow && snakePart.Col == foodCol;
            if (food)
            {
                ChangePoints(points + 1);
                InitFood();
            }*/

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
                    SetShape(r, col, row);
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

                        if (e.Key == Key.Up)
                        {
                            if (direction == Direction.Down)
                            {
                                direction = Direction.Down;
                            }

                            else
                            {
                                direction = Direction.Up;
                            }

                        }

                        if (e.Key == Key.Up)
                        {
                            if (direction == Direction.Down)
                            {
                                direction = Direction.Down;
                            }

                            else
                            {
                                direction = Direction.Up;
                            }

                        }

                        if (e.Key == Key.Down)
                        {
                            if (direction == Direction.Up)
                            {
                                direction = Direction.Up;
                            }

                            else
                            {
                                direction = Direction.Down;
                            }

                        }

                        if (e.Key == Key.Right)
                        {
                            if (direction == Direction.Left)
                            {
                                direction = Direction.Left;
                            }

                            else
                            {
                                direction = Direction.Right;
                            }

                        }

                        if (e.Key == Key.Left)
                        {
                            if (direction == Direction.Right)
                            {
                                direction = Direction.Right;
                            }

                            else
                            {
                                direction = Direction.Left;
                            }

                        }

                        return;
                }

                ChangeSnakeDirection(direction);
            }

            

        }


    }
}
