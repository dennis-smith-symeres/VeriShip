
using VeriShip.Application.Features.Projects.Queries;


namespace VeriShip.Application.Features.Projects;

public class ProjectStore() : IProjectStore
{
    public async Task Handle(GetProject request, CancellationToken cancellationToken)
    {
        
       // var response = await client.Entities["journal:6d72b837-7dc9-4cff-bbe5-a5aed9618e49"].GetAsync(cancellationToken: cancellationToken);
       
    }
}

public interface IProjectStore
{
    Task Handle(GetProject request, CancellationToken cancellationToken);
}