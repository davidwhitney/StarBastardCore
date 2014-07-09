using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Buildings
{
    public class Spaceport
    {

        public string Name
        {
            get { return "Spaceport"; }
        }

        public PlayerResources Produce()
        {

            return new PlayerResources();
        }

        public PlayerResources ConstructionCost()
        {
            var resourceMod = new PlayerResources();
            resourceMod.AddOre(4);
            return resourceMod;
        }
    }
}
