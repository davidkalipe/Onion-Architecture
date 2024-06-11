using Application.Features.CustomerFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Jwt;

namespace API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomerController : BaseApiController
{
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpCustomerCommand command)
    {
        try
        {
            var request = await Mediator.Send(command);
            if (request is null) return BadRequest("Try again, something wrong");
            return Ok("Hi, " + request.Fullname);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }




    [HttpPost("SignIn"), Authorize]
    public async Task<IActionResult> SignIn(SignInCustomerCommand command)
    {
        try
        {
            var request = await Mediator.Send(command);
            if (request is null)
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
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}