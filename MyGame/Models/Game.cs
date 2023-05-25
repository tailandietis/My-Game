using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGame
{
    public class Game
    {
        private GameState state = GameState.Start;
        public const int CellSize = 50;
        public event Action<GameState> ChangedState;
        
        public Map Map;
        public Player Player;
        public Monster[] Monsters;
        public int ProjectileOnMapCount { get; set; }
        public int ProjectileOnMapMaxCount { get; }
        public readonly Random Random = new Random();
        public int Score { get; set; }
        public int ScoreForMurder { get; }

        public Game(Map map, Monster[] monsters, int projectileOnMapMaxCount = 7, int scoreForMurder = 38)
        {
            Map = map;
            ProjectileOnMapMaxCount = projectileOnMapMaxCount;
            CreateProjectile();
            Monsters = monsters;
            ScoreForMurder = scoreForMurder;
        }

        public void CreateProjectile()
        {
            var emptyCells = GetEmptyCells();
            while (ProjectileOnMapCount != ProjectileOnMapMaxCount && emptyCells.Count != 0)
            {
                var nextCell = Random.Next(0, emptyCells.Count-1);
                Map.Cells[emptyCells[nextCell].Item1, emptyCells[nextCell].Item2] = Cell.Projectile;
                emptyCells.RemoveAt(nextCell);
                ProjectileOnMapCount++;
            }
        }
        
        public void CreateMonsters()
        {
            var emptyCells = GetEmptyCells().Where(location => Walker.GetDistanceBetweenPoints(Player.Location, new Point(location.Item1, location.Item2)) >= 5).ToList();
            var aliveMonstersCount = Monsters.Count(monster => !monster.IsDead);
            while (aliveMonstersCount != Monsters.Length && emptyCells.Count != 0)
            {
                var nextCell = Random.Next(0, emptyCells.Count - 1);
                Map.Cells[emptyCells[nextCell].Item1, emptyCells[nextCell].Item2] = Cell.Monster;
                var monster = Monsters.First(e => e.IsDead);
                monster.Location = new Point(emptyCells[nextCell].Item1, emptyCells[nextCell].Item2);
                monster.HP = 100;
                emptyCells.RemoveAt(nextCell);
                aliveMonstersCount++;
            }
        }

        private List<(int, int)> GetEmptyCells()
        {
            var emptyCells = new List<(int, int)>();
            var width = Map.Cells.GetLength(0);
            var height = Map.Cells.GetLength(1);
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
            {
                if (Map.Cells[i, j] == Cell.Empty)
                    emptyCells.Add((i, j));
            }

            return emptyCells;
        }

        public void ChangeState(GameState gameState)
        {
            state = gameState;
            ChangedState?.Invoke(gameState);
        }

        public void CreatePlayer(Player player)
        {
            Player = player;
        }
    }
}