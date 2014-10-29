using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StarBastard.Windows.Prototype.GameFlow;

namespace StarBastard.Windows.Prototype.Rendering
{
    public class WinformsRenderer : IRender<GameBoardViewModel>
    {
        public Action<object, object, EventArgs> OnGameboardInput
        {
            get { return _onGameboardInput; }
            set
            {
                _onGameboardInput = value;
                _renderers.ForEach(r => r.OnGameboardInput = _onGameboardInput);
            }
        }

        private readonly List<IRender<GameBoardViewModel>> _renderers;
        private Action<object, object, EventArgs> _onGameboardInput;

        public WinformsRenderer()
        {
            _renderers = new List<IRender<GameBoardViewModel>>
            {
                new BackgroundRenderer(),
                new TileRenderer()
            };

            OnGameboardInput = (target, sender, args) => { };
        }


        public void Render(GameBoardViewModel viewModel, Panel targetPanel)
        {
            foreach (var innerRenderer in _renderers)
            {
                innerRenderer.Render(viewModel, targetPanel);
            }
        }
    }
}