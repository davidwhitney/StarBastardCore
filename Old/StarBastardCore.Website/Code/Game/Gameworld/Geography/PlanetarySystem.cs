using System.Collections.Generic;
using System.Linq;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings;
using StarBastardCore.Website.Code.Game.Gameworld.Units;
using StarBastardCore.Website.Code.Game.Gameworld.Units.Starships;

namespace StarBastardCore.Website.Code.Game.Gameworld.Geography
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

        public PlanetarySystem(string name, string systemNumber, int buildingCap, Player owner)
            : this()
        {
            Name = name;
            SystemNumber = systemNumber;
            BuildingCap = buildingCap;
            Owner = owner;
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

        public bool CanBuild
        {
            get
            {
                return BuildingCap > City.Count 
                        && PlayerShipsInOrbit.Any(x => x.GetType() == typeof (ConstructionStarship));
            }
        }

        public static PlanetarySystem UndiscoveredSystem(string systemNumber)
        {
            return new PlanetarySystem("Undiscovered", systemNumber, 0, Player.Unknown);
        }
    }
}