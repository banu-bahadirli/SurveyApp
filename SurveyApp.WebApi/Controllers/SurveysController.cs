using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Surveys.Commands.Create;
using SurveyApp.Application.Features.Surveys.Commands.Delete;
using SurveyApp.Application.Features.Surveys.Commands.SubmitAnswers;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Features.Surveys.Queries.GetById;
using SurveyApp.Application.Features.Surveys.Queries.GetCompletedSurveyUser;
using SurveyApp.Application.Features.Surveys.Queries.GetList;
using SurveyApp.Application.Features.Surveys.Queries.GetNotCompletedSurveyUser;
using SurveyApp.Application.Features.Surveys.Queries.GetSurveyQuestions;
using SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;
using SurveyApp.Application.Features.Surveys.Queries.GetUserSurveyAnswers;
using SurveyApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class SurveysController : BaseController
{

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([FromBody] CreateSurveyCommand createSurveyCommand)
	{
		var response = await Mediator.Send(createSurveyCommand);
		return Ok(response);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update([FromBody] UpdateSurveyCommand updateSurveyCommand)
	{
		var response = await Mediator.Send(updateSurveyCommand);
		return Ok(response);
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id)
	{
		var command = new DeleteSurveyCommand { Id = id };
		var result = await Mediator.Send(command);
		return Ok(result);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetList([FromQuery] GetListSurveyQuery getListSurveyQuery)
	{
		var result = await Mediator.Send(getListSurveyQuery);
		return Ok(result);
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetById([FromRoute] int id)
	{
		var query = new GetByIdSurveyQuery { Id = id };
		var result = await Mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("{surveyId}/completed-users")]
	public async Task<IActionResult> GetCompletedSurveyUsers(int surveyId)
	{
		var users = await Mediator.Send(new GetCompletedSurveyUserQuery { SurveyId = surveyId });
		return Ok(users);
	}

	[HttpGet("{surveyId}/not-completed-users")]
	public async Task<IActionResult> GetNotCompletedSurveyUsers(int surveyId)
	{
		var users = await Mediator.Send(new GetNotCompletedSurveyUserQuery { SurveyId = surveyId });
		return Ok(users);
	}

	[HttpGet("{surveyId}/user/{userId}/answers")]
	public async Task<IActionResult> GetUserSurveyAnswers(int surveyId, int userId)
	{
		var answers = await Mediator.Send(new GetUserSurveyAnswersQuery { SurveyId = surveyId, UserId = userId });
		return Ok(answers);
	}




	[HttpGet("active")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> GetUserActiveSurveys([FromQuery] int userId)
	{
		var query = new GetUserActiveSurveysQuery { UserId = userId };
		var result = await Mediator.Send(query);
		return Ok(result);
	}

	[HttpPost("submit-answers")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> SubmitSurveyAnswers([FromBody] SubmitSurveyAnswersCommand command)
	{
		var result = await Mediator.Send(command);
		return Ok(result);
	}

	[HttpGet("{surveyId}/questions")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> GetSurveyQuestions([FromRoute] int surveyId)
	{
		var query = new GetSurveyQuestionsQuery { SurveyId = surveyId };
		var result = await Mediator.Send(query);
		return Ok(result);
	}

}
