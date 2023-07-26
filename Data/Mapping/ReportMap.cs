using Hvex.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hvex.Data.Mapping {
    public class ReportMap : IEntityTypeConfiguration<Report> {
        public void Configure(EntityTypeBuilder<Report> builder) {
            builder.ToTable("report");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(10).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdateAt).IsRequired(false);
            builder.Property(c => c.Status).IsRequired().HasDefaultValue(true);
            builder.Property(c => c.TestId).HasMaxLength(10).IsRequired();
            builder.Property(c => c.TransformerId).HasMaxLength(10).IsRequired();
        }
    }
}
