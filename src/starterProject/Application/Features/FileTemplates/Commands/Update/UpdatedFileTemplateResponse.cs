using Core.Application.Responses;

namespace Application.Features.FileTemplates.Commands.Update;

public class UpdatedFileTemplateResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}