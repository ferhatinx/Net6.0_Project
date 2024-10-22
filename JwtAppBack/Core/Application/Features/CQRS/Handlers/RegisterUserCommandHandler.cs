using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Enums;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, AppUserListDto>
{
    private readonly IRepository<AppUser> _appUserRepository;

    public RegisterUserCommandHandler(IRepository<AppUser> appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }


    public async Task<AppUserListDto> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _appUserRepository.CreateAsync(new AppUser{
            Username = request.Username,
            Password = request.Password,
            AppRoleId = (int)RoleTypeEnum.Member,

        });
        return new AppUserListDto(){Username = request.Username, Password = request.Password};
    }

}
