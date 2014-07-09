using System;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Units
{
    public abstract class UnitBase : IUnit
    {
        public Guid Id { get; private set; }
        public string Name { get { return GetType().Name; } }
        public Player Owner { get; protected set; }

        public UnitBase()
        {
            Id = Guid.NewGuid();
        }
    }
}