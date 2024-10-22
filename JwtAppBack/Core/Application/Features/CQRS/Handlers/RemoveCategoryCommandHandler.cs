using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;

    public RemoveCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<Unit> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
    {
       var deleted = await _categoryRepository.GetByFilterAsync(x=>x.Id == request.Id);
      if(deleted != null)
            await _categoryRepository.RemoveAsync(deleted);
      return Unit.Value;
    }

}
