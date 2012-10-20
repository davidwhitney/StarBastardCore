using System;
using System.Collections.Generic;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Systems;

namespace StarBastardCore.Website.Models.Game
{
    public class CurrentTurnViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }

        public List<PlanetarySystem> Systems { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayer { get; set; }

        public CurrentTurnViewModel()
        {
            Systems = new List<PlanetarySystem>();
            Players = new List<Player>();
        }

        public static CurrentTurnViewModel FromGameContext(GameContext game, bool fogOfWar = true)
        {
            var vm = new CurrentTurnViewModel
                {
                    Players = game.Players,
                    CurrentPlayer = game.CurrentPlayer,
                    Round = game.Round,
                    Id = game.Id,
                    Name = game.Name
                };

            foreach (var system in game.Systems)
            {
                var planetExplorationHistory = game.ExplorationMap[system];

                var systemToAdd = fogOfWar
                                      ? (planetExplorationHistory.PlayerCanSeeSystem(game.CurrentPlayer)
                                             ? system
                                             : PlanetarySystem.UndiscoveredSystem(system.SystemNumber))
                                      : system;

                vm.Systems.Add(systemToAdd);
            }
            return vm;
        }
    }
}