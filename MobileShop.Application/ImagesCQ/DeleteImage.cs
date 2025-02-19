using FluentValidation;
using MobileShop.Application.APIResponse;
using MyImageService.Interfaces;

namespace MobileShop.Application.ImagesCQ
{
    public record DeleteImageCommand(string Url) : IRequest<ApiResponse<UploadFileResponse>>
    {
        public class Validator : AbstractValidator<DeleteImageCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Url).NotEmpty();
            }
        }

    }

    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, ApiResponse<UploadFileResponse>>
    {
        private readonly IUploadFileService _uploadFileService;
        public DeleteImageHandler(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }
        public async Task<ApiResponse<UploadFileResponse>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            _uploadFileService.DeleteImage(request.Url);
            return ApiResponse<UploadFileResponse>.Success(null,"Image Deleted Successfuly");
        }
    }
}
