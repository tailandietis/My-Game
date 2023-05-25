using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    public sealed partial class StartControl : UserControl
    {
        private Game _game;
        public StartControl()
        {
            BackgroundImage = Image.FromFile("background.png");
            InitializeComponent();
        }

        public void Configure(Game game)
        {
            if (_game != null)
                return;
            _game = game;
            var button = new Button();
            button.Text = "Начать игру";
            button.Size = new Size(300, 150);
            button.Top = (Size.Height - button.Height) / 2;
            button.Left = (Size.Width - button.Width) / 2;
            button.Font = new Font(FontFamily.GenericMonospace, 20);
            button.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(button);
            button.Click += (sender, args) =>
            {
                game.ChangeState(GameState.SelectionPlayer);
            };
        }
    }
}