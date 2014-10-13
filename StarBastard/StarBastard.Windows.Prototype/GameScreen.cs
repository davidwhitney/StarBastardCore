using System.Windows.Forms;
using StarBastard.Core.Server;
using StarBastard.Windows.Prototype.Rendering;

namespace StarBastard.Windows.Prototype
{
    public partial class GameScreen : Form
    {
        private readonly GameServer _server;
        private readonly WinformsRenderer _renderer;

        public GameScreen()
        {
            InitializeComponent();

            _server = new GameServer();
            _renderer = new WinformsRenderer();
            _server.Changed += (sender, args) => _renderer.Render(args.CurrentState, panel1, label1);
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _server.NewGame();
        }
    }
}
