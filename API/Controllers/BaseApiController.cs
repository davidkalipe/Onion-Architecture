using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    private IMapper _mapper;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected IMapper Mapper => _mapper??= HttpContext.RequestServices.GetService<IMapper>();
}