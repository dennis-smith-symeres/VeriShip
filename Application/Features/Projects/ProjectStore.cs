
using Microsoft.Extensions.Logging;

using VeriShip.Infrastructure.Persistence;


namespace VeriShip.Application.Features.Projects;

public class ProjectStore(IApplicationDbContextFactory dbContextFactory, ILogger<ProjectStore> logger) : IProjectStore
{
    
}