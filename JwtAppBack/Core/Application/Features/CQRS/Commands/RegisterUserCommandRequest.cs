using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Commands;

public class RegisterUserCommandRequest : IRequest<AppUserListDto>
{
    public string? Password { get; set; }

    public string? Username { get; set; }
}
