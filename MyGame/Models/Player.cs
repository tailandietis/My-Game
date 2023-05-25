using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGame
{
    public class Player : PersonBase
    {
        public PlayerName Name { get; }
        public List<Projectile> Projectiles { get; set; } = new List<Projectile>();
        public List<(Projectile, Direction)> ProjectilesInAction { get; set; } = new List<(Projectile, Direction)>();

        public Player(PlayerName name, Point location) : base(location)
        {
            Name = name;
        }

        public override void Move(Game game, Direction direction)
        {
            var newPoint = Location + DirectionAndValue.DirectionsAndValues[direction];
            if (!game.Map.InBounds(newPoint) || game.Map.IsWall(newPoint)) return;
            if (game.Map.Cells[newPoint.X, newPoint.Y] == Cell.Monster)
            {
                HP = 0;
                return;
            }
            
            Location = newPoint;
            if (game.Map.Cells[Location.X, Location.Y] == Cell.Projectile)
            {
               Projectiles.Add(new Projectile(Location));
               game.Map.Cells[Location.X, Location.Y] = Cell.Empty;
               game.ProjectileOnMapCount--;
            }
        }

        public void ApplyAttackingAbility(Direction direction)
        {
            if (!Projectiles.Any()) return;
            var currentProjectile = Projectiles.Last();
            Projectiles.RemoveAt(Projectiles.Count - 1);
            currentProjectile.IsInAction = true;
            currentProjectile.Location = Location;

            if (currentProjectile.IsInAction)
            {
                ProjectilesInAction.Add((currentProjectile, direction));
            }
        }
    }
}