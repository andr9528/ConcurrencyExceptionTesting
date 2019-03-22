using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityFramework.Config
{
    public class DummyImplicitConfig : IEntityTypeConfiguration<DummyImplicit>
    {
        public DummyImplicitConfig()
        {
        }

        public void Configure(EntityTypeBuilder<DummyImplicit> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(x => x.Id).HasColumnName("DummyImplicitId");
        }
    }
}