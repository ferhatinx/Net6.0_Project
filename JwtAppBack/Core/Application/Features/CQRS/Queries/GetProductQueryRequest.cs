using JwtAppBack.Core.Application.Dtos;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Queries;

public class GetProductQueryRequest : IRequest<ProductListDto>
{
    public int Id { get; set; }
    public GetProductQueryRequest(int id)
    {
        Id = id;
    }
}
