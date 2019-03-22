using Domain.Core;

namespace Domain.Concrete
{
    public class DummyTimestamp : IDummyTimestamp
    {
        public string ArbitraryString { get; set; }
        public int ArbitraryInt { get; set; }
        public byte[] Version { get; set; }
        public int Id { get; set; }
    }
}