

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserSurveyAnswers
{
	public class GetUserSurveyAnswerResponse
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; } = string.Empty;
		public string AnswerText { get; set; } = string.Empty; 
		public int? AnswerOptionId { get; set; }
		public string? AnswerOptionText { get; set; } 
	}
}

