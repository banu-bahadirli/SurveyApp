using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class AnswerOption : Entity<int>
{
	public string Text { get; set; } = null!;
	public int Order { get; set; }
	public int AnswerTemplateId { get; set; }   // Hangi şablona ait olduğunu tutar
	public AnswerTemplate AnswerTemplate { get; set; } = null!;
}
