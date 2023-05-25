using System.Drawing;
using FluentAssertions;
using MyGame;
using NUnit.Framework;

namespace MyGameTests
{
    public class PlayerShould
    {
        private Game game;
        [SetUp]
        public void Setup()
        {
            var map = Initializer.CreateMap();
            game = new Game(map,
                new[] {new Monster(new Point(6, 0)), new Monster(new Point(6, 7)), new Monster(new Point(10, 12))})
            {
                Player = new Player(PlayerName.Bird, new Point(6, 0))
            };
        }

        [Test]
        public void Move_NewLocation_WhenDirectionNextPointIsEmpty()
        {
            var direction = Direction.Right;
            
            game.Player.Move(game, direction);

            game.Player.Location.Should().Be(new Point(7, 0));
        }
        
        [Test]
        public void Move_OldLocation_WhenDirectionNextPointIsWall()
        {
            var direction = Direction.Down;
            
            game.Player.Move(game, direction);

            game.Player.Location.Should().Be(new Point(6, 0));
        }
        
        [Test]
        public void ApplyAttackingAbility_IncreasedProjectileInActionCount_WhenSimpleDirection()
        {
            game.Player.Projectiles.Add(new Projectile(game.Player.Location));
            var direction = Direction.Right;
            
            game.Player.ApplyAttackingAbility(direction);

            game.Player.ProjectilesInAction.Should().HaveCount(1);
        }
        
        [Test]
        public void ApplyAttackingAbility_DecreasedProjectileInActionCount_WhenSimpleDirection()
        {
            game.Player.Projectiles.Add(new Projectile(game.Player.Location));
            var direction = Direction.Right;
            
            game.Player.ApplyAttackingAbility(direction);

            game.Player.Projectiles.Should().HaveCount(0);
        }
    }
}