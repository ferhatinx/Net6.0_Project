using JwtAppBack.Core.Application.Dtos;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Queries;

public class GetAllProductQueryRequest : IRequest<List<ProductListDto>>
{
    
}
