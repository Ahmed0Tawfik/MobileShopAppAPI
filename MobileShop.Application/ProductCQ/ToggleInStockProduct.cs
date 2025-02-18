using MobileShop.Application.APIResponse;
using MobileShop.Domain.Interfaces;
using MobileShop.Domain.Models;
using FluentValidation;

namespace MobileShop.Application.ProductCQ
{
    public class ToggleInStockProduct : IRequest<ApiResponse<ProductResponse>>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<ToggleInStockProduct>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            }
        }
    }

    public class ToggleInStockProductHandler : IRequestHandler<ToggleInStockProduct, ApiResponse<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToggleInStockProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(ToggleInStockProduct request, CancellationToken ct)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (product == null)
            {
                return ApiResponse<ProductResponse>.Error(null, "Product not found");
            }
            product.InStock = !product.InStock;
            var result = await _unitOfWork.Repository<Product>().UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            if (result == null)
            {
                return ApiResponse<ProductResponse>.Error(result, "Failed to update product");
            }
            return ApiResponse<ProductResponse>.Success(new ProductResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Price = result.Price,
                InStock = result.InStock
            });
        }
    }

}
