using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, List<CategoryListDto>>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    public GetAllCategoryQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;

    }


    public async Task<List<CategoryListDto>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _categoryRepository.GetAllAsync();
        return _mapper.Map<List<CategoryListDto>>(data);
    }

}
