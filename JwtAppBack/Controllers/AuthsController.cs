using JwtAppBack.Core.Application.Features.CQRS.Commands;
using JwtAppBack.Core.Application.Features.CQRS.Queries;
using JwtAppBack.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JwtAppBack.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserCommandRequest rq)
    {
        var response = await _mediator.Send(rq);
        return Created("",response);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(CheckUserQueryRequest rq)
    {
        var dto = await _mediator.Send(rq);
        if(dto.isExist)
            return Created("",JwtTokenGenerator.GenerateToken(dto));
        else
            return BadRequest("Hatali Giriş");

        
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetUserQueryRequest());
        return Ok(result);
    }
}
