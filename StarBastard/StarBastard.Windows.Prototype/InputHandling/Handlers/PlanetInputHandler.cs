using System;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Windows.Prototype.InputHandling.Handlers
{
    public class PlanetInputHandler : IHandleInputFor<Planet>
    {
        public void Handle(Planet target, object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Clicked on " + target.PlanetId);

            
        }
    }
}
