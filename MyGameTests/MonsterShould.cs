using System.Drawing;
using System.Linq;
using FluentAssertions;
using MyGame;
using NUnit.Framework;

namespace MyGameTests
{
    public class MonsterShould
    {
        private Game game;
        [SetUp]
        public void Setup()
        {
            var map = Initializer.CreateMap();
            game = new Game(map,
                new[] {new Monster(new Point(6, 0)), new Monster(new Point(6, 7)), new Monster(new Point(10, 12))})
            {
                Player = new Player(PlayerName.Bird, new Point(0, 0))
            };
        }

        [Test]
        public void Move_NewLocation_WhenDirectionIsNotNone()
        {
            var direction = Direction.Right;
            
            game.Monsters.First().Move(game, direction);

            game.Monsters.First().Location.Should().Be(new Point(7, 0));
        }
        
        [Test]
        public void Move_OldLocation_WhenDirectionNone()
        {
            var direction = Direction.None;
            
            game.Monsters.First().Move(game, direction);

            game.Monsters.First().Location.Should().Be(new Point(6, 0));
        }
        
        [Test]
        public void Move_PreviousCellIsEmpty_WhenDirectionIsNotNone()
        {
            var direction = Direction.Right;
            
            game.Monsters.First().Move(game, direction);

            game.Map.Cells[6, 0].Should().Be(Cell.Empty);
        }
        
        [Test]
        public void Move_CurrentCellIsMonster_WhenDirectionIsNotNone()
        {
            var direction = Direction.Right;
            
            game.Monsters.First().Move(game, direction);

            game.Map.Cells[7, 0].Should().Be(Cell.Monster);
        }
    }
}