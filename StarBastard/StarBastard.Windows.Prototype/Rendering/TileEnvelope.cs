using StarBastard.Core.Universe.Systems;

namespace StarBastard.Windows.Prototype.Rendering
{
    public class TileEnvelope
    {
        protected bool Equals(TileEnvelope other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Planet Planet { get; set; }

        public TileEnvelope(int x, int y, Planet planet)
        {
            X = x;
            Y = y;
            Planet = planet;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TileEnvelope)obj);
        }
    }
}