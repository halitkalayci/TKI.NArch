using Application.Features.FileTemplates.Constants;
using Application.Features.FileTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileTemplates.Commands.Delete;

public class DeleteFileTemplateCommand : IRequest<DeletedFileTemplateResponse>
{
    public Guid Id { get; set; }

    public class DeleteFileTemplateCommandHandler : IRequestHandler<DeleteFileTemplateCommand, DeletedFileTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileTemplateRepository _fileTemplateRepository;
        private readonly FileTemplateBusinessRules _fileTemplateBusinessRules;

        public DeleteFileTemplateCommandHandler(IMapper mapper, IFileTemplateRepository fileTemplateRepository,
                                         FileTemplateBusinessRules fileTemplateBusinessRules)
        {
            _mapper = mapper;
            _fileTemplateRepository = fileTemplateRepository;
            _fileTemplateBusinessRules = fileTemplateBusinessRules;
        }

        public async Task<DeletedFileTemplateResponse> Handle(DeleteFileTemplateCommand request, CancellationToken cancellationToken)
        {
            FileTemplate? fileTemplate = await _fileTemplateRepository.GetAsync(predicate: ft => ft.Id == request.Id, cancellationToken: cancellationToken);
            await _fileTemplateBusinessRules.FileTemplateShouldExistWhenSelected(fileTemplate);

            await _fileTemplateRepository.DeleteAsync(fileTemplate!);

            DeletedFileTemplateResponse response = _mapper.Map<DeletedFileTemplateResponse>(fileTemplate);
            return response;
        }
    }
}