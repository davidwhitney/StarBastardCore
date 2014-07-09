namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class Resources
    {
        public int Food { get; private set; }
        public int Ore { get; private set; }
        public int Tech { get; private set; }

        public Resources()
        {
        }

        public Resources(int food, int ore, int tech)
        {
            Food = food;
            Ore = ore;
            Tech = tech;
        }

        public void Modify(Resources changesToApply)
        {
            Food += changesToApply.Food;
            Ore += changesToApply.Ore;
            Tech += changesToApply.Tech;
        }
    }
}