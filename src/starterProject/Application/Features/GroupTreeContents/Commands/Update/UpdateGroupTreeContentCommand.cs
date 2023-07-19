using Application.Features.GroupTreeContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Domain.Enums;

namespace Application.Features.GroupTreeContents.Commands.Update;

public class UpdateGroupTreeContentCommand : IRequest<UpdatedGroupTreeContentResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Target { get; set; }
    public string Icon { get; set; }
    public int RowOrder { get; set; }
    public bool ShowOnAuth { get; set; }
    public bool HideOnAuth { get; set; }
    public int? ParentId { get; set; }
    public GroupTreeContentType Type { get; set; }

    public class UpdateGroupTreeContentCommandHandler : IRequestHandler<UpdateGroupTreeContentCommand, UpdatedGroupTreeContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupTreeContentRepository _groupTreeContentRepository;
        private readonly GroupTreeContentBusinessRules _groupTreeContentBusinessRules;

        public UpdateGroupTreeContentCommandHandler(IMapper mapper, IGroupTreeContentRepository groupTreeContentRepository,
                                         GroupTreeContentBusinessRules groupTreeContentBusinessRules)
        {
            _mapper = mapper;
            _groupTreeContentRepository = groupTreeContentRepository;
            _groupTreeContentBusinessRules = groupTreeContentBusinessRules;
        }

        public async Task<UpdatedGroupTreeContentResponse> Handle(UpdateGroupTreeContentCommand request, CancellationToken cancellationToken)
        {
            GroupTreeContent? groupTreeContent = await _groupTreeContentRepository.GetAsync(predicate: gtc => gtc.Id == request.Id, cancellationToken: cancellationToken);
            await _groupTreeContentBusinessRules.GroupTreeContentShouldExistWhenSelected(groupTreeContent);
            groupTreeContent = _mapper.Map(request, groupTreeContent);

            await _groupTreeContentRepository.UpdateAsync(groupTreeContent!);

            UpdatedGroupTreeContentResponse response = _mapper.Map<UpdatedGroupTreeContentResponse>(groupTreeContent);
            return response;
        }
    }
}