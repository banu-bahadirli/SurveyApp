namespace SurveyApp.Application.Features.AnswerTemplates.Dtos
{
	public class AnswerOptionDto
	{
		public int Id { get; set; }	
		public string Text { get; set; } = null!;
		public int Order { get; set; }
	}

}
