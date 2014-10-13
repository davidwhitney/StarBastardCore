namespace StarBastard.Core.Universe.Fleet
{
    public class ConstructionStarship : ICanOrbit, ICanBuild
    {
        public int PlayerOwner { get; private set; }
        public int Movement { get; private set; }
        public string Name { get { return "Construction Ship"; } }

        public ConstructionStarship(int playerOwner)
        {
            PlayerOwner = playerOwner;
            Movement = 1;
        }
    }
}