using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Commands;

public class RemoveCategoryCommandRequest  : IRequest
{
    public int Id { get; set; }
    public RemoveCategoryCommandRequest(int id)
    {
        Id = id;
    }
}
