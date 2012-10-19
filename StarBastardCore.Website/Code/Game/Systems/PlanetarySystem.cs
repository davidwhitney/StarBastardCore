using System.Collections.Generic;

namespace StarBastardCore.Website.Code.Game.Systems
{
    public class PlanetarySystem
    {
        public string Name { get; set; }
        public string SystemNumber { get; set; }
        public int BuildingCap { get; set; }
        public int Owner { get; set; }

        public List<object> Orbit { get; set; }
        public List<object> Battlefield { get; set; }
        public List<object> City { get; set; }

        public PlanetarySystem()
        {
            Orbit = new List<object>();
            Battlefield = new List<object>();
            City = new List<object>();
        }

        public PlanetarySystem(string name, string systemNumber, int buildingCap, int owner)
            : this()
        {
            Name = name;
            SystemNumber = systemNumber;
            BuildingCap = buildingCap;
            Owner = owner;
        }
    }
}