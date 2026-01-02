using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Create
{
	public class CreatedSurveyResponse
	{
		public int Id { get; set; }
		public string Title { get; private set; } = default!;
		public string Description { get; private set; } = default!;

		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }

		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }
	}
}
