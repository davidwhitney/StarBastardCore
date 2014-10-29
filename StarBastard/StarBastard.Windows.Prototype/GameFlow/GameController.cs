using StarBastard.Core.Server;

namespace StarBastard.Windows.Prototype.GameFlow
{
    public class GameController
    {
        private readonly IGameServer _server;

        public GameController(IGameServer server)
        {
            _server = server;
        }

        public GameBoardViewModel StartNewGame()
        {
            _server.NewGame();
            var state = _server.GetCurrentState();
            return new GameBoardViewModel { Gameboard = state };
        }

        public GameBoardViewModel GetBoardViewModel()
        {
            return new GameBoardViewModel
            {
                Gameboard = _server.GetCurrentState()
            };
        }
    }
}