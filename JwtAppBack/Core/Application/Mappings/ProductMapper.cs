using System.Data;
using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Persistance.Core.Domain;

namespace JwtAppBack.Core.Application.Mappings;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product,ProductListDto>().ReverseMap();
        CreateMap<Product,CreateProductCommandRequest>().ReverseMap();
        CreateMap<Product,UpdateProductCommandRequest>().ReverseMap();
    }
}
