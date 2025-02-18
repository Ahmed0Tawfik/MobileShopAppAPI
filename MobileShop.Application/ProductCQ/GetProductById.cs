using FluentValidation;
using MobileShop.Application.APIResponse;
using MobileShop.Domain.Interfaces;
using MobileShop.Domain.Models;

namespace MobileShop.Application.ProductCQ
{
    public class GetProductByIdCommnad : IRequest<ApiResponse<ProductResponse>>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<GetProductByIdCommnad>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            }
        }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdCommnad, ApiResponse<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdCommnad request, CancellationToken ct)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (product == null)
            {
                return ApiResponse<ProductResponse>.Error("Product not found");
            }
            return ApiResponse<ProductResponse>.Success(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsNew = product.IsNew
            });
        }
    }
}
