using System;
using System.Collections;
using System.Collections.Generic;
using StarBastardCore.Website.Code.Game.Systems;

namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class GameContext
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public List<PlanetarySystem> Systems { get; set; }
        public List<Player> Players { get; set; }

        private GameContext(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            Players = new List<Player>();
            Systems = new List<PlanetarySystem>();
        }

        public static GameContext Create(string name)
        {
            return new GameContext(name);
        }

        public GameContext WithSystems(IEnumerable<PlanetarySystem> systems)
        {
            Systems.AddRange(systems);
            return this;
        }

        public GameContext ForPlayers(IEnumerable<Player> players)
        {
            Players.AddRange(players);
            return this;
        }
    }
}