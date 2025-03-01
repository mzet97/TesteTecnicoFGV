﻿using DesafioFGV.Domain.Entities;
using DesafioFGV.Domain.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DesafioFGV.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>(options)
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (loggerFactory != null)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("CreatedAt") != null);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("CreatedAt").IsModified = true;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("CreatedAt").IsModified = false;
            }

            var updatedAtProp = entry.Entity.GetType().GetProperty("UpdatedAt");
            if (updatedAtProp != null)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
    public DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;
    public DbSet<ApplicationUserClaim> ApplicationUserClains { get; set; } = null!;
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; } = null!;
    public DbSet<ApplicationRoleClaim> ApplicationRoleClains { get; set; } = null!;
    public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;
}