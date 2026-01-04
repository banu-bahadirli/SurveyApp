using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;

public class Question : Entity<int>
{
	public string Text { get; set; } = string.Empty; 
	public int AnswerTemplateId { get; set; } 
	public AnswerTemplate AnswerTemplate { get; set; } = null!;

	public ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();
}
