namespace SurveyApp.Application.Features.AnswerTemplates.Dtos
{
	public class AnswerTemplateDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int OptionCount { get; set; }
		public List<AnswerOptionDto> Options { get; set; } = new();
	}
}
