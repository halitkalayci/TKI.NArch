using Core.Application.Responses;

namespace Application.Features.Models.Commands.Delete;

public class DeletedModelResponse : IResponse
{
    public long Id { get; set; }
}