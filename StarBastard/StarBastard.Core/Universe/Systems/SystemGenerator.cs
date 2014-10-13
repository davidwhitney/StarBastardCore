using System;
using System.Collections.Generic;
using System.Linq;
using StarBastard.Core.Universe.Fleet;

namespace StarBastard.Core.Universe.Systems
{
    public class SystemGenerator
    {
        public GameBoard GenerateSystems()
        {
            var playerOneSystems = GenerateSystemsForOnePlayer(1);
            var playerTwoSystems = GenerateSystemsForOnePlayer(2);

            var systems = new Planet[74];

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

            PositionSystemsIntoHex(systems);

            return new GameBoard(systems);
        }

        private static void PositionSystemsIntoHex(IList<Planet> systems)
        {
            var breaks = new List<int>
            {
                3, 8, 14, 21, 27, 32, 36
            };

            var boxesOnThisLine = 0;
            var lineOffset = 0;
            for (var index = 0; index < systems.Count; index++)
            {
                systems[index].Location = new Location(boxesOnThisLine, lineOffset);

                if (breaks.Contains(index) || breaks.Contains(index - 37))
                {
                    lineOffset++;
                    boxesOnThisLine = 0;
                }
                else
                {
                    boxesOnThisLine++;
                }
            }
        }

        public List<Planet> GenerateSystemsForOnePlayer(int playerNumber)
        {
            var systems = new Planet[37];
            var points = GenerateScoresForSetOfPlanets();

            var rng = new Random();
            

            for (var i = 0; i != 37; i++)
            {
                var name = Names.Options[rng.Next(16455)];
                var oneSystem = new Planet(name, playerNumber + "_" + (i + 1), points[i], playerNumber);
                systems[i] = oneSystem;
            }

            systems[18].Orbit.Add(new ConstructionStarship(playerNumber));


            return systems.ToList();
        }

        public List<int> GenerateScoresForSetOfPlanets()
        {
            var points = new int[37];
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
                for (int index = 0; index < points.Length; index++)
                {
                    var value = points[index];
                    
                    if (value > 5)
                    {
                        points[18] = value;
                        points[index] = 0;
                    }
                }
             
            }

            return points.ToList();
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
