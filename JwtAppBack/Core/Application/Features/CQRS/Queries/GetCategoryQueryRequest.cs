using JwtAppBack.Core.Application.Dtos;
using MediatR;

namespace JwtAppBack.Core.Application.Features.CQRS.Queries;

public class GetCategoryQueryRequest : IRequest<CategoryListDto>
{
    public int Id { get; set; }
    public GetCategoryQueryRequest(int id)
    {
        Id = id;
    }
}
