
using System;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace AeroPackage.Infrastructure.Persistence.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        ConfigureCustomersTable(builder);
        ConfigureCustomersPackageIdsTable(builder);
    }

    private static void ConfigureCustomersTable(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));

        builder.Property(m => m.FirstName)
               .HasMaxLength(60);

        builder.Property(m => m.LastName)
               .HasMaxLength(60);

        builder.Property(m => m.Email)
               .HasMaxLength(250);

        builder.HasIndex(m => m.Email);

        builder.Property(m => m.Gender)
               .HasConversion(
                    p => p.Value,
                    p => Gender.FromValue(p));

        builder.Property(m => m.BirthDate)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.Property(m => m.CellPhoneNumber)
               .HasConversion(
                id => id.Value,
                value => PhoneNumber.Create(value))
               .HasMaxLength(9);

        builder.Property(m => m.Identification)
               .HasConversion(
                id => id.Value,
                value => NationalIdentification.Create(value))
               .HasMaxLength(10);

        builder.Property(m => m.Address)
               .HasMaxLength(200);

        builder.Property(m => m.Status)
               .HasConversion(
                    p => p.Value,
                    p => CustomerStatus.FromValue(p));
    }

    private static void ConfigureCustomersPackageIdsTable(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsMany(c => c.PackageIds, p =>
        {
            p.WithOwner().HasForeignKey("CustomerId");

            p.ToTable("CustomerPackageIds");

            p.HasKey("Id");

            p.Property(pk => pk.Value)
                .ValueGeneratedNever()
                .HasColumnName("PackageId");
        });

        builder.Metadata.FindNavigation(nameof(Customer.PackageIds))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

