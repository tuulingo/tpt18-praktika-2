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

                Rectangle r = InitRectangle(CellSize, row, col, Brushes.MidnightBlue, 10);

                snakeParts.AddLast(r);

            }

            ChangeSnakeDirection(Direction.Left);
        }

        private void InitFood()
        {
            foodShape.Height = CellSize;
            foodShape.Width = CellSize;

            foodRow = rnd.Next(0, Cellcount);
            foodCol = rnd.Next(0, Cellcount);
            SetShape(foodShape, foodRow, foodCol);
            Panel.SetZIndex(foodShape, 11);
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
            {
                if (snakeDirection == Direction.Left &&
                   direction == Direction.Right)
                {
                    return;
                }

                if (snakeDirection == Direction.Right &&
                    direction == Direction.Left)
                {
                    return;
                }

                if (snakeDirection == Direction.Up &&
                    direction == Direction.Down)
                {
                    return;
                }

                if (snakeDirection == Direction.Down &&
                    direction == Direction.Up)
                {
                    return;
                }

                snakeDirection = direction;
                lblSnakeDirection.Content =
                    $"Direction: {direction}";
            }
        }

        private void MoveSnake()
        {
            Rectangle currentHead = snakeParts.First.Value;
            Location currentHeadLocation =
                (Location)currentHead.Tag;

            int newHeadRow = currentHeadLocation.Row;
            int newHeadCol = currentHeadLocation.Col;
    
            switch (snakeDirection)
            {
                case Direction.Up:
                    newHeadRow--;
                    break;
                case Direction.Down:
                    newHeadRow++;
                    break;
                case Direction.Left:
                    newHeadCol--;
                    break;
                case Direction.Right:
                    newHeadCol++;
                    break;
            }

            bool outOfBoundaries =
                newHeadRow < 0 || newHeadRow >= Cellcount ||
                newHeadCol < 0 || newHeadCol >= Cellcount;
            if (outOfBoundaries)
            {
                ChangeGameStatus(GameStatus.GameOver);
                return;
            }

            bool food =
                newHeadRow == foodRow &&
               newHeadCol == foodCol;

            foreach (Rectangle r in snakeParts)
            {
                if (!food && snakeParts.Last.Value == r)
                {
                    continue;
                }

                Location location = (Location)r.Tag;
                if (location.Row == newHeadRow &&
                   location.Col == newHeadCol)
                {
                    ChangeGameStatus(GameStatus.GameOver);
                    return;
                }
                
            }

            if (food)
            {
                ChangePoints(points + 1);
                InitFood();

                Rectangle r = InitRectangle(
                   CellSize,
                   newHeadRow,
                   newHeadCol,
                   Brushes.MidnightBlue,
                   10);
                snakeParts.AddFirst(r);
            }
            else
            {
                Rectangle newHead = snakeParts.Last.Value;
                newHead.Tag = new Location(newHeadRow, newHeadCol);

                SetShape(newHead, newHeadRow, newHeadCol);
                snakeParts.RemoveLast();
                snakeParts.AddFirst(newHead);
            }

        }

        private Rectangle InitRectangle(
            double size,
            int row,
            int col,
            Brush fill,
            int zIndex)
        {
            Rectangle r = new Rectangle();
            r.Height = size;
            r.Width = size;
            r.Fill = fill;
            Panel.SetZIndex(r, zIndex);
            r.Tag = new Location(row, col);

            SetShape(r, row, col);
            board.Children.Add(r);

            return r;
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

                    InitRectangle(
                        CellSize, row, col, color, 0);

                    color = color == color1 ? color2 : color1;
                }
            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
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
