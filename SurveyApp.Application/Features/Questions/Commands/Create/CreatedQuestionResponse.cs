using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Commands.Create;


	public class CreatedQuestionResponse
	{
		public int Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public int AnswerTemplateId { get; set; }
		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }
}



