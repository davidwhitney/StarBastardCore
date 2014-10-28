using System.Windows.Forms;
using StarBastard.Core.Server;
using StarBastard.Core.Universe.Systems;
using StarBastard.Windows.Prototype.Rendering;

namespace StarBastard.Windows.Prototype
{
    public partial class GameScreen : Form
    {
        private readonly GameServer _server;
        private readonly IRender<GameBoard> _renderer;

        public GameScreen()
        {
            InitializeComponent();

            _server = new GameServer();
            _renderer = new WinformsRenderer();
            _server.Changed += (sender, args) => _renderer.Render(args.CurrentState, renderTarget);

            _renderer.OnGameboardInput = (target, sender, args) =>
            {

            };
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _server.NewGame();
        }
    }
}
