using System.Collections;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Addresses;
using VeriShip.Domain.Entities.Attachments;
using VeriShip.Domain.Entities.Audits;
using VeriShip.Domain.Entities.Boxes;
using VeriShip.Domain.Entities.Email;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Domain.Entities.QcRequests.Items;
using AuditField = VeriShip.Domain.Entities.Audits.Field;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Domain.Entities.Settings;
using Stage = VeriShip.Domain.Entities.QcRequests.Stage;
using BoxItem = VeriShip.Domain.Entities.Boxes.Items.Item;
using BoxStage = VeriShip.Domain.Entities.Boxes.Stage;
using QCSpecifications_ProjectResult = VeriShip.Domain.Entities.QCSpecifications.ProjectResult;

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
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<QcRequest> QcRequests { get; set; }
    public DbSet<Item> QcRequestItems { get; set; }
    public DbSet<ItemResult> QcRequestItemResults { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<ProjectResult> ProjectQcRequestItemResults { get; set; }

    public DbSet<EmailMessage> EmailMessages { get; set; }

    public DbSet<BoxItem> BoxItems { get; set; }
    public DbSet<Box> Boxes { get; set; }
    public DbSet<BoxStage> BoxStages { get; set; }
    public DbSet<BoxType> BoxTypes { get; set; }
    public DbSet<Setting> Settings { get; set; }
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
        
        modelBuilder.Entity<Project>()
            .HasIndex(x => x.ProjectNumber)
            .IsUnique();
        modelBuilder.Entity<ProjectResult>()
            .HasIndex(x => new {x.ProjectId, x.QcSpecificationId})
            .IsUnique();
        modelBuilder.Entity<ItemResult>()
            .HasIndex(x => new {x.ItemId, x.QcSpecificationId})
            .IsUnique();
        
  
        modelBuilder.Entity<BoxItem>()
            .OwnsMany(
                item => item.ExtraFields, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                }
            );
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
            if (entry is { State: EntityState.Added, Entity: Entity b })
            {
                b.CreatedBy = userId;
                b.CreatedOn = DateTime.UtcNow;
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
                        if (entry.Entity is Entity)
                        {
                            entry.State = EntityState.Unchanged;
                            entry.CurrentValues["Active"] = false;

                            auditEntry.Action = nameof(EntityState.Deleted);
                        }

                        break;
                }
            }
        }

        return auditEntries;
    }

    private Task OnBeforeSave(List<AuditEntry>? audits)
    {
        if (audits == null || audits.Count == 0)
        {
            return Task.CompletedTask;
        }

        foreach (var trail in audits.Where(a => !a.HasTemporaryProperties))
        {
            Audits.Add(trail.ToAudit());
        }

        return Task.CompletedTask;
    }

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        var auditEntriesWithTemporaryProperties = auditEntries
            .Where(a => a.HasTemporaryProperties).ToArray();
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