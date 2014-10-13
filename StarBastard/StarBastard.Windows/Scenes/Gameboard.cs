using Microsoft.Xna.Framework;
using StarBastard.Core.Gameplay;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Windows.Scenes
{
    public class Gameboard : IScene
    {
        private GameContext _ctx;

        public void Initialize()
        {
            _ctx = new GameContext(new SystemGenerator().GenerateSystems(), x => { });
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
