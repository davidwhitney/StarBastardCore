using System;
using StarBastard.Core.Universe.Systems;
using StarBastard.Windows.Prototype.InputHandling.Handlers;

namespace StarBastard.Windows.Prototype.InputHandling
{
    public class WinformsInputRouter : IGameboardInputRouter
    {
        public void HandleGameboardInteraction(object interactionTarget, object sender, EventArgs eventArgs)
        {
            // Reflection magic here

            if (interactionTarget is Planet)
            {
                var handler = new PlanetInputHandler();
                handler.Handle(((Planet)interactionTarget), sender, eventArgs);
            }
        }
    }
}