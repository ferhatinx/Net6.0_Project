using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Commands;

public class RemoveProductCommandRequest : IRequest<int>
{
    public int Id { get; set; }
    public RemoveProductCommandRequest(int id)
    {
        Id = id;
    }
}
