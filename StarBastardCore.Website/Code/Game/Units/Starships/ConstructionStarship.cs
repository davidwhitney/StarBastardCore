using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Units.Starships
{
    public class ConstructionStarship : IStarship
    {
        public string Name { get { return "Construction Starship"; } }
        public int Movement { get; set; }
        public Player Owner { get; set; }

        public ConstructionStarship(Player playerOwner)
        {
            Owner = playerOwner;
            Movement = 1;
        }
    }
}