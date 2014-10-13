using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe.Buildings
{
    public class Farm : ICanBeBuilt, ICanProduce
    {
        public string Name { get { return "Farm"; } }
        public ResourceDelta Cost { get { return new ResourceDelta {Ore = -4}; } }
        public ResourceDelta Benefit { get { return new ResourceDelta { Food = 4 }; } }
    }
}