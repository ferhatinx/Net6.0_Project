using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class CheckUserQueryHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
{
    private readonly IRepository<AppUser> _appUserRepository;
    private readonly IRepository<AppRole> _appRoleRepository;

    public CheckUserQueryHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
    {
        _appUserRepository = appUserRepository;
        _appRoleRepository = appRoleRepository;
    }


    public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
    {
        var dto = new CheckUserResponseDto();
        var user = await _appUserRepository.GetByFilterAsync(x=>x.Username == request.Username && x.Password == request.Password);
        if(user == null)
        {
            dto.isExist = false;
        }
        else
        {
            dto.Username = user.Username;
            dto.Id = user.Id;
            dto.isExist = true;
            var role = await _appRoleRepository.GetByFilterAsync(x=>x.Id == user.AppRoleId);
            dto.Role = role?.Definition;


        }
        return dto;
        
    }

}
