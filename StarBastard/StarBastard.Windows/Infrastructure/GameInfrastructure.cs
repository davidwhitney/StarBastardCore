using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarBastard.Windows.Infrastructure
{
    public class GameInfrastructure
    {
        public GameTime GameTime { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
    }
}