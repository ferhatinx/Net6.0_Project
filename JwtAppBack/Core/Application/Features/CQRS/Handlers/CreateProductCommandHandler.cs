using AutoMapper;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;

    }


    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
       await _productRepository.CreateAsync(_mapper.Map<Product>(request));
       return Unit.Value; 
    }

}
