using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Units
{
    public class UnitBase : IUnit
    {
        public string Name { get { return GetType().Name; } }
        public Player Owner { get; protected set; }
    }
}