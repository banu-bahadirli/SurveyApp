using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Delete
{
	public class DeletedAnswerTemplateResponse
	{
		public int Id { get; set; }
		public string Message { get; set; } = default!;
		public bool Success { get; set; }
	}
}
