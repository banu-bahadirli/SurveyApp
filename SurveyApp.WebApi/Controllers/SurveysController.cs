using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Surveys.Commands.Create;
using SurveyApp.Application.Features.Surveys.Commands.Delete;
using SurveyApp.Application.Features.Surveys.Commands.SubmitAnswers;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Features.Surveys.Queries.GetById;
using SurveyApp.Application.Features.Surveys.Queries.GetList;
using SurveyApp.Application.Features.Surveys.Queries.GetSurveyQuestions;
using SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;
using System.Security.Claims;

namespace SurveyApp.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SurveysController : BaseController
	{
		// -------------------------------
		// Survey CRUD
		// -------------------------------

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateSurveyCommand createSurveyCommand)
		{
			var response = await Mediator.Send(createSurveyCommand);
			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateSurveyCommand updateSurveyCommand)
		{
			UpdatedSurveyResponse response = await Mediator.Send(updateSurveyCommand);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteSurveyCommand { Id = id };
			var result = await Mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListSurveyQuery getListSurveyQuery)
		{
			var result = await Mediator.Send(getListSurveyQuery);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var query = new GetByIdSurveyQuery { Id = id };
			var result = await Mediator.Send(query);
			return Ok(result);
		}


		[HttpGet("active")]
		public async Task<IActionResult> GetUserActiveSurveys([FromQuery] int userId)
		{
			var query = new GetUserActiveSurveysQuery { UserId = userId };
			var result = await Mediator.Send(query);
			return Ok(result);
		}

		// -------------------------------
		// Kullanıcının anket cevaplarını gönderme
		// -------------------------------
		[HttpPost("submit-answers")]
		public async Task<IActionResult> SubmitSurveyAnswers([FromBody] SubmitSurveyAnswersCommand command)
		{
			var result = await Mediator.Send(command);
			return Ok(result);
		}


		[HttpGet("{surveyId}/questions")]
		public async Task<IActionResult> GetSurveyQuestions([FromRoute] int surveyId)
		{
			var query = new GetSurveyQuestionsQuery { SurveyId = surveyId };
			var result = await Mediator.Send(query);
			return Ok(result);
		}


	}
}
