using Core.Application.Responses;

namespace Application.Features.FileTemplates.Queries.GetById;

public class GetByIdFileTemplateResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}