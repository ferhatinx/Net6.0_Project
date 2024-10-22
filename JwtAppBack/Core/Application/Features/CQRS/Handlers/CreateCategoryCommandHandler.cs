using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;

    public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        await _categoryRepository.CreateAsync(new Category{
            Definition = request.Definition,
        });
        return Unit.Value;
    }

}
