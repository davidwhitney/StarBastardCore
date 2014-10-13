namespace StarBastard.Core.Gameplay
{
    public class ResourceDelta
    {
        public int Food { get; set; }
        public int Ore { get; set; }
        public int Tech { get; set; }

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
