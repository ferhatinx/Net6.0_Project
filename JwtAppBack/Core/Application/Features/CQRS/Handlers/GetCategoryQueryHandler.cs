using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }


    public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var entity =await _categoryRepository.GetByIdAsync(request.Id);
        return entity != null    ? _mapper.Map<CategoryListDto>(entity): new CategoryListDto() ;
    }

}
