using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings
{
    public class Mine : BuildingBase
    {
        public override Resources PlayerBenefit { get { return new Resources(0, 4, 0); } }
        public override Resources ConstructionCost { get { return new Resources(4, 0, 0); } }
    }
}