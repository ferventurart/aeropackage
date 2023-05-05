using System;
using System.Globalization;
using System.Reflection;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AeroPackage.Infrastructure.Persistence.Configurations;

public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        ConfigurePackage(builder);
        ConfigurePackageTimeLine(builder);
    }

    private static void ConfigurePackage(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable("Packages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
             .HasConversion(
                id => id.Value,
                value => PackageId.Create(value))
             .ValueGeneratedOnAdd()
             .UseIdentityColumn();

        builder.Property(m => m.OwnTrackingNumber)
               .HasConversion(
                id => id.Value,
                value => OwnTrackingNumber.Create(value))
               .HasMaxLength(13);

        builder.HasIndex(d => d.OwnTrackingNumber)
               .IsUnique();

        builder.Property(m => m.UserId)
             .ValueGeneratedNever()
             .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(m => m.CustomerId)
             .ValueGeneratedNever()
             .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));

        builder.Property(d => d.Consignee)
            .HasMaxLength(120)
            .IsRequired(true);

        builder.Property(d => d.Store)
            .HasMaxLength(100);

        builder.Property(d => d.Courier)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(d => d.CourierTrackingNumber)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasIndex(d => d.CourierTrackingNumber);

        builder.Property(d => d.QuantityArticles)
               .IsRequired();

        builder.Property(d => d.Weight)
            .HasColumnType("decimal(10,2)");

        builder.Property(d => d.Description)
            .HasMaxLength(100);

        builder.Property(d => d.DeclaredValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired(true);

        builder.Property(d => d.TaxValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired(true);

        builder.Property(m => m.Status)
            .HasConversion(
            p => p.Value,
            p => PackageStatus.FromValue(p));

        builder.Property(d => d.CreatedBy)
            .HasMaxLength(120);

        builder.Property(d => d.Attachments)
               .HasColumnName("Attachments")
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
        .IsRequired(false);
    }

    private static void ConfigurePackageTimeLine(EntityTypeBuilder<Package> builder)
    {
        builder.OwnsMany(m => m.PackageHistories, ph =>
        {
            ph.ToTable("PackageHistories");

            ph.WithOwner().HasForeignKey("PackageId");

            ph.HasKey("Id", "PackageId");

            ph.Property(s => s.Id)
              .HasColumnName("PackageHistoryId")
              .ValueGeneratedNever()
              .HasConversion(
                    id => id.Value,
                    value => PackageHistoryId.Create(value));

            ph.Property(s => s.DateMovement)
              .IsRequired(true);

            ph.Property(s => s.Status)
              .HasMaxLength(30)
              .IsRequired(true);
        });

        builder.Metadata.FindNavigation(nameof(Package.PackageHistories))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

