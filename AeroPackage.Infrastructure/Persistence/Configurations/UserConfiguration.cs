using System;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.Enums;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroPackage.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(m => m.FirstName)
               .HasMaxLength(60);

        builder.Property(m => m.LastName)
               .HasMaxLength(60);

        builder.Property(m => m.Email)
               .HasMaxLength(250);

        builder.HasIndex(m => m.Email);

        builder.Property(m => m.Password)
               .HasMaxLength(255);

        builder.Property(m => m.Status)
               .HasConversion(
                    p => p.Value,
                    p => UserStatus.FromValue(p));

        builder.Property(m => m.Role)
               .HasConversion(
                    p => p.Value,
                    p => UserRole.FromValue(p));
    }
}

