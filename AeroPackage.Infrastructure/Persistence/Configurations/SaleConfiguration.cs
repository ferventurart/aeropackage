using System;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.SaleAggregate;
using AeroPackage.Domain.SaleAggregate.Enums;
using AeroPackage.Domain.SaleAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroPackage.Infrastructure.Persistence.Helpers;
using AeroSale.Domain.SaleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroPackage.Infrastructure.Services;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        ConfigureSale(builder);
        ConfigureSaleItems(builder);
    }

    private static void ConfigureSale(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
             .HasConversion(
                id => id.Value,
                value => SaleId.Create(value))
             .ValueGeneratedOnAdd()
             .UseIdentityColumn();

        builder.Property(m => m.InvoiceNumber)
               .HasConversion(
                id => id.Value,
                value => InvoiceNumber.Create(value))
               .HasMaxLength(13);

        builder.HasIndex(d => d.InvoiceNumber)
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

        builder.Property(d => d.DateIssued)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .IsRequired(true);

        builder.Property(d => d.DateDue)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .IsRequired(true);

        builder.Property(d => d.Reference)
               .HasMaxLength(120);

        builder.Property(d => d.Notes)
               .HasMaxLength(200);

        builder.Property(d => d.Terms)
               .HasMaxLength(200);

        builder.Property(d => d.Subtotal)
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);

        builder.Property(d => d.Discount)
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);

        builder.Property(d => d.Tax)
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);

        builder.Property(d => d.Deposit)
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);

        builder.Property(d => d.AmountDue)
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);

        builder.Property(m => m.Status)
               .HasConversion(
                    p => p.Value,
                    p => SaleStatus.FromValue(p));

        builder.Property(d => d.CreatedBy)
               .HasMaxLength(120);

        builder.Property(d => d.Attachments)
               .HasColumnName("Attachments")
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
        .IsRequired(false);
    }

    private static void ConfigureSaleItems(EntityTypeBuilder<Sale> builder)
    {
        builder.OwnsMany(m => m.Items, i =>
        {
            i.ToTable("SaleItems");

            i.WithOwner().HasForeignKey("SaleId");

            i.HasKey("Id", "SaleId");

            i.Property(s => s.Id)
             .HasColumnName("SaleItemId")
             .ValueGeneratedNever()
             .HasConversion(
                id => id.Value,
                value => SaleItemId.Create(value));

            i.Property(s => s.PackageId)
             .HasColumnName("PackageId")
             .ValueGeneratedNever()
             .HasConversion(
                id => id.Value,
                value => PackageId.Create(value))
             .IsRequired(false);

            i.Property(s => s.Description)
             .HasMaxLength(50)
             .IsRequired();

            i.Property(s => s.Quantity)
             .IsRequired(true);

            i.Property(d => d.Rate)
             .HasColumnType("decimal(10,2)")
             .IsRequired(true);

            i.Property(d => d.LineTotal)
             .HasColumnType("decimal(10,2)")
             .IsRequired(true);
        });

        builder.Metadata.FindNavigation(nameof(Sale.Items))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

