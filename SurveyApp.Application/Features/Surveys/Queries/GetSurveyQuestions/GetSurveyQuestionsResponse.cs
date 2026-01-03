using SurveyApp.Application.Features.AnswerTemplates.Dtos;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyQuestions;

public class GetSurveyQuestionsResponse
{
	public int QuestionId { get; set; }
	public string QuestionText { get; set; } = null!;
	public List<AnswerOptionDto> Options { get; set; } = new List<AnswerOptionDto>();
}
