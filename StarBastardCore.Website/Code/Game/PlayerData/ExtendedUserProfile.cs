using System;
using System.Collections.Generic;
using StarBastardCore.Website.Code.DataAccess;

namespace StarBastardCore.Website.Code.Game.PlayerData
{
    public class ExtendedUserProfile : ICanBeSaved
    {
        public object Id { get; set; }

        public IList<Guid> ActiveGames { get; set; }

        public ExtendedUserProfile()
        {
            Id = Guid.NewGuid();
            ActiveGames = new List<Guid>();
        }
    }
}