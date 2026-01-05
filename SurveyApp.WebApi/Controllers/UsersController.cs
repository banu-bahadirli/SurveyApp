using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Users.Commands.Create;
using SurveyApp.Application.Features.Users.Queries.GetById;
using SurveyApp.Application.Features.Users.Queries.GetList;

namespace SurveyApp.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : BaseController
	{


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
		{
			var result = await Mediator.Send(createUserCommand);
			return Created("", result);
		}

		[HttpGet("getUser")]
		[Authorize]
		public async Task<IActionResult> GetUser()
		{
			var query = new GetByIdUserQuery(); 
			var user = await Mediator.Send(query);
			return Ok(user);
		}


		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListUserQuery getUserQuery)
		{
			var result = await Mediator.Send(getUserQuery);
			return Ok(result);
		}
	}

}
