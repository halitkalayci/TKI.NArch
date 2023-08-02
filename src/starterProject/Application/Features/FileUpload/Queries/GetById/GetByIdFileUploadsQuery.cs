using Application.Features.FileUpload.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileUpload.Queries.GetById;

public class GetByIdFileUploadsQuery : IRequest<GetByIdFileUploadsResponse>
{
    public Guid Id { get; set; }

    public class GetByIdFileUploadsQueryHandler : IRequestHandler<GetByIdFileUploadsQuery, GetByIdFileUploadsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileUploadsRepository _fileUploadsRepository;
        private readonly FileUploadsBusinessRules _fileUploadsBusinessRules;

        public GetByIdFileUploadsQueryHandler(IMapper mapper, IFileUploadsRepository fileUploadsRepository, FileUploadsBusinessRules fileUploadsBusinessRules)
        {
            _mapper = mapper;
            _fileUploadsRepository = fileUploadsRepository;
            _fileUploadsBusinessRules = fileUploadsBusinessRules;
        }

        public async Task<GetByIdFileUploadsResponse> Handle(GetByIdFileUploadsQuery request, CancellationToken cancellationToken)
        {
            FileUploads? fileUploads = await _fileUploadsRepository.GetAsync(predicate: fu => fu.Id == request.Id, cancellationToken: cancellationToken);
            await _fileUploadsBusinessRules.FileUploadsShouldExistWhenSelected(fileUploads);

            GetByIdFileUploadsResponse response = _mapper.Map<GetByIdFileUploadsResponse>(fileUploads);
            return response;
        }
    }
}