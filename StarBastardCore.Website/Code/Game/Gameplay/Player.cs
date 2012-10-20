namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class Player
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public Resources Resources { get; set; }

        public Player(string userId, string Name)
        {
            UserId = userId;
            this.Name = Name;
            Resources = new Resources();
        }
    }
}