using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings
{
    public class Spaceport : BuildingBase
    {
        public override Resources PlayerBenefit { get { return new Resources(0, 0, 0); } }
        public override Resources ConstructionCost { get { return new Resources(0, 4, 0); } }
    }
}