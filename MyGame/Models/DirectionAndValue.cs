using System.Collections.Generic;
using System.Drawing;

namespace MyGame
{
    public class DirectionAndValue
    {
        public static readonly Dictionary<Direction, Size> DirectionsAndValues
            = new Dictionary<Direction, Size>
            {
                [Direction.Up] = new Size(0, -1),
                [Direction.Down] = new Size(0, 1),
                [Direction.Left] = new Size(-1, 0),
                [Direction.Right] = new Size(1, 0)
            };
    }
}