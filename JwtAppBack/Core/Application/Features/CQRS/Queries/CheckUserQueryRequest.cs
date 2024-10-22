using JwtAppBack.Core.Application.Dtos;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Queries;

public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
