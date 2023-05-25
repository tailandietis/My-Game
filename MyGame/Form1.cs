using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    public partial class Form1 : Form
    {
        private Game game;
        private StartControl startControl;
        private PlayerSelectionControl playerSelectionControl;
        private BattleControl battleControl;
        private FinishControl finishControl;

        public Form1()
        {
            InitializeComponent();
            var map = Initializer.CreateMap();
            startControl = new StartControl();
            playerSelectionControl = new PlayerSelectionControl();
            battleControl = new BattleControl();
            finishControl = new FinishControl();
            Controls.Add(startControl);
            Controls.Add(playerSelectionControl);
            Controls.Add(battleControl);
            Controls.Add(finishControl);
            playerSelectionControl.Hide();
            battleControl.Hide();
            finishControl.Hide();
            game = new Game(map, new[] {new Monster(new Point(6, 0)), new Monster(new Point(6, 7)), new Monster(new Point(10, 12))});
            startControl.Configure(game);
            game.ChangedState += state =>
            {
                HideScreens();
                switch (state)
                {
                    case GameState.Start:
                    {
                        startControl.Configure(game);
                        startControl.Show();
                    }
                        ;
                        break;
                    case GameState.SelectionPlayer:
                    {
                        playerSelectionControl.Configure(game);
                        playerSelectionControl.Show();
                    }
                        break;
                    case GameState.Battle:
                    {
                        battleControl.Configure(game);
                        battleControl.Show();
                    }
                        break;
                    case GameState.Result:
                    {
                        finishControl.Configure(game);
                        finishControl.Show();
                    }
                        break;
                }
            };


        }

        private void HideScreens()
        {
            startControl.Hide();
            playerSelectionControl.Hide();
            battleControl.Hide();
            finishControl.Hide();
        }
    }
}