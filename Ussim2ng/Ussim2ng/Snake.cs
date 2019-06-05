using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SnakeGame
{
    class Snake
    {
        Shape snakeShape;
        double CellSize;
        int Cellcount;

        Direction direction;

        public Snake(
            Shape snakeShape,
            double CellSize,
            int Cellcount)
        {
            this.snakeShape = snakeShape;
            this.CellSize = CellSize;
            this.Cellcount = Cellcount;
        }

        public void Init()
        {
            snakeShape.Height = CellSize;
            snakeShape.Width = CellSize;
            double coord = Cellcount * CellSize / 2;
            Canvas.SetTop(snakeShape, coord);
            Canvas.SetLeft(snakeShape, coord);

            ChangeDirection(Direction.Up);
        }

        public void ChangeDirection(Direction newDirection)
        {
            direction = newDirection;
        }

        public void Move()
        {
            if (direction == Direction.Up ||
               direction == Direction.Down)
            {
                double currentTop = Canvas.GetTop(snakeShape);
                double newTop = direction == Direction.Up
                    ? currentTop - CellSize
                    : currentTop + CellSize;
                Canvas.SetTop(snakeShape, newTop);
            }

            if (direction == Direction.Left ||
                direction == Direction.Right)
            {
                double currentLeft = Canvas.GetLeft(snakeShape);
                double newLeft = direction == Direction.Left
                    ? currentLeft - CellSize
                    : currentLeft + CellSize;
                Canvas.SetLeft(snakeShape, newLeft);
            }
        }

        public void borders()
        {
            if ()
        }
    }
}