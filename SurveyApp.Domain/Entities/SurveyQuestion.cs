using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class SurveyQuestion
{
	public int SurveyId { get; set; }
	public Survey Survey { get; set; } = null!;

	public int QuestionId { get; set; }
	public Question Question { get; set; } = null!;
}