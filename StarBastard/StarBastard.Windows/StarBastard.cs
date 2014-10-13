using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarBastard.Windows.Infrastructure;
using StarBastard.Windows.Renderers;
using StarBastard.Windows.Scenes;

namespace StarBastard.Windows
{
    public class StarBastard : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IScene> _scenes;
        private IScene _activeScene;
        private Dictionary<IScene, ICanRender> _renderers;

        public StarBastard()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _scenes = new List<IScene>
            {
                new MainMenu(),
                new Gameboard()
            };

            _renderers = new Dictionary<IScene, ICanRender>
            {
                {_scenes[0], new MenuRenderer()},
                {_scenes[1], new GameboardRenderer()}
            };

            _activeScene = _scenes.First();
            _activeScene.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderers[_activeScene].LoadContent(_spriteBatch, Content);
        }

        protected override void UnloadContent()
        {
            _renderers[_activeScene].UnloadContent(_spriteBatch, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _renderers[_activeScene].Render(_activeScene,
                new GameInfrastructure
                {
                    GameTime = gameTime,
                    GraphicsDeviceManager = _graphics,
                    GraphicsDevice = GraphicsDevice,
                    SpriteBatch = _spriteBatch
                });

            base.Draw(gameTime);
        }
    }
}
