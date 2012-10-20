using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Units.Buildings
{
    public interface IBuilding : IUnit
    {
        Resources Produce();
    }
}