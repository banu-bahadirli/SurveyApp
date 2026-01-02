using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Queries.GetList
{
	public class GetListQuestionResponse
	{
		public int Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public AnswerTemplateDto AnswerTemplate { get; set; } = new AnswerTemplateDto();
	}
}
