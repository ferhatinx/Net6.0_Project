using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, List<AppUserListDto>>
{
    private readonly IRepository<AppUser> _appUserRepository;

    public GetUserQueryHandler(IRepository<AppUser> appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }


    public async Task<List<AppUserListDto>> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
    {
       var entitiy = await _appUserRepository.GetAllAsync();
       var dtos = entitiy.Select(x=>new AppUserListDto{
            Username = x.Username,
            Password = x.Password,
        });
        return dtos.ToList();
    }

}
