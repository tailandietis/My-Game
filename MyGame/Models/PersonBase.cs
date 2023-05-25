using System.Drawing;

namespace MyGame
{
    public abstract class PersonBase
    {
        public Point Location { get; set; }
        public int HP { get; set; } = 100;
        public bool IsDead => HP <= 0;

        public PersonBase(Point location)
        {
            Location = location;
        }

        public abstract void Move(Game game, Direction direction);
    }
}