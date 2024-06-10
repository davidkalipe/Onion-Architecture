using Application.Features.CustomerFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Jwt;

namespace API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomerController : BaseApiController
{
    [HttpPost("SignIn"), Authorize]
    public async Task<IActionResult> SignIn(SignInCustomerCommand command)
    {
        var request = await Mediator.Send(command);
        if(request is null)
            return NotFound("Customer not found");
        var token = TokenGenerator.CreateCustomerToken(request);

        var response = new
        {
            request.Fullname,
            request.Phonenumber,
            token
        };
        return Ok(response);
    }
}