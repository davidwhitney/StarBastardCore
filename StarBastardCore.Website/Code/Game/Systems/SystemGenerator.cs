using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarBastardCore.Website.Code.Game.Fleet;

namespace StarBastardCore.Website.Code.Game.Systems
{
    public class SystemGenerator
    {
        public List<PlanetarySystem> Generate()
        {
            var system = new List<PlanetarySystem>();
            system.AddRange(GenerateSystemForOnePlayer(1));
            system.AddRange(GenerateSystemForOnePlayer(2));
            return system;
        }

        private IEnumerable<PlanetarySystem> GenerateSystemForOnePlayer(int playerNumber)
        {
            var systems = new PlanetarySystem[37];
            var points = generateScoresForSetOfPlanets();
            var random = new Random();

            for (var i = 0; i != 37; i++)
            {
                var name = PlanetarySystemNamesRepository.Names[random.Next(1, PlanetarySystemNamesRepository.Names.Count)];
                var oneSystem = new PlanetarySystem(name, playerNumber + "_" + (i + 1), points[i], playerNumber);
                systems[i] = oneSystem;
            }

            systems[18].Orbit.Add(new ConstructionStarship(playerNumber));


            return systems;
        }

        private List<int> generateScoresForSetOfPlanets()
        {
            var points = new int[37];
            var totalPoints = 100;
            var random = new Random();

            while (totalPoints > 0)
            { 
                // lets make sure we get allocated
                //generate planet points
                for (var p = 0; p != 37; p++)
                {
                    var randomBetweenOneAndTen = random.Next(1, 10);
                    if (randomBetweenOneAndTen > totalPoints)
                    {
                        randomBetweenOneAndTen = totalPoints;
                    }
                    totalPoints = totalPoints - randomBetweenOneAndTen;
                    points[p] = randomBetweenOneAndTen;
                }
            }

            Shuffle(points);

            
            // ensure the start position is workable.
            if (points[18] == 0)
            {
                for (var i = 0; i < points.Length; i++)
                {
                    if (points[i] > 5)
                    {
                        points[18] = points[i];
                        points[i] = 0;
                    }
                }
            }


            return points.ToList();
        }


        private static void Shuffle<T>(IList<T> array)
        {
            var random = new Random();
            for (var i = array.Count; i > 1; i--)
            {
                var j = random.Next(i);
                var tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }
}