using System.Collections.Generic;

namespace StarBastard.Core.Universe.Systems
{
    public class GameBoard : List<Planet>
    {
        public GameBoard()
        {
        }

        public GameBoard(IEnumerable<Planet> planets)
        {
            foreach (var planet in planets)
            {
                Add(planet);
            }
        }
    }
}