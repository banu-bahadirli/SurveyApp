

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserSurveyAnswers
{
	

	public class GetUserSurveyAnswerResponse
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; } = string.Empty;
		public string AnswerText { get; set; } = string.Empty; // Eğer metin cevabı varsa
		public int? AnswerOptionId { get; set; } // Şık tabanlı ise
		public string? AnswerOptionText { get; set; } // Seçilen şık metni
	}
}

