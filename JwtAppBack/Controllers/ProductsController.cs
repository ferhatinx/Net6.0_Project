using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JwtAppBack.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductQueryRequest());
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetProductQueryRequest(id));
        if (response == null)
            return NoContent();        
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new RemoveProductCommandRequest(id));      
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommandRequest rq)
    {
        var response = await _mediator.Send(rq);      
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductCommandRequest rq)
    {
        var response = await _mediator.Send(rq);
        return Ok();
    }
}
