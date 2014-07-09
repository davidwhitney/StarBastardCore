using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.UserProfile
{
    public class UserProfileData : ICanBeSaved
    {
        public object Id { get; set; }
        public List<GameInvite> GameInvites { get; set; }
        public IList<ActiveGameReference> ActiveGames { get; set; }

        public UserProfileData()
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