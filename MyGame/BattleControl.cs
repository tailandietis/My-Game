using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyGame
{
    public partial class BattleControl : UserControl
    {
        private Game _game;
        private readonly Timer timer;
        private static readonly Bitmap Grass = new Bitmap(Image.FromFile("grass.png"));
        private static readonly Bitmap Wall = new Bitmap(Image.FromFile("wall.png"));
        private static readonly Bitmap ProjectileImage = new Bitmap(Image.FromFile("Light.png"));
        private static readonly Bitmap ProjectileOnMapImage = new Bitmap(Image.FromFile("star.png").GetThumbnailImage(Game.CellSize, Game.CellSize, null, IntPtr.Zero));
        private Bitmap Player1;
        private Bitmap[] monstersImages;
        private readonly Random _random = new Random();

        public BattleControl()
        {
            DoubleBuffered = true;
            InitializeComponent();
            timer = new Timer {Interval = 300};
            KeyDown += OnKeyDown;
        }

        public void Configure(Game game)
        {
            if (_game != null)
                return;
            _game = game;
            Player1 = new Bitmap(Image.FromFile($"{_game.Player.Name}.png"));
            monstersImages = Enumerable.Range(1, _game.Monsters.Length)
                .Select(e =>
                    new Bitmap(Image.FromFile($@"monsters\{_random.Next(1, 40)}.png")
                        .GetThumbnailImage(Game.CellSize, Game.CellSize, null, IntPtr.Zero)))
                .ToArray();
            timer.Tick += TimerOnTick;
            timer.Start();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _game.Player.Move(_game, Direction.Up);
                    break;
                case Keys.S:
                    _game.Player.Move(_game, Direction.Down);
                    break;
                case Keys.A:
                    _game.Player.Move(_game, Direction.Left);
                    break;
                case Keys.D:
                    _game.Player.Move(_game, Direction.Right);
                    break;
                case Keys.I:
                    _game.Player.ApplyAttackingAbility(Direction.Up);
                    break;
                case Keys.K:
                    _game.Player.ApplyAttackingAbility(Direction.Down);
                    break;
                case Keys.J:
                    _game.Player.ApplyAttackingAbility(Direction.Left);
                    break;
                case Keys.L:
                    _game.Player.ApplyAttackingAbility(Direction.Right);
                    break;
                default:
                    return;
            }
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            MoveMonster();
            if ((double) _game.ProjectileOnMapCount / _game.ProjectileOnMapMaxCount < 0.3)
                _game.CreateProjectile();
            if (_game.Player.ProjectilesInAction.Any())
                MoveProjictilesInAction();
            if ((double) _game.Monsters.Count(monster => !monster.IsDead) / _game.Monsters.Length < 0.4)
                _game.CreateMonsters();
            Invalidate();
        }

        private void MoveProjictilesInAction()
        {
            foreach (var (projectile, direction) in _game.Player.ProjectilesInAction)
                projectile.Move(_game, direction);
            _game.Player.ProjectilesInAction = _game.Player.ProjectilesInAction.Where(e => e.Item1.IsInAction).ToList();
        }

        private void MoveMonster()
        {
            foreach (var monster in _game.Monsters)
            {
                if (monster.IsDead)
                    continue;
                var distance = Walker.GetDistanceBetweenPoints(monster.Location, _game.Player.Location);
                if (distance <= 4)
                {
                    var nextDirection = WayFinder.FindDirection(_game.Map, monster.Location, _game.Player.Location);
                    if (nextDirection == Direction.None)
                        return;
                    monster.Move(_game, nextDirection);
                }
                else
                {
                    var value = _random.Next(0, 3);
                    var direction = DirectionAndValue.DirectionsAndValues.Keys.ToArray()[value];
                    monster.Move(_game, direction);
                }

                if (monster.Location != _game.Player.Location) continue;
                _game.Player.HP = 0;
                _game.ChangeState(GameState.Result);
                return;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawMap(e);
            DrawScore(e);
            DrawProjectileOnMapCount(e);
            DrawPlayer(e);
            DrawMonsters(e);
            DrawProjectile(e);
        }

        private void DrawScore(PaintEventArgs e)
        {
            e.Graphics.DrawString($"Набрано очков: {_game.Score}", new Font(FontFamily.GenericMonospace, 14),
                new SolidBrush(Color.Red), new Point(_game.Map.Cells.GetLength(0) * Game.CellSize, 30));
        }

        private void DrawProjectileOnMapCount(PaintEventArgs e)
        {
            e.Graphics.DrawString($"Количество снарядов: {_game.Player.Projectiles.Count}",
                new Font(FontFamily.GenericMonospace, 14), new SolidBrush(Color.Green),
                new Point(_game.Map.Cells.GetLength(0) * Game.CellSize, 60));
        }

        private void DrawMonsters(PaintEventArgs e)
        {
            for (var i = 0; i < _game.Monsters.Length; i++)
            {
                var monster = _game.Monsters[i];
                if (monster.IsDead)
                    continue;
                e.Graphics.DrawImage(monstersImages[i], monster.Location.X * Game.CellSize,
                    monster.Location.Y * Game.CellSize);
            }
        }

        private void DrawPlayer(PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                _game.Player.Name == PlayerName.Fire ? Player1 : Player1.GetThumbnailImage(50, 50, null, IntPtr.Zero),
                _game.Player.Location.X * Game.CellSize,
                _game.Player.Location.Y * Game.CellSize,
                _game.Player.Name == PlayerName.Fire ? new Rectangle(237, 125, 50, 50) : new Rectangle(0, 0, 50, 50),
                GraphicsUnit.Pixel);
        }

        private void DrawProjectile(PaintEventArgs e)
        {
            foreach (var (projectile, _) in _game.Player.ProjectilesInAction)
            {
                e.Graphics.DrawImage(ProjectileImage, projectile.Location.X * Game.CellSize,
                    projectile.Location.Y * Game.CellSize);
            }
        }

        private void DrawMap(PaintEventArgs e)
        {
            var map = _game.Map;
            var width = _game.Map.Cells.GetLength(0);
            var height = _game.Map.Cells.GetLength(1);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var location = new Point(x, y);
                    e.Graphics.DrawImage(
                        map.IsWall(location) ? Wall : Grass,
                        location.X * Game.CellSize,
                        y * Game.CellSize);
                    if (map.Cells[location.X, location.Y] == Cell.Projectile)
                        e.Graphics.DrawImage(
                            ProjectileOnMapImage,
                            location.X * Game.CellSize,
                            y * Game.CellSize);
                }
            }
        }
    }
}