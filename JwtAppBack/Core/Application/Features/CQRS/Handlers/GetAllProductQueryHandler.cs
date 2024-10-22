using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<ProductListDto>>
{
    private readonly IRepository<Product> _productsRepository;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IRepository<Product> productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;

    }


    public async Task<List<ProductListDto>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
       var data = await _productsRepository.GetAllAsync();
       return  _mapper.Map<List<ProductListDto>>(data);
    }

}
