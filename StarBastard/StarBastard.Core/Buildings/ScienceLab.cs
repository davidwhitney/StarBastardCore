using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Buildings
{
    public class ScienceLab
    {
        public string Name
        {
            get { return "Science Lab"; }
        }

        public PlayerResources Produce()
        {
            var resourceMod = new PlayerResources();
            resourceMod.AddTech(4);
            return resourceMod;
        }

        public PlayerResources ConstructionCost()
        {
            var resourceMod = new PlayerResources();
            resourceMod.AddOre(4);
            return resourceMod;
        }
    }
}