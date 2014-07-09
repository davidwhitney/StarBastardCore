using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Buildings
{
    public class Mine
    {
        public string Name
        {
            get { return "Mine"; }
        }

        public PlayerResources Produce()
        {
            var resourceMod = new PlayerResources();
            resourceMod.AddOre(4);
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