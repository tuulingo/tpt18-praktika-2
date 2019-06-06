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

        public void ChangeDirection(Direction newDirection)
        {
            direction = newDirection;
        }


    }
}