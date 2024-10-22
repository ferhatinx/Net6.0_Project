using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAppBack.Controllers;


[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllCategoryQueryRequest());
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetCategoryQueryRequest(id));
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommandRequest rq)
    {
        await _mediator.Send(rq);
        return Created("",rq);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new RemoveProductCommandRequest(id));
        return NoContent();
    }
}
