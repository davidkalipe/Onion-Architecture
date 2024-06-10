using Application.Features.CustomerFeatures.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomerController : BaseApiController
{
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInCustomerCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}