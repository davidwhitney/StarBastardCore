using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Units
{
    public interface IUnit
    {
        string Name { get; }
        Player Owner { get; }
    }
}