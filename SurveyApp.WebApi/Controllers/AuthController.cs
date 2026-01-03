using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Auth.Login.Commands;

namespace SurveyApp.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : BaseController
	{

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{	
			var response = await Mediator.Send(command);
			if (!response.Success)
				return Unauthorized(response);

			return Ok(response);
		}
	}
}
