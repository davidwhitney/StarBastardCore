using System.Collections.Generic;
using System.Linq;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Units;
using StarBastardCore.Website.Code.Game.Units.Buildings;
using StarBastardCore.Website.Code.Game.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Systems
{
    public class PlanetarySystem
    {
        public string Name { get; set; }
        public string SystemNumber { get; set; }
        public int BuildingCap { get; set; }
        public Player Owner { get; set; }

        public List<IStarship> Orbit { get; set; }
        public List<IUnit> Battlefield { get; set; }
        public List<IBuilding> City { get; set; }

        public List<IStarship> PlayerShipsInOrbit
        {
            get { return Orbit.Where(x => x.Owner == Owner).ToList(); }
        } 

        public List<IStarship> EnemyShipsInOrbit
        {
            get { return Orbit.Where(x => x.Owner != Owner).ToList(); }
        } 

        public PlanetarySystem()
        {
            Orbit = new List<IStarship>();
            Battlefield = new List<IUnit>();
            City = new List<IBuilding>();
        }

        public PlanetarySystem(string name, string systemNumber, int buildingCap)
            : this()
        {
            Name = name;
            SystemNumber = systemNumber;
            BuildingCap = buildingCap;
        }

        public Resources ResourceIncreasePrediction
        {
            get
            {
                var resourceChange = new Resources();
                foreach (var building in City)
                {
                    resourceChange.Modify(building.Produce());
                }
                return resourceChange;
            }
        }
    }
}