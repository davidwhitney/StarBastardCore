using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Units
{
    public interface IUnit
    {
        string Name { get; }
        Player Owner { get; }
        int Movement { get; }
    }
}