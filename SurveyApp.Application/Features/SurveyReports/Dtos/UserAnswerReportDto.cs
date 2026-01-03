namespace SurveyApp.Application.Features.SurveyReports.Dtos
{
	public class UserAnswerReportDto
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; } = null!;
		public string SelectedOptionText { get; set; } = null!;
	}
}
