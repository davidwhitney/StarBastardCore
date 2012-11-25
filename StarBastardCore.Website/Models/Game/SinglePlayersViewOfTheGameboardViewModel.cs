using System;
using System.Collections.Generic;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameworld.Geography;

namespace StarBastardCore.Website.Models.Game
{
    public class SinglePlayersViewOfTheGameboardViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }

        public List<PlanetarySystem> Systems { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayer { get; set; }

        public List<GameActionBase> UncomittedActions { get; set; }

        public SinglePlayersViewOfTheGameboardViewModel()
        {
            Systems = new List<PlanetarySystem>();
            Players = new List<Player>();
            UncomittedActions = new List<GameActionBase>();
        }

        public static SinglePlayersViewOfTheGameboardViewModel FromGameContext(GameContext game, bool fogOfWar = true)
        {
            var vm = new SinglePlayersViewOfTheGameboardViewModel
                {
                    Players = game.Players,
                    CurrentPlayer = game.CurrentPlayer,
                    Round = game.Turn,
                    Id = (Guid)game.Id,
                    Name = game.Name,
                    UncomittedActions = new List<GameActionBase>()
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
            
            foreach (var action in game.UncommittedActions)
            {
                vm.UncomittedActions.Add(action);
            }

            return vm;
        }

    }
}