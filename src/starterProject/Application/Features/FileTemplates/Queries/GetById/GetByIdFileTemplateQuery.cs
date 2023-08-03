using Application.Features.FileTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FileTemplates.Queries.GetById;

public class GetByIdFileTemplateQuery : IRequest<GetByIdFileTemplateResponse>
{
    public Guid Id { get; set; }

    public class GetByIdFileTemplateQueryHandler : IRequestHandler<GetByIdFileTemplateQuery, GetByIdFileTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileTemplateRepository _fileTemplateRepository;
        private readonly FileTemplateBusinessRules _fileTemplateBusinessRules;

        public GetByIdFileTemplateQueryHandler(IMapper mapper, IFileTemplateRepository fileTemplateRepository, FileTemplateBusinessRules fileTemplateBusinessRules)
        {
            _mapper = mapper;
            _fileTemplateRepository = fileTemplateRepository;
            _fileTemplateBusinessRules = fileTemplateBusinessRules;
        }

        public async Task<GetByIdFileTemplateResponse> Handle(GetByIdFileTemplateQuery request, CancellationToken cancellationToken)
        {
            FileTemplate? fileTemplate = await _fileTemplateRepository.GetAsync(predicate: ft => ft.Id == request.Id, cancellationToken: cancellationToken);
            await _fileTemplateBusinessRules.FileTemplateShouldExistWhenSelected(fileTemplate);

            GetByIdFileTemplateResponse response = _mapper.Map<GetByIdFileTemplateResponse>(fileTemplate);
            return response;
        }
    }
}