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

        public Build(GameActionBase action)
        {
            Parameters = action.Parameters;
        }
    }
}