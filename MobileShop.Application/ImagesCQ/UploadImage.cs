using FluentValidation;
using Microsoft.AspNetCore.Http;
using MobileShop.Application.APIResponse;
using MyImageService.Interfaces;

namespace MobileShop.Application.ImagesCQ
{
    public class UploadImageCommand() : IRequest<ApiResponse<UploadFileResponse>>
    {

        public IFormFile File { get; set; }
        public string RequestScheme { get; set; }
        public string RequestHost { get; set; }

        public class validator : AbstractValidator<UploadImageCommand>
        {
            public validator()
            {
                RuleFor(x => x.File).NotNull().WithMessage("File is required");
            }
        }
    }

    public class UploadImageHandler : IRequestHandler<UploadImageCommand, ApiResponse<UploadFileResponse>>
    {
        private readonly IUploadFileService _uploadFileService;
        public UploadImageHandler(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        public async Task<ApiResponse<UploadFileResponse>> Handle(UploadImageCommand request, CancellationToken ct)
        {
            var url =  _uploadFileService.UploadFile(request.File,request.RequestScheme, request.RequestHost);
            return ApiResponse<UploadFileResponse>.Success(new UploadFileResponse { ImageUrl = url});
        }
    }
}
