using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Persistance.Core.Domain;

namespace JwtAppBack.Core.Application.Mappings;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category,CategoryListDto>().ReverseMap();
        CreateMap<Category,UpdateCategoryCommandRequest>().ReverseMap();
    }
}
