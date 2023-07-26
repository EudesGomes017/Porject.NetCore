using Hvex.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hvex.Data.Mapping {
    public class TestMap : IEntityTypeConfiguration<Test> {
        public void Configure(EntityTypeBuilder<Test> builder) {
            builder.ToTable("test");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(10).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdateAt);
            builder.Property(c => c.DurationInSeconds).HasDefaultValue(0);
            builder.Property(c => c.Status);
            builder.Property(c => c.TransformerId).HasMaxLength(10).IsRequired();

            builder.HasMany(rp => rp.Reports)
                   .WithOne(ts => ts.Test)
                   .HasForeignKey(ts => ts.TestId);

        }
    }
}
