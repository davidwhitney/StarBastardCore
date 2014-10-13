using System.Collections.Generic;
using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe.Systems
{
    public class Planet
    {
        public string Name { get; set; }
        public string PlanetId { get; set; }
        public int ResourcePoints { get; set; }
        public int Owner { get; set; }

        public List<ICanOrbit> Orbit { get; set; }
        public Location Location { get; set; }

        public Planet(string name, string planetId, int resourcePoints, int owner)
        {
            Name = name;
            PlanetId = planetId;
            ResourcePoints = resourcePoints;
            Owner = owner;
            Orbit = new List<ICanOrbit>();
        }

        public List<ResourceDelta> ProgressOneTurn()
        {
            return new List<ResourceDelta>();
        }
    }
}
