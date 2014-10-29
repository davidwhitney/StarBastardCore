using System;
using System.Windows.Forms;

namespace StarBastard.Windows.Prototype.Rendering
{
    public interface IRender<in TClass>
    {
        Action<object, object, EventArgs> OnGameboardInput { get; set; }
        void Render(TClass viewModel, Panel uiRoot);
    }
}