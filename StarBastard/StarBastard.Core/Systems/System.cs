using System.Collections.Generic;

namespace StarBastard.Core.Systems
{
    public class System
    {
        public string Name { get; set; }
        public string PlanetId { get; set; }
        public int ResourcePoints { get; set; }
        public int PlayerOwner { get; set; }
        
        public List<object> Orbit { get; set; }

        public System(string name, string planetId, int resourcePoints, int playerOwner)
        {
            Name = name;
            PlanetId = planetId;
            ResourcePoints = resourcePoints;
            PlayerOwner = playerOwner;
        }

    }
}
