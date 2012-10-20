using System.Linq;
using StarBastardCore.Website.Code.Game.Gameworld.Geography;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public class MoveAction : IAction
    {
        private readonly PlanetarySystem _destinationPlanet;
        private readonly ConstructionStarship _ship;

        public MoveAction(PlanetarySystem destinationPlanet, ConstructionStarship ship)
        {
            _destinationPlanet = destinationPlanet;
            _ship = ship;
        }

        public void Execute(Player currentPlayer, GameContext context)
        {
            if (_destinationPlanet.Owner == Player.None)
            {
                _destinationPlanet.Owner = currentPlayer;
            }

            context.ExplorationMap[_destinationPlanet].VisibleToUserInThisList.Add(currentPlayer);

            foreach (var system in context.Systems.Where(x => x.Orbit.Contains(_ship)))
            {
                system.Orbit.Remove(_ship);
            }

            _destinationPlanet.Orbit.Add(_ship);
        }
    }
}