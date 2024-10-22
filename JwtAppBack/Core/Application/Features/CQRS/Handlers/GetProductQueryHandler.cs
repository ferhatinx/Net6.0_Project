using AutoMapper;
using JwtAppBack.Core.Application.Dtos;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductListDto>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    public GetProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;

    }


    public async Task<ProductListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _productRepository.GetByIdAsync((int)request.Id);
        return _mapper.Map<ProductListDto>(data);
             
    }

}
