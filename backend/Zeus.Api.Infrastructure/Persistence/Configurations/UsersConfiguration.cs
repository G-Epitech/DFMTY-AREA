using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Common.Domain.UserAggregate;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

public class UsersConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, v => new UserId(v));
        builder.Property(u => u.Email)
            .HasMaxLength(255);
        builder.Property(u => u.FirstName)
            .HasMaxLength(100);
        builder.Property(u => u.LastName)
            .HasMaxLength(100);
        builder.Property(u => u.Password)
            .HasMaxLength(255);
        builder.Property(x => x.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedNever()
            .IsRequired();
    }
}
