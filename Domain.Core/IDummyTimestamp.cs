using Repository.Core;

namespace Domain.Core
{
    public interface IDummyTimestamp : IDummy
    {
        byte[] Version { get; set; }
    }
}