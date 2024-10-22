using JwtAppBack.Core.Application.Dtos;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Queries;

public class GetUserQueryRequest  :IRequest<List<AppUserListDto>>
{
    
}
