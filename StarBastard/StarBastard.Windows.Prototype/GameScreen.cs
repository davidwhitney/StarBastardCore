using System.Windows.Forms;
using StarBastard.Core.Server;
using StarBastard.Windows.Prototype.GameFlow;
using StarBastard.Windows.Prototype.InputHandling;
using StarBastard.Windows.Prototype.Rendering;

namespace StarBastard.Windows.Prototype
{
    public partial class GameScreen : Form
    {
        private readonly IRender<GameBoardViewModel> _renderer;
        private readonly IGameboardInputRouter _inputRouter;

        private readonly GameController _controller;

        public GameScreen()
        {
            InitializeComponent();

            _controller = new GameController(new GameServer());
            _renderer = new WinformsRenderer();
            _inputRouter = new WinformsInputRouter();
            _renderer.OnGameboardInput = _inputRouter.HandleGameboardInteraction;

            newToolStripMenuItem.Click += (sender, args) =>
            {
                var viewModel = _controller.StartNewGame();
                _renderer.Render(viewModel, renderTarget);
            };
        }
    }
}
