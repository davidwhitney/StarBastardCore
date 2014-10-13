using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe.Buildings
{
    public class GroundStructure : ICanBeBuilt, ICanProduce
    {
        public string Name { get; set; }
        public ResourceDelta Cost { get; set; }
        public ResourceDelta Benefit { get; set; }

        public GroundStructure(string name, ResourceDelta cost, ResourceDelta benefit)
        {
            Name = name;
            Cost = cost;
            Benefit = benefit;
        }
    }
}