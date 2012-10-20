using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameworld.Units;

namespace StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings
{
    public interface IBuilding : IUnit
    {
        Resources Produce();
        Resources PlayerBenefit { get; }
        Resources ConstructionCost { get; }
    }
}