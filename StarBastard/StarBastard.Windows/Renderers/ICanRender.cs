using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarBastard.Windows.Infrastructure;

namespace StarBastard.Windows.Renderers
{
    public interface ICanRender
    {
        void Render(object scene, GameInfrastructure infrastructure);
        void LoadContent(SpriteBatch batch, ContentManager contentManager);
        void UnloadContent(SpriteBatch batch, ContentManager contentManager);
    }

    public interface ICanRender<in TScene> : ICanRender
    {
        void Render(TScene scene, GameInfrastructure infrastructure);
    }
}
