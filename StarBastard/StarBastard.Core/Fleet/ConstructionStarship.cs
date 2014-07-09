namespace StarBastard.Core.Systems
{
    public class ConstructionStarship
    {
        public int PlayerOwner { get; private set; }
        public int Movement { get; private set; }
        public bool CanBuild { get; private set; }

        public string Name
        {
            get { return "Construction Ship"; }
        }

        public ConstructionStarship(int playerOwner)
        {
            PlayerOwner = playerOwner;
            Movement = 1;
            CanBuild = true;
        }
    }
}