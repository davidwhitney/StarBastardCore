using System;
using System.Collections.Generic;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.Game.PlayerData
{
    public class ExtendedUserProfile : ICanBeSaved
    {
        public object Id { get; set; }

        public IList<ActiveGameReference> ActiveGames { get; set; }

        public ExtendedUserProfile()
        {
            Id = Guid.NewGuid();
            ActiveGames = new List<ActiveGameReference>();
        }
    }

    public class ActiveGameReference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }

        public ActiveGameReference()
        {
            Players = new List<Player>();
        }
    }
}