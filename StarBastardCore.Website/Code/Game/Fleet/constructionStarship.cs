using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBastardCore.Website.Code.Game.Fleet
{
    public class ConstructionStarship
    {
        public int Movement { get; set; }
        public int PlayerOwner { get; set; }

        public ConstructionStarship(int playerOwner)
        {
            PlayerOwner = playerOwner;
            Movement = 1;
        }
    }
}