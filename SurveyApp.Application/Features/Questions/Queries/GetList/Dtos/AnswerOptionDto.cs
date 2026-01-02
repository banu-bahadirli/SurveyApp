using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Queries.GetList.Dtos
{
	public class AnswerOptionDto
	{
		public int Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public int Order { get; set; }
	}
}
