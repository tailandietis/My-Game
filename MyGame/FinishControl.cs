using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyGame
{
    public sealed partial class FinishControl : UserControl
    {
        private Game _game;
        public FinishControl()
        {
            BackgroundImage = Image.FromFile("background.png");
            InitializeComponent();
        }

        private void RestartOnClick(object sender, EventArgs e)
        {
            _game.Player.Location = Point.Empty;
            _game.Player.HP = 100;
            foreach (var monster in _game.Monsters.Where(currentMonster => !currentMonster.IsDead))
            {
                monster.HP = 0;
                _game.Map.Cells[monster.Location.X, monster.Location.Y] = Cell.Empty;
            }
            _game.CreateMonsters();
            _game.CreateProjectile();
            _game.ChangeState(GameState.Battle);
            _game.Player.Projectiles.Clear();
            _game.Player.ProjectilesInAction.Clear();
            _game.Score = 0;
        }

        public void Configure(Game game)
        {
            if (_game != null)
                return;
            _game = game;
            var restart = new Button();
            restart.Text = "Рестарт";
            restart.Size = new Size(250, 100);
            restart.Top = (Size.Height - restart.Height) / 2;
            restart.Left = (Size.Width - restart.Width) / 2;
            restart.Font = new Font(FontFamily.GenericMonospace, 20);
            restart.TextAlign = ContentAlignment.MiddleCenter;
            restart.Click += RestartOnClick;
            Controls.Add(restart);
        }
    }
}