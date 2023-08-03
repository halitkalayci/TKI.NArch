using Core.Application.Responses;

namespace Application.Features.FileTemplates.Commands.Delete;

public class DeletedFileTemplateResponse : IResponse
{
    public Guid Id { get; set; }
}