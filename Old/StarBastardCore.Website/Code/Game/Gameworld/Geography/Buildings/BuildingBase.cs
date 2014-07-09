using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameworld.Units;

namespace StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings
{
    public abstract class BuildingBase : UnitBase, IBuilding
    {
        public Resources Produce()
        {
            return PlayerBenefit;
        }

        public abstract Resources PlayerBenefit { get; }
        public abstract Resources ConstructionCost { get; }
    }
}