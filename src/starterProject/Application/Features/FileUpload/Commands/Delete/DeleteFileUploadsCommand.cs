using Application.Features.FileUpload.Constants;
using Application.Features.FileUpload.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileUpload.Commands.Delete;

public class DeleteFileUploadsCommand : IRequest<DeletedFileUploadsResponse>
{
    public Guid Id { get; set; }

    public class DeleteFileUploadsCommandHandler : IRequestHandler<DeleteFileUploadsCommand, DeletedFileUploadsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileUploadsRepository _fileUploadsRepository;
        private readonly FileUploadsBusinessRules _fileUploadsBusinessRules;

        public DeleteFileUploadsCommandHandler(IMapper mapper, IFileUploadsRepository fileUploadsRepository,
                                         FileUploadsBusinessRules fileUploadsBusinessRules)
        {
            _mapper = mapper;
            _fileUploadsRepository = fileUploadsRepository;
            _fileUploadsBusinessRules = fileUploadsBusinessRules;
        }

        public async Task<DeletedFileUploadsResponse> Handle(DeleteFileUploadsCommand request, CancellationToken cancellationToken)
        {
            FileUploads? fileUploads = await _fileUploadsRepository.GetAsync(predicate: fu => fu.Id == request.Id, cancellationToken: cancellationToken);
            await _fileUploadsBusinessRules.FileUploadsShouldExistWhenSelected(fileUploads);

            await _fileUploadsRepository.DeleteAsync(fileUploads!);

            DeletedFileUploadsResponse response = _mapper.Map<DeletedFileUploadsResponse>(fileUploads);
            return response;
        }
    }
}