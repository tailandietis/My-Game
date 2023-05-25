using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    public sealed partial class PlayerSelectionControl : UserControl
    {
        private Game game;
        public PlayerSelectionControl()
        {
            BackgroundImage = Image.FromFile("background.png");
            InitializeComponent();
        }

        public void Configure(Game game)
        {
            if (this.game == null)
            {
                this.game = game;
            }
            
            var label = new Label
            {
                Text = "Выберите персонажа", TextAlign = ContentAlignment.MiddleCenter, Width = 300, Height = 100,
            };
            label.Left = (Size.Width - label.Width) / 2;
            label.Top = Size.Height / 5;
            label.Font = new Font(FontFamily.GenericMonospace, 30);
            var buttonsIndent = 50;
            var buttonPlayer1 = new Button();
            buttonPlayer1.Text = "Огонёк";
            buttonPlayer1.TextAlign = ContentAlignment.BottomCenter;
            buttonPlayer1.Font = new Font(FontFamily.GenericMonospace, 12);
            buttonPlayer1.Image = Image.FromFile("Fire_button.png").GetThumbnailImage(90, 120, null,IntPtr.Zero);
            buttonPlayer1.Size = new Size(150, 150);
            buttonPlayer1.Left = (Size.Width - buttonPlayer1.Width * 3 - buttonsIndent * 2) / 2;
            buttonPlayer1.Top = (Size.Height - buttonPlayer1.Height) / 2;
            buttonPlayer1.Click += (sender, args) =>
            {
                var player = new Player(PlayerName.Fire, new Point(0, 0));
                game.CreatePlayer(player);
                game.ChangeState(GameState.Battle);
            };
            var buttonPlayer2 = new Button();
            buttonPlayer2.Left = buttonPlayer1.Location.X + buttonPlayer1.Width + buttonsIndent;
            buttonPlayer2.Text = "Острый глаз";
            buttonPlayer2.TextAlign = ContentAlignment.BottomCenter;
            buttonPlayer2.Font = new Font(FontFamily.GenericMonospace, 12);
            buttonPlayer2.Image = Image.FromFile("Bird.png").GetThumbnailImage(110, 90, null,IntPtr.Zero);
            buttonPlayer2.Size = new Size(150, 150);
            buttonPlayer2.Top = (Size.Height - buttonPlayer2.Height) / 2;
            buttonPlayer2.Click += (sender, args) =>
            {
                var player = new Player(PlayerName.Bird, new Point(0, 0));
                game.CreatePlayer(player);
                game.ChangeState(GameState.Battle);
            };
            var buttonPlayer3 = new Button();
            buttonPlayer3.Left = buttonPlayer2.Location.X + buttonPlayer2.Width + buttonsIndent;
            buttonPlayer3.Text = "Безумный кот";
            buttonPlayer3.TextAlign = ContentAlignment.BottomCenter;
            buttonPlayer3.Font = new Font(FontFamily.GenericMonospace, 12);
            buttonPlayer3.Image = Image.FromFile("Cat_button.png").GetThumbnailImage(90, 110, null,IntPtr.Zero);
            buttonPlayer3.Size = new Size(150, 150);
            buttonPlayer3.Top = (Size.Height - buttonPlayer3.Height) / 2;
            buttonPlayer3.Click += (sender, args) =>
            {
                var player = new Player(PlayerName.Cat, new Point(0, 0));
                game.CreatePlayer(player);
                game.ChangeState(GameState.Battle);
            };
            Controls.Add(buttonPlayer1);
            Controls.Add(buttonPlayer2);
            Controls.Add(buttonPlayer3);
            Controls.Add(label);
        }
    }
}