using System;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.Gameworld.Units
{
    public interface IUnit
    {
        Guid Id { get; }
        string Name { get; }
        Player Owner { get; }
    }
}