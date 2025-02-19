using Microsoft.AspNetCore.Mvc;
using MobileShop.Application;
using MobileShop.Application.APIResponse;
using MobileShop.Application.ImagesCQ;

namespace MobileShop.API.EndPoints
{
    public static class ImagesEndPoints
    {
        public static void MapImagesEndPoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/Images")
                .WithOpenApi();

            endpoints.MapPost("/upload-image", async (
                 HttpContext context, // Inject HttpContext
                 [FromForm] IFormFile file,
                 [FromServices] IRequestHandler<UploadImageCommand, ApiResponse<UploadFileResponse>> handler,
                 CancellationToken ct) =>
            {
                var request = new UploadImageCommand();
                request.File = file;
                request.RequestHost = context.Request.Host.Value; // Get the host
                request.RequestScheme = context.Request.Scheme;   // Get the scheme

                var result = await handler.Handle(request, ct);
                return result;
            })
            .WithSummary("Upload Image")
            .DisableAntiforgery();


            endpoints.MapDelete("/delete-image", async (
                HttpContext context, // Inject HttpContext
                [FromQuery] string url,
                [FromServices] IRequestHandler<DeleteImageCommand, ApiResponse<UploadFileResponse>> handler,
                CancellationToken ct) =>
            {
                var request = new DeleteImageCommand(url);
                var result = await handler.Handle(request, ct);
                return result;
            })
            .WithSummary("Delete Image");
        }
    }
}
