using Core.Application.Responses;

namespace Application.Features.GroupTreeContents.Commands.Delete;

public class DeletedGroupTreeContentResponse : IResponse
{
    public int Id { get; set; }
}