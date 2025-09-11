using Microsoft.AspNetCore.Mvc;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Templates;
using VeriShip.Application.Features.Templates.Commands;
using VeriShip.Domain.Templates;
using VeriShip.WebApp.Models;

namespace VeriShip.WebApp.Endpoints;

public static class UploadApi
{
    public class UploadRequest
    {
        public TemplateType TemplateType { get; set; }
        public IFormFile files { get; set; }
    }

    public static RouteGroupBuilder MapUpload(this IEndpointRouteBuilder routes)
    {
        var mapGroup = routes.MapGroup("/upload").RequireAuthorization(Roles.Access);

        mapGroup.MapPost("save",
            async Task<IResult> (HttpContext http, IConfiguration configuration, ITemplateStore templateStore, [FromForm] UploadRequest request) =>
            {
                var file = request.files;
                if (file is not { ContentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document" } || file.Length == 0)
                {
                    return TypedResults.BadRequest("Invalid file");
                }

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, CancellationToken.None);
                var fileBytes = memoryStream.ToArray();
                var upsert = new Upsert
                {
                    Bytes = fileBytes,
                    TemplateType = request.TemplateType,
                    User = http.User,
                    FileName = file.FileName
                };
                var result = await templateStore.Handle(upsert, CancellationToken.None); 
                return  TypedResults.Ok(result);
                
            }).DisableAntiforgery();

        mapGroup.MapPost("remove", async (HttpContext http, IWebHostEnvironment env, IConfiguration configuration) =>
        {
            var form = await http.Request.ReadFormAsync();
            var fileName = form["files"].FirstOrDefault();

            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                  
                }
                catch (Exception ex)
                {
                    http.Response.StatusCode = 500;
                    await http.Response.WriteAsync($"Delete failed. Error: {ex.Message}");
                    return;
                }
            }

            http.Response.StatusCode = 200;
        });


        return mapGroup;
    }
}