using System;
using System.Linq;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameplay.ActionHandlers
{
    public class MoveActionHandler : IActionHandler
    {
        private readonly Move _action;

        public MoveActionHandler(Move action)
        {
            _action = action;
        }

        public void Execute(Player currentPlayer, GameContext context)
        {
            var ship = context.FindStarship(_action.StarshipId) ?? _action.Starship;
            var destinationPlanet = context.FindPlanet(_action.DestinationPlanetId);

            if (destinationPlanet.Owner == Player.None)
            {
                destinationPlanet.Owner = currentPlayer;
            }

            context.ExplorationMap[destinationPlanet].VisibleToUserInThisList.Add(currentPlayer);

            foreach (var system in context.Systems.Where(x => x.Orbit.Contains(ship)))
            {
                system.Orbit.Remove(ship);
            }

            destinationPlanet.Orbit.Add(ship);
        }
    }
}