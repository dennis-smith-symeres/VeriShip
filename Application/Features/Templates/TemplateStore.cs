using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Templates.Commands;
using VeriShip.Application.Features.Templates.Queries;
using VeriShip.Domain.Templates;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.Templates;

public class TemplateStore(IApplicationDbContextFactory dbContextFactory, IConfiguration configuration, ILogger<TemplateStore> logger) : ITemplateStore
{
   
    public async Task<Result<IEnumerable<Template>>> Query(Get request, CancellationToken cancellationToken = default)
    {
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var templates = await db.Templates.AsNoTracking()
                .Where(x=>x.TemplateType == request.TemplateType
                && x.Active == true
                )
                .ToListAsync(cancellationToken);
            
            return new(templates);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting templates");
            return Result.Error(e.Message);
        }
    }

    public async Task<Result<int>> Handle(Upsert command, CancellationToken cancellationToken = default)
    {
        var path = configuration.GetValue<string>("Templates");
        if (path == null)
        {
            return Result.Error("Path templates not found in configuration");
        }

        var userResult = command.User.ToUserResult();
        if (!userResult.IsSuccess)
        {
            return userResult.Map();       
        }
        var db = await dbContextFactory.CreateAsync(cancellationToken);
        var template = new Template()
        {
            Active = true,
            TemplateType = command.TemplateType,
            Name = command.FileName,
            Size = command.Bytes.Length
        };
        db.Templates.Add(template);
        try
        {
            await db.SaveChangesAsync(userResult.Value.Name, cancellationToken);
            var fileName = Path.Combine(path, $"{template.Id}.docx");
            await File.WriteAllBytesAsync(fileName, command.Bytes, cancellationToken);
            return Result.Success(template.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Error(e.Message);
        }


    }

    public async Task<Result<int>> Handle(Remove command, CancellationToken cancellationToken = default)
    {
        var db = await dbContextFactory.CreateAsync(cancellationToken);
        var userResult = command.User.ToUserResult();
        if (!userResult.IsSuccess)
        {
            return userResult.Map();       
        }
        try
        {
            var template = await db.Templates.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            if (template is null)
            {
                return Result.NotFound("Template not found");
            }
            db.Templates.Remove(template);
            await db.SaveChangesAsync(userResult.Value.Name, cancellationToken);
            return new(template.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Error(e.Message);
        }
    }
}

