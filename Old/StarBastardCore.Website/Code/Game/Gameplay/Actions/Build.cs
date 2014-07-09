using System.Linq;
using StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings;

namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public class Build : GameActionBase
    {
        public string DestinationPlanetId
        {
            get { return Item<string>("DestinationPlanetId"); }
            set { Parameters["DestinationPlanetId"] = value; }
        }

        public string BuildingType
        {
            get { return Item<string>("BuildingType"); }
            set { Parameters["BuildingType"] = value; }
        }

        public Build()
        {
        }

        public override void Commit(GameContext entireContext)
        {
            var system = entireContext.Systems.Single(x => x.SystemNumber == DestinationPlanetId);
            
            if (!system.CanBuild)
            {
                return;
            }

            system.City.Add(new Farm());
        }

        public Build(GameActionBase action)
        {
            Parameters = action.Parameters;
        }
    }
}