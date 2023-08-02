using Application.Features.FileUpload.Rules;
using Application.Services.FileUpload;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.FileUpload.Commands.Create;

public class CreateFileUploadsCommand : IRequest<CreatedFileUploadsResponse>, ISecuredRequest
{
    public int UserId { get; set; }
    public List<IFormFile> Files { get; set; }
    public string Description { get; set; }

    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class CreateFileUploadsCommandHandler : IRequestHandler<CreateFileUploadsCommand, CreatedFileUploadsResponse>
    {
        private readonly IFileUploadsService _fileUploadService;
        private readonly IMapper _mapper;
        private readonly IFileUploadsRepository _fileUploadsRepository;
        private readonly FileUploadsBusinessRules _fileUploadsBusinessRules;

        public CreateFileUploadsCommandHandler(IMapper mapper, IFileUploadsRepository fileUploadsRepository,
                                         FileUploadsBusinessRules fileUploadsBusinessRules, IFileUploadsService fileUploadService)
        {
            _mapper = mapper;
            _fileUploadsRepository = fileUploadsRepository;
            _fileUploadsBusinessRules = fileUploadsBusinessRules;
            _fileUploadService = fileUploadService;
        }

        public async Task<CreatedFileUploadsResponse> Handle(CreateFileUploadsCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Files)
            {
                FileUploads fileUploads = _mapper.Map<FileUploads>(request);
                fileUploads.FileName = item.FileName;
                fileUploads.Destination = _fileUploadService.Upload(item);
                fileUploads.UserId = request.UserId;
                await _fileUploadsRepository.AddAsync(fileUploads);
            }
            CreatedFileUploadsResponse response = _mapper.Map<CreatedFileUploadsResponse>(null);
            return response;
        }
    }
}