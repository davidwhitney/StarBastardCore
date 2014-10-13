using Microsoft.Xna.Framework;

namespace StarBastard.Windows.Scenes
{
    public interface IScene
    {
        void Initialize();
        void Update(GameTime gameTime);
    }
}