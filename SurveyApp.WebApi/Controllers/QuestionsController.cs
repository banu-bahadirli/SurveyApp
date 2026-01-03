using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Questions.Commands.Create;
using SurveyApp.Application.Features.Questions.Commands.Delete;
using SurveyApp.Application.Features.Questions.Commands.Update;
using SurveyApp.Application.Features.Questions.Queries.GetById;
using SurveyApp.Application.Features.Questions.Queries.GetList;

namespace SurveyApp.WebApi.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class QuestionsController : BaseController
	{

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateQuestionCommand createQuestionCommand)
		{
			var response = await Mediator.Send(createQuestionCommand);
			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateQuestionCommand updateQuestionCommand)
		{
			var result = await Mediator.Send(updateQuestionCommand);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var command = new DeleteQuestionCommand { Id = id };
			var result = await Mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListQuestionQuery getListQuestionQuery)
		{
			var result = await Mediator.Send(getListQuestionQuery);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var query = new GetByIdQuestionQuery { Id = id };
			var result = await Mediator.Send(query);
			return Ok(result);
		}
	}
}
