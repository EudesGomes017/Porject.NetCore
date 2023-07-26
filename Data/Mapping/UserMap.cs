using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hvex.Domain.Entity;

namespace Hvex.Data.Mapping {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("user");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(10).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdateAt).IsRequired(false);
            builder.Property(c => c.Email).HasMaxLength(128).IsRequired();
            builder.Property(c => c.Password).IsRequired();

            builder.HasMany(tr => tr.Transformers)
                   .WithOne(u => u.User)
                   .HasForeignKey(tr => tr.UserId);
                   //.OnDelete(DeleteBehavior.Cascade);
              
        }
    }
}
