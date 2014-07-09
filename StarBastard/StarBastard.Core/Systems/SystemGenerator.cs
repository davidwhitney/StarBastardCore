using System;
using System.Collections.Generic;

namespace StarBastard.Core.Systems
{
    public class SystemGenerator
    {
        public List<System> Build()
        {
            var playerOneSystems = GenerateSystemsForOnePlayer(1);
            var playerTwoSystems = GenerateSystemsForOnePlayer(2);

            var systems = new List<System>();

            var offset = 0;
            foreach (var value in playerOneSystems)
            {
                systems[offset] = value;
                offset++;
            }

            foreach (var value in playerTwoSystems)
            {
                systems[offset] = value;
                offset++;
            }


            return systems;
        }

        public List<System> GenerateSystemsForOnePlayer(int playerNumber)
        {
            var systems = new List<System>();
            var points = GenerateScoresForSetOfPlanets();

            var rng = new Random();
            

            for (var i = 0; i != 37; i++)
            {
                var name = Names.Options[rng.Next(16455)];
                var oneSystem = new System(name, playerNumber + "_" + (i + 1), points[i], playerNumber);
                systems[i] = oneSystem;
            }

            systems[18].Orbit.Add(new ConstructionStarship(playerNumber));


            return systems;
        }

        public List<int> GenerateScoresForSetOfPlanets()
        {
            var points = new List<int>();
            var totalPoints = 100;

            while (totalPoints > 0)
            {
                // lets make sure we get allocated
                //generate planet points
                var rng = new Random();
                for (var p = 0; p != 37; p++)
                {
                    var randomBetweenOneAndTen = rng.Next(1, 10);
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
                for (int index = 0; index < points.Count; index++)
                {
                    var value = points[index];
                    
                    if (value > 5)
                    {
                        points[18] = value;
                        points[index] = 0;
                    }
                }
             
            }

            return points;
        }

        public static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
   
    }
}
