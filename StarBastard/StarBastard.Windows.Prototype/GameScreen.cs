using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using StarBastard.Core.Server;
using StarBastard.Windows.Prototype.Rendering;

namespace StarBastard.Windows.Prototype
{
    public partial class GameScreen : Form
    {
        public GameScreen()
        {
            InitializeComponent();

            var server = new GameServer();
            var renderer = new WinformsRenderer();

            server.Initilize();

            var state = server.GetState();
            renderer.Render(state, panel1, label1);
        }
    }
}
