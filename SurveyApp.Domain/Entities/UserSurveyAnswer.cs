using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class UserSurveyAnswer : Entity<int>
{
	public int UserSurveyId { get; set; }
	public UserSurvey? UserSurvey { get; set; }

	public int QuestionId { get; set; }
	public Question? Question { get; set; }

	public int SelectedOptionId { get; set; }
	public AnswerOption? SelectedOption { get; set; }
}
