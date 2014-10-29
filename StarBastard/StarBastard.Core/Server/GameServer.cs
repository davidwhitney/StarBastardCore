using System.Diagnostics;
using StarBastard.Core.Gameplay;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Core.Server
{
    public class GameServer : IGameServer
    {
        private GameContext _ctx;
        
        public void NewGame()
        {
            var gen = new SystemGenerator();
            var systems = gen.GenerateSystems();
            
            _ctx = new GameContext(systems, context => Debug.WriteLine("State changed"));

            _ctx.StartTurn();
        }

        public GameBoard GetCurrentState()
        {
            return _ctx.Systems;
        }
    }

    public interface IGameServer
    {
        GameBoard GetCurrentState();
        void NewGame();
    }
}
