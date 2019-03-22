using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityFramework.Config
{
    public class DummyExplicitConfig : IEntityTypeConfiguration<DummyExplicit>
    {
        public DummyExplicitConfig()
        {

        }

        public void Configure(EntityTypeBuilder<DummyExplicit> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(x => x.Id).HasColumnName("DummyExplicitId");

            builder.Property(x => x.ArbitraryInt).IsConcurrencyToken();
        }
    }
}