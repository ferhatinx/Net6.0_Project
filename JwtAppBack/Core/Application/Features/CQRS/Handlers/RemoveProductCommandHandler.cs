using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest>
{
    private readonly IRepository<Product> _productRepository;

    public RemoveProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<Unit> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {
        var deletedEntity = await _productRepository.GetByFilterAsync(x=>x.Id == request.Id);
        if (deletedEntity != null)
            await _productRepository.RemoveAsync(deletedEntity);
        return Unit.Value;
    }

}
