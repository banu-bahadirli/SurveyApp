using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetList;
using SurveyApp.Application.Features.Users.Commands.Create;
using SurveyApp.Application.Features.Users.Queries.GetById;
using SurveyApp.Application.Features.Users.Queries.GetList;

namespace SurveyApp.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
		{
			var result = await _mediator.Send(createUserCommand);
			return Created("", result);
		}

		[HttpGet("getUser")]
		[Authorize]
		public async Task<IActionResult> GetUser()
		{
			var query = new GetByIdUserQuery(); // parametre yok
			var user = await _mediator.Send(query);
			return Ok(user);
		}


		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListUserQuery getUserQuery)
		{
			var result = await _mediator.Send(getUserQuery);
			return Ok(result);
		}
	}

}
