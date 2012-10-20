using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Units.Starships
{
    public class ConstructionStarship : UnitBase, IStarship
    {
        public int Movement { get; set; }

        public ConstructionStarship(Player playerOwner)
        {
            Owner = playerOwner;
            Movement = 1;
        }
    }
}