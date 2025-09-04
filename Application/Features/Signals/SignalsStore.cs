using RefitClient;
using RefitClient.Queries;
using RefitClient.Responses;
using RefitClient.Responses.Data;
using VeriShip.Application.Features.Signals.Constants;
using VeriShip.Application.Features.Signals.Models;
using VeriShip.Domain.Entities.QCSpecifications;
using ZiggyCreatures.Caching.Fusion;

namespace VeriShip.Application.Features.Signals;

public record GetNotebook(string ProjectNumber, string User);

public class SignalsStore(IRefitClient client, IFusionCache cache) : ISignalsStore
{
    public async Task<Notebook?> Query(GetNotebook request)
    {
        var projectNumber = request.ProjectNumber;
        var user = request.User;

        var journalResponse = await cache.GetOrSetAsync(
            CacheKeys.Journal(projectNumber),
            _ => client.GetJournal(projectNumber)
        );

        if (journalResponse == null)
        {
            return null;
        }
        var canRead = await client.CheckPermission(journalResponse.Data[0].Attributes, Security.Read, user);
        if (!canRead && user.StartsWith("Admin."))
        {
            var userResponse = await cache.GetOrSetAsync(
                CacheKeys.User(user),
                _ => client.Users(new UserQuery(user))
            );
            
            if (userResponse.Data.Any(d => d.Relationships.Roles.Data.Any(d =>d.Id =="1")))
            {
                canRead = true;
            }
        }
      
        return new Notebook()
        {
            Description = journalResponse.Data[0].Attributes.Description,
            Client = journalResponse.Data[0].Attributes.Tags.FieldsClient,
            Id = journalResponse.Data[0].Attributes.Eid,
            CanRead = canRead
        };
        
    }
}