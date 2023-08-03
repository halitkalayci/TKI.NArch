using Application.Features.FileTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileTemplates.Commands.Create;

public class CreateFileTemplateCommand : IRequest<CreatedFileTemplateResponse>, ISecuredRequest
{
    public string Content { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class CreateFileTemplateCommandHandler : IRequestHandler<CreateFileTemplateCommand, CreatedFileTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileTemplateRepository _fileTemplateRepository;
        private readonly FileTemplateBusinessRules _fileTemplateBusinessRules;

        public CreateFileTemplateCommandHandler(IMapper mapper, IFileTemplateRepository fileTemplateRepository,
                                         FileTemplateBusinessRules fileTemplateBusinessRules)
        {
            _mapper = mapper;
            _fileTemplateRepository = fileTemplateRepository;
            _fileTemplateBusinessRules = fileTemplateBusinessRules;
        }

        public async Task<CreatedFileTemplateResponse> Handle(CreateFileTemplateCommand request, CancellationToken cancellationToken)
        {
            FileTemplate fileTemplate = _mapper.Map<FileTemplate>(request);

            await _fileTemplateRepository.AddAsync(fileTemplate);

            CreatedFileTemplateResponse response = _mapper.Map<CreatedFileTemplateResponse>(fileTemplate);
            return response;
        }
    }
}