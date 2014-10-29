using System;
using System.Drawing;
using System.Windows.Forms;
using StarBastard.Core.Universe.Systems;
using StarBastard.Windows.Prototype.GameFlow;

namespace StarBastard.Windows.Prototype.Rendering
{
    public class BackgroundRenderer : IRender<GameBoardViewModel>
    {
        public Action<object, object, EventArgs> OnGameboardInput { get; set; }

        public void Render(GameBoardViewModel viewModel, Panel uiRoot)
        {
            uiRoot.BackColor = Color.Black;
        }
    }
}