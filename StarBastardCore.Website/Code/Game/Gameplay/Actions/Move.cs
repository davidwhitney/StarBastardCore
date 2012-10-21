using System;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public class Move : GameActionBase
    {
        public string DestinationPlanetId
        {
            get { return Item<string>("DestinationPlanetId"); }
            set { Parameters["DestinationPlanetId"] = value; }
        }

        public Guid StarshipId
        {
            get { return Item<Guid>("StarshipId"); }
            set { Parameters["StarshipId"] = value; }
        }

        public IStarship Starship
        {
            get { return Item<IStarship>("Starship"); }
            set { Parameters["Starship"] = value; }
        }

        public Move()
        {
        }

        public Move(GameActionBase action)
        {
            Parameters = action.Parameters;
        }
    }
}