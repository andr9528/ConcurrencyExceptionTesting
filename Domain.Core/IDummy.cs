using Repository.Core;

namespace Domain.Core
{
    public interface IDummy : IEntity
    {
        string ArbitraryString { get; set; }
        int ArbitraryInt { get; set; }
    }
}