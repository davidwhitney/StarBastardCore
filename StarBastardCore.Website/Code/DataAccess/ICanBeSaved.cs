using System;

namespace StarBastardCore.Website.Code.DataAccess
{
    public interface ICanBeSaved
    {
        Guid Id { get; }
    }

}