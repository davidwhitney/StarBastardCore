using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Buildings
{
    public class Farm
    {
        public string Name
        {
            get { return "Farm"; }
        }

        public PlayerResources Produce()
        {
            var resourceMod = new PlayerResources();
            resourceMod.AddFood(4);
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