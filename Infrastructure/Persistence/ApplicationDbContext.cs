using System.Collections;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Audits;
using AuditField = VeriShip.Domain.Entities.Audits.Field;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,  ILogger<ApplicationDbContext> logger) : base(options)
    {
      
    }

    public DbSet<Audit> Audits { get; set; }
    public DbSet<AuditField> AuditFields { get; set; }
    
    public DbSet<QcSpecification> QcSpecifications { get; set; }


    public DbSet<Sort> QcSpecificationsSort { get; set; }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>()
            .HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sort>()
            .HasIndex(x => x.Table)
            .IsUnique();
    }
    
    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Use method SaveChangesAsync(string userIdentity)");
    }

    
    public async Task<int> SaveChangesAsync(string userIdentity, CancellationToken cancellationToken = default)
    {
        var auditEntries = GetChanges(userIdentity);
        await OnBeforeSave(auditEntries);
        var result = await base.SaveChangesAsync(cancellationToken);
        await OnAfterSaveChanges(auditEntries);
        await base.SaveChangesAsync(cancellationToken);
        return result;
    }

      private List<AuditEntry> GetChanges(string userId)
    {
        base.ChangeTracker.DetectChanges();

        var auditEntries = new List<AuditEntry>();

        var en = base.ChangeTracker.Entries();
        foreach (var entry in base.ChangeTracker.Entries())
        {
            if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                entry.State == EntityState.Unchanged)
                continue;

            var tableName = entry.Metadata.GetTableName();

            var auditEntry = new AuditEntry()
            {
                User = userId,
                TableName = tableName,
                Action = entry.State.ToString()
            };

            auditEntries.Add(auditEntry);
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is Entity b)
                {
                    b.CreatedBy = userId;
                    b.CreatedOn = DateTime.UtcNow;
                }
            }

            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    //primary key should be id
                    var rowId = (int)(property.CurrentValue ?? 0);
                    auditEntry.RowId = rowId < 0 ? 0 : rowId;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        if (property.CurrentValue != null)
                            auditEntry.NewValues.Add(new AuditField()
                            {
                                Name = propertyName,
                                Value = property.Metadata.IsPrimitiveCollection
                                    ? JsonSerializer.Serialize(property.CurrentValue as IEnumerable)
                                    : property.CurrentValue?.ToString(),
                                IsPrimitiveCollection = property.Metadata.IsPrimitiveCollection
                            });
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.NewValues.Add(new AuditField()
                            {
                                Name = propertyName,
                                Value = property.Metadata.IsPrimitiveCollection
                                    ? JsonSerializer.Serialize(property.CurrentValue as IEnumerable)
                                    : property.CurrentValue?.ToString(),
                                IsPrimitiveCollection = property.Metadata.IsPrimitiveCollection
                            });
                        }

                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is Entity b)
                        {
                            entry.State = EntityState.Unchanged;
                            entry.CurrentValues["Active"] = false;

                            auditEntry.Action = EntityState.Deleted.ToString();
                        }

                        break;
                }
            }
        }

        return auditEntries;
    }

    private Task OnBeforeSave(List<AuditEntry> audits)
    {
        if (audits == null || audits.Count == 0)
        {
            return Task.CompletedTask;
        }

        foreach (var trail in audits.Where(_ => !_.HasTemporaryProperties))
        {
            Audits.Add(trail.ToAudit());
        }

        return Task.CompletedTask;
    }

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        var auditEntriesWithTemporaryProperties = auditEntries.Where(a => a.HasTemporaryProperties);
        if (auditEntriesWithTemporaryProperties.Any() is false)
        {
            return Task.CompletedTask;
        }

        foreach (var auditEntry in auditEntriesWithTemporaryProperties)
        {
            foreach (var prop in auditEntry.TemporaryProperties
                         .Where(x => x.Metadata.IsPrimaryKey()))
            {
                var rowId = (int)(prop.CurrentValue ?? 0);
                auditEntry.RowId = rowId < 0 ? 0 : rowId;
            }

            Audits.Add(auditEntry.ToAudit());
        }

        return Task.CompletedTask;
    }
}