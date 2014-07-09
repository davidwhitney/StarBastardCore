using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code
{
    public static class CollectionExtensions
    {
        public static Player One(this IEnumerable<Player> collection)
        {
            return collection.First();
        }

        public static Player Two(this IEnumerable<Player> collection)
        {
            return collection.Skip(1).First();
        }
    }
}