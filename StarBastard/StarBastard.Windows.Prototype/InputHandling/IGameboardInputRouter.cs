using System;

namespace StarBastard.Windows.Prototype.InputHandling
{
    public interface IGameboardInputRouter
    {
        void HandleGameboardInteraction(object interactionTarget, object sender, EventArgs eventArgs);
    }
}