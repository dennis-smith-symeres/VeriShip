using Microsoft.EntityFrameworkCore;
using VeriShip.Domain.Entities.Attachments;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Domain.Templates;

namespace VeriShip.Infrastructure.Persistence;

public interface IApplicationDbContext : IAsyncDisposable
{
    DbSet<QcSpecification> QcSpecifications { get; set; }
    public DbSet<Sort> QcSpecificationsSort { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectResult> ProjectQcRequestItemResults { get; set; }
    public DbSet<Template> Templates { get; set; }
    
    public DbSet<QcRequest> QcRequests { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

    Task<int> SaveChangesAsync(string userIdentity, CancellationToken cancellationToken = default);

}