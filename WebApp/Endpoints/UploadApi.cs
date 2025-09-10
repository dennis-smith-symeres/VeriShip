using Microsoft.AspNetCore.Mvc;
using VeriShip.Application.Common;

namespace VeriShip.WebApp.Endpoints;

public static class UploadApi
{
    public class UploadRequest
    {
        public string dataKey { get; set; }
        public IFormFile files { get; set; }
    }

    public static RouteGroupBuilder MapUpload(this IEndpointRouteBuilder routes)
    {
        var mapGroup = routes.MapGroup("/upload").RequireAuthorization(Roles.Access);

        mapGroup.MapPost("save",
            async Task<IResult> (HttpContext http, IConfiguration configuration, [FromForm] UploadRequest request) =>
            {
                

                var baseFolder = configuration["UploadFolder"];
              


             
             
                //
                //
                // var saveLocation = Path.Combine(uploadFolder, "1.jpg");
                //
                // // Ensure proper disposal of file stream
                // await using (var fileStream =
                //              new FileStream(saveLocation, FileMode.Create, FileAccess.Write, FileShare.None))
                // {
                //     await file.CopyToAsync(fileStream);
                // }

                return TypedResults.NoContent();
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