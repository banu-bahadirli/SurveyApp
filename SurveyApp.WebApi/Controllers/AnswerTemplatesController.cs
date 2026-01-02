using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Create;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Delete;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Update;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetById;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetList;
using SurveyApp.Application.Features.Surveys.Queries.GetById;


namespace SurveyApp.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnswerTemplatesController : BaseController
	{

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateAnswerTemplateCommand createAnswerTemplateCommand)
		{
			var response = await Mediator.Send(createAnswerTemplateCommand);
			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateAnswerTemplateCommand updateAnswerTemplateCommand)
		{
			var result = await Mediator.Send(updateAnswerTemplateCommand);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var command = new DeleteAnswerTemplateCommand { Id = id };
			var result = await Mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListAnswerTemplateQuery getListAnswerTemplateQuery)
		{
			var result = await Mediator.Send(getListAnswerTemplateQuery);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var query = new GetByIdAnswerTemplateQuery { Id = id };
			var result = await Mediator.Send(query);
			return Ok(result);
		}
	}
}
