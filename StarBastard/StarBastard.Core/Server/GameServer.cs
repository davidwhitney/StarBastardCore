using System.Diagnostics;
using StarBastard.Core.Gameplay;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Core.Server
{
    public class GameServer
    {
        private GameContext _ctx;

        public void Initilize()
        {
            var gen = new SystemGenerator();
            var systems = gen.GenerateSystems();
            
            _ctx = new GameContext(systems, context => Debug.WriteLine("Render called due to change"));
        }

        public GameBoard GetState()
        {
            return _ctx.Systems;
        }
    }
}
