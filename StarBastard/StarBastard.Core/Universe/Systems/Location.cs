namespace StarBastard.Core.Universe.Systems
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("Location {{x:{0}, y:{1}}}", X, Y);
        }
    }
}