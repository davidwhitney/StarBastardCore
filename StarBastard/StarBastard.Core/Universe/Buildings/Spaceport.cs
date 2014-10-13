using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe.Buildings
{
    public class Spaceport : ICanBeBuilt, ICanProduce
    {
        public string Name { get { return "Spaceport"; } }
        public ResourceDelta Cost { get { return new ResourceDelta { Ore = -4 }; } }
        public ResourceDelta Benefit { get { return new ResourceDelta(); } }
    }
}
