using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

public class AuthenticationMethodsConfiguration : IEntityTypeConfiguration<AuthenticationMethod>
{
    public void Configure(EntityTypeBuilder<AuthenticationMethod> builder)
    {
        builder.ToTable("AuthenticationMethods");
        builder.HasDiscriminator(i => i.Type)
            .HasValue<GoogleAuthenticationMethod>(AuthenticationMethodType.Google)
            .HasValue<PasswordAuthenticationMethod>(AuthenticationMethodType.Password);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, v => new AuthenticationMethodId(v));
        builder.Property(i => i.Type)
            .IsRequired();
        builder.Property(i => i.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(i => i.UpdatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class GoogleAuthenticationMethodsConfiguration : IEntityTypeConfiguration<GoogleAuthenticationMethod>
{
    public void Configure(EntityTypeBuilder<GoogleAuthenticationMethod> builder)
    {
        builder.Property(i => i.AccessToken)
            .ValueGeneratedNever()
            .HasConversion(token => token.Value, v => new AccessToken(v));
        builder.Property(i => i.RefreshToken)
            .ValueGeneratedNever()
            .HasConversion(token => token.Value, v => new RefreshToken(v));
    }
}

public class PasswordAuthenticationMethodsConfiguration : IEntityTypeConfiguration<PasswordAuthenticationMethod>
{
    public void Configure(EntityTypeBuilder<PasswordAuthenticationMethod> builder)
    {
        builder.Property(i => i.Password)
            .ValueGeneratedNever()
            .HasConversion(password => password.Hash, v => new Password(v));
    }
}
