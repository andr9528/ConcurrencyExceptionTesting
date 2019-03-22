using Domain.Core;

namespace Domain.Concrete
{
    public class DummyExplicit : IDummyExplicit
    {
        public string ArbitraryString { get; set; }
        public int ArbitraryInt { get; set; }
        public int Id { get; set; }
    }
}