using AutoMapper;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private  readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;

    }


    public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var unchanged = await _productRepository.GetByFilterAsync(x=>x.Id == request.Id);
        if (unchanged != null)
            await _productRepository.UpdateAsync(_mapper.Map<Product>(request), unchanged);
        return Unit.Value;
    }

}
