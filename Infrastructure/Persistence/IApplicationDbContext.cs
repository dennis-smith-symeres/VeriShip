using Microsoft.EntityFrameworkCore;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Infrastructure.Persistence;

public interface IApplicationDbContext : IAsyncDisposable
{
    DbSet<QcSpecification> QcSpecifications { get; set; }
    public DbSet<Sort> QcSpecificationsSort { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<QcRequest> QcRequests { get; set; }

    Task<int> SaveChangesAsync(string userIdentity, CancellationToken cancellationToken = default);

}