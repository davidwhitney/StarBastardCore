using System;
using System.Collections.Generic;
using System.Linq;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay.ActionHandlers;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using StarBastardCore.Website.Code.Game.Gameworld.Geography;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class GameContext : ICanBeSaved
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public int Turn { get; private set; }

        public List<GameActionBase> UncommittedActions { get; set; } 

        public List<PlanetarySystem> Systems { get; set; }
        public ExplorationMap ExplorationMap { get; set; }
        public List<Player> Players { get; set; }


        public Player CurrentPlayer
        {
            get { return Turn%2 != 0 ? Players[0] : Players[1]; }
        }

        private GameContext(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            UncommittedActions = new List<GameActionBase>();
            Players = new List<Player>();
            Systems = new List<PlanetarySystem>();
            ExplorationMap = new ExplorationMap();
            Turn = 1;
        }

        public static GameContext Create(string name)
        {
            return new GameContext(name);
        }

        public GameContext WithSystems(IEnumerable<PlanetarySystem> systems)
        {
            Systems.AddRange(systems);
            foreach(var item in systems)
            {
                ExplorationMap.Add(item, new Visibility());
            }
            return this;
        }
        
        public GameContext AddPlayer(Player player)
        {
            var offset = Players.Count * GameDimensions.SectorSize;
            Players.Add(player);
            
            var startingSystem = Systems[offset + GameDimensions.StartingPlanetarySystem];
            
            var action = new Move {DestinationPlanetId = startingSystem.SystemNumber, Starship = new ConstructionStarship(player)};
            var actionHandler = new MoveActionHandler(action);
            actionHandler.Execute(player, this);

            return this;
        }

        public void EndTurn()
        {
            Turn++;
        }
    }

    public static class GameContextExtensions
    {
        public static IStarship FindStarship(this GameContext ctx, Guid id)
        {
            return ctx.Systems.Select(planet => planet.Orbit.FirstOrDefault(x => x.Id == id)).FirstOrDefault(ship => ship != null);
        }

        public static PlanetarySystem FindPlanet(this GameContext ctx, string id)
        {
            return ctx.Systems.FirstOrDefault(x => x.SystemNumber == id);
        }
    }
}