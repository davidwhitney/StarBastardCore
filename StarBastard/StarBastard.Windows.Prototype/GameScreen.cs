using System.Windows.Forms;
using StarBastard.Core.Server;
using StarBastard.Windows.Prototype.Rendering;

namespace StarBastard.Windows.Prototype
{
    public partial class GameScreen : Form
    {
        private GameServer _server;
        private WinformsRenderer _renderer;

        public GameScreen()
        {
            InitializeComponent();

            _server = new GameServer();
            _renderer = new WinformsRenderer();

        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _server.Initilize();
            var state = _server.GetState();
            _renderer.Render(state, panel1, label1);
        }
    }
}
