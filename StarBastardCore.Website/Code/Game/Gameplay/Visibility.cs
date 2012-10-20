using System.Collections.Generic;

namespace StarBastardCore.Website.Code.Game.Gameplay
{
    public class Visibility
    {
        public List<Player> VisibleToUserInThisList { get; set; }

        public Visibility()
        {
            VisibleToUserInThisList = new List<Player>();
        }
    }
}