using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;

public class Question : Entity<int>
{
	public string Text { get; set; } = string.Empty; // Soru metni
	public int AnswerTemplateId { get; set; } // Hangi şablon kullanılıyor
	public AnswerTemplate AnswerTemplate { get; set; } = null!;

	public ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();
}
