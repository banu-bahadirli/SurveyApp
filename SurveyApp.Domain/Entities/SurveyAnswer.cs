using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Core.Security.Entities;

namespace SurveyApp.Domain.Entities;
public class SurveyAnswer : Entity<int>
{
	public int UserId { get; set; }
	public User User { get; set; } = null!;   

	public int SurveyId { get; set; }
	public Survey Survey { get; set; } = null!;

	public int QuestionId { get; set; }
	public Question Question { get; set; } = null!;

	public int AnswerOptionId { get; set; }
	public AnswerOption AnswerOption { get; set; } = null!;
}
