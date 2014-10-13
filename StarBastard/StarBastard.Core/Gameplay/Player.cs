namespace StarBastard.Core.Gameplay
{
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string PlayerName { get; set; }
        public ResourceDelta Resources { get; set; }

        public Player(int playerNumber, string playerName)
        {
            PlayerNumber = playerNumber;
            PlayerName = playerName;
        }
        
        public void ModifyResources(ResourceDelta resourceModification)
        {
            Resources.AddFood(resourceModification.Food);
            Resources.AddOre(resourceModification.Ore);
            Resources.AddTech(resourceModification.Tech);

        }
    }
}