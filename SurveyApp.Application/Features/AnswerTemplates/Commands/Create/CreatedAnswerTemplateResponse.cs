using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Create
{
	public class CreatedAnswerTemplateResponse
	{
		public int Id { get; set; }
		public string Name { get;  set; }
		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }
	}
}
