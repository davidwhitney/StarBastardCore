using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Windows.Prototype.Rendering
{
    public class WinformsRenderer : IRender<GameBoard>
    {
        public Action<object, object, EventArgs> OnGameboardInput { get; set; }

        private readonly List<IRender<GameBoard>> _renderers;
        
        public WinformsRenderer()
        {
            OnGameboardInput = (target, sender, args) => { };
            _renderers = new List<IRender<GameBoard>>
            {
                new GameBoardRenderer {OnGameboardInput = OnGameboardInput}
            };
        }


        public void Render(GameBoard gameBoard, Panel targetPanel)
        {
            foreach (var innerRenderer in _renderers)
            {
                innerRenderer.Render(gameBoard, targetPanel);
            }
        }
    }
}