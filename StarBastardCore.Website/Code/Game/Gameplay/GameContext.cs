using System;
using System.Collections.Generic;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using StarBastardCore.Website.Code.Game.Gameworld.Geography;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class GameContext
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public List<PlanetarySystem> Systems { get; set; }
        public ExplorationMap ExplorationMap { get; set; }
        public List<Player> Players { get; set; }

        public int Turn { get; private set; }

        public Player CurrentPlayer
        {
            get { return Turn%2 != 0 ? Players[0] : Players[1]; }
        }

        private GameContext(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
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
            var moveAction = new MoveAction(startingSystem, new ConstructionStarship(player));
            moveAction.Execute(player, this);

            return this;
        }
    }
}