using Ardalis.Result;
using Microsoft.Extensions.Logging;
using RefitClient;
using RefitClient.Queries;
using RefitClient.Responses;
using RefitClient.Responses.Data;
using VeriShip.Application.Features.Signals.Constants;
using VeriShip.Application.Features.Signals.Models;
using VeriShip.Application.Features.Signals.Queries;
using VeriShip.Domain.Entities.QCSpecifications;
using ZiggyCreatures.Caching.Fusion;

namespace VeriShip.Application.Features.Signals;



public class SignalsStore(IRefitClient client, IFusionCache cache, ILogger<SignalsStore> logger) : ISignalsStore
{
    public async Task<Result<Notebook>> Query(GetNotebook request)
    {
        var projectNumber = request.ProjectNumber;
        var user = request.User;
        try
        {
            var journalResponse = await cache.GetOrSetAsync(
                CacheKeys.Journal(projectNumber),
                _ => client.GetJournal(projectNumber)
            );

            if (journalResponse == null)
            {
                return Result<Notebook>.NotFound("Signals journal not found");
            }

            var canRead = await cache.GetOrSetAsync(
                CacheKeys.UserJournalPermission(user, projectNumber, Security.Read),
                _ => client.CheckPermission(journalResponse.Data[0].Attributes, Security.Read, user)
            );
            if (!canRead && user.StartsWith("Admin."))
            {
                var userResponse = await cache.GetOrSetAsync(
                    CacheKeys.User(user),
                    _ => client.Users(new UserQuery(user))
                );

                if (userResponse.Data.Any(d => d.Relationships.Roles.Data.Any(d => d.Id == "1")))
                {
                    canRead = true;
                }
            }

            if (!canRead)
            {
                return Result<Notebook>.Forbidden();
            }

            return new(new Notebook()
            {
                Description = journalResponse.Data[0].Attributes.Description,
                Client = journalResponse.Data[0].Attributes.Tags.FieldsClient,
                Id = journalResponse.Data[0].Attributes.Eid,
                CanRead = canRead
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting notebook");
            return Result<Notebook>.Error(e.Message);
        }
    }
}