using System;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using AeroPackage.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroPackage.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        ConfigureServicesTable(builder);
    }

    private static void ConfigureServicesTable(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ServiceId.Create(value));

        builder.Property(m => m.Name)
               .HasMaxLength(60);

        builder.Property(d => d.Rate)
            .HasColumnType("decimal(10,2)")
            .IsRequired(true);

        builder.Property(m => m.Status)
            .HasConversion(
            p => p.Value,
            p => ServiceStatus.FromValue(p));
    }
}

