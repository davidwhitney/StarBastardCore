using System;
using System.Collections.Generic;
using System.Linq;

namespace StarBastardCore.Website.Code.Game.Systems
{
    public class GameDimensions
    {
        public const int SectorSize = 37;
        public const int StartingPlanetarySystem = 18;
    }

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
            var systems = new PlanetarySystem[GameDimensions.SectorSize];
            var points = generateScoresForSetOfPlanets();
            var random = new Random();

            for (var i = 0; i != GameDimensions.SectorSize; i++)
            {
                var name = NamesRepository.RandomName();
                var oneSystem = new PlanetarySystem(name, playerNumber + "_" + (i + 1), points[i]);
                systems[i] = oneSystem;
            }
           
            return systems;
        }

        private List<int> generateScoresForSetOfPlanets()
        {
            var points = new int[GameDimensions.SectorSize];
            var totalPoints = 100;
            var random = new Random();

            while (totalPoints > 0)
            { 
                // lets make sure we get allocated
                //generate planet points
                for (var p = 0; p != GameDimensions.SectorSize; p++)
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
            if (points[GameDimensions.StartingPlanetarySystem] == 0)
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