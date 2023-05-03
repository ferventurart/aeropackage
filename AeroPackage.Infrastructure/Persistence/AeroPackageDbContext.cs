using System;
using System.Reflection.Metadata;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.SaleAggregate;
using AeroPackage.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence;

public class AeroPackageDbContext : DbContext
{
    public AeroPackageDbContext(DbContextOptions<AeroPackageDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Package> Packages { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AeroPackageDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}