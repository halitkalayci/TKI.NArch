using Application.Features.FileTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileTemplates.Commands.Update;

public class UpdateFileTemplateCommand : IRequest<UpdatedFileTemplateResponse>
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }

    public class UpdateFileTemplateCommandHandler : IRequestHandler<UpdateFileTemplateCommand, UpdatedFileTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileTemplateRepository _fileTemplateRepository;
        private readonly FileTemplateBusinessRules _fileTemplateBusinessRules;

        public UpdateFileTemplateCommandHandler(IMapper mapper, IFileTemplateRepository fileTemplateRepository,
                                         FileTemplateBusinessRules fileTemplateBusinessRules)
        {
            _mapper = mapper;
            _fileTemplateRepository = fileTemplateRepository;
            _fileTemplateBusinessRules = fileTemplateBusinessRules;
        }

        public async Task<UpdatedFileTemplateResponse> Handle(UpdateFileTemplateCommand request, CancellationToken cancellationToken)
        {
            FileTemplate? fileTemplate = await _fileTemplateRepository.GetAsync(predicate: ft => ft.Id == request.Id, cancellationToken: cancellationToken);
            await _fileTemplateBusinessRules.FileTemplateShouldExistWhenSelected(fileTemplate);
            fileTemplate = _mapper.Map(request, fileTemplate);

            await _fileTemplateRepository.UpdateAsync(fileTemplate!);

            UpdatedFileTemplateResponse response = _mapper.Map<UpdatedFileTemplateResponse>(fileTemplate);
            return response;
        }
    }
}