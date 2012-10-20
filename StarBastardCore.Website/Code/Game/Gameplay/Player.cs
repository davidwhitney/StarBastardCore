namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class Player
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public Resources Resources { get; set; }

        public Player(int userId, string name)
        {
            UserId = userId;
            Name = name;
            Resources = new Resources();
        }

        public static Player None = new Player(0, "None");
        public static Player Unknown = new Player(0, "Unknown");
        public static Player UnauthenticatedPlayer1 = new Player(-1, "Player 1");
        public static Player UnauthenticatedPlayer2 = new Player(-2, "Player 2");
    }
}