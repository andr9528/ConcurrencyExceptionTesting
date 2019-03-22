using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityFramework.Config
{
    public class DummyTimestampConfig : IEntityTypeConfiguration<DummyTimestamp>
    {
        public DummyTimestampConfig()
        {
        }

        public void Configure(EntityTypeBuilder<DummyTimestamp> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(x => x.Id).HasColumnName("DummyTimestampId");

            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}