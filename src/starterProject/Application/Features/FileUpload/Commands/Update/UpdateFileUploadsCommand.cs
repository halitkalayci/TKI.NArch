using Application.Features.FileUpload.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileUpload.Commands.Update;

public class UpdateFileUploadsCommand : IRequest<UpdatedFileUploadsResponse>
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }

    public class UpdateFileUploadsCommandHandler : IRequestHandler<UpdateFileUploadsCommand, UpdatedFileUploadsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileUploadsRepository _fileUploadsRepository;
        private readonly FileUploadsBusinessRules _fileUploadsBusinessRules;

        public UpdateFileUploadsCommandHandler(IMapper mapper, IFileUploadsRepository fileUploadsRepository,
                                         FileUploadsBusinessRules fileUploadsBusinessRules)
        {
            _mapper = mapper;
            _fileUploadsRepository = fileUploadsRepository;
            _fileUploadsBusinessRules = fileUploadsBusinessRules;
        }

        public async Task<UpdatedFileUploadsResponse> Handle(UpdateFileUploadsCommand request, CancellationToken cancellationToken)
        {
            FileUploads? fileUploads = await _fileUploadsRepository.GetAsync(predicate: fu => fu.Id == request.Id, cancellationToken: cancellationToken);
            await _fileUploadsBusinessRules.FileUploadsShouldExistWhenSelected(fileUploads);
            fileUploads = _mapper.Map(request, fileUploads);

            await _fileUploadsRepository.UpdateAsync(fileUploads!);

            UpdatedFileUploadsResponse response = _mapper.Map<UpdatedFileUploadsResponse>(fileUploads);
            return response;
        }
    }
}