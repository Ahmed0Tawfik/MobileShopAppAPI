using MobileShop.Application;
using MobileShop.Application.APIResponse;
using MobileShop.Application.ProductCQ;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MobileShop.API.EndPoints
{
    public static class ProductEndPoints
    {
        public static void MapProductEndPoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/Products")
                .WithOpenApi();

            endpoints.MapPost("/add-product", async (
                [FromBody] AddProductCommand request,
                [FromServices] IRequestHandler<AddProductCommand, ApiResponse<ProductResponse>> handler,
                IValidator<AddProductCommand> validator,
                CancellationToken ct) =>
            {
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Add Product");




            endpoints.MapGet("/get-all", async (
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize,
                [FromQuery] bool? InStock,
                [FromQuery] string? search,
                [FromQuery] bool? IsNew,
                [FromServices] IRequestHandler<GetAllProductsCommand, ApiResponse<PagedResponse<ProductResponse>>> handler,
                IValidator<GetAllProductsCommand> validator,
                CancellationToken ct) =>
            {
                var request = new GetAllProductsCommand();
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;
                request.InStock = InStock;
                request.Search = search;
                request.IsNew = IsNew;
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Get All Products");

            endpoints.MapDelete("/delete-product", async (
                [FromQuery] Guid Id,
                [FromServices] IRequestHandler<DeleteProductCommnad, ApiResponse<ProductResponse>> handler,
                IValidator<DeleteProductCommnad> validator,
                CancellationToken ct) =>
            {
                var request = new DeleteProductCommnad();
                request.Id = Id;
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Delete Product"); ;

            endpoints.MapPut("/update-product", async (
                [FromBody] UpdateProductCommand request,
                [FromServices] IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponse>> handler,
                IValidator<UpdateProductCommand> validator,
                CancellationToken ct) =>
            {
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Update Product");


            endpoints.MapPut("/toggle-in-stock", async (
                [FromBody] ToggleInStockProduct request,
                [FromServices] IRequestHandler<ToggleInStockProduct, ApiResponse<ProductResponse>> handler,
                IValidator<ToggleInStockProduct> validator,
                CancellationToken ct) =>
            {
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Toggle InStock");


            endpoints.MapGet("/get-by-id", async (
                [FromQuery] Guid Id,
                [FromServices] IRequestHandler<GetProductByIdCommnad, ApiResponse<ProductResponse>> handler,
                IValidator<GetProductByIdCommnad> validator,
                CancellationToken ct) =>
            {
                var request = new GetProductByIdCommnad();
                request.Id = Id;
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Get Product By Id");


        }
    }
}
