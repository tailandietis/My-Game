using System.Drawing;
using System.Linq;

namespace MyGame
{
    public class Projectile
    {
        public Point Location { get; set; }
        public bool IsInAction { get; set; }
        public static int PowerOutput => 100;

        public Projectile(Point location)
        {
            Location = location;
        }

        public void Move(Game game, Direction direction)
        {
            var newLocation = Location + DirectionAndValue.DirectionsAndValues[direction];
            if (!game.Map.InBounds(newLocation) || game.Map.IsWall(newLocation))
            {
                IsInAction = false;
                return;
            }

            if (game.Map.Cells[newLocation.X, newLocation.Y] == Cell.Monster)
            {
                var monster = game.Monsters.First(e => e.Location == newLocation);
                monster.HP -= PowerOutput;
                IsInAction = false;
                if (monster.IsDead)
                {
                    game.Score += game.ScoreForMurder;
                    game.Map.Cells[newLocation.X, newLocation.Y] = Cell.Empty;
                }
            }
            else
                Location = newLocation;
        }
    }
}