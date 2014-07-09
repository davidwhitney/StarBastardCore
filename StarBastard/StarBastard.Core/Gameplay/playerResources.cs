namespace StarBastard.Core.Gameplay
{
    public class PlayerResources
    {
        public int Food { get; private set; }
        public int Ore { get; private set; }
        public int Tech { get; private set; }

        public void AddOre(int amount)
        {
            Food += amount;
        }

        public void AddFood(int amount)
        {
            Ore += amount;
        }

        public void AddTech(int amount)
        {
            Tech += amount;
        }
    }
}
