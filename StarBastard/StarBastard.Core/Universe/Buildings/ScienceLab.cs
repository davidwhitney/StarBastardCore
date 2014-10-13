using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe.Buildings
{
    public class ScienceLab : ICanBeBuilt, ICanProduce
    {
        public string Name { get { return "Science Lab"; } }
        public ResourceDelta Cost { get { return new ResourceDelta { Ore = -4 }; } }
        public ResourceDelta Benefit { get { return new ResourceDelta { Tech = 4 }; } }
    }
}