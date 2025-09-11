using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Exports.Queries;
using VeriShip.Application.Features.Projects;
using VeriShip.Application.Features.QcSpecifications;
using VeriShip.Application.Features.QcSpecifications.Queries;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Domain.Templates;
using VeriShip.Infrastructure.Persistence;
using VeriShip.Infrastructure.Services;
using VeriShip.Infrastructure.Services.Model.WordProcessor;
using Result = Ardalis.Result.Result;

namespace VeriShip.Application.Features.Exports;

public class ExportStore(
    IConfiguration configuration,
    IProjectStore projectStore,
    IQcSpecificationStore qcSpecificationStore,
    IApplicationDbContextFactory dbContextFactory,
    ILogger<ExportStore> logger
    ) : IExportStore
{
    public async Task<Result<byte[]>> Handle(PreviewCoA request, CancellationToken cancellationToken = default)
    {
        var path = configuration.GetValue<string>("Templates");
        if (path == null)
        {
            return Result.Error("Path templates not found in configuration");
        }
        var userResult = request.ClaimsPrincipal.ToUserResult();
        if (!userResult.IsSuccess) return userResult.Map();
        if (request.ProjectNumber!=string.Empty)
        {
            var projectResult = await projectStore.Query(request, cancellationToken);
            if (!projectResult.IsSuccess) return projectResult.Map();
        }
      
        
        var specsResult = await qcSpecificationStore.Query(new GetAll(), cancellationToken);
        if (!specsResult.IsSuccess) return specsResult.Map();
        var coas = specsResult.Value;
        
        var results = request.Results;
        var specificationIdsResults = results.Select(x => x.QcSpecificationId).ToList();
        var commentCheck = coas.FirstOrDefault(x => x.SpecialField == SpecialField.Comment);
        var comments = results.FirstOrDefault(x => x.QcSpecificationId == commentCheck?.Id)?.Value;
        var fields = new MergeCoA()
        {
            CreatedBy = userResult.Value.Name,
            GeneralInformations = coas
                .Where(x => x.Table == Table.GeneralInformation && specificationIdsResults.Contains(x.Id))
                .Select(x => new GeneralInformation()
                {
                    Category = x.Category,
                    Label = x.Acceptance,
                    Result = results.FirstOrDefault(y => y.QcSpecificationId == x.Id)?.Value,
                }).ToList(),
            Tests = coas
                .Where(x => x.Table == Table.Tests && specificationIdsResults.Contains(x.Id))
                .Select(x => new Test()
                {
                    Name = x.Category,
                    Acceptance = x.Acceptance,
                    Technique = x.Technique,
                    Result = results.FirstOrDefault(y => y.QcSpecificationId == x.Id)?.Value,
                }).ToList(),
            Comment = comments,
            Salts = request.Salts
            
        };
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var tempplate =
                await db.Templates.OrderBy(x=>x.Id).LastOrDefaultAsync(x =>
                    x.Active && x.TemplateType == TemplateType.CertificateOfAnalysis, cancellationToken: cancellationToken);
            if (tempplate is null)
            {
                return Result<byte[]>.NotFound("Template not found");
            }
            var filePath = Path.Combine(configuration["Templates"], $"{tempplate.Id}.docx");
            var wordProcessor = WordProcessor.Create(new Options
                {
                    PathTemplate = filePath
                }, fields)
                .PrettifyTables()
                .AddWatermark("PREVIEW")
                .ReplaceSvgStructure(request.Svg);
         
            var bytes =    wordProcessor.ExportToPdf();
            return new (bytes);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting all requests");
            return Result.Error(e.Message);
        }
    }
}