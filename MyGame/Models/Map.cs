using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    public class Map
    {
        public readonly Cell[,] Cells;

        public Map(Cell[,] cells)
        {
            Cells = cells;
        }

        public bool InBounds(Point location)
        {
            return location.X >= 0 && location.X < Cells.GetLength(0) && location.Y >= 0 &&
                   location.Y < Cells.GetLength(1);
        }

        public bool IsWall(Point location)
        {
            return Cells[location.X, location.Y] == Cell.Wall;
        }
    }
}