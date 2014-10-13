using StarBastard.Core.Gameplay;

namespace StarBastard.Core.Universe
{
    public interface ICanOrbit
    {
    }

    public interface ICanBeBuilt
    {
        ResourceDelta Cost { get; }
    }

    public interface ICanProduce
    {
        ResourceDelta Benefit { get; }
    }

    public interface ICanBuild
    {
        
    }
}