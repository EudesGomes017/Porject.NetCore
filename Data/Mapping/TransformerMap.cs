using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hvex.Domain.Entity;

namespace Hvex.Data.Mapping {
    public class TransformerMap : IEntityTypeConfiguration<Transformer> {
        public void Configure(EntityTypeBuilder<Transformer> builder) {
            builder.ToTable("transformer");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(10).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdateAt).IsRequired(false);
            builder.Property(c => c.Potency).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.InternalNumber).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.TensionClass).HasMaxLength(128).IsRequired();
            builder.Property(c => c.UserId).HasMaxLength(10).IsRequired();

            builder.HasMany(rp => rp.Reports)
                   .WithOne(tr => tr.Transformer)
                   .HasForeignKey(tr => tr.TransformerId);

            builder.HasMany(ts => ts.Tests)
                   .WithOne(tr => tr.Transformer)
                   .HasForeignKey(tr => tr.TransformerId);

        }
    }
}
