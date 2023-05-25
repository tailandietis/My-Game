using System.Drawing;
using System.Linq;

namespace MyGame
{
    public class Monster: PersonBase
    {
        public Monster(Point location) : base(location)
        {
        }

        public void Hit(Player player)
        {
            if (player.HP < 50)
                player.HP = 0;
            else player.HP -= 50;
        }

        public override void Move(Game game, Direction direction)
        {
            if (direction == Direction.None)
                return;
            var newPoint = Location + DirectionAndValue.DirectionsAndValues[direction];
            if (!game.Map.InBounds(newPoint) || game.Map.IsWall(newPoint) || game.Map.Cells[newPoint.X, newPoint.Y] == Cell.Monster) return;
            game.Map.Cells[Location.X, Location.Y] = Cell.Empty;
            var projectileMurder = game.Player.ProjectilesInAction.FirstOrDefault(e => e.Item1.Location == newPoint);
            if (projectileMurder != default)
            {
                HP -= Projectile.PowerOutput;
                game.Player.ProjectilesInAction.Remove(projectileMurder);
                if (IsDead)
                    return;
            }
            if (game.Map.Cells[newPoint.X, newPoint.Y] == Cell.Projectile)
                game.ProjectileOnMapCount--;
            Location = newPoint;
            game.Map.Cells[newPoint.X, newPoint.Y] = Cell.Monster;
        }
    }
}