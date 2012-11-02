using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBastardCore.Website.Code.UserProfile
{
    public class UserProfileData
    {
        public string UserId { get; set; }
        public List<string> ActiveGames { get; set; }
        public List<GameInvite> GameInvites { get; set; } 
    }
}