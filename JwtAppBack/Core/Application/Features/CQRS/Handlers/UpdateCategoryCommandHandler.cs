using AutoMapper;
using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Persistance.Core.Domain;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Handlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;

    }


    public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var unchanged = await _categoryRepository.GetByFilterAsync(x => x.Id == request.Id);
        if (unchanged != null)
        {
             await _categoryRepository.UpdateAsync(_mapper.Map<Category>(request), unchanged);
        }
        return Unit.Value;

    }

}
