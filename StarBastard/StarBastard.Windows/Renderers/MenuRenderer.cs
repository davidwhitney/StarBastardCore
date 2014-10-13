using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarBastard.Windows.Infrastructure;
using StarBastard.Windows.Scenes;

namespace StarBastard.Windows.Renderers
{
    public class MenuRenderer : ICanRender<MainMenu>
    {
        public void Render(object scene, GameInfrastructure infrastructure)
        {
            Render((MainMenu)scene, infrastructure);
        }

        public void LoadContent(SpriteBatch batch, ContentManager contentManager)
        {
        }

        public void UnloadContent(SpriteBatch batch, ContentManager contentManager)
        {
        }

        public void Render(MainMenu scene, GameInfrastructure infrastructure)
        {
            infrastructure.GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}