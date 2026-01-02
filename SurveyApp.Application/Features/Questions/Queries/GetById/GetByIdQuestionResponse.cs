using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Queries.GetById
{
	public class GetByIdQuestionResponse
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public AnswerTemplateDto AnswerTemplate { get; set; }
	}


}
